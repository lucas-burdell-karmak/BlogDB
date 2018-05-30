using System;
using System.Collections.Generic;
using Xunit;
using BlogDB.Core;
using BlogCore.Tests.Mocks;

namespace BlogCore.Tests
{
    public class PostRepoTests
    {
        private readonly List<Post> _testData;
        public PostRepoTests()
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
        public void TestAddPost_ValidData_Success(string title, string author, string body)
        {
            var mockBlogDB = new MockFileDB(_testData);
            var postRepo = new PostRepo(mockBlogDB);

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
        public void TestAddPost_InvalidData_Failure(string title, string author, string body)
        {
            var mockBlogDB = new MockFileDB(_testData);
            var postRepo = new PostRepo(mockBlogDB);

            var p = new Post(title, author, body);
            var isFailure = postRepo.TryAddPost(p, out var result);

            Assert.False(isFailure);
            Assert.Null(result);
        }

        [Theory]
        [InlineData("Title", "Author1", "Body", "2018-05-30T14:16:44.1562063Z", "7ad7f688-9c68-4f59-b241-2a20d9dfd216")]
        [InlineData("Title", "Author2", "Body", "2018-05-30T14:16:44.1562063Z", "7ad7f688-9d68-4f59-b241-2a27d9dfd216")]
        [InlineData("Title", "Author3", "Body", "2018-05-30T14:16:44.1562063Z", "7ad7f688-9e68-4f59-b241-2a20d9ddd216")]
        public void TestDeletePost_ValidData_Success(string title, string author, string body, string datetime, string guid)
        {
            var mockBlogDB = new MockFileDB(_testData);
            var postRepo = new PostRepo(mockBlogDB);

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
        public void TestDeletePost_InvalidData_Failure(string title, string author, string body, string datetime, string guid)
        {
            var mockBlogDB = new MockFileDB(_testData);
            var postRepo = new PostRepo(mockBlogDB);

            var p = new Post(title, author, body, Convert.ToDateTime(datetime), Guid.Parse(guid));
            var isFailure = postRepo.TryDeletePost(p.PostID, out var result);

            Assert.False(isFailure);
            Assert.Null(result);
        }

        [Theory]
        [InlineData("T", "A", "B", "1/1/2001 12:00:00 AM", "7ad7f688-9c68-4f59-b241-2a20d9dfd216")]
        [InlineData(" ", " ", " ", "1/1/2001 12:00:00 AM", "7ad7f688-9d68-4f59-b241-2a27d9dfd216")]
        [InlineData(" ", "  ", "", "1/1/2001 12:00:00 AM", "7ad7f688-9e68-4f59-b241-2a20d9ddd216")]
        public void TestEditPost_ValidData_Success(string title, string author, string body, string datetime, string guid)
        {
            var mockBlogDB = new MockFileDB(_testData);
            var postRepo = new PostRepo(mockBlogDB);

            var p = new Post(title, author, body, Convert.ToDateTime(datetime), Guid.Parse(guid));
            var isSuccessful = postRepo.TryEditPost(p, out var result);

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
        public void TestEditPost_InvalidData_Failure(string title, string author, string body, string datetime, string guid)
        {
            var mockBlogDB = new MockFileDB(_testData);
            var postRepo = new PostRepo(mockBlogDB);

            var p = new Post(title, author, body, Convert.ToDateTime(datetime), Guid.Parse(guid));
            var isFailure = postRepo.TryEditPost(p, out var result);

            Assert.False(isFailure);
            Assert.Null(result);
        }

        [Fact]
        public void TestGetAllPosts_ValidData_Success()
        {
            var mockBlogDB = new MockFileDB(_testData);
            var postRepo = new PostRepo(mockBlogDB);

            var list = postRepo.GetAllPosts();

            Assert.NotEmpty(list);
            Assert.Equal(_testData.Count, list.Count);
        }
    }
}