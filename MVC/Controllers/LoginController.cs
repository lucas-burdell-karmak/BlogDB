using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogDB.Core;
using System.Web;
using The_Intern_MVC.Models;

namespace The_Intern_MVC.Controllers
{
    public class LoginController : Controller
    {


        public readonly IAuthorizor _authorizor;

        public LoginController(IAuthorizor authorizor)
        {
            _authorizor = authorizor;
        }

        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        public IActionResult LoginConfirm(UserLogin userLogin) {
            
            

            // hashing
            // validation
            // sql check (does it exist)
            // return to home as "logged in"
            return View("Index");
        }

    }
}