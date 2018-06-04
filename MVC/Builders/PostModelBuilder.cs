using System;
using The_Intern_MVC.Models;
using BlogDB.Core;

namespace The_Intern_MVC.Builders {

    public class PostModelBuilder
    {


        public Post Post {get; set;}

        public PostModelBuilder(Post post)
        {
            Post = post;
        }
        

        public PostModel build()
        {
            if (Post == null)
                return null;

            var postModel = new PostModel();
            postModel.Title = Post.Title;
            postModel.AuthorName = Post.Author.Name;
            postModel.AuthorID = Post.Author.ID;
            postModel.Body = Post.Body;
            postModel.PostID = Post.PostID;
            postModel.Timestamp = Post.Timestamp;
            return postModel;
        }
    }
}