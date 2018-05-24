using System;
using System.Collections.Generic;

namespace BlogDB.Core
{
    public interface IBusinessLogic
    {
        string AddPost(Post post);
        string DeletePost(Post post);
        string EditPost(Post post);
        List<string> GetListOfAuthors();
        List<Post> GetListOfPostsByAuthor(string authorName);
        Post GetPostById(Guid id);
        int GetPostCount();
        Post GetPostFromList(List<Post> listOfPosts, Guid id);
        List<Post> GetSortedListOfPosts(PostComponent sortType);
    }
}