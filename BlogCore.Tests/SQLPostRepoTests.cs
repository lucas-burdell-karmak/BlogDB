using System;
using System.Collections.Generic;
using Xunit;
using BlogDB.Core;
using BlogCore.Tests.Mocks;

namespace BlogCore.Tests
{
    public class SQLPostRepoTests : IDisposable
    {
        private readonly List<Post> _testData;
        private readonly string sqlConnectionString = "Persist Security Info=False;Integrated Security=true;Initial Catalog=Internship_Lucas_Burdell;server=devsql";

        public SQLPostRepoTests()
        {
            _testData = BuildTestData();
        }

        private List<Post> BuildTestData()
        {
            var testData = new List<Post>();
            testData.Add(new Post("Title", "Author1", "Body", Convert.ToDateTime("2018-05-30T14:16:44.1562063Z"), Guid.Parse("7ad7f688-9c68-4f59-b241-2a20d9dfd216")));
            testData.Add(new Post("Title", "Author2", "Body", Convert.ToDateTime("2018-05-30T14:16:44.1562063Z"), Guid.Parse("7ad7f688-9d68-4f59-b241-2a27d9dfd216")));
            testData.Add(new Post("Title", "Author3", "Body", Convert.ToDateTime("2018-05-30T14:16:44.1562063Z"), Guid.Parse("7ad7f688-9e68-4f59-b241-2a20d9ddd216")));
            return testData;
        }

        [Theory]
        [InlineData("T", "A", "B")]
        [InlineData("", "", "")]
        [InlineData(" ", "   ", "       ")]
        public void TestTryAddPost_ValidData_Success(string title, string author, string body)
        {
            var sqlPostRepo = new SQLPostRepo(sqlConnectionString);
            var p = new Post(title, author, body);
            var isSuccessful = sqlPostRepo.TryAddPost(p, out var result);

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
            var sqlPostRepo = new SQLPostRepo(sqlConnectionString);
            var p = new Post(title, author, body);
            var isFailure = sqlPostRepo.TryAddPost(p, out var result);

            Assert.False(isFailure);
            Assert.Null(result);
        }

        [Theory]
        [InlineData("Title", "Author1", "Body", "2018-05-30T14:16:44.1562063Z", "7ad7f688-9c68-4f59-b241-2a20d9dfd216")]
        [InlineData("Title", "Author2", "Body", "2018-05-30T14:16:44.1562063Z", "7ad7f688-9d68-4f59-b241-2a27d9dfd216")]
        [InlineData("Title", "Author3", "Body", "2018-05-30T14:16:44.1562063Z", "7ad7f688-9e68-4f59-b241-2a20d9ddd216")]
        public void TestTryDeletePost_ValidData_Success(string title, string author, string body, string datetime, string guid)
        {
            var sqlPostRepo = new SQLPostRepo(sqlConnectionString);
            var p = new Post(title, author, body, Convert.ToDateTime(datetime), Guid.Parse(guid));
            var isSuccessful = sqlPostRepo.TryDeletePost(p.PostID, out var result);

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
            var sqlPostRepo = new SQLPostRepo(sqlConnectionString);
            var p = new Post(title, author, body, Convert.ToDateTime(datetime), Guid.Parse(guid));
            var isFailure = sqlPostRepo.TryDeletePost(p.PostID, out var result);

            Assert.False(isFailure);
            Assert.Null(result);
        }

        [Theory]
        [InlineData("T", "A", "B", "1/1/2001 12:00:00 AM", "7ad7f688-9c68-4f59-b241-2a20d9dfd216")]
        [InlineData(" ", " ", " ", "1/1/2001 12:00:00 AM", "7ad7f688-9d68-4f59-b241-2a27d9dfd216")]
        [InlineData(" ", "  ", "", "1/1/2001 12:00:00 AM", "7ad7f688-9e68-4f59-b241-2a20d9ddd216")]
        public void TestTryEditPost_ValidData_Success(string title, string author, string body, string datetime, string guid)
        {
            var sqlPostRepo = new SQLPostRepo(sqlConnectionString);
            var p = new Post(title, author, body, Convert.ToDateTime(datetime), Guid.Parse(guid));
            var isSuccessful = sqlPostRepo.TryEditPost(p, out var result);

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
        [InlineData(null, "Author1", "Body", "2018-05-30T14:16:44.1562063Z", "7ad7f688-9c68-4f59-b241-2a20d9dfd216")]
        [InlineData("Title", null, "Body", "2018-05-30T14:16:44.1562063Z", "7ad7f688-9d68-4f59-b241-2a27d9dfd216")]
        [InlineData("Title", "Author3", null, "2018-05-30T14:16:44.1562063Z", "7ad7f688-9e68-4f59-b241-2a20d9ddd216")]
        public void TestTryEditPost_InvalidData_Failure(string title, string author, string body, string datetime, string guid)
        {
            var sqlPostRepo = new SQLPostRepo(sqlConnectionString);
            var p = new Post(title, author, body, Convert.ToDateTime(datetime), Guid.Parse(guid));
            var isFailure = sqlPostRepo.TryEditPost(p, out var result);

            Assert.False(isFailure);
            Assert.Null(result);
        }

        [Fact]
        public void TestGetAllPosts()
        {
            var sqlPostRepo = new SQLPostRepo(sqlConnectionString);
            var list = sqlPostRepo.GetAllPosts();

            Assert.NotEmpty(list);
            Assert.Equal(_testData.Count, list.Count);
        }

        public void Dispose() { }
    }
}