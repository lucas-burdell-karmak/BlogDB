using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace The_Intern_MVC.Controllers
{
    public class ControllerBase : Controller
    {
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

    }
}