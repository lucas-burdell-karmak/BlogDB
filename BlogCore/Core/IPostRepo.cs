using System;
using System.Collections.Generic;

namespace BlogDB.Core
{
    public interface IPostRepo
    {
        bool TryAddPost(Post post, out Post result);
        bool TryDeletePost(Guid id, out Post result);
        bool TryEditPost(Post post, out Post result);
        List<Post> GetAllPosts();
        List<Post> GetAllPostsByAuthor(int authorID);
    }
}