using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDB.Core
{
    public class SqlDB : IBlogDB
    {
        public static string connString = "Persist Security Info=False;Integrated Security=true;Initial Catalog=Internship_Lucas_Burdell;server=devsql";
        public List<Post> ReadAll()
        {
            var list = new List<Post>();
            var commandText = "SELECT * FROM Blog_Post";
            using (SqlConnection connection = new SqlConnection(connString))
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

        public void WriteAll(List<Post> list)
        {
            var commandText = "INSERT INTO Blog_Post (author, body, id, timestamp, title) VALUES (@author, @body, @id, @timestamp, @title)";
            using (SqlConnection connection = new SqlConnection(connString))
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