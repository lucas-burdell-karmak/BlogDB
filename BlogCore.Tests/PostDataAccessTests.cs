using System;
using System.Collections.Generic;
using Xunit;
using BlogDB.Core;
using BlogCore.Tests.Mocks;

namespace BlogCore.Tests
{
    public class PostDataAccessTests : IDisposable
    {
        [Fact]
        public void TestAddPost_ValidData_Success()
        {
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo);
            var author = new Author("author", 0);
            var post = new Post("title", author, "body");

            mockPostValidator.StubValidPost(true);
            mockPostRepo.StubTryAddPostResult(post)
                        .StubTryAddPostBool(true);
            mockAuthorRepo.StubTryValidateAuthor(author)
                          .StubTryValidateAuthorBool(true);
            var returnedPost = postDataAccess.AddPost(post);

            mockPostValidator.AssertIsValidPostCalled();
            mockPostRepo.AssertTryAddPostCalled();
            mockAuthorRepo.AssertTryValidateAuthorCalled();
            Assert.NotNull(returnedPost);
            AssertPostsEqual(post, returnedPost);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("title", "author", "")]
        [InlineData("title", "", "body")]
        [InlineData("", "author", "body")]
        [InlineData(null, null, null)]
        [InlineData("title", "author", null)]
        [InlineData("title", null, "body")]
        [InlineData(null, "author", "body")]
        [InlineData(" ", " ", " ")]
        [InlineData("title", "author", " ")]
        [InlineData("title", " ", "body")]
        [InlineData(" ", "author", "body")]
        public void TestAddPost_InvalidData_Failure(string title, string authorName, string body)
        {
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo);
            var author = new Author(authorName, 0);
            var post = new Post(title, author, body);

            mockPostValidator.StubValidPost(false);

            Assert.Throws<ArgumentException>(() => postDataAccess.AddPost(post));
            mockPostValidator.AssertIsValidPostCalled();
        }

        [Fact]
        public void TestDeletePost_ValidData_Success()
        {
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var post = new Post("", new Author("", 0), "");
            var listOfPosts = new List<Post>();

            listOfPosts.Add(post);
            postValidatorMock.StubPostExists(true);
            postRepoMock.StubGetAllPosts(listOfPosts)
                        .StubTryDeletePostBool(true)
                        .StubTryDeletePostResult(post);
            var returnedPost = postDataAccess.DeletePost(post);

            postValidatorMock.AssertPostExitsCalled();
            postRepoMock.AssertGetAllPostCalled();
            postRepoMock.AssertTryDeletePostCalled();
            Assert.NotNull(returnedPost);
            AssertPostsEqual(post, returnedPost);
        }

        [Fact]
        public void TestDeletePost_InvalidData_Failure()
        {
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var post = new Post("", new Author("", 0), "");

            postValidatorMock.StubPostExists(false);

            Assert.Throws<ArgumentException>(() => postDataAccess.DeletePost(post));
            postValidatorMock.AssertPostExitsCalled();
        }

        [Fact]
        public void TestEditPost_ValidData_Success()
        {
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var post = new Post("OriginalTitle", new Author("OriginalAuthor", 0), "OriginalBody");
            var expectedPost = new Post("newTitle", new Author("newAuthor", 0), "newBody", post.Timestamp, post.PostID);
            var listOfPosts = new List<Post>();

            listOfPosts.Add(post);
            postValidatorMock.StubPostExists(true)
                             .StubValidPost(true);
            postRepoMock.StubGetAllPosts(listOfPosts)
                        .StubTryEditPostBool(true)
                        .StubTryEditPostResult(expectedPost);
            var returnedPost = postDataAccess.EditPost(post);

            postValidatorMock.AssertPostExitsCalled();
            postRepoMock.AssertGetAllPostCalled();
            postValidatorMock.AssertIsValidPostCalled();
            postRepoMock.AssertTryEditPostCalled();
            Assert.NotNull(returnedPost);
            AssertPostsEqual(listOfPosts[0], returnedPost);
        }

        [Fact]
        public void TestEditPost_PostDoesNotExists()
        {
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var post = new Post("", new Author("", 0), "");

            postValidatorMock.StubPostExists(false)
                             .StubValidPost(false);

            Assert.Throws<ArgumentException>(() => postDataAccess.EditPost(post));
            postValidatorMock.AssertPostExitsCalled();
        }

        [Fact]
        public void TestEditPost_PostExistsButInvalid()
        {
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var post = new Post("", new Author("", 0), "");

            postValidatorMock.StubPostExists(true)
                             .StubValidPost(false);

            Assert.Throws<ArgumentException>(() => postDataAccess.EditPost(post));
            postValidatorMock.AssertPostExitsCalled();
            postValidatorMock.AssertIsValidPostCalled();
        }


        [Fact]
        public void TestGetAllPosts()
        {
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var post = new Post("", new Author("", 0), "");
            var listOfPosts = new List<Post>();

            listOfPosts.Add(post);
            postRepoMock.StubGetAllPosts(listOfPosts);

            var returnedListOfPosts = postDataAccess.GetAllPosts();

            postRepoMock.AssertGetAllPostCalled();
            Assert.Equal(listOfPosts.Count, returnedListOfPosts.Count);
            AssertPostsEqual(listOfPosts[0], returnedListOfPosts[0]);
        }

        [Fact]
        public void TestGetListOfPostsByAuthors()
        {
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var postNotByAuthor = new Post("", new Author("NotAuthor", 0), "");
            var postByAuthor = new Post("", new Author("Author", 0), "");
            var listOfPosts = new List<Post>();

            listOfPosts.Add(postNotByAuthor);
            listOfPosts.Add(postByAuthor);

            postRepoMock.StubGetAllPosts(listOfPosts);

            var returnedListOfPosts = postDataAccess.GetListOfPostsByAuthor("Author");

            postRepoMock.AssertGetAllPostCalled();
            AssertPostsEqual(listOfPosts[0], returnedListOfPosts[0]);
        }

        [Fact]
        public void TestGetPostById_ValidID_Success()
        {
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var post = new Post("", new Author("", 0), "");
            var listOfPosts = new List<Post>();

            listOfPosts.Add(post);
            postRepoMock.StubGetAllPosts(listOfPosts);
            var returnedPost = postDataAccess.GetPostById(post.PostID);

            postRepoMock.AssertGetAllPostCalled();
            Assert.NotNull(returnedPost);
            AssertPostsEqual(post, returnedPost);
        }

        [Fact]
        public void TestGetPostById_InvalidID_Success()
        {
            var nonExistentPostID = "33b9def6-f7db-4120-9f00-6137bbeeb8d1";
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var post = new Post("", new Author("", 0), "", DateTime.Now, Guid.Parse("bb2da75b-3bec-4d92-ba7e-4cbdf7b50759"));
            var listOfPosts = new List<Post>();

            listOfPosts.Add(post);
            postRepoMock.StubGetAllPosts(listOfPosts);
            var returnedPost = postDataAccess.GetPostById(Guid.Parse(nonExistentPostID));

            postRepoMock.AssertGetAllPostCalled();
            Assert.Null(returnedPost);
        }

        [Fact]
        public void TestGetPostCount()
        {
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var post = new Post("", new Author("", 0), "");
            var listOfPosts = new List<Post>();

            listOfPosts.Add(post);
            postRepoMock.StubGetAllPosts(listOfPosts);
            var returnedCount = postDataAccess.GetPostCount();

            postRepoMock.AssertGetAllPostCalled();
            Assert.Equal(listOfPosts.Count, returnedCount);
        }

        [Fact]
        public void TestGetPostFromList_ValidID()
        {
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var post = new Post("", new Author("", 0), "");
            var listOfPosts = new List<Post>();

            listOfPosts.Add(post);
            var returnedPost = postDataAccess.GetPostFromList(listOfPosts, post.PostID);

            Assert.NotNull(returnedPost);
            AssertPostsEqual(post, returnedPost);
        }

        [Fact]
        public void TestGetPostFromList_invalidID()
        {
            var nonExistentPostID = "00000000-0000-0000-0000-000000000000";
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var post = new Post("", new Author("", 0), "");
            var listOfPosts = new List<Post>();

            listOfPosts.Add(post);
            var returnedPost = postDataAccess.GetPostFromList(listOfPosts, Guid.Parse(nonExistentPostID));

            Assert.Null(returnedPost);
        }

        [Fact]
        public void TestGetPostFromList_EmptyList()
        {
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var listOfPosts = new List<Post>();

            var returnedPost = postDataAccess.GetPostFromList(listOfPosts, Guid.NewGuid());

            Assert.Null(returnedPost);
        }

        [Theory]
        [InlineData(PostComponent.author)]
        [InlineData(PostComponent.title)]
        [InlineData(PostComponent.timestamp)]
        public void TestGetSortedListOfPosts(PostComponent postComp)
        {
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var aPost = new Post("AAA", new Author("AAA", 0), "AAA");
            var bPost = new Post("BBB", new Author("BBB", 1), "BBB");
            var cPost = new Post("CCC", new Author("CCC", 2), "CCC");
            var listOfPosts = new List<Post>();

            listOfPosts.Add(cPost);
            listOfPosts.Add(aPost);
            listOfPosts.Add(bPost);
            postRepoMock.StubGetAllPosts(listOfPosts);
            var returnedListOfPosts = postDataAccess.GetSortedListOfPosts(postComp);

            postRepoMock.AssertGetAllPostCalled();
            Assert.Equal(listOfPosts.Count, returnedListOfPosts.Count);
            AssertPostsEqual(aPost, returnedListOfPosts[0]);
            AssertPostsEqual(bPost, returnedListOfPosts[1]);
            AssertPostsEqual(cPost, returnedListOfPosts[2]);
        }

        private void AssertPostsEqual(Post post1, Post post2)
        {
            Assert.Equal(post1.Title, post2.Title);
            Assert.Equal(post1.Author.Name, post2.Author.Name);
            Assert.Equal(post1.Author.ID, post2.Author.ID);
            Assert.Equal(post1.Body, post2.Body);
            Assert.Equal(post1.Timestamp, post2.Timestamp);
            Assert.Equal(post1.PostID, post2.PostID);
        }

        [Fact]
        public void TestSearchBy_ValidData_Success()
        {
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);

            //TODO
        }

        [Fact]
        public void TestSearchBy_InvalidData_Failure()
        {
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);

            //TODO
        }

        public void Dispose() { }
    }
}