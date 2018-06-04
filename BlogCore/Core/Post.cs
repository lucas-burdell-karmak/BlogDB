using System;

namespace BlogDB.Core
{
    public class Post
    {
        public string Title { get; set; }
        public Author Author { get; set; }
        public string Body { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid PostID { get; set; }

        public Post() { }

        public Post(string title, Author author, string body)
        {
            Title = title;
            Author = author;
            Body = body;
            Timestamp = DateTime.Now;
            PostID = Guid.NewGuid();
        }

        public Post(string title, Author author, string body, DateTime timestamp, Guid postid)
        {
            Title = title;
            Author = author;
            Body = body;
            Timestamp = timestamp;
            PostID = postid;
        }

        public string GetPostPreview()
        {
            return $"\"{Title}\" by {Author} @ {Timestamp.ToString("h:mm:ss tt")}";
        }

        public override string ToString()
        {
            return $"{GetPostPreview()}:\n\t{Body}";
        }
    }
}
