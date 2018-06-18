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
            throw new ArgumentException("This post has invaild properties.");
        }

        public Post DeletePost(Post post)
        {
            if (_postValidator.PostExists(_postRepo.GetAllPosts(), post))
            {
                var isSuccessful = _postRepo.TryDeletePost(post.PostID, out var result);
                return (isSuccessful) ? result : throw new ArgumentException("Something went horribly wrong in PostDataAccess.DeletePost.");
            }
            throw new ArgumentException("The post does not exist.");
        }

        public Post EditPost(Post post)
        {
            if (_postValidator.PostExists(_postRepo.GetAllPosts(), post) && _postValidator.IsValidPost(post) && _authorValidator.IsValidAuthor(post.Author))
            {
                post.Timestamp = DateTime.UtcNow;
                var isSuccessful = _postRepo.TryEditPost(post, out var result);
                return (isSuccessful) ? result : throw new ArgumentException("Something went horribly wrong in PostDataAccess.EditPost.");
            }
            throw new ArgumentException("This post does not exist or has invaild properties.");
        }

        public List<Author> GetAllAuthors() => _authorRepo.GetListOfAuthors();
        public List<Post> GetAllPosts() => _postRepo.GetAllPosts();

        public List<int> GetListOfAuthorIDs() => _authorRepo.GetListOfAuthors().Select(x => x.ID).ToList();

        public List<string> GetListOfAuthorNames() => _authorRepo.GetListOfAuthors().Select(x => x.Name).ToList();

        public List<Post> GetListOfPostsByAuthorID(int authorID)
        {
            var listOfPostsByAuthor = _postRepo.GetAllPostsByAuthor(authorID);
            if (listOfPostsByAuthor == null)
                return new List<Post>();
            return listOfPostsByAuthor;
        }

        public Post GetPostById(Guid id) => _postRepo.GetAllPosts().FirstOrDefault(x => x.PostID == id);

        public int GetPostCount() => _postRepo.GetAllPosts().Count;

        public Post GetPostFromList(List<Post> listOfPosts, Guid id) => listOfPosts.FirstOrDefault(x => x.PostID == id);

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

        public List<Post> SearchBy(Func<Post, bool> filter) => _postRepo.GetAllPosts().Where(x => filter(x)).ToList();
    }
}