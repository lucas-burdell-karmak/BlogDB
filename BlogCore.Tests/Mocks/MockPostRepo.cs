using System;
using System.Collections.Generic;
using Xunit;
using BlogDB.Core;

namespace BlogCore.Tests.Mocks
{
    public class MockPostRepo : IPostRepo
    {
        private bool CalledGetAllPosts = false;
        private bool CalledGetAllPostsByAuthor = false;
        private bool CalledTryAddPost = false;
        private bool CalledTryDeletePost = false;
        private bool CalledTryEditPost = false;

        private List<Post> StubedGetAllPosts;
        private List<Post> StubedGetAllPostsByAuthor;
        private bool StubedTryAddPostBool;
        private Post StubedTryAddPostResult;
        private bool StubedTryDeletePostBool;
        private Post StubedTryDeletePostResult;
        private bool StubedTryEditPostBool;
        private Post StubedTryEditPostResult;

        public void AssertTryAddPostCalled() => Assert.True(CalledTryAddPost);
        public void AssertTryDeletePostCalled() => Assert.True(CalledTryDeletePost);
        public void AssertTryEditPostCalled() => Assert.True(CalledTryEditPost);
        public void AssertGetAllPostsCalled() => Assert.True(CalledGetAllPosts);
        public void AssertGetAllPostsByAuthorCalled() => Assert.True(CalledGetAllPostsByAuthor);

        public bool TryAddPost(Post post, out Post result)
        {
            CalledTryAddPost = true;
            result = StubedTryAddPostResult;
            return StubedTryAddPostBool;
        }
        public bool TryDeletePost(Guid id, out Post result)
        {
            CalledTryDeletePost = true;
            result = StubedTryDeletePostResult;
            return StubedTryDeletePostBool;
        }
        public bool TryEditPost(Post post, out Post result)
        {
            CalledTryEditPost = true;
            result = StubedTryEditPostResult;
            return StubedTryEditPostBool;
        }
        public List<Post> GetAllPosts()
        {
            CalledGetAllPosts = true;
            return StubedGetAllPosts;
        }
        public List<Post> GetAllPostsByAuthor(int authorID)
        {
            CalledGetAllPostsByAuthor = true;
            return StubedGetAllPostsByAuthor;
        }

        public MockPostRepo StubTryAddPostResult(Post post)
        {
            StubedTryAddPostResult = post;
            return this;
        }
        public MockPostRepo StubTryDeletePostResult(Post post)
        {
            StubedTryDeletePostResult = post;
            return this;
        }
        public MockPostRepo StubTryEditPostResult(Post post)
        {
            StubedTryEditPostResult = post;
            return this;
        }
        public MockPostRepo StubTryAddPostBool(bool b)
        {
            StubedTryAddPostBool = b;
            return this;
        }
        public MockPostRepo StubTryDeletePostBool(bool b)
        {
            StubedTryDeletePostBool = b;
            return this;
        }
        public MockPostRepo StubTryEditPostBool(bool b)
        {
            StubedTryEditPostBool = b;
            return this;
        }
        public MockPostRepo StubGetAllPosts(List<Post> list)
        {
            StubedGetAllPosts = list;
            return this;
        }
        public MockPostRepo StubGetAllPostsByAuthor(List<Post> list)
        {
            StubedGetAllPostsByAuthor = list;
            return this;
        }

        public MockPostRepo SetCalledTryAddPostToFalse()
        {
            CalledTryAddPost = false;
            return this;
        }
        public MockPostRepo SetCalledTryDeletePostToFalse()
        {
            CalledTryDeletePost = false;
            return this;
        }
        public MockPostRepo SetCalledTryEditPostToFalse()
        {
            CalledTryEditPost = false;
            return this;
        }
        public MockPostRepo SetCalledGetAllPostsToFalse()
        {
            CalledGetAllPosts = false;
            return this;
        }
        public MockPostRepo SetCalledGetAllPostsByAuthorToFalse()
        {
            CalledGetAllPostsByAuthor = false;
            return this;
        }
    }
}