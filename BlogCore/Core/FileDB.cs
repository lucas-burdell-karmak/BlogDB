using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;
using System.Linq;

namespace BlogDB.Core
{
    public class FileDB
    {
        public readonly string DatabasePath = Path.Combine(Directory.GetCurrentDirectory(), "blogDatabase.json");

        public List<Post> ReadFromJsonFile()
        {
            using (var reader = new StreamReader(new FileStream(DatabasePath, FileMode.OpenOrCreate)))
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

        public void WriteToJsonFile(List<Post> listOfPosts)
        {
            // false means overwrite
            using (var writer = new StreamWriter(DatabasePath, false))
            {
                var contentsToWrite = JsonConvert.SerializeObject(listOfPosts);
                writer.Write(contentsToWrite);
            }
        }
    }
}