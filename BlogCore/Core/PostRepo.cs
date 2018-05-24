using System;
using System.Collections.Generic;

namespace BlogDB.Core
{
    public class PostRepo
    {
        private readonly FileDB database;

        public PostRepo()
        {
            database = new FileDB();
        }

        public void AddPost(Post post)
        {
            post.PostID = Guid.NewGuid();
            List<Post> posts = ReadDatabase();
            posts.Add(post);
            WriteDatabase(posts);
        }

        public void DeletePost(Guid id)
        {
            List<Post> posts = ReadDatabase();
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
            WriteDatabase(posts);
        }

        public void EditPost(Post post)
        {
            var listOfPosts = ReadDatabase();

            for (int i = 0; i < listOfPosts.Count; i++)
            {
                if (listOfPosts[i].PostID == post.PostID)
                {
                    listOfPosts[i] = post;
                    break;
                }
            }
            WriteDatabase(listOfPosts);
        }

        public List<Post> GetAllPosts()
        {
            return ReadDatabase();
        }

        private List<Post> ReadDatabase()
        {
            return database.ReadFromJsonFile();
        }

        private void WriteDatabase(List<Post> listOfPosts)
        {
            database.WriteToJsonFile(listOfPosts);
        }
    }
}