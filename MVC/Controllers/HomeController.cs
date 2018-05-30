using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogDB.Core;
using The_Intern_MVC.Models;

namespace The_Intern_MVC.Controllers
{
    public class HomeController : Controller
    {



        private readonly IPostDataAccess logic;

        public HomeController(IPostDataAccess logic)
        {
            this.logic = logic;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddPostResult(Post post)
        {
            var postResult = logic.AddPost(post);
            if (postResult == null)
            {
                string[] errorMessage = {"Cannot add post.", "We couldn't add the post. :("};
                return View("NullPost", errorMessage);
            }
            return View("PostResult", postResult);
        }

        public IActionResult AddPost()
        {
            return View();
        }

        public IActionResult EditPostResult(Post post)
        {
            var postResult = logic.EditPost(post);
            if (postResult == null)
            {
                string[] errorMessage = {"Invalid Post.", "The post contained some empty boxes. :("};
                return View("NullPost", errorMessage);
            }

            return View("PostResult", postResult);
        }

        public IActionResult EditPost(String postid)
        {
            var postResult = logic.GetPostById(Guid.Parse(postid));
            if (postResult == null)
            {
                string[] errorMessage = {"Invalid Post.", "We couldn't find the post. :("};
                return View("NullPost", errorMessage);
            }
            return View(postResult);
        }

        public IActionResult DeletePostResult(Post post)
        {
            var postResult = logic.DeletePost(post);
            if (postResult == null)
            {
                string[] errorMessage = {"Invalid Post.", "We couldn't find the post. :("};
                return View("NullPost", errorMessage);
            }
            return View("Index");
        }

        public IActionResult DeletePost(String postid)
        {
            var postResult = logic.GetPostById(Guid.Parse(postid));
            if (postResult == null)
            {
                string[] errorMessage = {"Invalid Post.", "We couldn't find the post. :("};
                return View("NullPost", errorMessage);
            }
            return View(postResult);
        }

        public IActionResult NullPost(string message)
        {
            return View(message);
        }

        public IActionResult ViewSinglePost(String postid)
        {
            var postResult = logic.GetPostById(Guid.Parse(postid));
            if (postResult == null)
            {
                return View("NullPost", "Post does not exist.");
            }
            return View(postResult);
        }

        public IActionResult ViewAll()
        {
            var postResult = logic.GetAllPosts();
            if (postResult == null)
            {
                return View("NullPost", "There are no posts.");
            }
            return View(postResult);
        }

        public IActionResult ViewByAuthor(string author)
        {
            var list = logic.GetListOfPostsByAuthor(author);
            return View("ViewAll", list);
        }

        public IActionResult Authors()
        {

            return View(logic.GetListOfAuthors());
        }

        public IActionResult SearchResult(String searchCriteria)
        {
            var results = logic.SearchBy((post) => {
                return post.Title.IndexOf(searchCriteria, StringComparison.OrdinalIgnoreCase) != -1 || 
                        post.Author.IndexOf(searchCriteria, StringComparison.OrdinalIgnoreCase) != -1 ||
                        post.Body.IndexOf(searchCriteria, StringComparison.OrdinalIgnoreCase) != -1;
            });

            return View("ViewAll", results);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
