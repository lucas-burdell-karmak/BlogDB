using System;
using System.Collections.Generic;

namespace BlogDB.Core
{
    public class BusinessLogic
    {
        private readonly BlogDB blogDatabase;

        public BusinessLogic()
        {
            blogDatabase = new BlogDB();
        }

        public string AddPost()
        {
            return "TODO: BusinessLogic.AddPost() method";
        }

        public string DeletePost()
        {
            return "TODO: BusinessLogic.DeletePost() method";
        }

        public string EditPost()
        {
            return "TODO: BusinessLogic.EditPost() method";
        }

        public List<string> GetListOfAuthors()
        {
            string TODO = "TODO: BusinessLogic.GetListOfAuthors() method";
            return new List<string>();
        }

        public List<Post> GetListOfPostsByAuthor()
        {
            string TODO = "TODO: BusinessLogic.GetListOfPostsByAuthor() method";
            return new List<Post>();
        }

        public Post GetPostByID()
        {
            string TODO = "TODO: BusinessLogic.GetPostByID() method";
            return new Post();
        }

        public List<Post> GetAllPosts()
        {
            string TODO = "TODO: BusinessLogic.GetAllPosts() method";
            return new List<Post>();
        }
    }
}