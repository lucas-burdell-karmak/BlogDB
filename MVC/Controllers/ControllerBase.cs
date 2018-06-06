using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using The_Intern_MVC.Models;

namespace The_Intern_MVC.Controllers
{
    public class ControllerBase : Controller
    {

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