using System;
using Xunit;
using BlogDB.Core;
using System.Collections.Generic;

namespace BlogCore.Tests 
{
    public class MockIPostValidator : IPostValidator
    {
        public bool IsValidAuthor(string author)
        {
            return true;
        }

        public bool IsValidBody(string body)
        {
            return true;
        }

        public bool IsValidPost(Post post)
        {
            return true;
        }

        public bool IsValidString(string str)
        {
            return true;
        }

        public bool IsValidTitle(string title)
        {
            return true;
        }

        public bool PostExists(List<Post> listOfPosts, Post post)
        {
            return true;
        }
    }
}