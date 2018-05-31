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
    public class SQLPostRepo : IPostRepo
    {
        public readonly IConfiguration _config;

        public SQLPostRepo(IConfiguration config)
        {
            _config = config;
        }

        private Post DeletePostByID(Guid postID)
        {
            var post = ReadPost(postID);
            var commandText = "DELETE FROM Blog_Post WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_config["SQLConnectionString"]))
            {
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.Add("@id", SqlDbType.NChar);
                command.Parameters["@id"].Value = postID.ToString();

                command.Connection.Open();
                var result = command.ExecuteNonQuery();

                if (result < 0) Console.WriteLine("Error deleting post from database!");
            }
            return post;
        }

        public List<Post> GetAllPosts()
        {
            return ReadAll();
        }

        private List<Post> ReadAll()
        {
            var list = new List<Post>();
            var commandText = "SELECT id, title, author, body, timestamp FROM Blog_Post";
            using (SqlConnection connection = new SqlConnection(_config["SQLConnectionString"]))
            {
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(new Post(reader.GetString(1),
                                          reader.GetString(2),
                                          reader.GetString(3),
                                          reader.GetDateTime(4),
                                          Guid.Parse(reader.GetString(0))));
                    }
                }
                reader.Close();
            }
            return list;
        }

        private Post ReadPost(Guid id)
        {
            Post post = null;
            var commandText = "SELECT id, title, author, body, timestamp FROM Blog_Post WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_config["SQLConnectionString"]))
            {
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.Add("@id", SqlDbType.NChar);
                command.Parameters["@id"].Value = id.ToString();

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        post = new Post(reader.GetString(1),
                                        reader.GetString(2),
                                        reader.GetString(3),
                                        reader.GetDateTime(4),
                                        Guid.Parse(reader.GetString(0)));
                    }
                }
                reader.Close();
            }
            return post;
        }

        public bool TryAddPost(Post post, out Post result)
        {
            if (post == null || post.Title == null || post.Author == null || post.Body == null)
            {
                result = null;
                return false;
            }
            else
            {
                result = WritePost(post);
                return true;
            }
        }

        public bool TryDeletePost(Guid id, out Post result)
        {
            result = DeletePostByID(id);
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
                result = UpdatePost(post);
            }
            return (result == null) ? false : true;
        }

        public Post UpdatePost(Post post)
        {
            var commandText = "UPDATE Blog_Post SET author = @author, body = @body, timestamp = @timestamp, title = @title WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(_config["SQLConnectionString"]))
            {
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.Add("@author", SqlDbType.NChar);
                command.Parameters["@author"].Value = post.Author;

                command.Parameters.Add("@body", SqlDbType.NChar);
                command.Parameters["@body"].Value = post.Body;

                command.Parameters.Add("@id", SqlDbType.NChar);
                command.Parameters["@id"].Value = post.PostID.ToString();

                command.Parameters.Add("@timestamp", SqlDbType.DateTime);
                command.Parameters["@timestamp"].Value = post.Timestamp;

                command.Parameters.Add("@title", SqlDbType.NChar);
                command.Parameters["@title"].Value = post.Title;

                command.Connection.Open();
                var result = command.ExecuteNonQuery();

                if (result < 0) return null;
            }
            return post;
        }

        public Post WritePost(Post post)
        {
            var commandText = "INSERT INTO Blog_Post (author, body, id, timestamp, title) VALUES (@author, @body, @id, @timestamp, @title)";
            using (SqlConnection connection = new SqlConnection(_config["SQLConnectionString"]))
            {
                SqlCommand command = new SqlCommand(commandText, connection);

                command.Parameters.Add("@author", SqlDbType.NChar);
                command.Parameters["@author"].Value = post.Author;

                command.Parameters.Add("@body", SqlDbType.NChar);
                command.Parameters["@body"].Value = post.Body;

                command.Parameters.Add("@id", SqlDbType.NChar);
                command.Parameters["@id"].Value = post.PostID.ToString();

                command.Parameters.Add("@timestamp", SqlDbType.DateTime);
                command.Parameters["@timestamp"].Value = post.Timestamp;

                command.Parameters.Add("@title", SqlDbType.NChar);
                command.Parameters["@title"].Value = post.Title;

                command.Connection.Open();
                var result = command.ExecuteNonQuery();

                if (result < 0) return null;
            }
            return post;
        }

        private void WriteAll(List<Post> list)
        {
            var commandText = "INSERT INTO Blog_Post (author, body, id, timestamp, title) VALUES (@author, @body, @id, @timestamp, @title)";
            using (SqlConnection connection = new SqlConnection(_config["SQLConnectionString"]))
            {
                SqlCommand command = new SqlCommand(commandText, connection);

                foreach (Post p in list)
                {
                    command.Parameters.Add("@author", SqlDbType.NChar);
                    command.Parameters["@author"].Value = p.Author;

                    command.Parameters.Add("@body", SqlDbType.NChar);
                    command.Parameters["@body"].Value = p.Body;

                    command.Parameters.Add("@id", SqlDbType.NChar);
                    command.Parameters["@id"].Value = p.PostID.ToString();

                    command.Parameters.Add("@timestamp", SqlDbType.DateTime);
                    command.Parameters["@timestamp"].Value = p.Timestamp;

                    command.Parameters.Add("@title", SqlDbType.NChar);
                    command.Parameters["@title"].Value = p.Title;
                }

                command.Connection.Open();
                var result = command.ExecuteNonQuery();

                if (result < 0) Console.WriteLine("Error inserting data into database!");
            }
        }
    }
}