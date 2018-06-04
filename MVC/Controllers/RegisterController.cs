using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using The_Intern_MVC.Models;

namespace The_Intern_MVC.Controllers
{
    public class RegisterController : ControllerBase
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(RegisterViewModel rvModel)
        {

            if (rvModel.Password.CompareTo(rvModel.ConfirmPassword) == 0)
            {
                if (true/* TODO: username is available*/)
                {
                    // register a new user in the database
                    SetCookie(new Guid().ToString(), rvModel.Username, 30);
                    return View("~/Views/Home/Index");
                }
                else
                {
                    ViewData["message"] = "Username is not available!";
                    return View("~/Views/Register/Index");
                }
            }
            else
            {
                ViewData["message"] = "Passwords do not match!";
                return View("~/Views/Register/Index");
            }
        }
    }
}