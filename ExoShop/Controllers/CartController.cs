using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Contexts;
using Interfaces.Logic;
using Logic.LogicObjects;
using Microsoft.AspNetCore.Mvc;
using Models.DataModels;

namespace ExoShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductLogic productLogic;
        private readonly ICartLogic cartLogic;

        public CartController(IProductContext productContext, ICartContext cartContext)
        {
            productLogic = new ProductLogic(productContext);
            cartLogic = new CartLogic(cartContext);
        }

        public IActionResult Index()
        {
            User loggedInUser = HttpContext.Session.GetObject<User>("loggedInUser");
            ViewBag.Products = cartLogic.GetCartByUser(loggedInUser);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int id)
        {
            User loggedInUser = HttpContext.Session.GetObject<User>("loggedInUser");
            cartLogic.AddToCart(productLogic.GetProductById(id),loggedInUser);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int id)
        {
            User loggedInUser = HttpContext.Session.GetObject<User>("loggedInUser");
            cartLogic.RemoveFromCart(productLogic.GetProductById(id), loggedInUser);
            return View();
        }
    }
}