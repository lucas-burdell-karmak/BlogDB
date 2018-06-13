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
            var mockAuthorValidator = new MockAuthorValidator();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo, mockAuthorValidator);
            var author = new Author("author", 0);
            var post = new Post("title", author, "body");

            mockPostValidator.StubValidPost(true);
            mockPostRepo.StubTryAddPostResult(post)
                        .StubTryAddPostBool(true);
            mockAuthorValidator.StubIsValidAuthor(true);
            var returnedPost = postDataAccess.AddPost(post);

            mockPostValidator.AssertIsValidPostCalled();
            mockPostRepo.AssertTryAddPostCalled();
            mockAuthorValidator.AssertIsValidAuthorCalled();
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
        public void TestAddPost_InvalidPost_Failure(string title, string authorName, string body)
        {
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var mockAuthorValidator = new MockAuthorValidator();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo, mockAuthorValidator);
            var author = new Author(authorName, 0);
            var post = new Post(title, author, body);

            mockPostValidator.StubValidPost(false);

            Assert.Throws<ArgumentException>(() => postDataAccess.AddPost(post));
            mockPostValidator.AssertIsValidPostCalled();
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
        public void TestAddPost_InvalidAuthor_Failure(string title, string authorName, string body)
        {
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var mockAuthorValidator = new MockAuthorValidator();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo, mockAuthorValidator);
            var author = new Author(authorName, 0);
            var post = new Post(title, author, body);

            mockPostValidator.StubValidPost(true);
            mockAuthorValidator.StubIsValidAuthor(false);

            Assert.Throws<ArgumentException>(() => postDataAccess.AddPost(post));
            mockPostValidator.AssertIsValidPostCalled();
            mockAuthorValidator.AssertIsValidAuthorCalled();
        }

        [Fact]
        public void TestDeletePost_ValidData_Success()
        {
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var mockAuthorValidator = new MockAuthorValidator();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo, mockAuthorValidator);
            var post = new Post("", new Author("", 0), "");
            var listOfPosts = new List<Post>();

            listOfPosts.Add(post);
            mockPostValidator.StubPostExists(true);
            mockPostRepo.StubGetAllPosts(listOfPosts)
                        .StubTryDeletePostBool(true)
                        .StubTryDeletePostResult(post);
            var returnedPost = postDataAccess.DeletePost(post);

            mockPostValidator.AssertPostExistsCalled();
            mockPostRepo.AssertGetAllPostsCalled();
            mockPostRepo.AssertTryDeletePostCalled();
            Assert.NotNull(returnedPost);
            AssertPostsEqual(post, returnedPost);
        }

        [Fact]
        public void TestDeletePost_InvalidPost_Failure()
        {
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var mockAuthorValidator = new MockAuthorValidator();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo, mockAuthorValidator);
            var post = new Post("", new Author("", 0), "");

            mockPostValidator.StubPostExists(false);

            Assert.Throws<ArgumentException>(() => postDataAccess.DeletePost(post));
            mockPostValidator.AssertPostExistsCalled();
        }

        [Fact]
        public void TestEditPost_ValidData_Success()
        {
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var mockAuthorValidator = new MockAuthorValidator();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo, mockAuthorValidator);
            var post = new Post("OriginalTitle", new Author("OriginalAuthor", 0), "OriginalBody");
            var expectedPost = new Post("newTitle", new Author("newAuthor", 0), "newBody", post.Timestamp, post.PostID);
            var listOfPosts = new List<Post>();

            listOfPosts.Add(post);
            mockPostValidator.StubPostExists(true)
                             .StubValidPost(true);
            mockPostRepo.StubGetAllPosts(listOfPosts)
                        .StubTryEditPostBool(true)
                        .StubTryEditPostResult(expectedPost);
            mockAuthorValidator.StubIsValidAuthor(true);
            var returnedPost = postDataAccess.EditPost(post);

            mockPostValidator.AssertPostExistsCalled();
            mockPostRepo.AssertGetAllPostsCalled();
            mockPostValidator.AssertIsValidPostCalled();
            mockPostRepo.AssertTryEditPostCalled();
            mockAuthorValidator.AssertIsValidAuthorCalled();
            Assert.NotNull(returnedPost);
            AssertPostsEqual(expectedPost, returnedPost);
        }

        [Fact]
        public void TestEditPost_PostDoesNotExists()
        {
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var mockAuthorValidator = new MockAuthorValidator();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo, mockAuthorValidator);
            var post = new Post("", new Author("", 0), "");

            mockPostValidator.StubPostExists(false);

            Assert.Throws<ArgumentException>(() => postDataAccess.EditPost(post));
            mockPostValidator.AssertPostExistsCalled();
        }

        [Fact]
        public void TestEditPost_PostExistsButInvalidPost()
        {
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var mockAuthorValidator = new MockAuthorValidator();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo, mockAuthorValidator);
            var post = new Post("", new Author("", 0), "");

            mockPostValidator.StubPostExists(true)
                             .StubValidPost(false);

            Assert.Throws<ArgumentException>(() => postDataAccess.EditPost(post));
            mockPostValidator.AssertPostExistsCalled();
            mockPostValidator.AssertIsValidPostCalled();
        }

        [Fact]
        public void TestEditPost_PostExistsButInvalidAuthor()
        {
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var mockAuthorValidator = new MockAuthorValidator();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo, mockAuthorValidator);
            var post = new Post("", new Author("", 0), "");

            mockPostValidator.StubPostExists(true)
                             .StubValidPost(true);
            mockAuthorValidator.StubIsValidAuthor(false);

            Assert.Throws<ArgumentException>(() => postDataAccess.EditPost(post));
            mockPostValidator.AssertPostExistsCalled();
            mockPostValidator.AssertIsValidPostCalled();
            mockAuthorValidator.AssertIsValidAuthorCalled();
        }


        [Fact]
        public void TestGetAllPosts()
        {
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var mockAuthorValidator = new MockAuthorValidator();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo, mockAuthorValidator);
            var post = new Post("", new Author("", 0), "");
            var listOfPosts = new List<Post>();

            listOfPosts.Add(post);
            mockPostRepo.StubGetAllPosts(listOfPosts);

            var returnedListOfPosts = postDataAccess.GetAllPosts();

            mockPostRepo.AssertGetAllPostsCalled();
            Assert.Equal(listOfPosts.Count, returnedListOfPosts.Count);
            AssertPostsEqual(listOfPosts[0], returnedListOfPosts[0]);
        }

        [Fact]
        public void TestGetListOfPostsByAuthorID()
        {
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var mockAuthorValidator = new MockAuthorValidator();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo, mockAuthorValidator);
            var postNotByAuthor = new Post("", new Author("NotAuthor", 1), "");
            var postByAuthor = new Post("", new Author("Author", 0), "");
            var listOfPosts = new List<Post>();

            listOfPosts.Add(postByAuthor);

            mockPostRepo.StubGetAllPostsByAuthor(listOfPosts);

            var returnedListOfPosts = postDataAccess.GetListOfPostsByAuthorID(0);

            mockPostRepo.AssertGetAllPostsByAuthorCalled();
            AssertPostsEqual(postByAuthor, returnedListOfPosts[0]);
        }

        [Fact]
        public void TestGetPostById_ValidID_Success()
        {
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var mockAuthorValidator = new MockAuthorValidator();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo, mockAuthorValidator);
            var post = new Post("", new Author("", 0), "");
            var listOfPosts = new List<Post>();

            listOfPosts.Add(post);
            mockPostRepo.StubGetAllPosts(listOfPosts);
            var returnedPost = postDataAccess.GetPostById(post.PostID);

            mockPostRepo.AssertGetAllPostsCalled();
            Assert.NotNull(returnedPost);
            AssertPostsEqual(post, returnedPost);
        }

        [Fact]
        public void TestGetPostById_InvalidID_Success()
        {
            var nonExistentPostID = "33b9def6-f7db-4120-9f00-6137bbeeb8d1";
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var mockAuthorValidator = new MockAuthorValidator();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo, mockAuthorValidator);
            var post = new Post("", new Author("", 0), "", DateTime.Now, Guid.Parse("bb2da75b-3bec-4d92-ba7e-4cbdf7b50759"));
            var listOfPosts = new List<Post>();

            listOfPosts.Add(post);
            mockPostRepo.StubGetAllPosts(listOfPosts);
            var returnedPost = postDataAccess.GetPostById(Guid.Parse(nonExistentPostID));

            mockPostRepo.AssertGetAllPostsCalled();
            Assert.Null(returnedPost);
        }

        [Fact]
        public void TestGetPostCount()
        {
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var mockAuthorValidator = new MockAuthorValidator();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo, mockAuthorValidator);
            var post = new Post("", new Author("", 0), "");
            var listOfPosts = new List<Post>();

            listOfPosts.Add(post);
            mockPostRepo.StubGetAllPosts(listOfPosts);
            var returnedCount = postDataAccess.GetPostCount();

            mockPostRepo.AssertGetAllPostsCalled();
            Assert.Equal(listOfPosts.Count, returnedCount);
        }

        [Fact]
        public void TestGetPostFromList_ValidID()
        {
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var mockAuthorValidator = new MockAuthorValidator();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo, mockAuthorValidator);
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
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var mockAuthorValidator = new MockAuthorValidator();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo, mockAuthorValidator);
            var post = new Post("", new Author("", 0), "");
            var listOfPosts = new List<Post>();

            listOfPosts.Add(post);
            var returnedPost = postDataAccess.GetPostFromList(listOfPosts, Guid.Parse(nonExistentPostID));

            Assert.Null(returnedPost);
        }

        [Fact]
        public void TestGetPostFromList_EmptyList()
        {
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var mockAuthorValidator = new MockAuthorValidator();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo, mockAuthorValidator);
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
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var mockAuthorValidator = new MockAuthorValidator();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo, mockAuthorValidator);
            var aPost = new Post("AAA", new Author("AAA", 0), "AAA", Convert.ToDateTime("1111-01-01T11:11:11.1111111Z"), Guid.Parse("11111111-1111-1111-1111-111111111111"));
            var bPost = new Post("BBB", new Author("BBB", 1), "BBB", Convert.ToDateTime("1112-02-02T12:22:22.2222222Z"), Guid.Parse("22222222-2222-2222-2222-222222222222"));
            var cPost = new Post("CCC", new Author("CCC", 2), "CCC", Convert.ToDateTime("1113-03-03T13:33:33.3333333Z"), Guid.Parse("33333333-3333-3333-3333-333333333333"));
            var listOfPosts = new List<Post>();

            listOfPosts.Add(cPost);
            listOfPosts.Add(aPost);
            listOfPosts.Add(bPost);
            mockPostRepo.StubGetAllPosts(listOfPosts);
            var returnedListOfPosts = postDataAccess.GetSortedListOfPosts(postComp);

            mockPostRepo.AssertGetAllPostsCalled();
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
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var mockAuthorValidator = new MockAuthorValidator();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo, mockAuthorValidator);

            //TODO
        }

        [Fact]
        public void TestSearchBy_InvalidData_Failure()
        {
            var mockPostRepo = new MockPostRepo();
            var mockPostValidator = new MockPostValidator();
            var mockAuthorRepo = new MockAuthorRepo();
            var mockAuthorValidator = new MockAuthorValidator();
            var postDataAccess = new PostDataAccess(mockPostRepo, mockPostValidator, mockAuthorRepo, mockAuthorValidator);

            //TODO
        }

        public void Dispose() { }
    }
}