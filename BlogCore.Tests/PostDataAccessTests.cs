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
            _testData = new List<Post>();
            _testData.Add(new Post("Title","Author1","Body"));
            _testData.Add(new Post("Title","Author2","Body"));
            _testData.Add(new Post("Title","Author3","Body"));
            _repo = new PostRepo(new MockIBlogDB(_testData));
            _validator = new MockIPostValidator();
            _postDataAccess = new PostDataAccess(_repo, _validator);
        }

        public void Dispose()
        {

        }

        [Fact]
        public void GetAllAuthors_Succeed()
        {
            var validList = _postDataAccess.GetListOfPostsByAuthor("Author1");

            Assert.Equal(_testData[0].Title, validList[0].Title);
            Assert.Equal(_testData[0].Author, validList[0].Author);
            Assert.Equal(_testData[0].Body, validList[0].Body);
        }
    }

}