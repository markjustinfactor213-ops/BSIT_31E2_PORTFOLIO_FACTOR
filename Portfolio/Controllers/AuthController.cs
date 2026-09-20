using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace PortfolioApp.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (model.Username == "admin" && model.Password == "Password123!")
            {
                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("Username", model.Username);

                return RedirectToAction("Index", "Projects");
            }

            ViewBag.Error = "Invalid username or password.";
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}