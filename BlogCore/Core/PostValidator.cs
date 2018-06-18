using System.Collections.Generic;
using System.Linq;

namespace BlogDB.Core
{
    public class PostValidator : IPostValidator
    {

        public bool PostExists(List<Post> listOfPosts, Post post) => listOfPosts.Exists(x => x.PostID == post.PostID);

        public bool IsValidBody(string body) => IsValidString(body);

        public bool IsValidTitle(string title) => IsValidString(title);

        public bool IsValidString(string str)
        {
            return !(string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str));
        }

        public bool IsValidPost(Post post)
        {
            if (post == null)
                return false;
            return IsValidBody(post.Body) && IsValidTitle(post.Title);
        }
    }
}
