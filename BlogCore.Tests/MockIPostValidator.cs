using System;
using Xunit;
using BlogDB.Core;
using System.Collections.Generic;

namespace BlogCore.Tests 
{
    public class MockIPostValidator : IPostValidator
    {
        public bool isValidAuthor(string author)
        {
            return true;
        }

        public bool isValidBody(string body)
        {
            return true;
        }

        public bool isValidPost(Post post)
        {
            return true;
        }

        public bool isValidString(string str)
        {
            return true;
        }

        public bool isValidTitle(string title)
        {
            return true;
        }

        public bool postExists(List<Post> listOfPosts, Post post)
        {
            return true;
        }
    }
}