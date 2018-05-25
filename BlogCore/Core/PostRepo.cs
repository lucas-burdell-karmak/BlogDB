using System;
using System.Collections.Generic;

namespace BlogDB.Core
{
    public class PostRepo
    {
        private readonly IPostDB<Post> database;

        public PostRepo()
        {
            database = new FileDB<Post>();
        }

        public PostRepo(IPostDB<Post> database)
        {
            this.database = database;
        }

        public Post AddPost(Post post)
        {
            List<Post> posts = database.ReadAll();
            posts.Add(post);
            database.WriteAll(posts);
            return post;
        }

        public Post DeletePost(Guid id)
        {
            List<Post> posts = database.ReadAll();
            Post toRemove = null;
            foreach (var p in posts)
            {
                if (p.PostID == id)
                {
                    toRemove = p;
                    break;
                }
            }
            if (toRemove != null)
            {
                posts.Remove(toRemove);
            }
            database.WriteAll(posts);
            return toRemove;
        }

        public Post EditPost(Post post)
        {
            var listOfPosts = database.ReadAll();

            for (int i = 0; i < listOfPosts.Count; i++)
            {
                if (listOfPosts[i].PostID == post.PostID)
                {
                    listOfPosts[i] = post;
                    break;
                }
            }
            database.WriteAll(listOfPosts);
            return post;
        }

        public List<Post> GetAllPosts()
        {
            return database.ReadAll();
        }
    }
}