using System;
using Xunit;
using BlogDB.Core;
using System.Collections.Generic;

namespace BlogCore.Tests
{
    public class MockIBlogDB : BlogDB.Core.IBlogDB<Post>
    {

        private List<Post> _testPosts;

        public MockIBlogDB(List<Post> testData)
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