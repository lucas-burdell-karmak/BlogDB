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

        [Authorize(Roles = "BlogAuthor")]
        public IActionResult Index()
        {
            return View();
        }

        public string GetCookie(string cookieID)
        {
            return Request.Cookies["cookieID"];
        }

        public void RemoveCookie(string cookieID)
        {
            Response.Cookies.Delete(cookieID);
        }

        public void SetCookie(string cookieID, string value, int expireTimeInMinutes)
        {
            CookieOptions cookieOption = new CookieOptions();
            if (expireTimeInMinutes > 0)
            {
                cookieOption.Expires = DateTime.Now.AddMinutes(expireTimeInMinutes);
            }
            else
            {
                cookieOption.Expires = DateTime.Now.AddMinutes(10);
            }
            Response.Cookies.Append(cookieID, value, cookieOption);
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
                Console.WriteLine(e.ToString());
                return View("NullPost", errorMessage);
            }
        }

        public IActionResult AddPost()
        {
            ViewBag.History = "/Home";
            return View();
        }

        public IActionResult Authors()
        {
            ViewBag.History = "/Home/";
            return View(_postDataAccess.GetListOfAuthors());
        }

        public IActionResult DeletePostResult(PostModel post)
        {
            try
            {
                PostModel postResult = _postDataAccess.DeletePost(post);
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
        public IActionResult EditPostResult(PostModel post)
        {
            try
            {
                ViewBag.History = "/Home/ViewAll";
                PostModel postResult = _postDataAccess.EditPost(post);
                return View("PostResult", postResult);
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


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel lvModel)
        {
            // TODO: Hash the password before init new Author
            Author user = new Author(lvModel.Username, 0);
            if (true/* TODO: Author is in DB && rememberMe is true*/)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.Name));
                string[] roles = { "BlogAuthor", "BlogReader" }; //look this up in the DB by UserID later

                foreach (string role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                SetCookie(new Guid().ToString(), lvModel.Username, 30);
                return View("Index");
            }
            else
            {
                //user.Roles = "InvalidUser";
                ViewData["message"] = "Invalid login credentials!";
                return View("Login");
            }
        }

        public IActionResult NullPost(string message)
        {
            ViewBag.History = "/Home";
            return View(message);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel rvModel)
        {

            if (rvModel.Password.CompareTo(rvModel.ConfirmPassword) == 0)
            {
                if (true/* TODO: username is available*/)
                {
                    // register a new user in the database
                    SetCookie(new Guid().ToString(), rvModel.Username, 30);
                    return View("Index");
                }
                else
                {
                    ViewData["message"] = "Username is not available!";
                    return View("Register");
                }
            }
            else
            {
                ViewData["message"] = "Passwords do not match!";
                return View("Register");
            }
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
            ).ConvertAll<PostModel>((p) => (PostModel)p);

            return View("ViewAll", results);
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
            List<PostModel> postResult = _postDataAccess.GetAllPosts().ConvertAll<PostModel>((p) => (PostModel)p);
            if (postResult == null)
            {
                return View("NullPost", "There are no posts.");
            }
            return View(postResult);
        }

        public IActionResult ViewByAuthor(string author)
        {
            ViewBag.History = "/Home/Authors";
            List<PostModel> list = _postDataAccess.GetListOfPostsByAuthor(author).ConvertAll<PostModel>((p) => (PostModel)p);
            return View("ViewAll", list);
        }
    }
}