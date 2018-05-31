using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace BlogDB.Core
{
    public class FileDB : IBlogDB
    {
        private readonly string DatabaseFilePath;

        public FileDB(IConfiguration config)
        {
            DatabaseFilePath = config["DatabaseFilePath"];
        }

        public FileDB(string path)
        {
            DatabaseFilePath = path;
        }

        public List<Post> ReadAll()
        {
            using (var reader = new StreamReader(new FileStream(DatabaseFilePath, FileMode.OpenOrCreate)))
            {
                var fileContents = reader.ReadToEnd();
                var posts = JsonConvert.DeserializeObject<List<Post>>(fileContents);
                if (posts == null)
                {
                    posts = new List<Post>();
                }
                return posts;
            }
        }

        public void WriteAll(List<Post> listOfPosts)
        {
            // false means overwrite
            using (var writer = new StreamWriter(DatabaseFilePath, false))
            {
                var contentsToWrite = JsonConvert.SerializeObject(listOfPosts);
                writer.Write(contentsToWrite);
            }
        }
    }
}