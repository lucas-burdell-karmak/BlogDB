using System.Collections.Generic;

namespace BlogDB.Core
{
    public class Author
    {
        public string Name {get; set;}
        public int ID {get; set;}
        public List<string> Roles { get; set; }

        public Author(string name, int id) 
        {
            Name = name;
            ID = id;
            Roles = new List<string>();
        }
    }
}
