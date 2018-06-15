using System.Collections.Generic;
using BlogDB.Core;
using Xunit;

namespace BlogCore.Tests.Mocks
{
    public class MockAuthorRepo : IAuthorRepo
    {
        private bool _calledTryValidateAuthorLogin = false;
        private bool _calledTryRegisterAuthor = false;
        private bool _calledGetAuthorByID = false;
        private bool _calledGetAuthorByName = false;
        private bool _calledGetListOfAuthors = false;
        private bool _calledTryUpdateAuthor = false;
        private bool _calledTryDeleteAuthor = false;

        private bool _stubedTryValidateAuthorLoginBool;
        private bool _stubedTryRegisterAuthorBool;
        private Author _stubedTryValidateAuthorLogin;
        private Author _stubedTryRegisterAuthor;
        private Author _stubedGetAuthor;
        private Author _stubedGetAuthorByName;
        private List<Author> _stubedGetListOfAuthors;
        private bool _stubedTryUpdateAuthorBool;
        private bool _stubedTryDeleteAuthorBool;

        public void AssertTryValidateAuthorLoginCalled() => Assert.True(_calledTryValidateAuthorLogin);
        public void AssertTryRegisterAuthorCalled() => Assert.True(_calledTryRegisterAuthor);
        public void AssertGetAuthorByIDCalled() => Assert.True(_calledGetAuthorByID);
        public void AssertGetAuthorByNameCalled() => Assert.True(_calledGetAuthorByName);
        public void AssertGetListOfAuthorCalled() => Assert.True(_calledGetListOfAuthors);
        public void AssertTryUodateAuthorCalled() => Assert.True(_calledTryUpdateAuthor);
        public void AssertTryDeleteAuthorCalled() => Assert.True(_calledTryDeleteAuthor);

        public void TryValidateAuthorLogin(string name, string passwordHash, out bool isSuccessful)
        {
            _calledTryValidateAuthorLogin = true;
            isSuccessful = _stubedTryValidateAuthorLoginBool;
        }
        public void TryRegisterAuthor(string name, string passwordHash, out bool isSuccessful)
        {
            _calledTryRegisterAuthor = true;
            isSuccessful = _stubedTryRegisterAuthorBool;
        }
        public Author GetAuthorByID(int id)
        {
            _calledGetAuthorByID = true;
            return _stubedGetAuthor;
        }
        public Author GetAuthorByName(string name)
        {
            _calledGetAuthorByName = true;
            return _stubedGetAuthorByName;
        }
        public List<Author> GetListOfAuthors()
        {
            _calledGetListOfAuthors = true;
            return _stubedGetListOfAuthors;
        }
        public void TryUpdateAuthor(Author author, out bool isSuccessful)
        {
            _calledTryUpdateAuthor = true;
            isSuccessful = _stubedTryUpdateAuthorBool;
        }
        public void TryDeleteAuthor(Author author, out bool isSuccessful)
        {
            _calledTryDeleteAuthor = true;
            isSuccessful = _stubedTryDeleteAuthorBool;
        }

        public MockAuthorRepo SetCalledTryValidateAuthorLoginToFalse()
        {
            _calledTryValidateAuthorLogin = false;
            return this;
        }
        public MockAuthorRepo SetCalledTryRegisterAuthorToFalse()
        {
            _calledTryRegisterAuthor = false;
            return this;
        }
        public MockAuthorRepo SetCalledGetAuthorByIDToFalse()
        {
            _calledGetAuthorByID = false;
            return this;
        }
        public MockAuthorRepo SetCalledGetAuthorByNameToFalse()
        {
            _calledGetAuthorByName = false;
            return this;
        }
        public MockAuthorRepo SetCalledGetListOfAuthorsToFalse()
        {
            _calledGetListOfAuthors = false;
            return this;
        }
        public MockAuthorRepo SetCalledTryUpdateAuthorToFalse()
        {
            _calledTryUpdateAuthor = false;
            return this;
        }
        public MockAuthorRepo SetCalledTryDeleteAuthorToFalse()
        {
            _calledTryDeleteAuthor = false;
            return this;
        }

        public MockAuthorRepo StubTryValidateAuthorLoginBool(bool b)
        {
            _stubedTryValidateAuthorLoginBool = b;
            return this;
        }
        public MockAuthorRepo StubTryRegisterAuthorBool(bool b)
        {
            _stubedTryRegisterAuthorBool = b;
            return this;
        }
        public MockAuthorRepo StubTryValidateAuthorLogin(Author author)
        {
            _stubedTryValidateAuthorLogin = author;
            return this;
        }
        public MockAuthorRepo StubTryRegisterAuthor(Author author)
        {
            _stubedTryRegisterAuthor = author;
            return this;
        }
        public MockAuthorRepo StubGetAuthorByID(Author author)
        {
            _stubedGetAuthor = author;
            return this;
        }
        public MockAuthorRepo StubGetAuthorByName(Author author)
        {
            _stubedGetAuthorByName = author;
            return this;
        }
        public MockAuthorRepo StubGetListOfAuthors(List<Author> authors)
        {
            _stubedGetListOfAuthors = authors;
            return this;
        }
        public MockAuthorRepo StubTryUpdateAuthorBool(bool b)
        {
            _stubedTryUpdateAuthorBool = b;
            return this;
        }
        public MockAuthorRepo StubTryDeleteAuthorBool(bool b)
        {
            _stubedTryDeleteAuthorBool = b;
            return this;
        }
    }
}