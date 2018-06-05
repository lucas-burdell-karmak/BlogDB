using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BlogDB.Core;
using The_Intern_MVC.Builders;
using The_Intern_MVC.Models;

namespace The_Intern_MVC.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly IPostDataAccess _postDataAccess;

        public HomeController(IPostDataAccess logic)
        {
            this._postDataAccess = logic;
        }

        [Authorize(Roles = "BlogAuthor")]
        public IActionResult Index()
        {
            return View("~/Views/Home/Index");
        }

        public PartialViewResult AddPostConfirmation(PostModel post)
        {
            return PartialView("~/Views/Partials/AddButton", post);
        }

        public IActionResult AddPostResult(PostModel post)
        {
            try
            {
                var postBuilder = new PostBuilder(post);
                var postResult = _postDataAccess.AddPost(postBuilder.build());
                ViewBag.History = "~/Views/Home";

                var pmBuilder = new PostModelBuilder(postResult);
                return View("~/Views/Home/ViewSinglePost", pmBuilder.build());
            }
            catch (ArgumentException e)
            {
                // TODO exception logging

                string[] errorMessage = { "Cannot add post.", "The post had empty properties. :(" };
                Console.WriteLine(e.ToString());
                return View("~/Views/NullPost/Index", errorMessage);
            }
        }

        public IActionResult AddPost()
        {
            ViewBag.History = "~/Views/Home";
            return View("~/Views/Home/AddPost");
        }

        public IActionResult Authors()
        {
            ViewBag.History = "~/Views/Home";
            return View(_postDataAccess.GetListOfAuthors());
        }

        public IActionResult DeletePostResult(PostModel post)
        {
            try
            {
                var postBuilder = new PostBuilder(post);
                var postResult = _postDataAccess.DeletePost(postBuilder.build());
                ViewBag.History = "~/Views/Home";
                return RedirectToAction("~/Views/Home/ViewAll");
            }
            catch (ArgumentException e)
            {
                string[] errorMessage = { "Invalid Post.", "We couldn't find the post. :(" };
                ViewBag.History = "~/Views/Home/ViewAll";
                Console.WriteLine(e.ToString());
                return View("~/Views/NullPost/Index", errorMessage);
            }
        }
        public IActionResult EditPostResult(PostModel post)
        {
            try
            {
                ViewBag.History = "~/Views/Home/ViewAll";
                var postBuilder = new PostBuilder(post);
                var postResult = _postDataAccess.EditPost(postBuilder.build());
                var pmBuilder = new PostModelBuilder(postResult);
                return View("~/Views/Home/PostResult", pmBuilder.build());
            }
            catch (ArgumentException e)
            {
                string[] errorMessage = { "Invalid Post.", "The post contained some empty boxes. :(" };
                Console.WriteLine(e.ToString());
                return View("~/Views/NullPost/Index", errorMessage);
            }
        }

        public IActionResult EditPost(String postid)
        {
            var postResult = _postDataAccess.GetPostById(Guid.Parse(postid));
            if (postResult == null)
            {
                string[] errorMessage = { "Invalid Post.", "We couldn't find the post. :(" };
                ViewBag.History = "~/Views/Home/ViewAll";
                return View("~/Views/NullPost/Index", errorMessage);
            }
            ViewBag.History = "~/Views/Home/ViewSinglePost?postid=" + postid;
            var postModelBuilder = new PostModelBuilder(postResult);
            return View("~/Views/Home/EditPost", postModelBuilder.build());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SearchResult(SearchCriteria searchCriteria)
        {
            ViewBag.History = "~/Views/Home/";
            if (String.IsNullOrEmpty(searchCriteria.SearchString))
            {
                return RedirectToAction("~/Views/Home/ViewAll");
            }
            List<PostModel> results = _postDataAccess.SearchBy((post) =>
                    {
                        return post.Title.IndexOf(searchCriteria.SearchString, StringComparison.OrdinalIgnoreCase) != -1 ||
                                post.Author.Name.IndexOf(searchCriteria.SearchString, StringComparison.OrdinalIgnoreCase) != -1 ||
                                post.Body.IndexOf(searchCriteria.SearchString, StringComparison.OrdinalIgnoreCase) != -1;
                    }
            ).ConvertAll<PostModel>((p) =>
            {
                var pmBuilder = new PostModelBuilder(p);
                return pmBuilder.build();
            });

            return View("~/Views/Home/ViewAll", results);
        }
        public IActionResult ViewSinglePost(String postid)
        {
            var postResult = _postDataAccess.GetPostById(Guid.Parse(postid));
            if (postResult == null)
            {
                ViewBag.History = "~/Views/Home/";
                return View("~/Views/NullPost/Index", "Post does not exist.");
            }

            ViewBag.History = Request.Headers["Referer"].ToString();
            var pmBuilder = new PostModelBuilder(postResult);
            return View("~/Views/Home/ViewSinglePost", pmBuilder.build());
        }

        public IActionResult ViewAll()
        {

            ViewBag.History = "~/Views/Home";
            List<PostModel> postResult = _postDataAccess.GetAllPosts().ConvertAll<PostModel>((p) =>
            {
                var pmBuilder = new PostModelBuilder(p);
                return pmBuilder.build();
            });
            if (postResult == null)
            {
                return View("~/Views/NullPost/Index", "There are no posts.");
            }
            return View(postResult);
        }

        public IActionResult ViewByAuthor(string author)
        {
            ViewBag.History = "~/Views/Home/Authors";
            List<PostModel> list = _postDataAccess.GetListOfPostsByAuthor(author).ConvertAll<PostModel>((p) =>
            {
                var pmBuilder = new PostModelBuilder(p);
                return pmBuilder.build();
            });
            return View("~/Views/Home/ViewAll", list);
        }
    }
}