using System;
using Xunit;
using BlogDB.Core;
using System.Collections.Generic;

namespace BlogCore.Tests.Mocks
{
    public class MockPostDataAccess : IPostDataAccess
    {
        public Post AddPost(Post post)
        {
            throw new NotImplementedException();
        }

        public Post DeletePost(Post post)
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

        public List<string> GetListOfAuthors()
        {
            throw new NotImplementedException();
        }

        public List<Post> GetListOfPostsByAuthor(string authorName)
        {
            throw new NotImplementedException();
        }

        public Post GetPostById(Guid id)
        {
            throw new NotImplementedException();
        }

        public int GetPostCount()
        {
            throw new NotImplementedException();
        }

        public Post GetPostFromList(List<Post> listOfPosts, Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetSortedListOfPosts(PostComponent sortType)
        {
            throw new NotImplementedException();
        }

        public List<Post> SearchBy(Func<Post, bool> criteria)
        {
            throw new NotImplementedException();
        }
    }
}