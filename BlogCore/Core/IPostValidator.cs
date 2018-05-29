using System;
using System.Collections.Generic;

namespace BlogDB.Core
{
    public interface IPostValidator
    {


        bool postExists(List<Post> listOfPosts, Post post);

        bool isValidAuthor(string author);

        bool isValidBody(string body);

        bool isValidTitle(string title);

        bool isValidString(string str);

        bool isValidPost(Post post);
    }
}