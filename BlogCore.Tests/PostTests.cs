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
            var author = new Author("authorName", 0);
            var post = new BlogDB.Core.Post("titleName", author, "bodyText", DateTime.UtcNow, Guid.NewGuid());

            Assert.Equal("authorName", post.Author.Name);
            Assert.Equal(0, post.Author.ID);
        }

        [Fact]
        public void TestTitle()
        {
            var author = new Author("authorName", 0);
            var post = new BlogDB.Core.Post("titleName", author, "bodyText", DateTime.UtcNow, Guid.NewGuid());

            Assert.Equal("titleName", post.Title);
        }

        [Fact]
        public void TestBody()
        {
            var author = new Author("authorName", 0);
            var post = new BlogDB.Core.Post("titleName", author, "bodyText", DateTime.UtcNow, Guid.NewGuid());

            Assert.Equal("bodyText", post.Body);
        }

        [Fact]
        public void TestDateTime()
        {
            var date = DateTime.UtcNow;
            var author = new Author("authorName", 0);
            var post = new BlogDB.Core.Post("titleName", author, "bodyText", date, Guid.NewGuid());

            Assert.Equal(date, post.Timestamp);
        }

        [Fact]
        public void TestPostID()
        {
            var guid = Guid.NewGuid();
            var author = new Author("authorName", 0);
            var post = new BlogDB.Core.Post("titleName", author, "bodyText", DateTime.UtcNow, guid);

            Assert.Equal(guid, post.PostID);
        }

        public void Dispose() { }
    }
}
