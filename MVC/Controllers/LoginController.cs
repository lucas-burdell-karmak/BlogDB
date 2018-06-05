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

        private readonly IAuthorRepo _authorRepo;

        public LoginController(IAuthorRepo authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string passwordHash)
        {
            Author author = _authorRepo.GetAuthorByName(username);
            bool authorExists = (author != null);

            if (authorExists)
            {
                _authorRepo.TryValidateAuthor(username, passwordHash, out var isSuccessful);

                if (isSuccessful)
                {
                    AddClaims(author);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["message"] = "Login failed! Did you type the right password?";
                    return View("Index");
                }
            }
            else
            {
                TempData["message"] = "Username does not exist. Click the Register button to claim it as your own!";
                return View("Index");
            }
        }

        private void AddClaims(Author user)
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
        }
    }
}