using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using BlogDB.Core;
using BlogCore.Tests.Mocks;
using Newtonsoft.Json;

namespace BlogCore.Tests
{
    public class FilePostRepoTests : IDisposable
    {
        private readonly List<Post> _testData;
        private readonly string _testDBPath = Path.Combine(Directory.GetCurrentDirectory(), "testDB.json");

        public FilePostRepoTests()
        {
            _testData = LoadTestData();
        }

        private List<Post> LoadTestData()
        {
            var testData = new List<Post>();
            Author[] listOfAuthors = { new Author("Author1", 0), new Author("Author2", 1), new Author("Author3", 2) };
            testData.Add(new Post("Title1", listOfAuthors[0], "Body1", Convert.ToDateTime("1111-01-01T11:11:11.1111111Z"), Guid.Parse("11111111-1111-1111-1111-111111111111")));
            testData.Add(new Post("Title2", listOfAuthors[1], "Body2", Convert.ToDateTime("2222-02-02T12:22:22.2222222Z"), Guid.Parse("22222222-2222-2222-2222-222222222222")));
            testData.Add(new Post("Title3", listOfAuthors[2], "Body3", Convert.ToDateTime("3333-03-03T13:33:33.3333333Z"), Guid.Parse("33333333-3333-3333-3333-333333333333")));
            using (var writer = new StreamWriter(_testDBPath, false)) // false means overwrite
            {
                var contentsToWrite = JsonConvert.SerializeObject(testData);
                writer.Write(contentsToWrite);
            }
            return testData;
        }

        [Theory]
        [InlineData("title", "author", "body")]
        [InlineData("", "", "")]
        [InlineData(" ", " ", " ")]
        public void TestTryAddPost_ValidData_Success(string title, string authorName, string body)
        {
            var postRepo = new FilePostRepo(_testDBPath);
            var author = new Author(authorName, 0);
            var post = new Post(title, author, body);

            var isSuccessful = postRepo.TryAddPost(post, out var result);

            Assert.True(isSuccessful);
            Assert.NotNull(result);
            Assert.Equal(post.Title, result.Title);
            Assert.Equal(post.Author.Name, result.Author.Name);
            Assert.Equal(post.Author.ID, result.Author.ID);
            Assert.Equal(post.Body, result.Body);
        }

        [Theory]
        [InlineData("", "", null)]
        [InlineData("", null, "")]
        [InlineData(null, "", "")]
        [InlineData(null, null, null)]
        public void TestTryAddPost_InvalidData_Failure(string title, string authorName, string body)
        {
            var postRepo = new FilePostRepo(_testDBPath);
            var author = new Author(authorName, 0);
            var post = new Post(title, author, body);

            var isFailure = postRepo.TryAddPost(post, out var result);

            Assert.False(isFailure);
            Assert.Null(result);
        }

        [Fact]
        public void TestTryDeletePost_ValidData_Success()
        {
            var postRepo = new FilePostRepo(_testDBPath);
            var author = new Author("Author1", 0);
            var post = new Post("Title1", author, "Body1", Convert.ToDateTime("1111-01-01T11:11:11.1111111Z"), Guid.Parse("11111111-1111-1111-1111-111111111111"));

            var isSuccessful = postRepo.TryDeletePost(post.PostID, out var result);

            Assert.True(isSuccessful);
            Assert.NotNull(result);
            Assert.Equal(post.Title, result.Title);
            Assert.Equal(post.Author.Name, result.Author.Name);
            Assert.Equal(post.Author.ID, result.Author.ID);
            Assert.Equal(post.Body, result.Body);
        }

        [Fact]
        public void TestTryDeletePost_InvalidData_Failure()
        {
            var invalidGuid = "12345678-1234-1234-1234-123456789012";
            var postRepo = new FilePostRepo(_testDBPath);
            var author = new Author("", 0);
            var post = new Post("", author, "", DateTime.UtcNow, Guid.Parse(invalidGuid));

            var isFailure = postRepo.TryDeletePost(post.PostID, out var result);

            Assert.False(isFailure);
            Assert.Null(result);
        }

        [Theory]
        [InlineData("New Title", "Author", "Body", "2018-05-30T14:16:44.1562063Z", "11111111-1111-1111-1111-111111111111")]
        [InlineData("Title", "New Author", "Body", "2018-05-30T14:16:44.1562063Z", "22222222-2222-2222-2222-222222222222")]
        [InlineData("Title", "Author", "New Body", "2018-05-30T14:16:44.1562063Z", "33333333-3333-3333-3333-333333333333")]
        public void TestTryEditPost_ValidData_Success(string title, string authorName, string body, string datetime, string guid)
        {
            var postRepo = new FilePostRepo(_testDBPath);
            var author = new Author(authorName, 0);
            var post = new Post(title, author, body, Convert.ToDateTime(datetime), Guid.Parse(guid));

            var isSuccessful = postRepo.TryEditPost(post, out var result);

            Assert.True(isSuccessful);
            Assert.NotNull(result);
            Assert.Equal(post.Title, result.Title);
            Assert.Equal(post.Author.Name, result.Author.Name);
            Assert.Equal(post.Author.ID, result.Author.ID);
            Assert.Equal(post.Body, result.Body);
        }

        [Theory]
        [InlineData("Title", "Author", "Body", "1111-01-01T11:11:11.1111111Z", "00000000-0000-0000-0000-000000000000")]
        [InlineData(null, "Author", "Body", "1111-01-01T11:11:11.1111111Z", "11111111-1111-1111-1111-111111111111")]
        [InlineData("Title", null, "Body", "1111-01-01T11:11:11.1111111Z", "11111111-1111-1111-1111-111111111111")]
        [InlineData("Title", "Author", null, "1111-01-01T11:11:11.1111111Z", "11111111-1111-1111-1111-111111111111")]
        public void TestTryEditPost_InvalidData_Failure(string title, string authorName, string body, string datetime, string guid)
        {
            var postRepo = new FilePostRepo(_testDBPath);
            var author = new Author(authorName, 0);
            var post = new Post(title, author, body, Convert.ToDateTime(datetime), Guid.Parse(guid));

            var isFailure = postRepo.TryEditPost(post, out var result);

            Assert.False(isFailure);
            Assert.Null(result);
        }

        [Fact]
        public void TestGetAllPosts()
        {
            var postRepo = new FilePostRepo(_testDBPath);

            var list = postRepo.GetAllPosts();

            Assert.NotEmpty(list);
            Assert.Equal(_testData.Count, list.Count);
        }

        public void Dispose() { }
    }
}