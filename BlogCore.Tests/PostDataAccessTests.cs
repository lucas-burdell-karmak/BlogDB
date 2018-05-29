using System;
using Xunit;
using BlogDB.Core;
using System.Collections.Generic;

namespace BlogCore.Tests 
{
    public class PostDataAccessTests : IDisposable
    {

        
        private readonly PostRepo _repo;
        private readonly IPostValidator _validator;
        private readonly IPostDataAccess _postDataAccess;
        private readonly List<Post> _testData;

        public PostDataAccessTests()
        {
            _testData = BuildTestData();
            _repo = new PostRepo(new MockIBlogDB(_testData));
            _validator = new MockIPostValidator();
            _postDataAccess = new PostDataAccess(_repo, _validator);
        }

        private List<Post> BuildTestData()
        {
            var testData = new List<Post>();
            testData.Add(new Post("Title","Author1","Body"));
            testData.Add(new Post("Title","Author2","Body"));
            testData.Add(new Post("Title","Author3","Body"));
            return testData;
        }

        public void Dispose()
        {

        }


        [Fact]
        public void AddPost_Null_Failure()
        {
            Assert.Throws<ArgumentException>(() => _postDataAccess.AddPost(null));
        }

        [Fact]
        public void AddPost_Empty_Failure()
        {
            Assert.Throws<ArgumentException>(() => _postDataAccess.AddPost(new Post()));
        }

        [Fact]
        public void DeletePost_

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
        public void AddPost_Success()
        {
            var post = new Post("Title", "Body", "Author");

            var postResult =_postDataAccess.AddPost(post);
            
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