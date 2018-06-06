using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BlogDB.Core;
using The_Intern_MVC.Builders;
using The_Intern_MVC.Models;
using Newtonsoft.Json;

namespace The_Intern_MVC.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly IPostDataAccess _postDataAccess;

        public HomeController(IPostDataAccess postdataccess)
        {
            this._postDataAccess = postdataccess;
            /*
            string roleJson = GetCookie("BlogAuth");
            string[] roles = (string[]) JsonConvert.DeserializeObject(roleJson);
            */

            //add roles from cookie to context.user.request.roles 
        }

        [Authorize(Policy = "BlogReader")]
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }

        [Authorize(Policy = "BlogWriter")]
        [HttpPost]
        public IActionResult AddPostResult(PostModel post)
        {
            try
            {
                var claims = HttpContext.User.Claims;
                post.AuthorName = claims.Where(c => c.Type == ClaimTypes.Name)
                        .Select(c => c.Value).SingleOrDefault();
                var authorId = -1;
                Int32.TryParse(claims.Where(c => c.Type == "AuthorID")
                        .Select(c => c.Value).SingleOrDefault(), out authorId);
                post.AuthorID = authorId;
                var postBuilder = new PostBuilder(post);
                var postToAdd = postBuilder.build();
                var postResult = _postDataAccess.AddPost(postToAdd);
                ViewBag.History = "/Home";

                var pmBuilder = new PostModelBuilder(postResult);
                return View("ViewSinglePost", pmBuilder.build());
            }
            catch (ArgumentException e)
            {
                // TODO exception logging

                var errorMessage = new ErrorPageModel("Cannot add post.", "The post had empty properties.");
                Console.WriteLine(e.ToString());
                return ShowError(errorMessage);
            }
        }

        [Authorize(Policy = "BlogWriter")]
        [HttpGet]
        public IActionResult AddPost()
        {
            ViewBag.History = "/Home";
            return View();
        }

        [Authorize(Policy = "BlogReader")]
        [HttpGet]
        public IActionResult Authors()
        {
            ViewBag.History = "/Home";
            var listOfAuthors = _postDataAccess.GetAllAuthors();
            return View(listOfAuthors);
        }

        [Authorize(Policy = "BlogDeleter")]
        [HttpPost]
        public IActionResult DeletePostResult(PostModel post)
        {
            try
            {
                var postBuilder = new PostBuilder(post);
                var postResult = _postDataAccess.DeletePost(postBuilder.build());
                ViewBag.History = "/Home";
                return RedirectToAction("ViewAll");
            }
            catch (ArgumentException e)
            {
                var errorMessage = new ErrorPageModel("Invalid Post.", "We couldn't find the post.");
                ViewBag.History = "/Home/ViewAll";
                Console.WriteLine(e.ToString());
                return ShowError(errorMessage);
            }
        }

        [Authorize(Policy = "BlogEditor")]
        [HttpPost]
        public IActionResult EditPostResult(PostModel post)
        {
            try
            {
                ViewBag.History = "/Home/ViewAll";
                var postBuilder = new PostBuilder(post);
                var postResult = _postDataAccess.EditPost(postBuilder.build());
                var pmBuilder = new PostModelBuilder(postResult);
                return View("Home/PostResult", pmBuilder.build());
            }
            catch (ArgumentException e)
            {
                ErrorPageModel errorMessage = new ErrorPageModel("Invalid Post.", "The post contained invalid input.");
                Console.WriteLine(e.ToString());
                return ShowError(errorMessage);
            }
        }

        [Authorize(Policy = "BlogEditor")]
        [HttpGet]
        public IActionResult EditPost(String postid)
        {
            var postResult = _postDataAccess.GetPostById(Guid.Parse(postid));
            if (postResult == null)
            {
                var errorMessage = new ErrorPageModel("Invalid Post.", "We couldn't find the post. :(" );
                ViewBag.History = "/Home/ViewAll";
                return ShowError(errorMessage);
            }
            ViewBag.History = "/Home/ViewSinglePost?postid=" + postid;
            var postModelBuilder = new PostModelBuilder(postResult);
            var postToEdit = postModelBuilder.build();
            return View("EditPost", postToEdit);
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Authorize(Policy = "BlogReader")]
        [HttpPost]
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
                var pmBuilder = new PostModelBuilder(p);
                return pmBuilder.build();
            });

            return View("ViewAll", results);
        }

        [Authorize(Policy = "BlogReader")]
        [HttpGet]
        public IActionResult ViewSinglePost(String postid)
        {
            var postResult = _postDataAccess.GetPostById(Guid.Parse(postid));
            if (postResult == null)
            {
                ViewBag.History = "/Home";
                var errorMessage = new ErrorPageModel("Post Does Not Exist", "This post does not exist.");
                return ShowError(errorMessage);
            }

            ViewBag.History = Request.Headers["Referer"].ToString();
            var pmBuilder = new PostModelBuilder(postResult);
            return View("ViewSinglePost", pmBuilder.build());
        }


        [Authorize(Policy = "BlogReader")]
        [HttpGet]
        public IActionResult ViewAll()
        {

            ViewBag.History = "/Home";
            List<PostModel> postResult = _postDataAccess.GetAllPosts().ConvertAll<PostModel>((p) =>
            {
                var pmBuilder = new PostModelBuilder(p);
                return pmBuilder.build();
            });
            if (postResult == null)
            {
                var errorMessage = new ErrorPageModel("No Posts", "There are no posts.");
                return ShowError(errorMessage);
            }
            return View(postResult);
        }


        [Authorize(Policy = "BlogReader")]
        [HttpGet]
        public IActionResult ViewByAuthor(int authorID)
        {
            ViewBag.History = "/Home/Authors";
            var listOfPostsByAuthor = _postDataAccess.GetListOfPostsByAuthorID(authorID);
            var listOfPostModels = new List<PostModel>();

            foreach (Post p in listOfPostsByAuthor)
            {
                var pmBuilder = new PostModelBuilder(p);
                listOfPostModels.Add(pmBuilder.build());
            }

            return View("ViewAll", listOfPostModels);
        }
    }
}