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
    }

}