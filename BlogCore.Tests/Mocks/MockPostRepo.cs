using System;
using System.Collections.Generic;
using Xunit;
using BlogDB.Core;

namespace BlogCore.Tests.Mocks
{
    public class MockPostRepo : IPostRepo
    {
        private bool CalledAddPost = false;
        private bool CalledDeletePost = false;
        private bool CalledEditPost = false;
        private bool CalledGetAllPosts = false;

        private Post StubedAddPost;
        private Post StubedDeletePost;
        private Post StubedEditPost;
        private List<Post> StubedGetAllPosts;

        public void AssertAddPostCalled() => Assert.True(CalledAddPost);
        public void AssertDeletePostCalled() => Assert.True(CalledDeletePost);
        public void AssertEditPostCalled() => Assert.True(CalledEditPost);
        public void AssertGetAllPostCalled() => Assert.True(CalledGetAllPosts);

        public Post AddPost(Post post)
        {
            CalledAddPost = true;
            return StubedAddPost;
        }

        public Post DeletePost(Guid id)
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

        public MockPostRepo SetCalledAddPostToFalse()
        {
            CalledAddPost = false;
            return this;
        }

        public MockPostRepo SetCalledDeletePostToFalse()
        {
            CalledDeletePost = false;
            return this;
        }

        public MockPostRepo SetCalledEditPostToFalse()
        {
            CalledEditPost = false;
            return this;
        }

        public MockPostRepo SetCalledGetAllPostsToFalse()
        {
            CalledGetAllPosts = false;
            return this;
        }

        public MockPostRepo StubAddPost(Post post)
        {
            StubedAddPost = post;
            return this;
        }

        public MockPostRepo StubDeletePost(Post post)
        {
            StubedDeletePost = post;
            return this;
        }

        public MockPostRepo StubEditPost(Post post)
        {
            StubedEditPost = post;
            return this;
        }

        public MockPostRepo StubGetAllPosts(List<Post> list)
        {
            StubedGetAllPosts = list;
            return this;
        }
    }
}