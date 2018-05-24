using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogDB.Core
{
    public class BlogRepo
    {
        private readonly FileDB database;

        public BlogRepo()
        {
            database = new FileDB();
        }

        public void AddPost(Post post)
        {
            post.PostID = Guid.NewGuid();
            List<Post> posts = database.ReadFromJsonFile();
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

        public void EditPost(Guid id, PostProperty variable, string newValue)
        {
            var posts = ReadDatabase();
            var post = GetPostFromList(posts, id);
            switch (variable)
            {
                case PostProperty.title:
                    post.Title = newValue;
                    break;
                case PostProperty.author:
                    post.Author = newValue;
                    break;
                case PostProperty.body:
                    post.Body = newValue;
                    break;
            }
            WriteDatabase(posts);
        }

        public List<string> GetListOfAuthors()
        {
            var posts = ReadDatabase();
            var authors = new List<string>();
            posts.ForEach((Post post) =>
            {
                if (!authors.Contains(post.Author, StringComparer.OrdinalIgnoreCase))
                {
                    authors.Add(post.Author);
                }
            });
            return authors;
        }

        public List<Post> GetListOfPostsByAuthor(string authorName)
        {
            var postsByAuthor = new List<Post>();

            foreach (var post in ReadDatabase())
            {
                if (authorName.CompareTo(post.Author) == 0) postsByAuthor.Add(post);
            }
            return postsByAuthor;
        }

        public Post GetPostById(Guid id)
        {
            var posts = ReadDatabase();
            foreach (var post in posts)
            {
                if (post.PostID == id)
                    return post;
            }
            return null;
        }

        public int GetPostCount()
        {
            var posts = ReadDatabase();
            return posts.Count;
        }

        public Post GetPostFromList(List<Post> listOfPosts, Guid id)
        {
            foreach (var post in listOfPosts)
            {
                if (post.PostID == id)
                {
                    return post;
                }
            }

            return null;
        }

        public List<Post> GetSortedListOfPosts(PostProperty sortType)
        {
            var posts = ReadDatabase();
            switch (sortType)
            {
                case PostProperty.author:
                    return posts.OrderBy(x => x.Author).ToList();
                case PostProperty.title:
                    return posts.OrderBy(x => x.Title).ToList();
                case PostProperty.timestamp:
                    return posts.OrderBy(x => x.Timestamp).ToList();
                default:
                    return posts;
            }
        }

        private List<Post> ReadDatabase() {
            return database.ReadFromJsonFile();
        }

        private void WriteDatabase(List<Post> listOfPosts) {
            database.WriteToJsonFile(listOfPosts);
        }
    }
}