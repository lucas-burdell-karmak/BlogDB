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
        public void TestAddPost_ValidData_Success(string title, string author, string body)
        {
            var mockBlogDB = new MockFileDB(_testData);
            var postRepo = new PostRepo(mockBlogDB);

            var p = new Post(title, author, body);
            var result = postRepo.AddPost(p);

            Assert.Equal(p.Title, result.Title);
            Assert.Equal(p.Author, result.Author);
            Assert.Equal(p.Body, result.Body);
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
            var result = postRepo.DeletePost(Guid.Parse(guid));

            Assert.Equal(p.Title, result.Title);
            Assert.Equal(p.Author, result.Author);
            Assert.Equal(p.Body, result.Body);
        }
    }
}