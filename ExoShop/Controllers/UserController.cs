using Exceptions.User;
using ExoShop;
using GeoChatting.Models;
using Interfaces.Contexts;
using Interfaces.Logic;
using Logic.LogicObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DataModels;
using System;

namespace GeoChatting.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserLogic UserLogic;

        public UserController(IUserContext context)
        {
            UserLogic = new UserLogic(context);
        }

        public IActionResult Index()
        {
            User loggedInUser = HttpContext.Session.GetObject<User>("loggedInUser");
            return String.IsNullOrEmpty(loggedInUser.Name) ? (IActionResult)RedirectToAction("SignIn", "User") : View();
        }

        public ActionResult SignIn()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User local = UserLogic.Login(user.Email, user.Password);
                    HttpContext.Session.SetInt32("id", local.ID);
                    HttpContext.Session.SetString("name", local.Name);
                    HttpContext.Session.SetString("admin", local.IsAdmin.ToString().ToLower());
                    HttpContext.Session.SetObject("loggedInUser", local);
                    return RedirectToAction("Index", "Home");
                }
                catch (AddingProductFailedException)
                {
                    ModelState.AddModelError("", "The email or password provided is incorrect.");
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserLogic.Register(new User
                    {
                        Email = user.Email,
                        Name = user.Name,
                        Password = user.Password
                    });
                    return RedirectToAction("SignIn", "User");
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult SignOut()
        {
            HttpContext.Session.SetInt32("id", 0);
            HttpContext.Session.SetString("name", string.Empty);
            HttpContext.Session.SetString("admin", "false");
            HttpContext.Session.SetObject("loggedInUser", null);
            return RedirectToAction("Index", "Home");
        }
    }
}