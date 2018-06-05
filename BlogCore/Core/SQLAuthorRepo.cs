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

        private static byte[] HexStringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        private byte[] GetPasswordHash(int id) 
        {
            string passwordHash = null;
            var commandText = "SELECT PasswordHash FROM author WHERE id = @id";
            var command = new SqlCommand(commandText, _connection);
            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = id.ToString();

            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    passwordHash = reader.GetString(0);
                }
            }
            reader.Close();
            return HexStringToByteArray(passwordHash);
        }

        private byte[] GetSalt(int id)
        {
            string salt = null;
            var commandText = "SELECT Salt FROM author WHERE id = @id";
            var command = new SqlCommand(commandText, _connection);
            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = id.ToString();

            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    salt = reader.GetString(0);
                }
            }
            reader.Close();
            return HexStringToByteArray(salt);
        }

        public bool TryValidateAuthor(string name, string passwordHash, out Author author)
        {
            var authorInDB = GetAuthorByName(name);
            author = null;
            if (authorInDB == null) {
                return false;
            }

            byte[] salt = GetSalt(authorInDB.ID);
            byte[] passwordHashInDB = GetPasswordHash(authorInDB.ID);

            HMACSHA512 hash = new HMACSHA512();

            hash.Key = salt;
            byte[] computedHash = hash.ComputeHash(HexStringToByteArray(passwordHash));

            var authorized = computedHash.SequenceEqual(passwordHashInDB);
            if (authorized) {
                author = authorInDB;

            }

            return authorized;

        }

        public bool TryRegisterAuthor(string name, string passwordHash, out Author author)
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

            command.Parameters["@Roles"].Value = JsonConvert.SerializeObject(new List<string>(){
                "BlogWriter"
            });

            var result = command.ExecuteNonQuery();

            if (result < 0)
            {
                author = null;
                return false;
            }
            author = GetAuthorByName(name);
            return true;
        }

        public Author GetAuthor(int id)
        {
            Author author = null;
            var commandText = "SELECT name FROM author WHERE id = @id";
            var command = new SqlCommand(commandText, _connection);
            command.Parameters.Add("@id", SqlDbType.Int);
            command.Parameters["@id"].Value = id.ToString();

            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    author = new Author(reader.GetString(0), id);
                }
            }
            reader.Close();
            return author;
        }

        public Author GetAuthorByName(string name)
        {
            Author author = null;
            var commandText = "SELECT id FROM author WHERE name = @name";
            var command = new SqlCommand(commandText, _connection);
            command.Parameters.Add("@name", SqlDbType.NVarChar);
            command.Parameters["@name"].Value = name;

            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    author = new Author(name, reader.GetInt32(0));
                }
            }
            reader.Close();
            return author;
        }
    }
}
