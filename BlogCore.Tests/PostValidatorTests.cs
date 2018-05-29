using System;
using System.Collections.Generic;
using Xunit;
using BlogDB.Core;

namespace BlogCore.Tests
{
    public class PostValidatorTests
    {
        private readonly IPostValidator _postValidator;

        public PostValidatorTests()
        {
            _postValidator = new PostValidator();
        }

        [Fact]
        public void PostExists_True_Success()
        {
            var listOfPosts = new List<Post>();
            var postInList = new Post("I Like To Code", "Codes Alot", "This is my body, this is my soul.");
            listOfPosts.Add(postInList);
            Assert.Equal(true, _postValidator.PostExists(listOfPosts, postInList));
        }

        [Fact]
        public void PostExists_False_Success()
        {
            var listOfPosts = new List<Post>();
            var postNotInList = new Post("I Love To Code", "Codes Alotmore", "This is my body, seriously... I'm trapped here.");
            Assert.Equal(false, _postValidator.PostExists(listOfPosts, postNotInList));
        }

        [Fact]
        public void IsValidAuthor_Empty_Success()
        {
            var emptyStr = "";
            Assert.Equal(false, _postValidator.IsValidAuthor(emptyStr));
        }

        [Fact]
        public void IsValidAuthor_Null_Success()
        {
            string nullStr = null;
            Assert.Equal(false, _postValidator.IsValidAuthor(nullStr));
        }

        [Fact]
        public void IsValidAuthor_Whitespace_Success()
        {
            var whitespaceStr = " ";
            Assert.Equal(false, _postValidator.IsValidAuthor(whitespaceStr));
        }

        [Fact]
        public void IsValidAuthor_Valid_Success()
        {
            var notEmptyNullOrWhitespace = "Codes Alot";
            Assert.Equal(true, _postValidator.IsValidAuthor(notEmptyNullOrWhitespace));
        }

        [Fact]
        public void IsValidBody_Empty_Success()
        {
            var emptyStr = "";
            Assert.Equal(false, _postValidator.IsValidBody(emptyStr));
        }

        [Fact]
        public void IsValidBody_Null_Success()
        {
            string nullStr = null;
            Assert.Equal(false, _postValidator.IsValidBody(nullStr));
        }

        [Fact]
        public void IsValidBody_Whitespace_Success()
        {
            var whitespaceStr = " ";
            Assert.Equal(false, _postValidator.IsValidBody(whitespaceStr));
        }

        [Fact]
        public void IsValidBody_Valid_Success()
        {
            var notEmptyNullOrWhitespace = "This is my body, this is my soul.";
            Assert.Equal(true, _postValidator.IsValidBody(notEmptyNullOrWhitespace));
        }

        [Fact]
        public void IsValidTitle_Empty_Success()
        {
            var emptyStr = "";
            Assert.Equal(false, _postValidator.IsValidTitle(emptyStr));
        }

        [Fact]
        public void IsValidTitle_Null_Success()
        {
            string nullStr = null;
            Assert.Equal(false, _postValidator.IsValidTitle(nullStr));
        }

        [Fact]
        public void IsValidTitle_Whitespace_Success()
        {
            var whitespaceStr = " ";
            Assert.Equal(false, _postValidator.IsValidTitle(whitespaceStr));
        }

        [Fact]
        public void IsValidTitle_Valid_Success()
        {
            var notEmptyNullOrWhitespace = "I Like To Code Stuff";
            Assert.Equal(true, _postValidator.IsValidTitle(notEmptyNullOrWhitespace));
        }

        [Fact]
        public void IsValidString_Empty_Success()
        {
            var emptyStr = "";
            Assert.Equal(false, _postValidator.IsValidString(emptyStr));
        }

        [Fact]
        public void IsValidString_Null_Success()
        {
            string nullStr = null;
            Assert.Equal(false, _postValidator.IsValidString(nullStr));
        }

        [Fact]
        public void IsValidString_Whitespace_Success()
        {
            var whitespaceStr = " ";
            Assert.Equal(false, _postValidator.IsValidString(whitespaceStr));
        }

        [Fact]
        public void IsValidString_Valid_Success()
        {
            var notEmptyNullOrWhitespace = "I'm a string";
            Assert.Equal(true, _postValidator.IsValidString(notEmptyNullOrWhitespace));
        }



        [Fact]
        public void IsValidPost_EmptyPostValues_Success()
        {
            var emptyPostValues = new Post("", "", "");
            Assert.Equal(false, _postValidator.IsValidPost(emptyPostValues));
        }

        [Fact]
        public void IsValidPost_EmptyAuthor_Success()
        {
            var emptyAuthorPost = new Post("I Like To Code", "", "This is my body, this is my soul.");
            Assert.Equal(false, _postValidator.IsValidPost(emptyAuthorPost));
        }

        [Fact]
        public void IsValidPost_EmptyBody_Success()
        {
            var emptyBodyPost = new Post("I Like To Code", "Codes Alot", "");
            Assert.Equal(false, _postValidator.IsValidPost(emptyBodyPost));
        }

        [Fact]
        public void IsValidPost_EmptyTitle_Success()
        {
            var emptyTitlePost = new Post("", "Codes Alot", "This is my body, this is my soul.");
            Assert.Equal(false, _postValidator.IsValidPost(emptyTitlePost));
        }

        [Fact]
        public void IsValidPost_EmptyAuthorAndBody_Success()
        {
            var emptyAuthorBodyPost = new Post("I Like To Code", "", "");
            Assert.Equal(false, _postValidator.IsValidPost(emptyAuthorBodyPost));
        }

        [Fact]
        public void IsValidPost_EmptyAuthorAndTitle_Success()
        {
            var emptyAuthorTitlePost = new Post("", "", "This is my body, this is my soul.");
            Assert.Equal(false, _postValidator.IsValidPost(emptyAuthorTitlePost));
        }


        [Fact]
        public void IsValidPost_EmptyBodyAndTitle_Success()
        {
            var emptyBodyTitlePost = new Post("", "Codes Alot", "");
            Assert.Equal(false, _postValidator.IsValidPost(emptyBodyTitlePost));
        }

        [Fact]
        public void IsValidPost_NullPost_Success()
        {
            Post nullPost = null;
            Assert.Equal(false, _postValidator.IsValidPost(nullPost));
        }

        [Fact]
        public void IsValidPost_NullPostValues_Success()
        {
            var nullPostValues = new Post(null, null, null);
            Assert.Equal(false, _postValidator.IsValidPost(nullPostValues));
        }
        [Fact]
        public void IsValidPost_NullAuthor_Success()
        {
            var nullAuthorPost = new Post("I Like To Code", null, "This is my body, this is my soul.");
            Assert.Equal(false, _postValidator.IsValidPost(nullAuthorPost));
        }

        [Fact]
        public void IsValidPost_NullBody_Success()
        {
            var nullBodyPost = new Post("I Like To Code", "Codes Alot", null);
            Assert.Equal(false, _postValidator.IsValidPost(nullBodyPost));
        }

        [Fact]
        public void IsValidPost_NullTitle_Success()
        {
            var nullTitlePost = new Post(null, "Codes Alot", "This is my body, this is my soul.");
            Assert.Equal(false, _postValidator.IsValidPost(nullTitlePost));
        }

        [Fact]
        public void IsValidPost_NullAuthorAndBody_Success()
        {
            var nullAuthorBodyPost = new Post("I Like To Code", null, null);
            Assert.Equal(false, _postValidator.IsValidPost(nullAuthorBodyPost));
        }

        [Fact]
        public void IsValidPost_NullAuthorAndTitle_Success()
        {
            var nullAuthorTitlePost = new Post(null, null, "This is my body, this is my soul.");
            Assert.Equal(false, _postValidator.IsValidPost(nullAuthorTitlePost));
        }

        [Fact]
        public void IsValidPost_NullBodyAndTitle_Success()
        {
            var nullBodyTitlePost = new Post(null, "Codes Alot", null);
            Assert.Equal(false, _postValidator.IsValidPost(nullBodyTitlePost));
        }

        [Fact]
        public void IsValidPost_WhitespacePostValues_Success()
        {
            var whitespacePostValues = new Post(" ", " ", " ");
            Assert.Equal(false, _postValidator.IsValidPost(whitespacePostValues));
        }

        [Fact]
        public void IsValidPost_WhitespaceAuthor_Success()
        {
            var whitespaceAuthorPost = new Post("I Like To Code", " ", "This is my body, this is my soul.");
            Assert.Equal(false, _postValidator.IsValidPost(whitespaceAuthorPost));
        }


        [Fact]
        public void IsValidPost_WhitespaceBody_Success()
        {
            var whitespaceBodyPost = new Post("I Like To Code", "Codes Alot", " ");
            Assert.Equal(false, _postValidator.IsValidPost(whitespaceBodyPost));

        }

        [Fact]
        public void IsValidPost_WhitespaceTitle_Success()
        {
            var whitespaceTitlePost = new Post(" ", "Codes Alot", "This is my body, this is my soul.");
            Assert.Equal(false, _postValidator.IsValidPost(whitespaceTitlePost));
        }

        [Fact]
        public void IsValidPost_WhitespaceAuthorAndBody_Success()
        {
            var whitespaceAuthorBodyPost = new Post("I Like To Code", " ", " ");
            Assert.Equal(false, _postValidator.IsValidPost(whitespaceAuthorBodyPost));
        }

        [Fact]
        public void IsValidPost_WhitespaceAuthorAndTitle_Success()
        {
            var whitespaceAuthorTitlePost = new Post(" ", " ", "This is my body, this is my soul.");
            Assert.Equal(false, _postValidator.IsValidPost(whitespaceAuthorTitlePost));
        }

        [Fact]
        public void IsValidPost_WhitespaceBodyAndTitle_Success()
        {
            var whitespaceBodyTitlePost = new Post(" ", "Codes Alot", " ");
            Assert.Equal(false, _postValidator.IsValidPost(whitespaceBodyTitlePost));
        }


        [Fact]
        public void IsValidPost_Valid_Success()
        {
            var validPostValues = new Post("I Like To Code", "Codes Alot", "This is my body, this is my soul.");
            Assert.Equal(true, _postValidator.IsValidPost(validPostValues));
        }
    }
}
