using System;
using System.Collections.Generic;
using Xunit;
using BlogDB.Core;
using System.IO;
using Newtonsoft.Json;

namespace BlogCore.Tests
{
    public class FileDBTests : IDisposable
    {
        private readonly string _path;
        public FileDBTests()
        {
            // init database with test value
            _path = Path.Combine(Directory.GetCurrentDirectory(), "testBlogDatabase.json");
        }

        [Fact]
        public void TestWriteToFile()
        {
            var fileDB = new FileDB<Post>(_path);
            var post = new Post("Title", "Author", "Body");
            var list = new List<Post>();
            list.Add(post);
            fileDB.WriteAll(list);

            List<Post> listFromDB;
            using (var reader = new StreamReader(new FileStream(_path, FileMode.OpenOrCreate)))
            {
                var fileContents = reader.ReadToEnd();
                listFromDB = JsonConvert.DeserializeObject<List<Post>>(fileContents);
                if (listFromDB == null)
                {
                    listFromDB = new List<Post>();
                }
            }

            Assert.NotEmpty(listFromDB);
            Assert.Equal(list.Count, listFromDB.Count);
            Assert.Equal(list[0].PostID, listFromDB[0].PostID);
            Assert.Equal(list[0].Title, listFromDB[0].Title);
            Assert.Equal(list[0].Author, listFromDB[0].Author);
            Assert.Equal(list[0].Body, listFromDB[0].Body);
            Assert.Equal(list[0].Timestamp, listFromDB[0].Timestamp);
        }

        [Fact]
        public void TestReadFromFile()
        {
            var fileDB = new FileDB<Post>(_path);
            var list = new List<Post>();
            var post = new Post("Title", "Author", "Body");
            list.Add(post);
            using (var writer = new StreamWriter(_path, false)) // false means overwrite
            {
                var contentsToWrite = JsonConvert.SerializeObject(list);
                writer.Write(contentsToWrite);
            }

            var listFromDB = fileDB.ReadAll();

            Assert.NotEmpty(listFromDB);
            Assert.Equal(list.Count, listFromDB.Count);
            Assert.Equal(list[0].PostID, listFromDB[0].PostID);
            Assert.Equal(list[0].Title, listFromDB[0].Title);
            Assert.Equal(list[0].Author, listFromDB[0].Author);
            Assert.Equal(list[0].Body, listFromDB[0].Body);
            Assert.Equal(list[0].Timestamp, listFromDB[0].Timestamp);
        }

        public void Dispose() { }
    }
}