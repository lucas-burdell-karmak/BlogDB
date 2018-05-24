using System;
using System.Collections.Generic;

namespace BlogDB.Core
{
    public interface IBlogRepo
    {
        void AddPost(Post post);
        void DeletePost(Guid id);
        void EditPost(Guid id, PostProperty variable, string newValue);
        List<string> GetListOfAuthors();
        List<Post> GetListOfPostsByAuthor(string authorName);
        Post GetPostById(Guid id);
        int GetPostCount();
        List<Post> GetPostFromList(List<Post> listOfPosts, Guid id);
        List<Post> GetSortedListOfPosts(PostProperty sortType);
        List<Post> ReadDatabase();
        void WriteDatabase();
    }
}