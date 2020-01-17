using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exceptions.Shop;
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
        private readonly IOrderLogic orderLogic;
        private readonly IBillingLogic billingLogic;

        public ShopController(IProductContext productContext, IReviewContext reviewContext, IOrderContext orderContext, IBillingContext billingContext)
        {
            productLogic = new ProductLogic(productContext);
            reviewLogic = new ReviewLogic(reviewContext);
            orderLogic = new OrderLogic(orderContext);
            billingLogic = new BillingLogic(billingContext);

            products = productLogic.GetAll();
            foreach(Product product in products)
            {
                product.reviews = reviewLogic.GetAllByProduct(product);
            }
        }

        public IActionResult Index()
        {
            User loggedInUser = HttpContext.Session.GetUser();
            ViewBag.Products = products;
            return String.IsNullOrEmpty(loggedInUser.Name) ? (IActionResult)RedirectToAction("SignIn", "User") : View();
        }

        public IActionResult Checkout()
        {
            User loggedInUser = HttpContext.Session.GetUser();
            if (loggedInUser.Cart.Count >= 1)
            {
                return View();
            }
            return RedirectToAction("Index", "Shop");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Payment(CheckoutViewModel billingViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Billing billing = CreateBilling(billingViewModel);
                    User loggedInUser = HttpContext.Session.GetUser();
                    CreateOrder(billing, loggedInUser);

                    ViewBag.Billing = billing;
                    ViewBag.Products = loggedInUser.Cart;
                    ViewBag.TotalPrice = loggedInUser.Cart.Sum(product => product.Price);
                    return View();
                }
                catch(Exception)
                {
                    throw new PaymentFailedException();
                }
            }
            return RedirectToAction("Checkout", "Shop");   
        }

        private void CreateOrder(Billing billing, User loggedInUser)
        {
            Order order = new Order
            {
                User = loggedInUser,
                Billing = billing,
                Products = loggedInUser.Cart,
                Status = "Complete",
                Date = DateTime.Now
            };
            orderLogic.AddOrder(order);
        }

        private Billing CreateBilling(CheckoutViewModel billingViewModel)
        {
            Billing billing = new Billing
            {
                FirstName = billingViewModel.FirstName,
                LastName = billingViewModel.LastName,
                PhoneNumber = billingViewModel.PhoneNumber,
                Address = billingViewModel.Address,
                City = billingViewModel.City
            };
            billingLogic.AddBilling(billing);
            return billing;
        }
    }
}