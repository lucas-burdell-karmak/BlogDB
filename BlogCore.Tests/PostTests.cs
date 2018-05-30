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
            var post = new BlogDB.Core.Post("titleName", "authorName", "bodyText", DateTime.UtcNow, Guid.NewGuid());

            Assert.Equal(post.Author, "authorName");
        }

        [Fact]
        public void TestTitle()
        {
            var post = new BlogDB.Core.Post("titleName", "authorName", "bodyText", DateTime.UtcNow, Guid.NewGuid());

            Assert.Equal("titleName", post.Title);
        }

        [Fact]
        public void TestBody()
        {
            var post = new BlogDB.Core.Post("titleName", "authorName", "bodyText", DateTime.UtcNow, Guid.NewGuid());

            Assert.Equal("bodyText", post.Body);
        }

        [Fact]
        public void TestDateTime()
        {
            var date = DateTime.UtcNow;

            var post = new BlogDB.Core.Post("titleName", "authorName", "bodyText", date, Guid.NewGuid());

            Assert.Equal(date, post.Timestamp);
        }

        [Fact]
        public void TestPostID()
        {
            var guid = Guid.NewGuid();

            var post = new BlogDB.Core.Post("titleName", "authorName", "bodyText", DateTime.UtcNow, guid);

            Assert.Equal(guid, post.PostID);
        }

        public void Dispose() { }
    }
}
