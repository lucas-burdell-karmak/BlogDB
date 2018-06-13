using BlogDB.Core;
using Xunit;

namespace BlogCore.Tests.Mocks
{
    public class MockAuthorValidator : IAuthorValidator
    {
        private bool CalledIsValidAuthor = false;

        private bool StubedIsValidAuthor;

        public void AssertIsValidAuthorCalled() => Assert.True(CalledIsValidAuthor);

        public bool IsValidAuthor(Author author)
        {
            CalledIsValidAuthor = true;
            return StubedIsValidAuthor;
        }

        public MockAuthorValidator SetCalledIsValidAuthorToFalse()
        {
            CalledIsValidAuthor = false;
            return this;
        }

        public MockAuthorValidator StubIsValidAuthor(bool b)
        {
            StubedIsValidAuthor = b;
            return this;
        }
    }
}