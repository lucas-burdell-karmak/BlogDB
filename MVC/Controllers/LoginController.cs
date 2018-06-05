using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using BlogDB.Core;

using The_Intern_MVC.Models;

namespace The_Intern_MVC.Controllers
{
    public class LoginController : ControllerBase
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel lvModel)
        {
            // TODO: Hash the password before init new Author
            Author user = new Author(lvModel.Username, 0);
            if (true/* TODO: Author is in DB && rememberMe is true*/)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.Name));
                claims.Add(new Claim("AuthorID", user.ID.ToString()));

                string[] roles = { "BlogWriter", "BlogReader" }; //look this up in the DB by UserID later

                foreach (string role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties()
                );

                SetCookie("BlogAuth", JsonConvert.SerializeObject(roles), 30);
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                //user.Roles = "InvalidUser";
                ViewData["message"] = "Invalid login credentials!";
                return View("~/Views/Login/Index");
            }
        }
    }
}