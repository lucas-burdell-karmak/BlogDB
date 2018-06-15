using System.Collections.Generic;
using Xunit;
using BlogDB.Core;

namespace BlogCore.Tests.Mocks
{
    public class MockPostValidator : IPostValidator
    {
        private bool _calledIsValidBody = false;
        private bool _calledIsValidPost = false;
        private bool _calledIsValidString = false;
        private bool _calledIsValidTitle = false;
        private bool _calledPostExits = false;

        private bool _stubedValidTitle;
        private bool _stubedValidBody;
        private bool _stubedValidString;
        private bool _stubedValidPost;
        private bool _stubedPostExists;

        public void AssertIsValidBodyCalled() => Assert.True(_calledIsValidBody);
        public void AssertIsValidPostCalled() => Assert.True(_calledIsValidPost);
        public void AssertIsValidStringCalled() => Assert.True(_calledIsValidString);
        public void AssertIsValidTitleCalled() => Assert.True(_calledIsValidTitle);
        public void AssertPostExistsCalled() => Assert.True(_calledPostExits);

        public bool IsValidBody(string body)
        {
            _calledIsValidBody = true;
            return _stubedValidBody;
        }
        public bool IsValidPost(Post post)
        {
            _calledIsValidPost = true;
            return _stubedValidPost;
        }
        public bool IsValidString(string str)
        {
            _calledIsValidString = true;
            return _stubedValidString;
        }
        public bool IsValidTitle(string title)
        {
            _calledIsValidTitle = true;
            return _stubedValidTitle;
        }
        public bool PostExists(List<Post> listOfPosts, Post post)
        {
            _calledPostExits = true;
            return _stubedPostExists;
        }

        public MockPostValidator SetCalledIsValidBodyToFalse()
        {
            _calledIsValidBody = false;
            return this;
        }
        public MockPostValidator SetCalledIsValidPostToFalse()
        {
            _calledIsValidPost = false;
            return this;
        }
        public MockPostValidator SetCalledIsValidStringToFalse()
        {
            _calledIsValidString = false;
            return this;
        }
        public MockPostValidator SetCalledIsValidTitleToFalse()
        {
            _calledIsValidTitle = false;
            return this;
        }
        public MockPostValidator SetCalledPostExitsToFalse()
        {
            _calledPostExits = false;
            return this;
        }

        public MockPostValidator StubValidTitle(bool validTitle)
        {
            _stubedValidTitle = validTitle;
            return this;
        }
        public MockPostValidator StubValidBody(bool validBody)
        {
            _stubedValidBody = validBody;
            return this;
        }
        public MockPostValidator StubValidString(bool validString)
        {
            _stubedValidString = validString;
            return this;
        }
        public MockPostValidator StubPostExists(bool postExists)
        {
            _stubedPostExists = postExists;
            return this;
        }
        public MockPostValidator StubValidPost(bool validPost)
        {
            _stubedValidPost = validPost;
            return this;
        }
    }
}