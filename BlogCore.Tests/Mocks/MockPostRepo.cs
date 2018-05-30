using System;
using Xunit;
using BlogDB.Core;
using System.Collections.Generic;

namespace BlogCore.Tests.Mocks
{
    public class MockPostRepo : IPostRepo
    {
        public Post AddPost(Post post)
        {
            throw new NotImplementedException();
        }

        public Post DeletePost(Guid id)
        {
            throw new NotImplementedException();
        }

        public Post EditPost(Post post)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetAllPosts()
        {
            throw new NotImplementedException();
        }
    }
}