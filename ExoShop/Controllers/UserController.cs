using Exceptions.User;
using ExoShop;
using ExoShop.Models;
using Interfaces.Contexts;
using Interfaces.Logic;
using Logic.LogicObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DataModels;
using System;

namespace ExoShop.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserLogic userLogic;
        private readonly IWishLogic wishLogic;
        private readonly IOrderLogic orderLogic;

        public UserController(IUserContext userContext, IWishContext wishContext, IOrderContext orderContext)
        {
            userLogic = new UserLogic(userContext);
            wishLogic = new WishLogic(wishContext);
            orderLogic = new OrderLogic(orderContext);
        }

        public IActionResult Index()
        {
            User loggedInUser = HttpContext.Session.GetObject<User>("loggedInUser");
            ViewBag.User = loggedInUser;
            ViewBag.Orders = orderLogic.GetAllOrdersByUser(loggedInUser);
            ViewBag.Wishes = wishLogic.GetWishesByUser(loggedInUser);
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
                    User loggedInUser = userLogic.Login(user.Email, user.Password);
                    loggedInUser.WishList = wishLogic.GetWishesByUser(loggedInUser);
                    HttpContext.Session.DeleteObject("loggedInUser");
                    HttpContext.Session.SetInt32("id", loggedInUser.ID);
                    HttpContext.Session.SetString("name", loggedInUser.Name);
                    HttpContext.Session.SetString("admin", loggedInUser.IsAdmin.ToString().ToLower());
                    HttpContext.Session.SetObject("loggedInUser", loggedInUser);
                    return RedirectToAction("Index", "User");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "The email or password provided is incorrect.");
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateUser(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User loggedInUser = HttpContext.Session.GetObject<User>("loggedInUser");
                    loggedInUser.Email = user.Email;
                    loggedInUser.Name = user.Name;
                    loggedInUser.Password = user.Password;
                    userLogic.EditUser(loggedInUser);
                    HttpContext.Session.SetInt32("id", 0);
                    HttpContext.Session.SetString("name", string.Empty);
                    HttpContext.Session.SetString("admin", "false");
                    HttpContext.Session.DeleteObject("loggedInUser");
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "User");
                }
            }
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userLogic.Register(new User
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
            HttpContext.Session.DeleteObject("loggedInUser");
            return RedirectToAction("Index", "Home");
        }
    }
}