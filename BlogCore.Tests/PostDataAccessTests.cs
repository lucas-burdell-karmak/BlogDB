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
            var title = "title";
            var authorName = "author";
            var body = "body";
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var author = new Author(authorName, 0);
            var post = new Post(title, author, body);

            postValidatorMock.StubValidPost(true);
            postRepoMock.StubTryAddPostResult(post)
                        .StubTryAddPostBool(true);
            var returnedPost = postDataAccess.AddPost(post);

            postValidatorMock.AssertIsValidPostCalled();
            postRepoMock.AssertTryAddPostCalled();
            Assert.NotNull(returnedPost);
            Assert.Equal(post.Title, returnedPost.Title);
            Assert.Equal(post.Author, returnedPost.Author);
            Assert.Equal(post.Body, returnedPost.Body);
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
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var author = new Author(authorName, 0);
            var post = new Post(title, author, body);

            postValidatorMock.StubValidPost(false);

            Assert.Throws<ArgumentException>(() => postDataAccess.AddPost(post));
            postValidatorMock.AssertIsValidPostCalled();
        }

        [Fact]
        public void TestDeletePost_ValidData_Success()
        {
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var author = new Author("", 0);
            var post = new Post("", author, "");
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
            Assert.Equal(post.Title, returnedPost.Title);
            Assert.Equal(post.Author, returnedPost.Author);
            Assert.Equal(post.Body, returnedPost.Body);
        }

        [Fact]
        public void TestDeletePost_InvalidData_Failure()
        {
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var author = new Author("", 0);
            var post = new Post("", author, "");

            postValidatorMock.StubPostExists(false);

            Assert.Throws<ArgumentException>(() => postDataAccess.DeletePost(post));
            postValidatorMock.AssertPostExitsCalled();
        }

        [Fact]
        public void TestEditPost_ValidData_Success()
        {
            var newTitle = "newTitle";
            var newAuthorName = "newAuthor";
            var newBody = "newBody";
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var author = new Author("OriginalAuthor", 0);
            var post = new Post("OriginalTitle", author, "OriginalBody");
            var newAuthor = new Author(newAuthorName, 0);
            var expectedPost = new Post(newTitle, newAuthor, newBody, post.Timestamp, post.PostID);
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
            Assert.Equal(newTitle, returnedPost.Title);
            Assert.Equal(newAuthor, returnedPost.Author);
            Assert.Equal(newBody, returnedPost.Body);
        }

        [Fact]
        public void TestEditPost_PostDoesNotExists()
        {
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var author = new Author("", 0);
            var post = new Post("", author, "");

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
            var author = new Author("", 0);
            var post = new Post("", author, "");

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
            var author = new Author("", 0);
            var post = new Post("", author, "");
            var listOfPosts = new List<Post>();

            listOfPosts.Add(post);
            postRepoMock.StubGetAllPosts(listOfPosts);

            var returnedListOfPosts = postDataAccess.GetAllPosts();

            postRepoMock.AssertGetAllPostCalled();
            Assert.Equal(listOfPosts.Count, returnedListOfPosts.Count);
            Assert.Equal(listOfPosts[0].Author, returnedListOfPosts[0].Author);
            Assert.Equal(listOfPosts[0].Title, returnedListOfPosts[0].Title);
            Assert.Equal(listOfPosts[0].Body, returnedListOfPosts[0].Body);
            Assert.Equal(listOfPosts[0].Timestamp, returnedListOfPosts[0].Timestamp);
            Assert.Equal(listOfPosts[0].PostID, returnedListOfPosts[0].PostID);
        }

        [Fact]
        public void TestGetListOfPostsByAuthors()
        {
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var author = new Author("Author", 0);
            var notAuthor = new Author("NotAuthor", 0);
            var postNotByAuthor = new Post("", notAuthor, "");
            var postByAuthor = new Post("", author, "");
            var listOfPosts = new List<Post>();

            listOfPosts.Add(postNotByAuthor);
            listOfPosts.Add(postByAuthor);

            postRepoMock.StubGetAllPosts(listOfPosts);

            var returnedListOfPosts = postDataAccess.GetListOfPostsByAuthor("Author");

            postRepoMock.AssertGetAllPostCalled();
            Assert.Equal(postByAuthor.Author.Name, returnedListOfPosts[0].Author.Name);
            Assert.Equal(postByAuthor.Title, returnedListOfPosts[0].Title);
            Assert.Equal(postByAuthor.Body, returnedListOfPosts[0].Body);
            Assert.Equal(postByAuthor.Timestamp, returnedListOfPosts[0].Timestamp);
            Assert.Equal(postByAuthor.PostID, returnedListOfPosts[0].PostID);
        }

        [Fact]
        public void TestGetPostById_ValidID_Success()
        {
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var author = new Author("", 0);
            var post = new Post("", author, "");
            var listOfPosts = new List<Post>();

            listOfPosts.Add(post);
            postRepoMock.StubGetAllPosts(listOfPosts);
            var returnedPost = postDataAccess.GetPostById(post.PostID);

            postRepoMock.AssertGetAllPostCalled();
            Assert.NotNull(returnedPost);
            Assert.Equal(post.Author, returnedPost.Author);
            Assert.Equal(post.Title, returnedPost.Title);
            Assert.Equal(post.Body, returnedPost.Body);
            Assert.Equal(post.Timestamp, returnedPost.Timestamp);
            Assert.Equal(post.PostID, returnedPost.PostID);
        }

        [Fact]
        public void TestGetPostById_InvalidID_Success()
        {
            var nonExistentPostID = "33b9def6-f7db-4120-9f00-6137bbeeb8d1";
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var author = new Author("", 0);
            var post = new Post("", author, "", DateTime.Now, Guid.Parse("bb2da75b-3bec-4d92-ba7e-4cbdf7b50759"));
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
            var author = new Author("", 0);
            var post = new Post("", author, "");
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
            var author = new Author("", 0);
            var post = new Post("", author, "");
            var listOfPosts = new List<Post>();

            listOfPosts.Add(post);
            var returnedPost = postDataAccess.GetPostFromList(listOfPosts, post.PostID);

            Assert.NotNull(returnedPost);
            Assert.Equal(post.Author, returnedPost.Author);
            Assert.Equal(post.Title, returnedPost.Title);
            Assert.Equal(post.Body, returnedPost.Body);
            Assert.Equal(post.Timestamp, returnedPost.Timestamp);
            Assert.Equal(post.PostID, returnedPost.PostID);
        }

        [Fact]
        public void TestGetPostFromList_invalidID()
        {
            var nonExistentPostID = "00000000-0000-0000-0000-000000000000";
            var postRepoMock = new MockPostRepo();
            var postValidatorMock = new MockPostValidator();
            var postDataAccess = new PostDataAccess(postRepoMock, postValidatorMock);
            var author = new Author("", 0);
            var post = new Post("", author, "");
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
            var aAuthor = new Author("AAA", 0);
            var aPost = new Post("AAA", aAuthor, "AAA");
            var bAuthor = new Author("BBB", 1);
            var bPost = new Post("BBB", bAuthor, "BBB");
            var cAuthor = new Author("CCC", 2);
            var cPost = new Post("CCC", cAuthor, "CCC");
            var listOfPosts = new List<Post>();

            listOfPosts.Add(cPost);
            listOfPosts.Add(aPost);
            listOfPosts.Add(bPost);
            postRepoMock.StubGetAllPosts(listOfPosts);
            var returnedListOfPosts = postDataAccess.GetSortedListOfPosts(postComp);

            postRepoMock.AssertGetAllPostCalled();
            Assert.Equal(listOfPosts.Count, returnedListOfPosts.Count);
            Assert.Equal(aPost.Author.Name, returnedListOfPosts[0].Author.Name);
            Assert.Equal(aPost.Title, returnedListOfPosts[0].Title);
            Assert.Equal(aPost.Body, returnedListOfPosts[0].Body);
            Assert.Equal(aPost.Timestamp, returnedListOfPosts[0].Timestamp);
            Assert.Equal(aPost.PostID, returnedListOfPosts[0].PostID);
            Assert.Equal(bPost.Author.Name, returnedListOfPosts[1].Author.Name);
            Assert.Equal(bPost.Title, returnedListOfPosts[1].Title);
            Assert.Equal(bPost.Body, returnedListOfPosts[1].Body);
            Assert.Equal(bPost.Timestamp, returnedListOfPosts[1].Timestamp);
            Assert.Equal(bPost.PostID, returnedListOfPosts[1].PostID);
            Assert.Equal(cPost.Author.Name, returnedListOfPosts[2].Author.Name);
            Assert.Equal(cPost.Title, returnedListOfPosts[2].Title);
            Assert.Equal(cPost.Body, returnedListOfPosts[2].Body);
            Assert.Equal(cPost.Timestamp, returnedListOfPosts[2].Timestamp);
            Assert.Equal(cPost.PostID, returnedListOfPosts[2].PostID);
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