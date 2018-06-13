namespace BlogDB.Core
{
    public interface IAuthorValidator
    {
        bool IsValidAuthor(Author author);
    }
}