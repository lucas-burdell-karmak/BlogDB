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
        private readonly IPostDataAccess _postDataAccess;

        public HomeController(IPostDataAccess logic)
        {
            this._postDataAccess = logic;
        }
        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult AddPostConfirmation(PostModel post)
        {
            return PartialView("AddButton", post);
        }

        public IActionResult AddPostResult(PostModel post)
        {
            try
            {
                PostModel postResult = _postDataAccess.AddPost(post);
                ViewBag.History = "/Home";

                return View("ViewSinglePost", postResult);
            }
            catch (ArgumentException e)
            {
                // TODO exception logging

                string[] errorMessage = { "Cannot add post.", "The post had empty properties. :(" };
                return View("NullPost", errorMessage);
            }
        }

        public IActionResult AddPost()
        {
            ViewBag.History = "/Home";
            return View(PostModel.Empty);
        }

        public IActionResult EditPostResult(PostModel post)
        {
            try
            {
                ViewBag.History = "/Home/ViewAll";
                PostModel postResult = _postDataAccess.EditPost(post);
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
            PostModel postResult = _postDataAccess.GetPostById(Guid.Parse(postid));
            if (postResult == null)
            {
                string[] errorMessage = { "Invalid Post.", "We couldn't find the post. :(" };
                ViewBag.History = "/Home/ViewAll";
                return View("NullPost", errorMessage);
            }
            ViewBag.History = "/Home/ViewSinglePost?postid=" + postid;
            return View(postResult);
        }

        public IActionResult DeletePostResult(PostModel post)
        {
            try
            {
                PostModel postResult = _postDataAccess.DeletePost(post);
                ViewBag.History = "/Home";
                return RedirectToAction("ViewAll");
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
            PostModel postResult = _postDataAccess.GetPostById(Guid.Parse(postid));
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
            PostModel postResult = _postDataAccess.GetPostById(Guid.Parse(postid));
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
            List<PostModel> postResult = _postDataAccess.GetAllPosts().ConvertAll<PostModel>((p) => (PostModel) p);
            if (postResult == null)
            {
                return View("NullPost", "There are no posts.");
            }
            return View(postResult);
        }

        public IActionResult ViewByAuthor(string author)
        {
            ViewBag.History = "/Home/Authors";
            List<PostModel> list = _postDataAccess.GetListOfPostsByAuthor(author).ConvertAll<PostModel>((p) => (PostModel) p);
            return View("ViewAll", list);
        }

        public IActionResult Authors()
        {
            ViewBag.History = "/Home/";
            return View(_postDataAccess.GetListOfAuthors());
        }

        public IActionResult SearchResult(SearchCriteria searchCriteria)
        {
            ViewBag.History = "/Home/";
            if (String.IsNullOrEmpty(searchCriteria.SearchString))
            {
                return RedirectToAction("ViewAll");
            }
            List<PostModel> results = _postDataAccess.SearchBy((post) =>
                    {
                        return post.Title.IndexOf(searchCriteria.SearchString, StringComparison.OrdinalIgnoreCase) != -1 ||
                                post.Author.IndexOf(searchCriteria.SearchString, StringComparison.OrdinalIgnoreCase) != -1 ||
                                post.Body.IndexOf(searchCriteria.SearchString, StringComparison.OrdinalIgnoreCase) != -1;
                    }
            ).ConvertAll<PostModel>((p) => (PostModel) p);

            return View("ViewAll", results);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}