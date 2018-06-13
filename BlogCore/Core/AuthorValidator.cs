namespace BlogDB.Core
{
    public class AuthorValidator : IAuthorValidator
    {
        private IAuthorRepo _authorRepo;

        public AuthorValidator(IAuthorRepo authorRepo) => _authorRepo = authorRepo;
        
        public bool IsValidAuthor(Author author)
        {
            foreach(var a in _authorRepo.GetListOfAuthors())
                if(author.Name == a.Name && author.ID == a.ID)
                    return true;
            return false;
        }
    }
}