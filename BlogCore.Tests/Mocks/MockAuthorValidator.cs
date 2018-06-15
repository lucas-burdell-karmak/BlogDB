using BlogDB.Core;
using Xunit;

namespace BlogCore.Tests.Mocks
{
    public class MockAuthorValidator : IAuthorValidator
    {
        private bool _calledIsValidAuthor = false;

        private bool _stubedIsValidAuthor;

        public void AssertIsValidAuthorCalled() => Assert.True(_calledIsValidAuthor);

        public bool IsValidAuthor(Author author)
        {
            _calledIsValidAuthor = true;
            return _stubedIsValidAuthor;
        }

        public MockAuthorValidator SetCalledIsValidAuthorToFalse()
        {
            _calledIsValidAuthor = false;
            return this;
        }

        public MockAuthorValidator StubIsValidAuthor(bool b)
        {
            _stubedIsValidAuthor = b;
            return this;
        }
    }
}