using System;
using Xunit;
using BlogDB.Core;

namespace BlogCore.Tests
{
    public class AuthorTests : IDisposable
    {
        [Fact]
        public void TestAuthorName()
        {
            var author = new Author("name", 0);
            
            Assert.Equal("name", author.Name);
        }

        [Fact]
        public void TestAuthorID()
        {
            var author = new Author("", 123);
            
            Assert.Equal(123, author.ID);
        }

        [Fact]
        public void TestAuthorRoles()
        {
            var author = new Author("", 0);
            author.Roles.Add("role");

            Assert.Equal("role", author.Roles[0]);
        }

        public void Dispose() { }
    }   
}