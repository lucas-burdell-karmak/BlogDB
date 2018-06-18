using The_Intern_MVC.Models;
using BlogDB.Core;

namespace The_Intern_MVC.Builders
{
    public class PostModelBuilder
    {
        public Post Post { get; set; }

        public PostModelBuilder(Post post) => Post = post;

        public PostModel build()
        {
            return (Post == null) ? null :
                new PostModel()
                {
                    Title = Post.Title,
                    Body = Post.Body,
                    AuthorName = Post.Author.Name,
                    AuthorID = Post.Author.ID,
                    PostID = Post.PostID,
                    Timestamp = Post.Timestamp
                };
        }
    }
}
