using System;
using System.Collections.Generic;
using BlogDB.Core;
using Xunit;

namespace BlogCore.Tests.Mocks
{
    public class MockAuthorValidator : IAuthorValidator
    {
        private bool CalledIsValidAuthor = false;

        private bool StubedIsValidAuthorBool;

        public void AssertIsValidAuthorCalled() => Assert.True(CalledIsValidAuthor);

        public bool IsValidAuthor(Author author)
        {
            CalledIsValidAuthor = true;
            return StubedIsValidAuthorBool;
        }

        public MockAuthorValidator SetCalledIsValidAuthorToFalse()
        {
            CalledIsValidAuthor = false;
            return this;
        }

        public MockAuthorValidator StubIsValidAuthorBool(bool b)
        {
            StubedIsValidAuthorBool = b;
            return this;
        }
    }
}