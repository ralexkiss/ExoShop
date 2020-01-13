using Exceptions.User;
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
            return HttpContext.Session.GetString("name") != null ? View() : (IActionResult)RedirectToAction("SignIn", "User");
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
                    User local = UserLogic.AuthenticateUser(user.Email, user.Password);
                    HttpContext.Session.SetInt32("id", local.ID);
                    HttpContext.Session.SetString("name", local.Name);
                    HttpContext.Session.SetString("admin", local.IsAdmin.ToString().ToLower());
                    return RedirectToAction("Index", "Home");
                }
                catch (AuthenticationFailedException)
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
                    UserLogic.Insert(new User
                    {
                        Email = user.Email,
                        Name = user.Name,
                        Password = user.Password
                    });
                    HttpContext.Session.SetString("Email", user.Email);
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
            return RedirectToAction("Index", "Home");
        }
    }
}