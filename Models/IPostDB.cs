using System;
using System.Collections.Generic;

namespace The_Intern_MVC.Models
{
    public interface IPostDB {
        
        void AddPost(Post post);
        void DeletePost(Guid id);
        void EditPost(Guid id, PostProperty variable, string newValue);
        List<Post> GetListOfPosts();
        Post GetPostById(Guid id);
        int GetPostCount();
        List<string> GetAllAuthors();
        List<Post> GetAllPostsByAuthor(string authorName);
        List<Post> SortListBy(PostProperty sortType);
    }
}