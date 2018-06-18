using Microsoft.AspNetCore.Mvc;
using BlogDB.Core;

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
        public IActionResult Index(string username, string passwordHash)
        {
            if (_authorRepo.GetAuthorByName(username) == null)
            {
                _authorRepo.TryRegisterAuthor(username, passwordHash, out bool isSuccessful);

                if (isSuccessful)
                {
                    return RedirectToAction("Index", "Login");
                }
                TempData["message"] = "Something went horribly wrong when you tried to register... :(";
            }
            else
            {
                TempData["message"] = "Username is not available!";
            }
            return View("Index");
        }
    }
}
