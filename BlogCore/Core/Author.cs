using System;

namespace BlogDB.Core
{
    public class Author
    {

        public string Name {get; set;}
        public int ID {get; set;}

        public Author(string name, int id) 
        {
            Name = name;
            ID = id;
        }
    }
}
