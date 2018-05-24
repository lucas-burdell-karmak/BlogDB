using System;

namespace BlogDB.Core
{
    public class Post
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid PostID { get; set; }

        public Post() { }

        public Post(string title, string author, string body)
        {
            Title = title;
            Author = author;
            Body = body;
            Timestamp = DateTime.Now;
        }

        public Post(string title, string author, string body, DateTime timestamp, Guid postid) : this(title, author, body)
        {
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
