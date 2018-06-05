using System;
using BlogDB.Core;
using System.ComponentModel.DataAnnotations;

namespace The_Intern_MVC.Models
{
    public class PostModel
    {
        [Required]
        [StringLength(60)]
        public string Title { get; set; }

        [Required]
        [StringLength(30)]
        public string AuthorName { get; set; }

        public int AuthorID { get; set; }

        [Required]
        [StringLength(7950)]
        public string Body { get; set; }

        public DateTime Timestamp { get; set; }

        public Guid PostID { get; set; }

        public static PostModel Empty { get => new PostModel(); }
    }
}

