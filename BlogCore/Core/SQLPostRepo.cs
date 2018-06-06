using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BlogDB.Core
{
    public class SQLPostRepo : IPostRepo, IDisposable
    {
        private readonly SqlConnection _connection;
        private readonly string _sqlConnectionString;
        private readonly IAuthorRepo _authorRepo;

        public SQLPostRepo(IAuthorRepo authorRepo, IConfiguration config)
        {
            _authorRepo = authorRepo;
            _sqlConnectionString = config["SQLConnectionString"];
            _connection = new SqlConnection(_sqlConnectionString);
            _connection.Open();
        }

        public SQLPostRepo(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
            _connection = new SqlConnection(_sqlConnectionString);
            _connection.Open();
        }

        public void Dispose()
        {
            _connection.Close();
        }

        ~SQLPostRepo()
        {
            Dispose();
        }

        private Post DeletePostByID(Guid postID)
        {
            var post = ReadPost(postID);
            var commandText = "DELETE FROM Blog_Post WHERE id = @id";
            var command = new SqlCommand(commandText, _connection);

            command.Parameters.Add("@id", SqlDbType.NChar);
            command.Parameters["@id"].Value = postID.ToString();

            //command.Connection.Open();
            var result = command.ExecuteNonQuery();

            if (result < 0) Console.WriteLine("Error deleting post from database!");

            return post;
        }

        public List<Post> GetAllPosts()
        {
            return ReadAll();
        }

        public List<Post> GetAllPostsByAuthor(int authorID)
        {
            var list = new List<Post>();
            var commandText = "SELECT id, title, authorID, body, timestamp FROM Blog_Post WHERE authorID = @id";
            var command = new SqlCommand(commandText, _connection);

            command.Parameters.Add("@id", SqlDbType.NChar);
            command.Parameters["@id"].Value = authorID;

            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    list.Add(new Post(reader.GetString(1),
                                      GetAuthor(reader.GetInt32(2)),  // TODO: AUTHOR CREATION MOVED TO IAuthorRepo
                                      reader.GetString(3),
                                      reader.GetDateTime(4),
                                      Guid.Parse(reader.GetString(0))));
                }
            }
            reader.Close();

            return list;
        }

        private Author GetAuthor(int id)
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

        private List<Post> ReadAll()
        {
            var list = new List<Post>();
            var commandText = "SELECT id, title, authorID, body, timestamp FROM Blog_Post";
            var command = new SqlCommand(commandText, _connection);

            //command.Connection.Open();
            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    list.Add(new Post(reader.GetString(1),
                                      GetAuthor(reader.GetInt32(2)),  // TODO: AUTHOR CREATION MOVED TO IAuthorRepo
                                      reader.GetString(3),
                                      reader.GetDateTime(4),
                                      Guid.Parse(reader.GetString(0))));
                }
            }
            reader.Close();

            return list;
        }

        private Post ReadPost(Guid id)
        {
            Post post = null;
            var commandText = "SELECT id, title, authorID, body, timestamp FROM Blog_Post WHERE id = @id";
            var command = new SqlCommand(commandText, _connection);
            command.Parameters.Add("@id", SqlDbType.NChar);
            command.Parameters["@id"].Value = id.ToString();

            //command.Connection.Open();
            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    post = new Post(reader.GetString(1),
                                    GetAuthor(reader.GetInt32(2)), // TODO: AUTHOR CREATION MOVED TO IAuthorRepo
                                    reader.GetString(3),
                                    reader.GetDateTime(4),
                                    Guid.Parse(reader.GetString(0)));
                }
            }
            reader.Close();
            return post;
        }

        public bool TryAddPost(Post post, out Post result)
        {
            if (post == null || post.Title == null || post.Author.Name == null || post.Body == null)
            {
                result = null;
                return false;
            }
            else
            {
                try
                {
                    result = WritePost(post);
                }
                catch (SqlException e)
                {
                    result = null;
                    Console.WriteLine(e);
                    return false;
                }
                return true;
            }
        }

        public bool TryDeletePost(Guid id, out Post result)
        {
            try
            {
                result = DeletePostByID(id);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                result = null;
            }

            if (result == null)
            {
                return false;
            }
            return true;
        }

        public bool TryEditPost(Post post, out Post result)
        {
            if (post == null || post.Title == null || post.Author == null || post.Body == null)
            {
                result = null;
            }
            else
            {
                try
                {
                    result = UpdatePost(post);
                }
                catch (SqlException e)
                {
                    result = null;
                    Console.WriteLine(e);
                }

            }
            return (result == null) ? false : true;
        }

        public Post UpdatePost(Post post)
        {
            var commandText = "UPDATE Blog_Post SET body = @body, timestamp = @timestamp, title = @title WHERE id = @id";
            var command = new SqlCommand(commandText, _connection);
            command.Parameters.Add("@body", SqlDbType.NChar);
            command.Parameters["@body"].Value = post.Body;

            command.Parameters.Add("@id", SqlDbType.NChar);
            command.Parameters["@id"].Value = post.PostID.ToString();

            command.Parameters.Add("@timestamp", SqlDbType.DateTime);
            command.Parameters["@timestamp"].Value = post.Timestamp;

            command.Parameters.Add("@title", SqlDbType.NChar);
            command.Parameters["@title"].Value = post.Title;

            //command.Connection.Open();
            var result = command.ExecuteNonQuery();

            if (result < 0) return null;

            return post;
        }

        public Post WritePost(Post post)
        {
            var commandText = "INSERT INTO Blog_Post (authorid, body, id, timestamp, title) VALUES (@authorid, @body, @id, @timestamp, @title)";
            var command = new SqlCommand(commandText, _connection);

            command.Parameters.Add("@authorid", SqlDbType.Int);
            command.Parameters["@authorid"].Value = post.Author.ID;

            command.Parameters.Add("@body", SqlDbType.NChar);
            command.Parameters["@body"].Value = post.Body;

            command.Parameters.Add("@id", SqlDbType.NChar);
            command.Parameters["@id"].Value = post.PostID.ToString();

            command.Parameters.Add("@timestamp", SqlDbType.DateTime);
            command.Parameters["@timestamp"].Value = post.Timestamp;

            command.Parameters.Add("@title", SqlDbType.NChar);
            command.Parameters["@title"].Value = post.Title;

            //command.Connection.Open();
            var result = command.ExecuteNonQuery();

            if (result < 0) return null;
            return post;
        }

        private void WriteAll(List<Post> list)
        {
            var commandText = "INSERT INTO Blog_Post (authorid, body, id, timestamp, title) VALUES (@author, @body, @id, @timestamp, @title)";
            var command = new SqlCommand(commandText, _connection);

            foreach (var post in list)
            {
                command.Parameters.Add("@author", SqlDbType.NChar);
                command.Parameters["@author"].Value = post.Author.ID;

                command.Parameters.Add("@body", SqlDbType.NChar);
                command.Parameters["@body"].Value = post.Body;

                command.Parameters.Add("@id", SqlDbType.NChar);
                command.Parameters["@id"].Value = post.PostID.ToString();

                command.Parameters.Add("@timestamp", SqlDbType.DateTime);
                command.Parameters["@timestamp"].Value = post.Timestamp;

                command.Parameters.Add("@title", SqlDbType.NChar);
                command.Parameters["@title"].Value = post.Title;
            }

            //command.Connection.Open();
            var result = command.ExecuteNonQuery();

            if (result < 0) Console.WriteLine("Error inserting data into database!");
        }
    }
}