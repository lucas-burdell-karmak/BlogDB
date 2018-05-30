using System;
using Xunit;
using BlogDB.Core;
using System.Collections.Generic;

namespace BlogCore.Tests.Mocks
{
    public class MockFileDB : BlogDB.Core.IBlogDB<Post>
    {

        private List<Post> _testPosts;

        public MockFileDB(List<Post> testData)
        {
            _testPosts = testData;
        }
        public List<Post> ReadAll()
        {
            return this._testPosts;
        }

        public void WriteAll(List<Post> posts)
        {
            _testPosts = posts;
        }
    }
}