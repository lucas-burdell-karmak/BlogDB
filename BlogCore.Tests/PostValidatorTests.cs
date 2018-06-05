using System;
using System.Collections.Generic;
using Xunit;
using BlogDB.Core;

namespace BlogCore.Tests
{
    public class PostValidatorTests : IDisposable
    {
        private readonly IPostValidator _postValidator;

        public PostValidatorTests() { _postValidator = new PostValidator(); }

        [Fact]
        public void PostExists_True_Success()
        {
            var listOfPosts = new List<Post>();
            var postInList = new Post("title", new Author("author", 0), "body");
            listOfPosts.Add(postInList);

            Assert.True(_postValidator.PostExists(listOfPosts, postInList));
        }

        [Fact]
        public void PostExists_False_Success()
        {
            var listOfPosts = new List<Post>();
            var postNotInList = new Post("title", new Author("author", 0), "body");

            Assert.False(_postValidator.PostExists(listOfPosts, postNotInList));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void IsValidBody_CaughtInvalid_Success(string invalidInput)
        {
            Assert.False(_postValidator.IsValidBody(invalidInput));
        }

        [Fact]
        public void IsValidBody_Valid_Success()
        {
            var notEmptyNullOrWhitespace = "body";

            Assert.True(_postValidator.IsValidBody(notEmptyNullOrWhitespace));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void IsValidTitle_CatchInvalid_Success(string invalidInput)
        {
            Assert.False(_postValidator.IsValidTitle(invalidInput));
        }

        [Fact]
        public void IsValidTitle_Valid_Success()
        {
            var notEmptyNullOrWhitespace = "title";

            Assert.True(_postValidator.IsValidTitle(notEmptyNullOrWhitespace));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
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
        [InlineData("", "", "")]
        [InlineData("title", "author", "")]
        [InlineData("title", "", "body")]
        [InlineData("", "author", "body")]
        [InlineData(null, null, null)]
        [InlineData("title", "author", null)]
        [InlineData("title", null, "body")]
        [InlineData(null, "author", "body")]
        [InlineData(" ", " ", " ")]
        [InlineData("title", "author", " ")]
        [InlineData("title", " ", "body")]
        [InlineData(" ", "author", "body")]
        public void IsValidPost_CatchInvalid_Success(string title, string authorName, string body)
        {
            var invalidPost = new Post(title, new Author(authorName, 0), body);

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
            var validPostValues = new Post("title", new Author("author", 0), "body");

            Assert.True(_postValidator.IsValidPost(validPostValues));
        }

        public void Dispose() { }
    }
}