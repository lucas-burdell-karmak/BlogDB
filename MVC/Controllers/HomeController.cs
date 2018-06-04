using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogDB.Core;
using System.Web;
using The_Intern_MVC.Models;
using The_Intern_MVC.Builders;

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

        public IActionResult AddPostResult(PostModel postModel)
        {
            try
            {
                var builder = new PostBuilder(postModel);
                var postResult = _postDataAccess.AddPost(builder.build());
                var modelBuilder = new PostModelBuilder(postResult);

                ViewBag.History = "/Home";

                return View("ViewSinglePost", modelBuilder.build());
            }
            catch (ArgumentException e)
            {
                // TODO exception logging

                string[] errorMessage = { "Cannot add post.", "The post had empty properties. :(" };
                Console.WriteLine(e.ToString());
                return View("NullPost", errorMessage);
            }
        }

        public IActionResult AddPost()
        {
            ViewBag.History = "/Home";
            return View();
        }

        public IActionResult EditPostResult(PostModel post)
        {
            try
            {
                var postBuilder = new PostBuilder(post);
                ViewBag.History = "/Home/ViewAll";
                var postResult = _postDataAccess.EditPost(postBuilder.build());
                var postModelBuilder = new PostModelBuilder(postResult);
                return View("PostResult", postModelBuilder.build());
            }
            catch (ArgumentException e)
            {
                string[] errorMessage = { "Invalid Post.", "The post contained some empty boxes. :(" };
                Console.WriteLine(e.ToString());
                return View("NullPost", errorMessage);
            }
        }

        public IActionResult EditPost(String postid)
        {
            var postResult = _postDataAccess.GetPostById(Guid.Parse(postid));
            if (postResult == null)
            {
                string[] errorMessage = { "Invalid Post.", "We couldn't find the post. :(" };
                ViewBag.History = "/Home/ViewAll";
                return View("NullPost", errorMessage);
            }
            ViewBag.History = "/Home/ViewSinglePost?postid=" + postid;
            var modelBuilder = new PostModelBuilder(postResult);
            return View(modelBuilder.build());
        }

        public IActionResult DeletePostResult(PostModel postModel)
        {
            try
            {
                var postBuilder = new PostBuilder(postModel);
                var postResult = _postDataAccess.DeletePost(postBuilder.build());
                ViewBag.History = "/Home";
                return RedirectToAction("ViewAll");
            }
            catch (ArgumentException e)
            {
                string[] errorMessage = { "Invalid Post.", "We couldn't find the post. :(" };
                ViewBag.History = "/Home/ViewAll";
                Console.WriteLine(e.ToString());
                return View("NullPost", errorMessage);
            }
        }


        public IActionResult NullPost(string message)
        {
            ViewBag.History = "/Home";
            return View(message);
        }

        public IActionResult ViewSinglePost(String postid)
        {
            var postResult = _postDataAccess.GetPostById(Guid.Parse(postid));
            if (postResult == null)
            {
                ViewBag.History = "/Home/";
                return View("NullPost", "Post does not exist.");
            }
            ViewBag.History = Request.Headers["Referer"].ToString();
            var modelBuilder = new PostModelBuilder(postResult);
            return View(modelBuilder.build());
        }

        public IActionResult ViewAll()
        {

            ViewBag.History = "/Home";
            List<PostModel> postResult = _postDataAccess.GetAllPosts().ConvertAll<PostModel>((p) =>
            {
                var modelBuilder = new PostModelBuilder(p);
                return modelBuilder.build();
            });
            if (postResult == null)
            {
                return View("NullPost", "There are no posts.");
            }
            return View(postResult);
        }

        public IActionResult ViewByAuthor(string author)
        {
            ViewBag.History = "/Home/Authors";
            List<PostModel> list = _postDataAccess.GetListOfPostsByAuthor(author).ConvertAll<PostModel>((p) =>
            {
                var modelBuilder = new PostModelBuilder(p);
                return modelBuilder.build();
            });
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
                                post.Author.Name.IndexOf(searchCriteria.SearchString, StringComparison.OrdinalIgnoreCase) != -1 ||
                                post.Body.IndexOf(searchCriteria.SearchString, StringComparison.OrdinalIgnoreCase) != -1;
                    }
            ).ConvertAll<PostModel>((p) =>
            {
                var modelBuilder = new PostModelBuilder(p);
                return modelBuilder.build();
            });

            return View("ViewAll", results);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}