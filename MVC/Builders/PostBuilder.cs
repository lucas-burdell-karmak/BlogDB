using The_Intern_MVC.Models;
using BlogDB.Core;

namespace The_Intern_MVC.Builders
{
    public class PostBuilder
    {
        public PostModel Model { get; set; }

        public PostBuilder(PostModel model)
        {
            Model = model;
        }

        public Post build()
        {
            return (Model == null) ? null :
            new Post()
            {
                Title = Model.Title,
                Body = Model.Body,
                Timestamp = Model.Timestamp,
                PostID = Model.PostID,
                Author = new Author(Model.AuthorName, Model.AuthorID)
            };
        }
    }
}
