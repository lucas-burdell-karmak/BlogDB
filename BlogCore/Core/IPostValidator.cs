using System;
using System.Collections.Generic;

namespace BlogDB.Core
{
    public interface IPostValidator
    {
        bool PostExists(List<Post> listOfPosts, Post post);
        bool IsValidBody(string body);
        bool IsValidTitle(string title);
        bool IsValidString(string str);
        bool IsValidPost(Post post);
    }
}