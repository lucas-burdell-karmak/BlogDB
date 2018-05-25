using System;
using System.Collections.Generic;

namespace BlogDB.Core
{
    public class PostValidator
    {

        public bool postExists(List<Post> listOfPosts, Post post)
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

        public bool isValidAuthor(string author) => isValidString(author);

        public bool isValidBody(string body) => isValidString(body);

        public bool isValidTitle(string title) => isValidString(title);

        public bool isValidString(string str)
        {
            return !(string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str));
        }

        public bool isValidPost(Post post)
        {
            return isValidAuthor(post.Author) && isValidBody(post.Body) && isValidTitle(post.Title);
        }
    }
}