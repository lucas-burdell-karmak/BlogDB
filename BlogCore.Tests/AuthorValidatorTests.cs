using System;
using System.Collections.Generic;
using Xunit;
using BlogDB.Core;
using BlogCore.Tests.Mocks;

namespace BlogCore.Tests
{
    public class AuthorValidatorTests : IDisposable
    {
        private readonly IAuthorValidator _authorValidator;
        private readonly MockAuthorRepo _mockAuthorRepo;

        public AuthorValidatorTests()
        {
            _mockAuthorRepo = new MockAuthorRepo();
            _authorValidator = new AuthorValidator(_mockAuthorRepo);
        }

        [Fact]
        public void IsValidAuthor_ValidData_Success()
        {
            var listOfAuthors = new List<Author>();
            var authorInRepo = new Author("inRepo", 0);
            var authorNotInRepo = new Author("notInRepo", 1);

            listOfAuthors.Add(authorInRepo);
            _mockAuthorRepo.StubGetListOfAuthors(listOfAuthors);

            Assert.True(_authorValidator.IsValidAuthor(authorInRepo));
            _mockAuthorRepo.AssertGetListOfAuthorCalled();
        }

        [Fact]
        public void IsValidAuthor_InvalidData_Failure()
        {

        }







        public void Dispose() { }
    }
}