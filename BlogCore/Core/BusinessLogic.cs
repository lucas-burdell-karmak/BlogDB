using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogDB.Core
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly PostRepo postRepo;
        private readonly PostValidator postValidator;

        public BusinessLogic()
        {
            postRepo = new PostRepo();
            postValidator = new PostValidator();
        }

        public Post AddPost(Post post)
        {
            if (postValidator.isValidPost(post))
            {
                post.PostID = Guid.NewGuid();
                post.Timestamp = DateTime.UtcNow;
                return postRepo.AddPost(post);
            }
            else
            {
                return null;
            }
        }

        public Post DeletePost(Post post)
        {
            if(postValidator.postExists(postRepo.GetAllPosts(), post)) {
                return postRepo.DeletePost(post.PostID);
            }
            return null;
        }

        public Post EditPost(Post post)
        {
            if (postValidator.postExists(postRepo.GetAllPosts(), post) && postValidator.isValidPost(post))
            {
                return postRepo.EditPost(post);
            }
            else
            {
                return null;
            }
        }

        public List<Post> GetAllPosts() => postRepo.GetAllPosts();


        public List<string> GetListOfAuthors()
        {
            var posts = postRepo.GetAllPosts();
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

            foreach (var post in postRepo.GetAllPosts())
            {
                if (authorName.CompareTo(post.Author) == 0) postsByAuthor.Add(post);
            }
            return postsByAuthor;
        }

        public Post GetPostById(Guid id)
        {
            var listOfPosts = postRepo.GetAllPosts();
            foreach (var post in listOfPosts)
            {
                if (post.PostID == id)
                    return post;
            }
            return null;
        }

        public int GetPostCount()
        {
            var posts = postRepo.GetAllPosts();
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

        public List<Post> GetSortedListOfPosts(PostComponent sortType)
        {
            var posts = postRepo.GetAllPosts();
            switch (sortType)
            {
                case PostComponent.author:
                    return posts.OrderBy(x => x.Author).ToList();
                case PostComponent.title:
                    return posts.OrderBy(x => x.Title).ToList();
                case PostComponent.timestamp:
                    return posts.OrderBy(x => x.Timestamp).ToList();
                default:
                    return posts;
            }
        }
    }
}