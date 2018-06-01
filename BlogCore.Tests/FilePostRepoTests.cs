using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using BlogDB.Core;
using BlogCore.Tests.Mocks;
using Newtonsoft.Json;

namespace BlogCore.Tests
{
    public class FilePostRepoTests : IDisposable
    {
        private readonly List<Post> _testData;
        private readonly string _testDBPath = Path.Combine(Directory.GetCurrentDirectory(), "testDB.json");

        public FilePostRepoTests()
        {
            _testData = LoadTestData();
        }

        private List<Post> LoadTestData()
        {
            var testData = new List<Post>();
            testData.Add(new Post("Title0", "Author0", "Body0", Convert.ToDateTime("2018-05-30T14:16:44.1562063Z"), Guid.Parse("11111111-1111-1111-1111-111111111111")));
            testData.Add(new Post("Title1", "Author1", "Body1", Convert.ToDateTime("2018-05-30T14:16:44.1562063Z"), Guid.Parse("22222222-2222-2222-2222-222222222222")));
            testData.Add(new Post("Title2", "Author2", "Body2", Convert.ToDateTime("2018-05-30T14:16:44.1562063Z"), Guid.Parse("33333333-3333-3333-3333-333333333333")));

            // false means overwrite
            using (var writer = new StreamWriter(_testDBPath, false))
            {
                var contentsToWrite = JsonConvert.SerializeObject(testData);
                writer.Write(contentsToWrite);
            }
            return testData;
        }

        [Theory]
        [InlineData("T", "A", "B")]
        [InlineData("", "", "")]
        [InlineData(" ", "   ", "       ")]
        public void TestTryAddPost_ValidData_Success(string title, string author, string body)
        {
            var postRepo = new FilePostRepo(_testDBPath);
            var p = new Post(title, author, body);
            var isSuccessful = postRepo.TryAddPost(p, out var result);

            Assert.True(isSuccessful);
            Assert.NotNull(result);
            Assert.Equal(p.Title, result.Title);
            Assert.Equal(p.Author, result.Author);
            Assert.Equal(p.Body, result.Body);
        }

        [Theory]
        [InlineData("", "", null)]
        [InlineData("", null, "")]
        [InlineData("", null, null)]
        [InlineData(null, "", "")]
        [InlineData(null, "", null)]
        [InlineData(null, null, "")]
        [InlineData(null, null, null)]
        public void TestTryAddPost_InvalidData_Failure(string title, string author, string body)
        {
            var postRepo = new FilePostRepo(_testDBPath);
            var p = new Post(title, author, body);
            var isFailure = postRepo.TryAddPost(p, out var result);

            Assert.False(isFailure);
            Assert.Null(result);
        }

        [Theory]
        [InlineData("Title0", "Author0", "Body0", "2018-05-30T14:16:44.1562063Z", "11111111-1111-1111-1111-111111111111")]
        [InlineData("Title1", "Author1", "Body1", "2018-05-30T14:16:44.1562063Z", "22222222-2222-2222-2222-222222222222")]
        [InlineData("Title2", "Author2", "Body2", "2018-05-30T14:16:44.1562063Z", "33333333-3333-3333-3333-333333333333")]
        public void TestTryDeletePost_ValidData_Success(string title, string author, string body, string datetime, string guid)
        {
            var postRepo = new FilePostRepo(_testDBPath);
            var p = new Post(title, author, body, Convert.ToDateTime(datetime), Guid.Parse(guid));
            var isSuccessful = postRepo.TryDeletePost(p.PostID, out var result);

            Assert.True(isSuccessful);
            Assert.NotNull(result);
            Assert.Equal(p.Title, result.Title);
            Assert.Equal(p.Author, result.Author);
            Assert.Equal(p.Body, result.Body);
        }

        [Theory]
        [InlineData("Title", "Author1", "Body", "2018-05-30T14:16:44.1562063Z", "7ad7f688-9c68-4f59-b241-2a2000dfd216")]
        [InlineData("Title", "Author2", "Body", "2018-05-30T14:16:44.1562063Z", "7ad7f688-9d68-4f59-b241-2a27d9dfd126")]
        [InlineData("Title", "Author3", "Body", "2018-05-30T14:16:44.1562063Z", "7ad7f699-9e68-4f59-b241-2a20d9ddd216")]
        [InlineData(null, null, null, "1/1/0001 12:00:00 AM", "00000000-0000-0000-0000-000000000000")]
        public void TestTryDeletePost_InvalidData_Failure(string title, string author, string body, string datetime, string guid)
        {
            var postRepo = new FilePostRepo(_testDBPath);
            var p = new Post(title, author, body, Convert.ToDateTime(datetime), Guid.Parse(guid));
            var isFailure = postRepo.TryDeletePost(p.PostID, out var result);

            Assert.False(isFailure);
            Assert.Null(result);
        }

        [Theory]
        [InlineData("Title0", "Author0", "Body0", "2018-05-30T14:16:44.1562063Z", "11111111-1111-1111-1111-111111111111")]
        [InlineData("Title1", "Author1", "Body1", "2018-05-30T14:16:44.1562063Z", "22222222-2222-2222-2222-222222222222")]
        [InlineData("Title2", "Author2", "Body2", "2018-05-30T14:16:44.1562063Z", "33333333-3333-3333-3333-333333333333")]
        public void TestTryEditPost_ValidData_Success(string title, string author, string body, string datetime, string guid)
        {
            var postRepo = new FilePostRepo(_testDBPath);
            var p = new Post(title, author, body, Convert.ToDateTime(datetime), Guid.Parse(guid));
            var isSuccessful = postRepo.TryEditPost(p, out var result);

            Assert.True(isSuccessful);
            Assert.NotNull(result);
            Assert.Equal(p.Title, result.Title);
            Assert.Equal(p.Author, result.Author);
            Assert.Equal(p.Body, result.Body);
        }

        [Theory]
        [InlineData("Title", "Author1", "Body", "2018-05-30T14:16:44.1562063Z", "00000000-0000-0000-0000-000000000000")]
        [InlineData("Title", "Author2", "Body", "2018-05-30T14:16:44.1562063Z", "7ad7f688-9d68-4f59-b241-2a27d9dfd126")]
        [InlineData("Title", "Author3", "Body", "2018-05-30T14:16:44.1562063Z", "7ad7f699-9e68-4f59-b241-2a20d9ddd216")]
        [InlineData(null, "Author1", "Body", "2018-05-30T14:16:44.1562063Z", "7ad7f688-9c68-4f59-b241-2a20d9dfd216")]
        [InlineData("Title", null, "Body", "2018-05-30T14:16:44.1562063Z", "7ad7f688-9d68-4f59-b241-2a27d9dfd216")]
        [InlineData("Title", "Author3", null, "2018-05-30T14:16:44.1562063Z", "7ad7f688-9e68-4f59-b241-2a20d9ddd216")]
        public void TestTryEditPost_InvalidData_Failure(string title, string author, string body, string datetime, string guid)
        {
            var postRepo = new FilePostRepo(_testDBPath);
            var p = new Post(title, author, body, Convert.ToDateTime(datetime), Guid.Parse(guid));
            var isFailure = postRepo.TryEditPost(p, out var result);

            Assert.False(isFailure);
            Assert.Null(result);
        }

        [Fact]
        public void TestGetAllPosts()
        {
            var postRepo = new FilePostRepo(_testDBPath);
            var list = postRepo.GetAllPosts();

            Assert.NotEmpty(list);
            Assert.Equal(_testData.Count, list.Count);
        }

        public void Dispose() { }
    }
}