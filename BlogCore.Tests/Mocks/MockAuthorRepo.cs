using System;
using System.Collections.Generic;
using BlogDB.Core;
using Xunit;

namespace BlogCore.Tests.Mocks
{
    public class MockAuthorRepo : IAuthorRepo
    {
        private bool CalledTryValidateAuthor = false;
        private bool CalledTryRegisterAuthor = false;
        private bool CalledGetAuthor = false;
        private bool CalledGetAuthorByName = false;
        private bool CalledGetListOfAuthors = false;

        private bool StubedTryValidateAuthorBool;
        private bool StubedTryRegisterAuthorBool;
        private Author StubedTryValidateAuthor;
        private Author StubedTryRegisterAuthor;
        private Author StubedGetAuthor;
        private Author StubedGetAuthorByName;
        private List<Author> StubedGetListOfAuthors;

        public void AssertTryValidateAuthorCalled() => Assert.True(CalledTryValidateAuthor);
        public void AssertTryRegisterAuthorCalled() => Assert.True(CalledTryRegisterAuthor);
        public void AssertGetAuthorCalled() => Assert.True(CalledGetAuthor);
        public void AssertGetAuthorByNameCalled() => Assert.True(CalledGetAuthorByName);
        public void AssertGetListOfAuthorCalled() => Assert.True(CalledGetListOfAuthors);

        public bool TryValidateAuthorLogin(string name, string passwordHash, out Author author)
        {
            CalledTryValidateAuthor = true;
            author = StubedTryValidateAuthor;
            return StubedTryValidateAuthorBool;
        }

        public bool TryRegisterAuthor(string name, string passwordHash, out Author author)
        {
            CalledTryRegisterAuthor = true;
            author = StubedTryRegisterAuthor;
            return StubedTryRegisterAuthorBool;
        }

        public Author GetAuthor(int id)
        {
            CalledGetAuthor = true;
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

        public MockAuthorRepo SetCalledTryValidateAuthorToFalse()
        {
            CalledTryValidateAuthor = false;
            return this;
        }

        public MockAuthorRepo SetCalledTryRegisterAuthorToFalse()
        {
            CalledTryRegisterAuthor = false;
            return this;
        }

        public MockAuthorRepo SetCalledGetAuthorToFalse()
        {
            CalledGetAuthor = false;
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

        public MockAuthorRepo StubTryValidateAuthorBool(bool b)
        {
            StubedTryValidateAuthorBool = b;
            return this;
        }

        public MockAuthorRepo StubTryRegisterAuthorBool(bool b)
        {
            StubedTryRegisterAuthorBool = b;
            return this;
        }

        public MockAuthorRepo StubTryValidateAuthor(Author author)
        {
            StubedTryValidateAuthor = author;
            return this;
        }

        public MockAuthorRepo StubTryRegisterAuthor(Author author)
        {
            StubedTryRegisterAuthor = author;
            return this;
        }

        public MockAuthorRepo StubGetAuthor(Author author)
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
    }
}