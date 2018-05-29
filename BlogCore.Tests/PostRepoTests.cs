using System;
using Xunit;
using BlogDB.Core;
using System.Collections.Generic;

namespace BlogCore.Tests 
{
    public class PostRepoTests 
    {

        [Fact]
        public void GetAllAuthors_Succeed()
        {
            var testData = new List<Post>();
            testData.Add(new Post("Title","Author1","Body"));
            testData.Add(new Post("Title","Author2","Body"));
            testData.Add(new Post("Title","Author3","Body"));
            var repo = new PostRepo(new MockIBlogDB(testData));
            var validator = new MockIPostValidator();
            var dataAccess = new PostDataAccess(repo, validator);

            var validList = dataAccess.GetListOfPostsByAuthor("Author1");


            Assert.Equal(testData[0].Title, validList[0].Title);
            Assert.Equal(testData[0].Author, validList[0].Author);
            Assert.Equal(testData[0].Body, validList[0].Body);
        }
    }

}