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



        private readonly IBusinessLogic logic;

        public HomeController(IBusinessLogic logic)
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
                return View("NullPost", "Failed to add post.");
            }
            return View("ViewSinglePost", postResult);
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
                return View("NullPost", "Failed to edit post.");
            }

            return View("ViewSinglePost", postResult);
        }

        public IActionResult EditPost(String postid)
        {
            var postResult = logic.GetPostById(Guid.Parse(postid));
            if (postResult == null)
            {
                return View("NullPost", "Post does not exist.");
            }
            return View(postResult);
        }

        public IActionResult DeletePostResult(Post post)
        {
            var postResult = logic.DeletePost(post);
            if (postResult == null)
            {
                return View("NullPost", "Failed to delete post.");
            }
            return View("Index");
        }

        public IActionResult DeletePost(String postid)
        {
            var postResult = logic.GetPostById(Guid.Parse(postid));
            if (postResult == null)
            {
                return View("NullPost", "Post does not exist.");
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
