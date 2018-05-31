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

        private Post StubedAddPost;
        private Post StubedDeletePost;
        private Post StubedEditPost;
        private List<Post> StubedGetAllPosts;
        private List<string> StubedGetListOfAuthors;
        private List<Post> StubedGetListOfPostsByAuthor;
        private Post StubedGetPostById;
        private int StubedGetPostCount;
        private Post StubedGetPostFromList;
        private List<Post> StubedGetSortedListOfPosts;
        private List<Post> StubedSearchBy;


        public Post AddPost(Post post)
        {
            CalledAddPost = true;
            return StubedAddPost;
        }

        public Post DeletePost(Post post)
        {
            CalledDeletePost = true;
            return StubedDeletePost;
        }

        public Post EditPost(Post post)
        {
            CalledEditPost = true;
            return StubedEditPost;
        }

        public List<Post> GetAllPosts()
        {
            CalledGetAllPosts = true;
            return StubedGetAllPosts;
        }

        public List<string> GetListOfAuthors()
        {
            CalledGetListOfAuthors = true;
            return StubedGetListOfAuthors;
        }

        public List<Post> GetListOfPostsByAuthor(string authorName)
        {
            CalledGetListOfPostsByAuthor = true;
            return StubedGetListOfPostsByAuthor;
        }

        public Post GetPostById(Guid id)
        {
            CalledGetPostById = true;
            return StubedGetPostById;
        }

        public int GetPostCount()
        {
            CalledGetPostCount = true;
            return StubedGetPostCount;
        }

        public Post GetPostFromList(List<Post> listOfPosts, Guid id)
        {
            CalledGetPostFromList = true;
            return StubedGetPostFromList;
        }

        public List<Post> GetSortedListOfPosts(PostComponent sortType)
        {
            CalledGetSortedListOfPosts = true;
            return StubedGetSortedListOfPosts;
        }

        public List<Post> SearchBy(Func<Post, bool> criteria)
        {
            CalledSearchBy = true;
            return StubedSearchBy;
        }

        public void AssertAddPostCalled() => Assert.True(CalledAddPost);
        public void AssertDeletePostCalled() => Assert.True(CalledDeletePost);
        public void AssertEditPostCalled() => Assert.True(CalledEditPost);
        public void AssertGetAllPostsCalled() => Assert.True(CalledGetAllPosts);
        public void AssertGetListOfAuthorsCalled() => Assert.True(CalledGetListOfAuthors);
        public void AssertGetListOfPostsByAuthorCalled() => Assert.True(CalledGetListOfPostsByAuthor);
        public void AssertGetPostByIdCalled() => Assert.True(CalledGetPostById);
        public void AssertGetPostCountCalled() => Assert.True(CalledGetPostCount);
        public void AssertGetPostFromListCalled() => Assert.True(CalledGetPostFromList);
        public void AssertGetSortedListOfPostsCalled() => Assert.True(CalledGetSortedListOfPosts);
        public void AssertSearchByCalled() => Assert.True(CalledSearchBy);

        public MockPostDataAccess StubAddPost(Post post)
        {
            StubedAddPost = post;
            return this;
        }

        public MockPostDataAccess StubDeletePost(Post post)
        {
            StubedDeletePost = post;
            return this;
        }

        public MockPostDataAccess StubEditPost(Post post)
        {
            StubedEditPost = post;
            return this;
        }

        public MockPostDataAccess StubGetAllPosts(List<Post> list)
        {
            StubedGetAllPosts = list;
            return this;
        }

        public MockPostDataAccess StubGetListOfAuthors(List<string> list)
        {
            StubedGetListOfAuthors = list;
            return this;
        }

        public MockPostDataAccess StubGetListOfPostsByAuthor(List<Post> list)
        {
            StubedGetListOfPostsByAuthor = list;
            return this;
        }

        public MockPostDataAccess StubGetPostById(Post post)
        {
            StubedGetPostById = post;
            return this;
        }

        public MockPostDataAccess StubGetPostCount(int count)
        {
            StubedGetPostCount = count;
            return this;
        }

        public MockPostDataAccess StubGetPostFromList(Post post)
        {
            StubedGetPostFromList = post;
            return this;
        }

        public MockPostDataAccess StubGetSortedListOfPosts(List<Post> list)
        {
            StubedGetSortedListOfPosts = list;
            return this;
        }

        public MockPostDataAccess StubSearchBy(List<Post> list)
        {
            StubedSearchBy = list;
            return this;
        }

        public MockPostDataAccess SetCalledAddPostToFalse()
        {
            CalledAddPost = false;
            return this;
        }

        public MockPostDataAccess SetCalledDeletePostToFalse()
        {
            CalledDeletePost = false;
            return this;
        }
        public MockPostDataAccess SetCalledEditPostToFalse()
        {
            CalledEditPost = false;
            return this;
        }
        public MockPostDataAccess SetCalledCalledGetAllPostsToFalse()
        {
            CalledGetAllPosts = false;
            return this;
        }
        public MockPostDataAccess SetCalledCalledGetListOfAuthorsToFalse()
        {
            CalledGetListOfAuthors = false;
            return this;
        }
        public MockPostDataAccess SetCalledCalledGetListOfPostsByAuthorToFalse()
        {
            CalledGetListOfPostsByAuthor = false;
            return this;
        }
        public MockPostDataAccess SetCalledCalledGetPostByIdToFalse()
        {
            CalledGetPostById = false;
            return this;
        }
        public MockPostDataAccess SetCalledCalledGetPostCountToFalse()
        {
            CalledGetPostCount = false;
            return this;
        }
        public MockPostDataAccess SetCalledCalledGetPostFromListToFalse()
        {
            CalledGetPostFromList = false;
            return this;
        }
        public MockPostDataAccess SetCalledCalledGetSortedListOfPostsToFalse()
        {
            CalledGetSortedListOfPosts = false;
            return this;
        }
        public MockPostDataAccess SetCalledCalledSearchByToFalse()
        {
            CalledSearchBy = false;
            return this;
        }
    }
}