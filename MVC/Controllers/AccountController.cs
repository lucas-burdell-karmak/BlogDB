using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace The_Intern_MVC.Controllers
{
    public class AccountController : ControllerBase
    {

        [AllowAnonymous]
        [HttpGet]
        public IActionResult AccessDenied() => View();

        [Authorize]
        [HttpGet]
        public IActionResult Index() => View();

        [Authorize]
        [HttpPost]
        public IActionResult DeleteProfile() => View();
    }
}
