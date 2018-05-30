using System;
using Xunit;
using BlogDB.Core;
using System.Collections.Generic;

namespace BlogCore.Tests.Mocks
{
    public class MockPostValidator : IPostValidator
    {

        private bool StubValidAuthor = true;
        private bool StubValidTitle = true;
        private bool StubValidBody = true;
        private bool StubValidString = true;
        private bool StubValidPost = true;
        private bool StubPostExists = true;


        public MockPostValidator SetStubValidAuthor(bool validAuthor)
        {
            StubValidAuthor = validAuthor;
            return this;
        }

        public MockPostValidator SetStubValidTitle(bool validTitle)
        {
            StubValidTitle = validTitle;
            return this;
        }

        public MockPostValidator SetStubValidBody(bool validBody)
        {
            StubValidBody = validBody;
            return this;
        }

        public MockPostValidator SetStubValidString(bool validString)
        {
            StubValidString = validString;
            return this;
        }

        public MockPostValidator SetStubPostExists(bool postExists)
        {
            StubPostExists = postExists;
            return this;
        }

        public MockPostValidator SetStubValidPost(bool validPost)
        {
            StubValidPost = validPost;
            return this;
        }

        public bool IsValidAuthor(string author)
        {
            return StubValidAuthor;
        }

        public bool IsValidBody(string body)
        {
            return StubValidBody;
        }

        public bool IsValidPost(Post post)
        {
            return StubValidPost;
        }

        public bool IsValidString(string str)
        {
            return StubValidString;
        }

        public bool IsValidTitle(string title)
        {
            return StubValidTitle;
        }

        public bool PostExists(List<Post> listOfPosts, Post post)
        {
            return StubPostExists;
        }
    }
}