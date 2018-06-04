using System;
using Xunit;
using BlogDB.Core;

namespace BlogCore.Tests
{
    public class PostTests : IDisposable
    {
        [Fact]
        public void TestAuthor()
        {
            var author = new Author("author", 0);
            var post = new BlogDB.Core.Post("", author, "");

            Assert.Equal("author", post.Author.Name);
            Assert.Equal(0, post.Author.ID);
        }

        [Fact]
        public void TestTitle()
        {
            var post = new BlogDB.Core.Post("title", new Author("", 0), "");

            Assert.Equal("title", post.Title);
        }

        [Fact]
        public void TestBody()
        {
            var post = new BlogDB.Core.Post("", new Author("", 0), "body");

            Assert.Equal("body", post.Body);
        }

        [Fact]
        public void TestDateTime()
        {
            var date = DateTime.UtcNow;
            var post = new BlogDB.Core.Post("", new Author("", 0), "", date, Guid.NewGuid());

            Assert.Equal(date, post.Timestamp);
        }

        [Fact]
        public void TestPostID()
        {
            var guid = Guid.NewGuid();
            var post = new BlogDB.Core.Post("", new Author("", 0), "", DateTime.UtcNow, guid);

            Assert.Equal(guid, post.PostID);
        }

        public void Dispose() { }
    }
}
