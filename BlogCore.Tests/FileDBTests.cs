using System;
using Xunit;
using BlogDB.Core;
using System.Collections.Generic;

namespace BlogCore.Tests
{
    public class FileDBTests
    {
        private readonly IBlogDB<Post> _blogDB;

        public FileDBTests()
        {
            _blogDB = new FileDB<Post>("TestDBvalidposts.json")
        }

        [Fact]
        public void ReadAll_TestDBvalidposts_Success()
        {
            
        }

        [Fact]
        public void WriteAll_TestDBvalidposts_Success()
        {

        }
    }
}