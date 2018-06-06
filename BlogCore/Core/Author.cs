using System;

namespace BlogDB.Core
{
    public class Author
    {

        public string Name {get; set;}
        public int ID {get; set;}
        public string[] Roles { get; set; }

        public Author(string name, int id) 
        {
            Name = name;
            ID = id;
        }
    }
}
