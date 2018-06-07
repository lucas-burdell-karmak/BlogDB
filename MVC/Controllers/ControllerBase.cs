using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using The_Intern_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics;
using System.Linq;

namespace The_Intern_MVC.Controllers
{
    public class ControllerBase : Controller
    {

        protected int GetUserID()
        {
            var claims = HttpContext.User.Claims;
            var userID = -1;
            Int32.TryParse(claims.Where(c => c.Type == "AuthorID")
                                 .Select(c => c.Value)
                                 .SingleOrDefault(), out userID);
            return userID;
        }

        public IActionResult ShowError(ErrorPageModel message)
        {
            if (message == null)
            {
                message = new ErrorPageModel();
            }
            ViewBag.History = "/Home/Index";
            return View("~/Views/Error/Index.cshtml", message);
        }
    }
}