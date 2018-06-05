using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BlogDB.Core;
using The_Intern_MVC.Models;

namespace The_Intern_MVC.Controllers
{
    public class RegisterController : ControllerBase
    {
        private readonly IAuthorRepo _authorRepo;

        public RegisterController(IAuthorRepo authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(RegisterViewModel rvModel)
        {
            Author author = _authorRepo.GetAuthorByName(rvModel.Username);
            bool authorDoesNotAlreadyExist = (author == null);
            bool passwordsAreSame = rvModel.Password.CompareTo(rvModel.ConfirmPassword) == 0;

            if (passwordsAreSame && authorDoesNotAlreadyExist)
            {
                // register a new user in the database
                var isSuccessful = _authorRepo.TryRegisterAuthor(rvModel.Username, rvModel.Password, out var Author);

                if (isSuccessful)
                {
                    return RedirectToAction("Index", "Login");
                }
                else 
                {
                    ViewData["message"] = "Something went horribly wrong when you tried to register...:(";
                }
            }
            else if (!passwordsAreSame)
            {
                ViewData["message"] = "Passwords do not match!";

            }
            else if (!authorDoesNotAlreadyExist)
            {
                ViewData["message"] = "Username is not available!";
            }
            return View("~/Views/Register/Index");
        }
    }
}