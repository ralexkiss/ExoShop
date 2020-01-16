using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExoShop.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddToCart()
        {
            return View();
        }
        public IActionResult RemoveFromCart()
        {
            return View();
        }
    }
}