using System;
using System.Collections.Generic;
using Xunit;
using BlogDB.Core;

namespace BlogCore.Tests.Mocks
{
    public class MockPostValidator : IPostValidator
    {
        private bool CalledIsValidBody = false;
        private bool CalledIsValidPost = false;
        private bool CalledIsValidString = false;
        private bool CalledIsValidTitle = false;
        private bool CalledPostExits = false;

        private bool StubedValidTitle;
        private bool StubedValidBody;
        private bool StubedValidString;
        private bool StubedValidPost;
        private bool StubedPostExists;

        public void AssertIsValidBodyCalled() => Assert.True(CalledIsValidBody);
        public void AssertIsValidPostCalled() => Assert.True(CalledIsValidPost);
        public void AssertIsValidStringCalled() => Assert.True(CalledIsValidString);
        public void AssertIsValidTitleCalled() => Assert.True(CalledIsValidTitle);
        public void AssertPostExitsCalled() => Assert.True(CalledPostExits);

        public bool IsValidBody(string body)
        {
            CalledIsValidBody = true;
            return StubedValidBody;
        }

        public bool IsValidPost(Post post)
        {
            CalledIsValidPost = true;
            return StubedValidPost;
        }

        public bool IsValidString(string str)
        {
            CalledIsValidString = true;
            return StubedValidString;
        }

        public bool IsValidTitle(string title)
        {
            CalledIsValidTitle = true;
            return StubedValidTitle;
        }

        public bool PostExists(List<Post> listOfPosts, Post post)
        {
            CalledPostExits = true;
            return StubedPostExists;
        }

        public MockPostValidator SetCalledIsValidBodyToFalse()
        {
            CalledIsValidBody = false;
            return this;
        }

        public MockPostValidator SetCalledIsValidPostToFalse()
        {
            CalledIsValidPost = false;
            return this;
        }

        public MockPostValidator SetCalledIsValidStringToFalse()
        {
            CalledIsValidString = false;
            return this;
        }

        public MockPostValidator SetCalledIsValidTitleToFalse()
        {
            CalledIsValidTitle = false;
            return this;
        }

        public MockPostValidator SetCalledPostExitsToFalse()
        {
            CalledPostExits = false;
            return this;
        }

        public MockPostValidator StubValidTitle(bool validTitle)
        {
            StubedValidTitle = validTitle;
            return this;
        }
        public MockPostValidator StubValidBody(bool validBody)
        {
            StubedValidBody = validBody;
            return this;
        }
        public MockPostValidator StubValidString(bool validString)
        {
            StubedValidString = validString;
            return this;
        }
        public MockPostValidator StubPostExists(bool postExists)
        {
            StubedPostExists = postExists;
            return this;
        }
        public MockPostValidator StubValidPost(bool validPost)
        {
            StubedValidPost = validPost;
            return this;
        }

    }
}