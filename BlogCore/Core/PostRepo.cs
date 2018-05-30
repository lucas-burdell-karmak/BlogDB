using System;
using System.Collections.Generic;

namespace BlogDB.Core
{
    public class PostRepo : IPostRepo
    {
        private readonly IBlogDB<Post> _database;

        public PostRepo(IBlogDB<Post> database)
        {
            _database = database;
        }

        public Post AddPost(Post post)
        {
            List<Post> posts = _database.ReadAll();
            posts.Add(post);
            _database.WriteAll(posts);
            return post;
        }

        public bool TryAddPost(Post post, out Post result)
        {
            if (post == null || post.Title == null || post.Author == null || post.Body == null)
            {
                result = null;
                return false;
            }
            else
            {
                result = AddPost(post);
                return true;
            }
        }

        public Post DeletePost(Guid id)
        {
            List<Post> posts = _database.ReadAll();
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
            _database.WriteAll(posts);
            return toRemove;
        }

        public bool TryDeletePost(Guid id, out Post result)
        {
            result = DeletePost(id);
            if (result == null)
            {
                return false;
            }
            return true;
        }

        public Post EditPost(Post post)
        {
            var listOfPosts = _database.ReadAll();

            for (int i = 0; i < listOfPosts.Count; i++)
            {
                if (listOfPosts[i].PostID == post.PostID)
                {
                    listOfPosts[i] = post;
                    break;
                }
            }
            _database.WriteAll(listOfPosts);
            return post;
        }

        public List<Post> GetAllPosts()
        {
            return _database.ReadAll();
        }
    }
}