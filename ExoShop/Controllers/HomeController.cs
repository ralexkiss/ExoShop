using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExoShop.Models;
using Models.DataModels;
using ExoShop;

namespace ExoShop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!HttpContext.Session.ContainsObject("loggedInUser"))
            {
                User tempUser = new User
                {
                    Name = "",
                    IsAdmin = false,
                    WishList = new List<Product>(),
                    Cart = new List<Product>()
                };
                HttpContext.Session.SetObject("loggedInUser", tempUser);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Tos()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
