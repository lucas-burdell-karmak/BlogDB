namespace BlogDB.Core
{
    public class AuthorValidator : IAuthorValidator
    {
        private IAuthorRepo _authorRepo;

        public AuthorValidator(IAuthorRepo authorRepo) => _authorRepo = authorRepo;

        public bool IsValidAuthor(Author author) => _authorRepo.GetListOfAuthors().Exists(x => x.Name == author.Name && x.ID == author.ID);
    }
}
