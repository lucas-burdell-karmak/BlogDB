using System;
using The_Intern_MVC.Models;
using BlogDB.Core;

namespace The_Intern_MVC.Builders {

    public class PostBuilder
    {



        public PostModel Model {get; set;}

        public PostBuilder(PostModel model)
        {
            Model = model;
        }

        public Post build()
        {
            if (Model == null)
                return null;

            var post = new Post();
            post.Title = Model.Title;
            post.Body = Model.Body;
            post.Timestamp = Model.Timestamp;
            post.PostID = Model.PostID;
            post.Author = new Author(Model.AuthorName, Model.AuthorID);
            return post;
        }
    }
}