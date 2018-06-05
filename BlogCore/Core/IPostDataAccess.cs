using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogDB.Core
{
    public interface IPostDataAccess
    {
        Post AddPost(Post post);
        Post DeletePost(Post post);
        Post EditPost(Post post);
        List<Author> GetAllAuthors();
        List<Post> GetAllPosts();
        List<string> GetListOfAuthorNames();
        List<int> GetListOfAuthorIDs();
        List<Post> GetListOfPostsByAuthorID(int authorID);
        Post GetPostById(Guid id);
        int GetPostCount();
        Post GetPostFromList(List<Post> listOfPosts, Guid id);
        List<Post> GetSortedListOfPosts(PostComponent sortType);
        List<Post> SearchBy(Func<Post, bool> criteria);
    }
}