using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BlogDB.Core;
using System.Linq;

namespace The_Intern_MVC.Controllers
{
    public class LoginController : ControllerBase
    {

        private readonly IAuthorRepo _authorRepo;

        public LoginController(IAuthorRepo authorRepo) => _authorRepo = authorRepo;

        public IActionResult Index() => View();


        [HttpPost]
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Index(string username, string passwordHash)
        {
            Author author = _authorRepo.GetAuthorByName(username);
            if (author != null)
            {
                _authorRepo.TryValidateAuthorLogin(username, passwordHash, out var isSuccessful);

                if (isSuccessful)
                {
                    AddClaims(author);
                    return RedirectToAction("Index", "Home");
                }
                TempData["message"] = "Login failed! Did you type the right password?";
                return View("Index");
            }
            TempData["message"] = "Username does not exist. Click the Register button to claim it as your own!";
            return View("Index");
        }

        private void AddClaims(Author user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("AuthorID", user.ID.ToString())
            };

            claims.AddRange(
                user.Roles.Select(
                    x => new Claim(ClaimTypes.Role, x)
                )
            );

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)),
                new AuthenticationProperties()
            );
        }
    }
}