using System;
using System.Collections.Generic;
using Xunit;
using BlogDB.Core;

namespace BlogCore.Tests
{
    public class PostValidatorTests : IDisposable
    {
        private readonly IPostValidator _postValidator;

        public PostValidatorTests() => _postValidator = new PostValidator();

        [Fact]
        public void PostExists_True_Success()
        {
            var postInList = new Post("title", new Author("author", 0), "body");
            var listOfPosts = new List<Post>(){ postInList };

            Assert.True(_postValidator.PostExists(listOfPosts, postInList));
        }

        [Fact]
        public void PostExists_False_Success()
        {
            var postNotInList = new Post("title", new Author("author", 0), "body");
            var listOfPosts = new List<Post>();

            Assert.False(_postValidator.PostExists(listOfPosts, postNotInList));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void IsValidBody_CaughtInvalid_Success(string invalidInput) => Assert.False(_postValidator.IsValidBody(invalidInput));

        [Fact]
        public void IsValidBody_Valid_Success() => Assert.True(_postValidator.IsValidBody("body"));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void IsValidTitle_CatchInvalid_Success(string invalidInput) => Assert.False(_postValidator.IsValidTitle(invalidInput));

        [Fact]
        public void IsValidTitle_Valid_Success() => Assert.True(_postValidator.IsValidTitle("title"));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void IsValidString_CatchInvalid_Success(string invalidInput) => Assert.False(_postValidator.IsValidString(invalidInput));

        [Fact]
        public void IsValidString_Valid_Success() => Assert.True(_postValidator.IsValidString("string"));

        [Theory]
        [InlineData("title", "")]
        [InlineData("", "body")]
        [InlineData("title", null)]
        [InlineData(null, "body")]
        [InlineData("title", " ")]
        [InlineData(" ", "body")]
        public void IsValidPost_CatchInvalid_Success(string title, string body)
        {
            var invalidPost = new Post(title, new Author("authorName", 0), body);

            Assert.False(_postValidator.IsValidPost(invalidPost));
        }

        [Fact]
        public void IsValidPost_CatchNullPost_Success() => Assert.False(_postValidator.IsValidPost(null));

        [Fact]
        public void IsValidPost_Valid_Success() => Assert.True(_postValidator.IsValidPost(new Post("title", new Author("author", 0), "body")));

        public void Dispose() { }
    }
}