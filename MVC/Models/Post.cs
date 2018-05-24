using System;

namespace The_Intern_MVC.Models
{
    // Post Class - defines a Post object that contains Post properties
    public class Post
    {
        // Post properties
        public string Title { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid PostID { get; set;}

        // Post constructor
        public Post() 
        {
            
        }

        public Post(string title, string author, string body)
        {
            Title = title;
            Author = author;
            Body = body;
            Timestamp = DateTime.Now;
        }

        // Post constructor that calls the above post constructor (avoiding code duplication) and additionally takes a DateTime and ID 
        public Post(string title, string author, string body, DateTime timestamp, Guid postid) : this(title, author, body) 
        {
            Timestamp = timestamp;
            PostID = postid;
        }

        // GetPostPreview method - returns a string "preview" of the post
        public string GetPostPreview()
        {
            return $"\"{Title}\" by {Author} @ {Timestamp.ToString("h:mm:ss tt")}";
        }

        // ToString method - overrides default ToString to display what we really want
        public override string ToString()
        {
            return $"{GetPostPreview()}:\n\t{Body}";
        }

    }//end Post Class

}//end namespace The_Intern_Blog
