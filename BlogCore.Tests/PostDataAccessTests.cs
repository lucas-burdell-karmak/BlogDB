using System;
using System.Collections.Generic;
using Xunit;
using BlogDB.Core;
using BlogCore.Tests.Mocks;

namespace BlogCore.Tests
{
    public class PostDataAccessTests : IDisposable
    {
        private readonly IPostDataAccess _postDataAccess;
        private readonly List<Post> _testData;

        public PostDataAccessTests()
        {
            _testData = BuildTestData();
            _postDataAccess = new MockPostDataAccess();
        }

        private List<Post> BuildTestData()
        {
            var testData = new List<Post>();
            testData.Add(new Post("Title", "Author1", "Body"));
            testData.Add(new Post("Title", "Author2", "Body"));
            testData.Add(new Post("Title", "Author3", "Body"));
            return testData;
        }

        public void Dispose()
        {

        }


        [Fact]
        public void 






        [Fact]
        public void AddPost_InvalidPost_Failure()
        {
            _validator(false);

            Assert.Throws<ArgumentException>(() => _postDataAccess.AddPost(null));
        }

        [Fact]
        public void DeletePost_InvalidPost_Failure()
        {
            Assert.Throws<ArgumentException>(() => _postDataAccess.DeletePost(null));
        }

        [Fact]
        public void DeletePost_InexistantPost_Failure()
        {
            _validator.SetStubValidPost(true).SetStubPostExists(false);
            Assert.Throws<ArgumentException>(() => _postDataAccess.DeletePost(new Post()));
        }



        [Fact]
        public void DeletePost_Success()
        {
            var post = new Post("Title", "Body", "Author");

            _postDataAccess.AddPost(post);
            var resultPost = _postDataAccess.DeletePost(post);

            Assert.Equal(post.Title, resultPost.Title);
            Assert.Equal(post.Author, resultPost.Author);
            Assert.Equal(post.Body, resultPost.Body);
        }

        [Fact]
        public void EditPost_Success()
        {
            var testPost = _testData[0];

            var resultPost = _postDataAccess.EditPost(testPost);

            Assert.Equal(testPost.Title, resultPost.Title);
            Assert.Equal(testPost.Author, resultPost.Author);
            Assert.Equal(testPost.Body, resultPost.Body);
        }

        [Fact]
        public void EditPost_Null_Failure()
        {
            Assert.Throws<ArgumentException>(() => _postDataAccess.EditPost(null));
        }

        [Fact]
        public void EditPost_Empty_Failure()
        {
            Assert.Throws<ArgumentException>(() => _postDataAccess.EditPost(new Post()));
        }

        [Fact]
        public void AddPost_Success()
        {
            var post = new Post("Title", "Body", "Author");

            var postResult = _postDataAccess.AddPost(post);

            Assert.NotNull(postResult);
            Assert.Equal(post.Title, postResult.Title);
            Assert.Equal(post.Author, postResult.Author);
            Assert.Equal(post.Body, postResult.Body);
        }

        [Fact]
        public void GetAllAuthors_Success()
        {
            var validList = _postDataAccess.GetListOfPostsByAuthor("Author1");

            Assert.Equal(_testData[0].Title, validList[0].Title);
            Assert.Equal(_testData[0].Author, validList[0].Author);
            Assert.Equal(_testData[0].Body, validList[0].Body);
        }
    }

}