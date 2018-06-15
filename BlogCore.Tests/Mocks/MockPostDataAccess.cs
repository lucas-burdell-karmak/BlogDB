using System;
using System.Collections.Generic;
using Xunit;
using BlogDB.Core;

namespace BlogCore.Tests.Mocks
{
    public class MockPostDataAccess : IPostDataAccess
    {
        private bool _calledAddPost = false;
        private bool _calledDeletePost = false;
        private bool _calledEditPost = false;
        private bool _calledGetAllAuthors = false;
        private bool _calledGetAllPosts = false;
        private bool _calledGetListOfAuthorNames = false;
        private bool _calledGetListOfAuthorIDs = false;
        private bool _calledGetListOfPostsByAuthor = false;
        private bool _calledGetPostById = false;
        private bool _calledGetPostCount = false;
        private bool _calledGetPostFromList = false;
        private bool _calledGetSortedListOfPosts = false;
        private bool _calledSearchBy = false;

        private Post _stubedAddPost;
        private Post _stubedDeletePost;
        private Post _stubedEditPost;
        private List<Author> _stubedGetAllAuthors;
        private List<Post> _stubedGetAllPosts;
        private List<string> _stubedGetListOfAuthorNames;
        private List<int> _stubedGetListOfAuthorIDs;
        private List<Post> _stubedGetListOfPostsByAuthor;
        private Post _stubedGetPostById;
        private int _stubedGetPostCount;
        private Post _stubedGetPostFromList;
        private List<Post> _stubedGetSortedListOfPosts;
        private List<Post> _stubedSearchBy;


        public Post AddPost(Post post)
        {
            _calledAddPost = true;
            return _stubedAddPost;
        }

        public Post DeletePost(Post post)
        {
            _calledDeletePost = true;
            return _stubedDeletePost;
        }

        public Post EditPost(Post post)
        {
            _calledEditPost = true;
            return _stubedEditPost;
        }

        public List<Post> GetAllPosts()
        {
            _calledGetAllPosts = true;
            return _stubedGetAllPosts;
        }

        public List<string> GetListOfAuthorNames()
        {
            _calledGetListOfAuthorNames = true;
            return _stubedGetListOfAuthorNames;
        }
        public List<int> GetListOfAuthorIDs()
        {
            _calledGetListOfAuthorIDs = true;
            return _stubedGetListOfAuthorIDs;
        }

        public List<Author> GetAllAuthors()
        {
            _calledGetAllAuthors = true;
            return _stubedGetAllAuthors;
        }

        public List<Post> GetListOfPostsByAuthorID(int authorID)
        {
            throw new NotImplementedException();
        }

        public Post GetPostById(Guid id)
        {
            _calledGetPostById = true;
            return _stubedGetPostById;
        }

        public int GetPostCount()
        {
            _calledGetPostCount = true;
            return _stubedGetPostCount;
        }

        public Post GetPostFromList(List<Post> listOfPosts, Guid id)
        {
            _calledGetPostFromList = true;
            return _stubedGetPostFromList;
        }

        public List<Post> GetSortedListOfPosts(PostComponent sortType)
        {
            _calledGetSortedListOfPosts = true;
            return _stubedGetSortedListOfPosts;
        }

        public List<Post> SearchBy(Func<Post, bool> criteria)
        {
            _calledSearchBy = true;
            return _stubedSearchBy;
        }

        public void AssertAddPostCalled() => Assert.True(_calledAddPost);
        public void AssertDeletePostCalled() => Assert.True(_calledDeletePost);
        public void AssertEditPostCalled() => Assert.True(_calledEditPost);
        public void AssertGetAllAuthorsCalled() => Assert.True(_calledGetAllAuthors);
        public void AssertGetAllPostsCalled() => Assert.True(_calledGetAllPosts);
        public void AssertGetListOfAuthorNamesCalled() => Assert.True(_calledGetListOfAuthorNames);
        public void AssertGetListOfAuthorIDsCalled() => Assert.True(_calledGetListOfAuthorIDs);
        public void AssertGetListOfPostsByAuthorCalled() => Assert.True(_calledGetListOfPostsByAuthor);
        public void AssertGetPostByIdCalled() => Assert.True(_calledGetPostById);
        public void AssertGetPostCountCalled() => Assert.True(_calledGetPostCount);
        public void AssertGetPostFromListCalled() => Assert.True(_calledGetPostFromList);
        public void AssertGetSortedListOfPostsCalled() => Assert.True(_calledGetSortedListOfPosts);
        public void AssertSearchByCalled() => Assert.True(_calledSearchBy);

        public MockPostDataAccess StubAddPost(Post post)
        {
            _stubedAddPost = post;
            return this;
        }

        public MockPostDataAccess StubDeletePost(Post post)
        {
            _stubedDeletePost = post;
            return this;
        }

        public MockPostDataAccess StubEditPost(Post post)
        {
            _stubedEditPost = post;
            return this;
        }
        public MockPostDataAccess StubGetAllAuthors(List<Author> list)
        {
            _stubedGetAllAuthors = list;
            return this;
        }

        public MockPostDataAccess StubGetAllPosts(List<Post> list)
        {
            _stubedGetAllPosts = list;
            return this;
        }

        public MockPostDataAccess StubGetListOfAuthorNames(List<string> list)
        {
            _stubedGetListOfAuthorNames = list;
            return this;
        }
        public MockPostDataAccess StubGetListOfAuthorIDs(List<int> list)
        {
            _stubedGetListOfAuthorIDs = list;
            return this;
        }

        public MockPostDataAccess StubGetListOfPostsByAuthor(List<Post> list)
        {
            _stubedGetListOfPostsByAuthor = list;
            return this;
        }

        public MockPostDataAccess StubGetPostById(Post post)
        {
            _stubedGetPostById = post;
            return this;
        }

        public MockPostDataAccess StubGetPostCount(int count)
        {
            _stubedGetPostCount = count;
            return this;
        }

        public MockPostDataAccess StubGetPostFromList(Post post)
        {
            _stubedGetPostFromList = post;
            return this;
        }

        public MockPostDataAccess StubGetSortedListOfPosts(List<Post> list)
        {
            _stubedGetSortedListOfPosts = list;
            return this;
        }

        public MockPostDataAccess StubSearchBy(List<Post> list)
        {
            _stubedSearchBy = list;
            return this;
        }

        public MockPostDataAccess SetCalledAddPostToFalse()
        {
            _calledAddPost = false;
            return this;
        }

        public MockPostDataAccess SetCalledDeletePostToFalse()
        {
            _calledDeletePost = false;
            return this;
        }
        public MockPostDataAccess SetCalledEditPostToFalse()
        {
            _calledEditPost = false;
            return this;
        }
        public MockPostDataAccess SetCalledGetAllAuthorsToFalse()
        {
            _calledGetAllAuthors = false;
            return this;
        }
        public MockPostDataAccess SetCalledGetAllPostsToFalse()
        {
            _calledGetAllPosts = false;
            return this;
        }
        public MockPostDataAccess SetCalledGetListOfAuthorNamesToFalse()
        {
            _calledGetListOfAuthorNames = false;
            return this;
        }
        public MockPostDataAccess SetCalledGetListOfAuthorIDsToFalse()
        {
            _calledGetListOfAuthorIDs = false;
            return this;
        }
        public MockPostDataAccess SetCalledGetListOfPostsByAuthorToFalse()
        {
            _calledGetListOfPostsByAuthor = false;
            return this;
        }
        public MockPostDataAccess SetCalledGetPostByIdToFalse()
        {
            _calledGetPostById = false;
            return this;
        }
        public MockPostDataAccess SetCalledGetPostCountToFalse()
        {
            _calledGetPostCount = false;
            return this;
        }
        public MockPostDataAccess SetCalledGetPostFromListToFalse()
        {
            _calledGetPostFromList = false;
            return this;
        }
        public MockPostDataAccess SetCalledGetSortedListOfPostsToFalse()
        {
            _calledGetSortedListOfPosts = false;
            return this;
        }
        public MockPostDataAccess SetCalledSearchByToFalse()
        {
            _calledSearchBy = false;
            return this;
        }
    }
}