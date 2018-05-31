using System;
using System.Collections.Generic;
using Xunit;
using BlogDB.Core;

// TODO: Update this mock or delete it, code has changed
namespace BlogCore.Tests.Mocks
{
    public class MockFileDB
    {

        private List<Post> StubedListOfPosts;

        private bool CalledReadAll = false;
        private bool CalledWriteAll = false;

        public MockFileDB(List<Post> stubedData)
        {
            StubedListOfPosts = stubedData;
        }

        public void AssertReadAllCalled() => Assert.True(CalledReadAll);
        public void AssertWriteAllCalled() => Assert.True(CalledWriteAll);

        public List<Post> ReadAll()
        {
            CalledReadAll = true;
            return this.StubedListOfPosts;
        }

        public void WriteAll(List<Post> listOfPosts)
        {
            CalledWriteAll = true;
            StubedListOfPosts = listOfPosts;
        }

        public MockFileDB SetCalledReadAllToFalse()
        {
            CalledReadAll = false;
            return this;
        }

        public MockFileDB SetCalledWriteAllToFalse()
        {
            CalledWriteAll = false;
            return this;
        }
    }
}