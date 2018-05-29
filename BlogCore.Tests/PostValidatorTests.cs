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
        public void PostExists_Success()
        {
            // Assign
            var listOfPosts = new List<Post>();
            var postInList = new Post("I Like To Code", "Codes Alot", "This is my body, this is my soul.");
            var postNotInList = new Post("I Love To Code", "Codes Alotmore", "This is my body, seriously... I'm trapped here.");

            // Act
            listOfPosts.Add(postInList);

            // Asserts
            Assert.Equal(true, _postValidator.PostExists(listOfPosts, postInList));
            Assert.Equal(false, _postValidator.PostExists(listOfPosts, postNotInList));
        }

        [Fact]
        public void IsValidAuthor_Success()
        {
            // Assign
            var emptyStr = "";
            string nullStr = null;
            var whitespaceStr = " ";
            var notEmptyNullOrWhitespace = "Codes Alot";

            // Act

            // Asserts
            Assert.Equal(false, _postValidator.IsValidAuthor(emptyStr));
            Assert.Equal(false, _postValidator.IsValidAuthor(nullStr));
            Assert.Equal(false, _postValidator.IsValidAuthor(whitespaceStr));
            Assert.Equal(true, _postValidator.IsValidAuthor(notEmptyNullOrWhitespace));
        }

        [Fact]
        public void IsValidBody_Success()
        {
            // Assign
            var emptyStr = "";
            string nullStr = null;
            var whitespaceStr = " ";
            var notEmptyNullOrWhitespace = "This is my body, this is my soul.";

            // Act

            // Asserts
            Assert.Equal(false, _postValidator.IsValidBody(emptyStr));
            Assert.Equal(false, _postValidator.IsValidBody(nullStr));
            Assert.Equal(false, _postValidator.IsValidBody(whitespaceStr));
            Assert.Equal(true, _postValidator.IsValidBody(notEmptyNullOrWhitespace));
        }

        [Fact]
        public void IsValidTitle_Success()
        {
            // Assign
            var emptyStr = "";
            string nullStr = null;
            var whitespaceStr = " ";
            var notEmptyNullOrWhitespace = "I Like To Code Stuff";

            // Act

            // Asserts
            Assert.Equal(false, _postValidator.IsValidTitle(emptyStr));
            Assert.Equal(false, _postValidator.IsValidTitle(nullStr));
            Assert.Equal(false, _postValidator.IsValidTitle(whitespaceStr));
            Assert.Equal(true, _postValidator.IsValidTitle(notEmptyNullOrWhitespace));
        }

        [Fact]
        public void IsValidString_Success()
        {
            // Assign
            var emptyStr = "";
            string nullStr = null;
            var whitespaceStr = " ";
            var notEmptyNullOrWhitespace = "I'm a string";

            // Act

            // Asserts
            Assert.Equal(false, _postValidator.IsValidString(emptyStr));
            Assert.Equal(false, _postValidator.IsValidString(nullStr));
            Assert.Equal(false, _postValidator.IsValidString(whitespaceStr));
            Assert.Equal(true, _postValidator.IsValidString(notEmptyNullOrWhitespace));
        }

        [Fact]
        public void IsValidPost_Success()
        {
            // Assign
            // remember Post constructor takes params in order Title, Author, and Body
            var emptyPostValues = new Post("", "", "");
            var emptyAuthorPostValues = new Post("I Like To Code", "", "This is my body, this is my soul.");
            var emptyBodyPostValues = new Post("I Like To Code", "Codes Alot", "");
            var emptyTitlePostValues = new Post("", "Codes Alot", "This is my body, this is my soul.");
            var emptyAuthorBodyPostValues = new Post("I Like To Code", "", "");
            var emptyAuthorTitlePostValues = new Post("", "", "This is my body, this is my soul.");
            var emptyBodyTitlePostValues = new Post("", "Codes Alot", "");

            var nullPostValues = new Post(null, null, null);
            var nullAuthorPostValues = new Post("I Like To Code", null, "This is my body, this is my soul.");
            var nullBodyPostValues = new Post("I Like To Code", "Codes Alot", null);
            var nullTitlePostValues = new Post(null, "Codes Alot", "This is my body, this is my soul.");
            var nullAuthorBodyPostValues = new Post("I Like To Code", null, null);
            var nullAuthorTitlePostValues = new Post(null, null, "This is my body, this is my soul.");
            var nullBodyTitlePostValues = new Post(null, "Codes Alot", null);

            Post nullPost = null;

            var whitespacePostValues = new Post(" ", " ", " ");
            var whitespaceAuthorPostValues = new Post("I Like To Code", " ", "This is my body, this is my soul.");
            var whitespaceBodyPostValues = new Post("I Like To Code", "Codes Alot", " ");
            var whitespaceTitlePostValues = new Post(" ", "Codes Alot", "This is my body, this is my soul.");
            var whitespaceAuthorBodyPostValues = new Post("I Like To Code", " ", " ");
            var whitespaceAuthorTitlePostValues = new Post(" ", " ", "This is my body, this is my soul.");
            var whitespaceBodyTitlePostValues = new Post(" ", "Codes Alot", " ");

            var validPostValues = new Post("I Like To Code", "Codes Alot", "This is my body, this is my soul.");

            // Act

            // Asserts
            Assert.Equal(false, _postValidator.IsValidPost(emptyPostValues));
            Assert.Equal(false, _postValidator.IsValidPost(emptyAuthorPostValues));
            Assert.Equal(false, _postValidator.IsValidPost(emptyBodyPostValues));
            Assert.Equal(false, _postValidator.IsValidPost(emptyTitlePostValues));
            Assert.Equal(false, _postValidator.IsValidPost(emptyAuthorBodyPostValues));
            Assert.Equal(false, _postValidator.IsValidPost(emptyAuthorTitlePostValues));
            Assert.Equal(false, _postValidator.IsValidPost(emptyBodyTitlePostValues));

            Assert.Equal(false, _postValidator.IsValidPost(nullPostValues));
            Assert.Equal(false, _postValidator.IsValidPost(nullAuthorPostValues));
            Assert.Equal(false, _postValidator.IsValidPost(nullBodyPostValues));
            Assert.Equal(false, _postValidator.IsValidPost(nullTitlePostValues));
            Assert.Equal(false, _postValidator.IsValidPost(nullAuthorBodyPostValues));
            Assert.Equal(false, _postValidator.IsValidPost(nullAuthorTitlePostValues));
            Assert.Equal(false, _postValidator.IsValidPost(nullBodyTitlePostValues));
            Assert.Equal(false, _postValidator.IsValidPost(nullPost));

            Assert.Equal(false, _postValidator.IsValidPost(whitespacePostValues));
            Assert.Equal(false, _postValidator.IsValidPost(whitespaceAuthorPostValues));
            Assert.Equal(false, _postValidator.IsValidPost(whitespaceBodyPostValues));
            Assert.Equal(false, _postValidator.IsValidPost(whitespaceTitlePostValues));
            Assert.Equal(false, _postValidator.IsValidPost(whitespaceAuthorBodyPostValues));
            Assert.Equal(false, _postValidator.IsValidPost(whitespaceAuthorTitlePostValues));
            Assert.Equal(false, _postValidator.IsValidPost(whitespaceBodyTitlePostValues));

            Assert.Equal(true, _postValidator.IsValidPost(validPostValues));
        }
    }
}
