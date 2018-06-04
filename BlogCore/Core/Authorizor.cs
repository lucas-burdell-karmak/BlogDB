using System;
using System.Security.Cryptography;

namespace BlogDB.Core
{
    public class Authorizor : IAuthorizor
    {
        public void RegisterAuthor(string name, string passwordHash)
        {
            // TODO;
            Guid salt = Guid.NewGuid();
            HMACSHA512 hash = new HMACSHA512();
        }

        public Author ValidateAuthor(string name, string passwordHash)
        {
            // TODO
            return null;
        }
    }
}
