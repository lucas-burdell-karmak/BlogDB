using System;
using System.Collections.Generic;

namespace BlogDB.Core
{
    public class PostValidator : IPostValidator
    {

        public bool PostExists(List<Post> listOfPosts, Post post)
        {
            foreach (Post p in listOfPosts)
            {
                if (p.PostID == post.PostID)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsValidBody(string body) => IsValidString(body);

        public bool IsValidTitle(string title) => IsValidString(title);

        public bool IsValidString(string str)
        {
            return !(string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str));
        }

        public bool IsValidPost(Post post)
        {
            if (post == null)
            {
                return false;
            }
            return IsValidAuthor(post.Author.Name) && IsValidBody(post.Body) && IsValidTitle(post.Title);
        }
    }
}