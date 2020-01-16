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
    public class WishController : Controller
    {
        private readonly IProductLogic productLogic;
        private readonly IWishLogic wishLogic;

        public WishController(IProductContext productContext, IWishContext wishContext)
        {
            productLogic = new ProductLogic(productContext);
            wishLogic = new WishLogic(wishContext);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToWishes(int id)
        {
            User loggedInUser = HttpContext.Session.GetObject<User>("loggedInUser");
            wishLogic.AddToWishList(productLogic.GetProductById(id),loggedInUser);
            HttpContext.Session.SetObject("loggedInUser", loggedInUser);
            return RedirectToAction("Index", "Shop");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromWishes(int id)
        {
            User loggedInUser = HttpContext.Session.GetObject<User>("loggedInUser");
            wishLogic.RemoveFromWishList(productLogic.GetProductById(id), loggedInUser);    
            HttpContext.Session.SetObject("loggedInUser", loggedInUser);
            return RedirectToAction("Index", "User");
        }
    }
}