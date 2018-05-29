using System;
using System.Collections.Generic;

namespace BlogDB.Core
{
    public interface IPostRepo
    {
        Post AddPost(Post post);

        Post DeletePost(Guid id);

        Post EditPost(Post post);

        List<Post> GetAllPosts();
    }
}