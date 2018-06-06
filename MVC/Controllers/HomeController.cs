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
        }

        [Authorize(Roles = "BlogReader")]
        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult AddPostResult(PostModel post)
        {
            try
            {
                var claims = HttpContext.User.Claims;
                post.AuthorName = claims.Where(c => c.Type == ClaimTypes.Name)
                                        .Select(c => c.Value)
                                        .SingleOrDefault();
                var authorID = -1;
                Int32.TryParse(claims.Where(c => c.Type == "AuthorID")
                                     .Select(c => c.Value)
                                     .SingleOrDefault(), out authorID);
                post.AuthorID = authorID;
                var postBuilder = new PostBuilder(post);
                var postToAdd = postBuilder.build();
                var postResult = _postDataAccess.AddPost(postToAdd);
                ViewBag.History = "/Home";

                var pmBuilder = new PostModelBuilder(postResult);
                return View("ViewSinglePost", pmBuilder.build());
            }
            catch (ArgumentException e)
            {
                var errorMessage = new ErrorPageModel("Cannot add post.", "The post had empty properties.");
                Console.WriteLine(e.ToString());
                return RedirectToAction("Index", "NullPost", errorMessage);
            }
        }

        public IActionResult AddPost()
        {
            try
            {
                ViewBag.History = "/Home";
                var claims = HttpContext.User.Claims;
                var authorName = claims.Where(c => c.Type == ClaimTypes.Name)
                                       .Select(c => c.Value)
                                       .SingleOrDefault();
                var authorID = -1;
                Int32.TryParse(claims.Where(c => c.Type == "AuthorID")
                                     .Select(c => c.Value)
                                     .SingleOrDefault(), out authorID);
                var author = new Author(authorName, authorID);
                var post = new Post("", author, "");
                var pmBuilder = new PostModelBuilder(post);
                return View("AddPost", pmBuilder.build());
            }
            catch (Exception e)
            {
                var errorMessage = new ErrorPageModel("Eror adding post.", "We couldn't add the post.");
                ViewBag.History = "/Home";
                Console.WriteLine(e.ToString());
                return RedirectToAction("Index", "NullPost", errorMessage);
            }
        }

        public IActionResult Authors()
        {
            ViewBag.History = "/Home";
            var listOfAuthors = _postDataAccess.GetAllAuthors();
            return View(listOfAuthors);
        }

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
                return RedirectToAction("Index", "NullPost", errorMessage);
            }
        }
        public IActionResult EditPostResult(PostModel post)
        {
            try
            {
                ViewBag.History = "/Home/ViewAll";
                if (post.AuthorName != HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Name)
                                                              .Select(c => c.Value)
                                                              .SingleOrDefault())
                {
                    throw new ArgumentException();
                }
                var postBuilder = new PostBuilder(post);
                var postResult = _postDataAccess.EditPost(postBuilder.build());
                var pmBuilder = new PostModelBuilder(postResult);
                return View("Home/PostResult", pmBuilder.build());
            }
            catch (ArgumentException e)
            {
                var errorMessage = new ErrorPageModel("Invalid Post.", "The post contained invalid input.");
                Console.WriteLine(e.ToString());
                return RedirectToAction("Index", "NullPost", errorMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return View("Home/");
            }
        }

        public IActionResult EditPost(String postid)
        {
            var postResult = _postDataAccess.GetPostById(Guid.Parse(postid));
            if (postResult == null)
            {
                string[] errorMessage = { "Invalid Post.", "We couldn't find the post. :(" };
                ViewBag.History = "/Home/ViewAll";
                return RedirectToAction("Index", "NullPost", errorMessage);
            }
            ViewBag.History = "/Home/ViewSinglePost?postid=" + postid;
            var postModelBuilder = new PostModelBuilder(postResult);
            return View("EditPost", postModelBuilder.build());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
                var pmBuilder = new PostModelBuilder(p);
                return pmBuilder.build();
            });

            return View("ViewAll", results);
        }
        public IActionResult ViewSinglePost(String postid)
        {
            var postResult = _postDataAccess.GetPostById(Guid.Parse(postid));
            if (postResult == null)
            {
                ViewBag.History = "/Home";
                return RedirectToAction("Index", "NullPost", new ErrorPageModel("Post Does Not Exist", "This post does not exist."));
            }

            ViewBag.History = Request.Headers["Referer"].ToString();
            var pmBuilder = new PostModelBuilder(postResult);
            return View("ViewSinglePost", pmBuilder.build());
        }

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
                return RedirectToAction("Index", "NullPost", new ErrorPageModel("No Posts", "There are no posts."));
            }
            return View(postResult);
        }

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