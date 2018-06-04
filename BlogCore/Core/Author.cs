using System;

namespace BlogDB.Core
{
    public class Author
    {

        public string Name {get; set;}
        public string PasswordHash {get; set;}
        public int ID {get; set;}
        public string Roles { get; set; }
        
        public Author(string name, string passwordHash)
        {
            Name = name;
            PasswordHash = passwordHash;
        }

        public Author(string name, string passwordHash, int id) 
        {
            Name = name;
            PasswordHash = passwordHash;
            ID = id;
        }
    }
}
