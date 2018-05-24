using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogDB.Core;

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
            // add post to DB
            // pull post from DB
            // view new post object
            Post finishedPost = post;

            return View("ViewSinglePost", finishedPost);
        }

        public IActionResult AddPost()
        {
            return View();
        }

        public IActionResult EditPostResult(Post post)
        {
            logic.EditPost(post);

            return View("ViewSinglePost", post);
        }

        public IActionResult EditPost(String postid)
        {
            var post = logic.GetPostById(Guid.Parse(postid));
            return View(post);
        }

        public IActionResult DeletePostResult(Post post)
        {
            // delete post from DB
            logic.DeletePost(post);
            return View("Index");
        }

        public IActionResult DeletePost(String postid)
        {
            var post = logic.GetPostById(Guid.Parse(postid));
            return View(post);
        }

        public IActionResult ViewSinglePost(String postid)
        {
            var post = logic.GetPostById(Guid.Parse(postid));
            return View(post);
        }

        public IActionResult ViewAll()
        {
            var posts = logic.GetAllPosts();
            return View(posts);
        }

        public IActionResult ViewByAuthor(string author)
        {
            var list = database.GetAllPostsByAuthor(author);
            return View("ViewAll", list);
        }

        public IActionResult Authors()
        {

            return View(database.GetAllAuthors());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
