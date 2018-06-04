using System;
using System.Collections.Generic;

namespace BlogDB.Core
{
    public interface IAuthorRepo
    {
        bool TryValidateAuthor(string name, string passwordHash, out Author author);
        bool TryRegisterAuthor(string name, string passwordHash, out Author author);
        Author GetAuthor(int id);
        Author GetAuthorByName(string name);
    }
}
