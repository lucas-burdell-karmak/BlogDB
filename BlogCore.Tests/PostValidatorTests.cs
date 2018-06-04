using System;
using System.Collections.Generic;
using Xunit;
using BlogDB.Core;

namespace BlogCore.Tests
{
    public class PostValidatorTests : IDisposable
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
            var author = new Author("authorName", 0);
            var postInList = new Post("I Like To Code", author, "This is my body, this is my soul.");
            listOfPosts.Add(postInList);

            Assert.True(_postValidator.PostExists(listOfPosts, postInList));
        }

        [Fact]
        public void PostExists_False_Success()
        {
            var listOfPosts = new List<Post>();
            var author = new Author("authorName", 0);
            var postNotInList = new Post("I Love To Code", author, "This is my body, seriously... I'm trapped here.");

            Assert.False(_postValidator.PostExists(listOfPosts, postNotInList));
        }

        [Theory]
        [InlineData(""  )]
        [InlineData("  ")]
        [InlineData(null)]
        public void IsValidAuthor_CaughtInvalid_Success(string invalidInput)
        {
            Assert.False(_postValidator.IsValidAuthor(invalidInput));
        }

        [Fact]
        public void IsValidAuthor_Valid_Success()
        {
            var notEmptyNullOrWhitespace = "Codes Alot";

            Assert.True(_postValidator.IsValidAuthor(notEmptyNullOrWhitespace));
        }

        [Theory]
        [InlineData(""  )]
        [InlineData(" " )]
        [InlineData(null)]
        public void IsValidBody_CaughtInvalid_Success(string invalidInput)
        {
            Assert.False(_postValidator.IsValidBody(invalidInput));
        }

        [Fact]
        public void IsValidBody_Valid_Success()
        {
            var notEmptyNullOrWhitespace = "This is my body, this is my soul.";

            Assert.True(_postValidator.IsValidBody(notEmptyNullOrWhitespace));
        }

        [Theory]
        [InlineData(""  )]
        [InlineData(" " )]
        [InlineData(null)]
        public void IsValidTitle_CatchInvalid_Success(string invalidInput)
        {
            Assert.False(_postValidator.IsValidTitle(invalidInput));
        }

        [Fact]
        public void IsValidTitle_Valid_Success()
        {
            var notEmptyNullOrWhitespace = "I Like To Code Stuff";

            Assert.True(_postValidator.IsValidTitle(notEmptyNullOrWhitespace));
        }

        [Theory]
        [InlineData(""  )]
        [InlineData(" " )]
        [InlineData(null)]
        public void IsValidString_CatchInvalid_Success(string invalidInput)
        {
            Assert.False(_postValidator.IsValidString(invalidInput));
        }

        [Fact]
        public void IsValidString_Valid_Success()
        {
            var notEmptyNullOrWhitespace = "I'm a string";

            Assert.True(_postValidator.IsValidString(notEmptyNullOrWhitespace));
        }

        [Theory]
        [InlineData("",      "",       ""    )]
        [InlineData("title", "author", ""    )]
        [InlineData("title", "",       "body")]
        [InlineData("",      "author", "body")]
        [InlineData(null,    null,     null  )]
        [InlineData("title", "author", null  )]
        [InlineData("title", null,     "body")]
        [InlineData(null,    "author", "body")]
        [InlineData(" ",     " ",      " "   )]
        [InlineData("title", "author", " "   )]
        [InlineData("title", " ",      "body")]
        [InlineData(" ",     "author", "body")]
        public void IsValidPost_CatchInvalid_Success(string title, string authorName, string body)
        {
            var author = new Author(authorName, 0);
            var invalidPost = new Post(title, author, body);

            Assert.False(_postValidator.IsValidPost(invalidPost));
        }

        [Fact]
        public void IsValidPost_CatchNullPost_Success()
        {
            Post nullPost = null;

            Assert.False(_postValidator.IsValidPost(nullPost));
        }

        [Fact]
        public void IsValidPost_Valid_Success()
        {
            var author = new Author("authorName", 0);
            var validPostValues = new Post("title", author, "body");

            Assert.True(_postValidator.IsValidPost(validPostValues));
        }

        public void Dispose() { }
    }
}
