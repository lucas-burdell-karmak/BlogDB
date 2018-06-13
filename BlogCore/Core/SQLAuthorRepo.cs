using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlogDB.Core
{
    public class SQLAuthorRepo : IAuthorRepo
    {

        private readonly SqlConnection _connection;
        private readonly string _sqlConnectionString;
        private readonly string _defaultRoles;

        public SQLAuthorRepo(IConfiguration config)
        {

            _sqlConnectionString = config["SQLConnectionString"];
            _defaultRoles = config["DefaultBlogRoles"];
            _connection = new SqlConnection(_sqlConnectionString);
            _connection.Open();
        }

        public SQLAuthorRepo(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
            _defaultRoles = "{\"BlogWriter\"}";
            _connection = new SqlConnection(_sqlConnectionString);
            _connection.Open();
        }

        private static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public Author GetAuthorByID(int id)
        {
            Author author = null;
            var commandText = "SELECT name FROM author WHERE id = @id";
            var command = new SqlCommand(commandText, _connection);
            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = id.ToString();

            var reader = command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                    author = new Author(reader.GetString(0), id);
            reader.Close();

            return author;
        }

        public Author GetAuthorByName(string name)
        {
            Author author = null;
            var commandText = "SELECT id, roles FROM author WHERE name = @name";
            var command = new SqlCommand(commandText, _connection);
            command.Parameters.Add("@name", SqlDbType.NVarChar);
            command.Parameters["@name"].Value = name;

            var reader = command.ExecuteReader();

            if (reader.HasRows)
                while (reader.Read())
                {
                    author = new Author(name, reader.GetInt32(0));
                    author.Roles = JsonConvert.DeserializeObject<List<string>>(reader.GetString(1));
                }
            reader.Close();

            return author;
        }

        public List<Author> GetListOfAuthors()
        {
            var authors = new List<Author>();
            var commandText = "SELECT name, id, roles FROM author";
            var command = new SqlCommand(commandText, _connection);

            var reader = command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    var author = new Author(reader.GetString(0), reader.GetInt32(1));
                    author.Roles = JsonConvert.DeserializeObject<List<string>>(reader.GetString(2));
                    authors.Add(author);
                }

            return authors;
        }

        private byte[] GetPasswordHashByAuthorID(int id)
        {
            string passwordHash = null;
            var commandText = "SELECT PasswordHash FROM author WHERE id = @id";
            var command = new SqlCommand(commandText, _connection);
            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = id.ToString();

            var reader = command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                    passwordHash = reader.GetString(0);
            reader.Close();
            return HexStringToByteArray(passwordHash);
        }

        private static byte[] HexStringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        private byte[] GetSaltByAuthorID(int id)
        {
            string salt = null;
            var commandText = "SELECT Salt FROM author WHERE id = @id";
            var command = new SqlCommand(commandText, _connection);
            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = id.ToString();

            var reader = command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                    salt = reader.GetString(0);
            reader.Close();
            return HexStringToByteArray(salt);
        }

        public void TryValidateAuthorLogin(string name, string passwordHash, out bool isSuccessful)
        {
            try
            {
                var authorInDB = GetAuthorByName(name);
                if (authorInDB == null)
                    isSuccessful = false;
                else
                {
                    byte[] salt = GetSaltByAuthorID(authorInDB.ID);
                    byte[] passwordHashInDB = GetPasswordHashByAuthorID(authorInDB.ID);

                    HMACSHA512 hash = new HMACSHA512();

                    hash.Key = salt;
                    byte[] computedHash = hash.ComputeHash(HexStringToByteArray(passwordHash));

                    isSuccessful = (computedHash.SequenceEqual(passwordHashInDB));
                }
            }
            catch (Exception)
            {
                isSuccessful = false;
            }
        }



        public void TryRegisterAuthor(string name, string passwordHash, out bool isSuccessful)
        {
            try
            {
                Guid salt = Guid.NewGuid();
                HMACSHA512 hash = new HMACSHA512();

                hash.Key = salt.ToByteArray();
                byte[] computedHash = hash.ComputeHash(HexStringToByteArray(passwordHash));

                string hexOfComputedHash = ByteArrayToHexString(computedHash);

                var commandText = "INSERT INTO Author (Name, PasswordHash, Salt, Roles) VALUES (@Name, @PasswordHash, @Salt, @Roles)";
                var command = new SqlCommand(commandText, _connection);

                command.Parameters.Add("@Name", SqlDbType.NVarChar);
                command.Parameters["@Name"].Value = name;

                command.Parameters.Add("@PasswordHash", SqlDbType.NVarChar);
                command.Parameters["@PasswordHash"].Value = hexOfComputedHash;

                command.Parameters.Add("@Salt", SqlDbType.NVarChar);
                command.Parameters["@Salt"].Value = ByteArrayToHexString(salt.ToByteArray());

                command.Parameters.Add("@Roles", SqlDbType.NVarChar);

                command.Parameters["@Roles"].Value = _defaultRoles;

                command.ExecuteNonQuery();
                isSuccessful = true;
            }
            catch (Exception)
            {
                isSuccessful = false;
            }
        }

        public void TryUpdateAuthor(Author toUpdate, out bool isSuccessful)
        {
            try
            {
                var commandText = "UPDATE Author SET Name = @name WHERE id = @id";
                var command = new SqlCommand(commandText, _connection);
                command.Parameters.Add("@name", SqlDbType.NVarChar);
                command.Parameters["@name"].Value = toUpdate.Name;

                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = toUpdate.ID;

                command.ExecuteNonQuery();
                isSuccessful = true;
            }
            catch (Exception)
            {
                isSuccessful = false;
            }
        }

        public void TryDeleteAuthor(Author toDelete, out bool isSuccessful)
        {
            try
            {
                var commandText = "DELETE FROM Author WHERE id = @id";
                var command = new SqlCommand(commandText, _connection);
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = toDelete.ID;

                command.ExecuteNonQuery();
                isSuccessful = true;
            }
            catch (Exception)
            {
                isSuccessful = false;
            }
        }
    }
}
