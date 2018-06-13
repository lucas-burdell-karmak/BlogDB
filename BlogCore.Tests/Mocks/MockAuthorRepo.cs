using System.Collections.Generic;
using BlogDB.Core;
using Xunit;

namespace BlogCore.Tests.Mocks
{
    public class MockAuthorRepo : IAuthorRepo
    {
        private bool CalledTryValidateAuthorLogin = false;
        private bool CalledTryRegisterAuthor = false;
        private bool CalledGetAuthorByID = false;
        private bool CalledGetAuthorByName = false;
        private bool CalledGetListOfAuthors = false;
        private bool CalledTryUpdateAuthor = false;
        private bool CalledTryDeleteAuthor = false;

        private bool StubedTryValidateAuthorLoginBool;
        private bool StubedTryRegisterAuthorBool;
        private Author StubedTryValidateAuthorLogin;
        private Author StubedTryRegisterAuthor;
        private Author StubedGetAuthor;
        private Author StubedGetAuthorByName;
        private List<Author> StubedGetListOfAuthors;
        private bool StubedTryUpdateAuthorBool;
        private bool StubedTryDeleteAuthorBool;

        public void AssertTryValidateAuthorLoginCalled() => Assert.True(CalledTryValidateAuthorLogin);
        public void AssertTryRegisterAuthorCalled() => Assert.True(CalledTryRegisterAuthor);
        public void AssertGetAuthorByIDCalled() => Assert.True(CalledGetAuthorByID);
        public void AssertGetAuthorByNameCalled() => Assert.True(CalledGetAuthorByName);
        public void AssertGetListOfAuthorCalled() => Assert.True(CalledGetListOfAuthors);
        public void AssertTryUodateAuthorCalled() => Assert.True(CalledTryUpdateAuthor);
        public void AssertTryDeleteAuthorCalled() => Assert.True(CalledTryDeleteAuthor);

        public void TryValidateAuthorLogin(string name, string passwordHash, out bool isSuccessful)
        {
            CalledTryValidateAuthorLogin = true;
            isSuccessful = StubedTryValidateAuthorLoginBool;
        }
        public void TryRegisterAuthor(string name, string passwordHash, out bool isSuccessful)
        {
            CalledTryRegisterAuthor = true;
            isSuccessful = StubedTryRegisterAuthorBool;
        }
        public Author GetAuthorByID(int id)
        {
            CalledGetAuthorByID = true;
            return StubedGetAuthor;
        }
        public Author GetAuthorByName(string name)
        {
            CalledGetAuthorByName = true;
            return StubedGetAuthorByName;
        }
        public List<Author> GetListOfAuthors()
        {
            CalledGetListOfAuthors = true;
            return StubedGetListOfAuthors;
        }
        public void TryUpdateAuthor(Author author, out bool isSuccessful)
        {
            CalledTryUpdateAuthor = true;
            isSuccessful = StubedTryUpdateAuthorBool;
        }
        public void TryDeleteAuthor(Author author, out bool isSuccessful)
        {
            CalledTryDeleteAuthor = true;
            isSuccessful = StubedTryDeleteAuthorBool;
        }

        public MockAuthorRepo SetCalledTryValidateAuthorLoginToFalse()
        {
            CalledTryValidateAuthorLogin = false;
            return this;
        }
        public MockAuthorRepo SetCalledTryRegisterAuthorToFalse()
        {
            CalledTryRegisterAuthor = false;
            return this;
        }
        public MockAuthorRepo SetCalledGetAuthorByIDToFalse()
        {
            CalledGetAuthorByID = false;
            return this;
        }
        public MockAuthorRepo SetCalledGetAuthorByNameToFalse()
        {
            CalledGetAuthorByName = false;
            return this;
        }
        public MockAuthorRepo SetCalledGetListOfAuthorsToFalse()
        {
            CalledGetListOfAuthors = false;
            return this;
        }
        public MockAuthorRepo SetCalledTryUpdateAuthorToFalse()
        {
            CalledTryUpdateAuthor = false;
            return this;
        }
        public MockAuthorRepo SetCalledTryDeleteAuthorToFalse()
        {
            CalledTryDeleteAuthor = false;
            return this;
        }

        public MockAuthorRepo StubTryValidateAuthorLoginBool(bool b)
        {
            StubedTryValidateAuthorLoginBool = b;
            return this;
        }
        public MockAuthorRepo StubTryRegisterAuthorBool(bool b)
        {
            StubedTryRegisterAuthorBool = b;
            return this;
        }
        public MockAuthorRepo StubTryValidateAuthorLogin(Author author)
        {
            StubedTryValidateAuthorLogin = author;
            return this;
        }
        public MockAuthorRepo StubTryRegisterAuthor(Author author)
        {
            StubedTryRegisterAuthor = author;
            return this;
        }
        public MockAuthorRepo StubGetAuthorByID(Author author)
        {
            StubedGetAuthor = author;
            return this;
        }
        public MockAuthorRepo StubGetAuthorByName(Author author)
        {
            StubedGetAuthorByName = author;
            return this;
        }
        public MockAuthorRepo StubGetListOfAuthors(List<Author> authors)
        {
            StubedGetListOfAuthors = authors;
            return this;
        }
        public MockAuthorRepo StubTryUpdateAuthorBool(bool b)
        {
            StubedTryUpdateAuthorBool = b;
            return this;
        }
        public MockAuthorRepo StubTryDeleteAuthorBool(bool b)
        {
            StubedTryDeleteAuthorBool = b;
            return this;
        }
    }
}