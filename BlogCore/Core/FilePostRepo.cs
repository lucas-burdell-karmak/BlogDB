using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace BlogDB.Core
{
    public class FilePostRepo : IPostRepo
    {
        private readonly string _pathToDB;

        public FilePostRepo(IConfiguration config)
        {
            _pathToDB = config["DatabaseFilePath"];
        }

        public FilePostRepo(string pathToDB)
        {
            _pathToDB = pathToDB;
        }

        private List<Post> ReadAll()
        {
            using (var reader = new StreamReader(new FileStream(_pathToDB, FileMode.OpenOrCreate)))
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

        private void WriteAll(List<Post> listOfPosts)
        {
            // false means overwrite
            using (var writer = new StreamWriter(_pathToDB, false))
            {
                var contentsToWrite = JsonConvert.SerializeObject(listOfPosts);
                writer.Write(contentsToWrite);
            }
        }

        private Post AddPost(Post post)
        {
            List<Post> posts = ReadAll();
            posts.Add(post);
            WriteAll(posts);
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

        private Post DeletePost(Guid id)
        {
            List<Post> posts = ReadAll();
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
            WriteAll(posts);
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

        private Post EditPost(Post post)
        {
            var listOfPosts = ReadAll();

            for (int i = 0; i < listOfPosts.Count; i++)
            {
                if (listOfPosts[i].PostID == post.PostID)
                {
                    listOfPosts[i] = post;
                    WriteAll(listOfPosts);
                    return post;
                }
            }
            return null;
        }

        public bool TryEditPost(Post post, out Post result)
        {
            if (post == null || post.Title == null || post.Author == null || post.Body == null)
            {
                result = null;
            }
            else
            {
                result = EditPost(post);
            }
            return (result == null) ? false : true;
        }

        public List<Post> GetAllPosts()
        {
            return ReadAll();
        }
    }
}