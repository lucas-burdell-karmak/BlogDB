using System;
using System.Collections.Generic;

namespace BlogDB.Core
{
    public interface IAuthorRepo
    {
        Author GetAuthorByID(int id);
        Author GetAuthorByName(string name);
        List<Author> GetListOfAuthors();
        void TryRegisterAuthor(string name, string passwordHash, out bool isSuccessful);
        void TryValidateAuthorLogin(string name, string passwordHash, out bool isSuccessful);
        void TryUpdateAuthor(Author toUpdate, out bool isSuccessful);
        void TryDeleteAuthor(Author toDelete, out bool isSuccessful);
    }
}
