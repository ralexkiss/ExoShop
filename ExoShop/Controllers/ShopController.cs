using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExoShop;
using ExoShop.Models;
using Interfaces.Contexts;
using Interfaces.Logic;
using Logic.LogicObjects;
using Microsoft.AspNetCore.Mvc;
using Models.DataModels;

namespace ExoShop.Controllers
{
    public class ShopController : Controller
    {
        List<Product> products = new List<Product>();

        private readonly IProductLogic productLogic;
        private readonly IReviewLogic reviewLogic;

        public ShopController(IProductContext productContext, IReviewContext reviewContext)
        {
            productLogic = new ProductLogic(productContext);
            reviewLogic = new ReviewLogic(reviewContext);
            products = productLogic.GetAll();
            foreach(Product product in products)
            {
                product.reviews = reviewLogic.GetAllByProduct(product);
            }
        }


        public IActionResult Index()
        {
            ViewBag.Products = products;
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Payment(CheckoutViewModel billingViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User loggedInUser = HttpContext.Session.GetObject<User>("loggedInUser");
                    Billing billing = new Billing
                    {
                        FirstName = billingViewModel.FirstName,
                        LastName = billingViewModel.LastName,
                        PhoneNumber = billingViewModel.PhoneNumber,
                        Address = billingViewModel.Address,
                        City = billingViewModel.City
                    };
                    ViewBag.Products = loggedInUser.Cart;
                    ViewBag.Billing = billing;
                    ViewBag.TotalPrice = loggedInUser.Cart.Sum(product => product.Price);
                    return View();
                } 
                catch
                {

                }
            }
            return RedirectToAction("Checkout", "Shop");   
        }
    }
}