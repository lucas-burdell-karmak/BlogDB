using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogDB.Core;
using System.Web;
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
            try
            {
                var postResult = logic.AddPost(post);
                ViewBag.History = "/Home";

                return View("ViewSinglePost", postResult);
            }
            catch (ArgumentException ae)
            {
                // TODO exception logging

                string[] errorMessage = { "Cannot add post.", "We couldn't add the post. :(" };
                return View("NullPost", errorMessage);
            }
        }

        public IActionResult AddPost()
        {
            ViewBag.History = "/Home";
            return View();
        }

        public IActionResult EditPostResult(Post post)
        {
            try
            {
                ViewBag.History = "/Home/ViewAll";
                var postResult = logic.EditPost(post);
                return View("PostResult", postResult);
            }
            catch (ArgumentException ae)
            {
                string[] errorMessage = { "Invalid Post.", "The post contained some empty boxes. :(" };
                return View("NullPost", errorMessage);
            }
        }

        public IActionResult EditPost(String postid)
        {
            var postResult = logic.GetPostById(Guid.Parse(postid));
            if (postResult == null)
            {
                string[] errorMessage = { "Invalid Post.", "We couldn't find the post. :(" };
                ViewBag.History = "/Home/ViewAll";
                return View("NullPost", errorMessage);
            }
            ViewBag.History = "/Home/ViewSinglePost?postid=" + postid;
            return View(postResult);
        }

        public IActionResult DeletePostResult(Post post)
        {
            try
            {
                var postResult = logic.DeletePost(post);
                ViewBag.History = "/Home";
                return ViewAll();
            }
            catch (ArgumentException ae)
            {
                string[] errorMessage = { "Invalid Post.", "We couldn't find the post. :(" };
                ViewBag.History = "/Home/ViewAll";
                return View("NullPost", errorMessage);
            }
        }

        public IActionResult DeletePost(String postid)
        {
            var postResult = logic.GetPostById(Guid.Parse(postid));
            if (postResult == null)
            {
                string[] errorMessage = { "Invalid Post.", "We couldn't find the post. :(" };
                ViewBag.History = "/Home/ViewAll";
                return View("NullPost", errorMessage);
            }
            ViewBag.History = "/Home/ViewAll";
            return View(postResult);
        }

        public IActionResult NullPost(string message)
        {
            ViewBag.History = "/Home";
            return View(message);
        }

        public IActionResult ViewSinglePost(String postid)
        {
            var postResult = logic.GetPostById(Guid.Parse(postid));
            if (postResult == null)
            {
                ViewBag.History = "/Home/";
                return View("NullPost", "Post does not exist.");
            }
            ViewBag.History = Request.Headers["Referer"].ToString();
            return View(postResult);
        }

        public IActionResult ViewAll()
        {

            ViewBag.History = "/Home";
            var postResult = logic.GetAllPosts();
            if (postResult == null)
            {
                return View("NullPost", "There are no posts.");
            }
            return View(postResult);
        }

        public IActionResult ViewByAuthor(string author)
        {
            ViewBag.History = "/Home/Authors";
            var list = logic.GetListOfPostsByAuthor(author);
            return View("ViewAll", list);
        }

        public IActionResult Authors()
        {
            ViewBag.History = "/Home/";
            return View(logic.GetListOfAuthors());
        }

        public IActionResult SearchResult(String searchCriteria)
        {
            ViewBag.History = "/Home/";
            if (String.IsNullOrEmpty(searchCriteria))
            {
                return ViewAll();
            }
            var results = logic.SearchBy((post) =>
                    {
                        return post.Title.IndexOf(searchCriteria, StringComparison.OrdinalIgnoreCase) != -1 ||
                                post.Author.IndexOf(searchCriteria, StringComparison.OrdinalIgnoreCase) != -1 ||
                                post.Body.IndexOf(searchCriteria, StringComparison.OrdinalIgnoreCase) != -1;
                    }
            );

            return View("ViewAll", results);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}