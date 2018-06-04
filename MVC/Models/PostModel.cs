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


        public static implicit operator PostModel(Post post)
        {


            if (post == null)
                return null;

            var postModel = new PostModel();
            postModel.Title = post.Title;
            postModel.AuthorName = post.Author.Name;
            postModel.Body = post.Body;
            postModel.PostID = post.PostID;
            postModel.Timestamp = post.Timestamp;
            return postModel;
        }

        public static implicit operator Post(PostModel postModel)
        {
            if (postModel == null)
                return null;

            var post = new Post();
            post.Title = postModel.Title;
            post.Author = null; // TODO
            post.Body = postModel.Body;
            post.Timestamp = postModel.Timestamp;
            post.PostID = postModel.PostID;
            return post;
        }

        public static PostModel Empty { get => new PostModel(); }

    }
}

