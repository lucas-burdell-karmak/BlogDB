using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;
using System.Linq; //SortListOfPostsBy*

namespace The_Intern_MVC.Models
{
    // FileDB Class - defines a FileDB object that read/writes posts to disk
    public class FileDB : IPostDB
    {      
        
        //fix path
        public readonly string BlogDatabasePath = Path.Combine(Directory.GetCurrentDirectory(), "blogDatabase.json");
        

        public FileDB()
        {
        }

        public void AddPost(Post post)
        {
            post.PostID = Guid.NewGuid();
            List<Post> posts = ReadFromJsonFile();
            posts.Add(post);
            WriteToJsonFile(posts);
        }

        public void DeletePost(Guid id)
        {
            // read from JSON file
            List<Post> posts = ReadFromJsonFile();
            // find that ID
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
            WriteToJsonFile(posts);
        }

        private Post GetPostInListById(List<Post> posts, Guid id)
        {
            foreach (var post in posts)
            {
                if (post.PostID == id)
                {
                    return post;
                }
            }

            return null;
        }

        public List<string> GetAllAuthors()
        {
            var posts = ReadFromJsonFile();
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

        public List<Post> GetListOfPosts()
        {
            return ReadFromJsonFile();
        }

        public List<Post> GetAllPostsByAuthor(string authorName)
        {
            var postsByAuthor = new List<Post>();

            foreach (var post in ReadFromJsonFile())
            {
                if (authorName.CompareTo(post.Author) == 0) postsByAuthor.Add(post);
            }
            return postsByAuthor;
        }

        public Post GetPostById(Guid id)
        {
            var posts = ReadFromJsonFile();
            foreach (var post in posts)
            {
                if (post.PostID == id)
                    return post;
            }
            return null;
        }

        public int GetPostCount()
        {
            var posts = ReadFromJsonFile();
            return posts.Count;
        }


        public List<Post> ReadFromJsonFile()
        {
            using (var reader = new StreamReader(new FileStream(BlogDatabasePath, FileMode.OpenOrCreate)))
            {
                var fileContents = reader.ReadToEnd();
                var posts = JsonConvert.DeserializeObject<List<Post>>(fileContents);
                if (posts == null)
                {
                    posts = new List<Post>();
                }
                return posts;
            }
        }

        public List<Post> SortListBy(PostProperty sortType)
        {
            var posts = ReadFromJsonFile();
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


        public void WriteToJsonFile(List<Post> listOfPosts)
        {
            // false means overwrite
            using (var writer = new StreamWriter(BlogDatabasePath, false))
            {
                var contentsToWrite = JsonConvert.SerializeObject(listOfPosts);
                writer.Write(contentsToWrite);
            }
        }

        public void EditPost(Guid id, PostProperty variable, string newValue)
        {
            var posts = ReadFromJsonFile();
            var post = GetPostInListById(posts, id);
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
            WriteToJsonFile(posts);
        }
    }
}