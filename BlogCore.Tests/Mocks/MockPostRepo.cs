using System;
using System.Collections.Generic;
using Xunit;
using BlogDB.Core;

namespace BlogCore.Tests.Mocks
{
    public class MockPostRepo : IPostRepo
    {
        private bool _calledGetAllPosts = false;
        private bool _calledGetAllPostsByAuthor = false;
        private bool _calledTryAddPost = false;
        private bool _calledTryDeletePost = false;
        private bool _calledTryEditPost = false;

        private List<Post> _stubedGetAllPosts;
        private List<Post> _stubedGetAllPostsByAuthor;
        private bool _stubedTryAddPostBool;
        private Post _stubedTryAddPostResult;
        private bool _stubedTryDeletePostBool;
        private Post _stubedTryDeletePostResult;
        private bool _stubedTryEditPostBool;
        private Post _stubedTryEditPostResult;

        public void AssertTryAddPostCalled() => Assert.True(_calledTryAddPost);
        public void AssertTryDeletePostCalled() => Assert.True(_calledTryDeletePost);
        public void AssertTryEditPostCalled() => Assert.True(_calledTryEditPost);
        public void AssertGetAllPostsCalled() => Assert.True(_calledGetAllPosts);
        public void AssertGetAllPostsByAuthorCalled() => Assert.True(_calledGetAllPostsByAuthor);

        public bool TryAddPost(Post post, out Post result)
        {
            _calledTryAddPost = true;
            result = _stubedTryAddPostResult;
            return _stubedTryAddPostBool;
        }
        public bool TryDeletePost(Guid id, out Post result)
        {
            _calledTryDeletePost = true;
            result = _stubedTryDeletePostResult;
            return _stubedTryDeletePostBool;
        }
        public bool TryEditPost(Post post, out Post result)
        {
            _calledTryEditPost = true;
            result = _stubedTryEditPostResult;
            return _stubedTryEditPostBool;
        }
        public List<Post> GetAllPosts()
        {
            _calledGetAllPosts = true;
            return _stubedGetAllPosts;
        }
        public List<Post> GetAllPostsByAuthor(int authorID)
        {
            _calledGetAllPostsByAuthor = true;
            return _stubedGetAllPostsByAuthor;
        }

        public MockPostRepo StubTryAddPostResult(Post post)
        {
            _stubedTryAddPostResult = post;
            return this;
        }
        public MockPostRepo StubTryDeletePostResult(Post post)
        {
            _stubedTryDeletePostResult = post;
            return this;
        }
        public MockPostRepo StubTryEditPostResult(Post post)
        {
            _stubedTryEditPostResult = post;
            return this;
        }
        public MockPostRepo StubTryAddPostBool(bool b)
        {
            _stubedTryAddPostBool = b;
            return this;
        }
        public MockPostRepo StubTryDeletePostBool(bool b)
        {
            _stubedTryDeletePostBool = b;
            return this;
        }
        public MockPostRepo StubTryEditPostBool(bool b)
        {
            _stubedTryEditPostBool = b;
            return this;
        }
        public MockPostRepo StubGetAllPosts(List<Post> list)
        {
            _stubedGetAllPosts = list;
            return this;
        }
        public MockPostRepo StubGetAllPostsByAuthor(List<Post> list)
        {
            _stubedGetAllPostsByAuthor = list;
            return this;
        }

        public MockPostRepo SetCalledTryAddPostToFalse()
        {
            _calledTryAddPost = false;
            return this;
        }
        public MockPostRepo SetCalledTryDeletePostToFalse()
        {
            _calledTryDeletePost = false;
            return this;
        }
        public MockPostRepo SetCalledTryEditPostToFalse()
        {
            _calledTryEditPost = false;
            return this;
        }
        public MockPostRepo SetCalledGetAllPostsToFalse()
        {
            _calledGetAllPosts = false;
            return this;
        }
        public MockPostRepo SetCalledGetAllPostsByAuthorToFalse()
        {
            _calledGetAllPostsByAuthor = false;
            return this;
        }
    }
}