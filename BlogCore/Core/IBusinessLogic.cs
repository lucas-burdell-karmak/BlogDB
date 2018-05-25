using System;
using System.Collections.Generic;

namespace BlogDB.Core
{
    public interface IBusinessLogic
    {
        Post AddPost(Post post);
        Post DeletePost(Post post);
        Post EditPost(Post post);
        List<string> GetListOfAuthors();
        List<Post> GetListOfPostsByAuthor(string authorName);
        Post GetPostById(Guid id);
        int GetPostCount();
        Post GetPostFromList(List<Post> listOfPosts, Guid id);
        List<Post> GetSortedListOfPosts(PostComponent sortType);
        List<Post> GetAllPosts();
    }
}