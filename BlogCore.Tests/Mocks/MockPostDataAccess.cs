using System;
using System.Collections.Generic;
using Xunit;
using BlogDB.Core;

namespace BlogCore.Tests.Mocks
{
    public class MockPostDataAccess : IPostDataAccess
    {
        private bool CalledAddPost = false;
        private bool CalledDeletePost = false;
        private bool CalledEditPost = false;
        private bool CalledGetAllPosts = false;
        private bool CalledGetListOfAuthors = false;
        private bool CalledGetListOfPostsByAuthor = false;
        private bool CalledGetPostById = false;
        private bool CalledGetPostCount = false;
        private bool CalledGetPostFromList = false;
        private bool CalledGetSortedListOfPosts = false;
        private bool CalledSearchBy = false;

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