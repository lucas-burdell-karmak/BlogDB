using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogDB.Core
{
    public class PostDataAccess : IPostDataAccess
    {
        private readonly IPostRepo _postRepo;
        private readonly IPostValidator _postValidator;
        private readonly IAuthorRepo _authorRepo;

        private readonly IAuthorValidator _authorValidator;

        public PostDataAccess(IPostRepo postRepo, IPostValidator postValidator, IAuthorRepo authorRepo, IAuthorValidator authorValidator)
        {
            _postRepo = postRepo;
            _postValidator = postValidator;
            _authorRepo = authorRepo;
            _authorValidator = authorValidator;
        }

        public Post AddPost(Post post)
        {
            if (_postValidator.IsValidPost(post) && _authorValidator.IsValidAuthor(post.Author))
            {
                post.PostID = Guid.NewGuid();
                post.Timestamp = DateTime.UtcNow;
                var isSuccessful = _postRepo.TryAddPost(post, out var result);
                return (isSuccessful) ? result : throw new ArgumentException("Something went horribly wrong in PostDataAccess.AddPost.");
            }
            else
            {
                throw new ArgumentException("This post has invaild properties.");
            }
        }

        public Post DeletePost(Post post)
        {
            if (_postValidator.PostExists(_postRepo.GetAllPosts(), post))
            {
                var isSuccessful = _postRepo.TryDeletePost(post.PostID, out var result);
                return (isSuccessful) ? result : throw new ArgumentException("Something went horribly wrong in PostDataAccess.DeletePost.");
            }
            else
            {
                throw new ArgumentException("The post does not exist.");
            }
        }

        public Post EditPost(Post post)
        {
            if (_postValidator.PostExists(_postRepo.GetAllPosts(), post) && _postValidator.IsValidPost(post) && _authorValidator.IsValidAuthor(post.Author))
            {
                post.Timestamp = DateTime.UtcNow;
                var isSuccessful = _postRepo.TryEditPost(post, out var result);
                return (isSuccessful) ? result : throw new ArgumentException("Something went horribly wrong in PostDataAccess.EditPost.");
            }
            else
            {
                throw new ArgumentException("This post does not exist or has invaild properties.");
            }
        }

        public List<Author> GetAllAuthors() => _authorRepo.GetListOfAuthors();
        public List<Post> GetAllPosts() => _postRepo.GetAllPosts();

        public List<int> GetListOfAuthorIDs()
        {
            List<Author> authors = _authorRepo.GetListOfAuthors();
            List<int> authorIDs = new List<int>();
            foreach (Author a in authors)
            {
                authorIDs.Add(a.ID);
            }
            return authorIDs;
        }

        public List<string> GetListOfAuthorNames()
        {
            List<Author> authors = _authorRepo.GetListOfAuthors();
            List<string> authorNames = new List<string>();
            foreach (Author a in authors)
            {
                authorNames.Add(a.Name);
            }
            return authorNames;
        }

        public List<Post> GetListOfPostsByAuthorID(int authorID)
        {
            var listOfPostsByAuthor = _postRepo.GetAllPostsByAuthor(authorID);
            if (listOfPostsByAuthor == null) return new List<Post>();

            return listOfPostsByAuthor;
        }

        public Post GetPostById(Guid id)
        {
            var listOfPosts = _postRepo.GetAllPosts();
            foreach (var post in listOfPosts)
            {
                if (post.PostID == id)
                    return post;
            }
            return null;
        }

        public int GetPostCount()
        {
            var posts = _postRepo.GetAllPosts();
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
            var posts = _postRepo.GetAllPosts();
            switch (sortType)
            {
                case PostComponent.author:
                    return posts.OrderBy(x => x.Author.Name).ToList();
                case PostComponent.title:
                    return posts.OrderBy(x => x.Title).ToList();
                case PostComponent.timestamp:
                    return posts.OrderBy(x => x.Timestamp).ToList();
                default:
                    return posts;
            }
        }

        public List<Post> SearchBy(Func<Post, bool> filter)
        {
            var posts = _postRepo.GetAllPosts();
            var results = new List<Post>();
            posts.ForEach((post) =>
            {
                if (filter(post))
                {
                    results.Add(post);
                }
            });
            return results;
        }
    }
}