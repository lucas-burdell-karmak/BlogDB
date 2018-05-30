using System;
using System.Collections.Generic;
using Xunit;
using BlogDB.Core;
using BlogCore.Tests.Mocks;

namespace BlogCore.Tests
{
    public class PostDataAccessTests : IDisposable
    {
        private readonly MockPostRepo _postRepo;
        private readonly IPostDataAccess _postDataAccess;
        private readonly MockPostValidator _postValidator;
        private readonly List<Post> _testData;

        public PostDataAccessTests()
        {
            _testData = BuildTestData();
            _postRepo = new MockPostRepo();
            _postValidator = new MockPostValidator();
            _postDataAccess = new PostDataAccess(_postRepo, _postValidator);
        }

        private List<Post> BuildTestData()
        {
            var testData = new List<Post>();
            testData.Add(new Post("Title", "Author1", "Body"));
            testData.Add(new Post("Title", "Author2", "Body"));
            testData.Add(new Post("Title", "Author3", "Body"));
            return testData;
        }

        [Fact]
        public void TestTryAddPost_ValidData_Success()
        {

        }

        [Fact]
        public void TestAddPost_InvalidData_Failure()
        {

        }

        [Fact]
        public void TestDeletePost_ValidData_Success()
        {

        }

        [Fact]
        public void TestDeletePost_InvalidData_Failure()
        {

        }

        [Fact]
        public void TestEditPost_ValidData_Success()
        {

        }

        [Fact]
        public void TestEditPost_InvalidData_Failure()
        {

        }

        [Fact]
        public void TestGetAllPosts()
        {

        }

        [Fact]
        public void TestGetListOfPostsByAuthors()
        {

        }

        [Fact]
        public void TestGetPostById()
        {

        }

        [Fact]
        public void TestGetPostCount()
        {

        }

        [Fact]
        public void TestGetPostFromList()
        {

        }

        [Fact]
        public void TestGetSortedListOfPosts()
        {

        }

        [Fact]
        public void TestSearchBy_ValidData_Success()
        {

        }

        [Fact]
        public void TestSearchBy_InvalidData_Failure()
        {

        }

        public void Dispose()
        {

        }
    }

}