using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Contexts;
using Interfaces.Logic;
using Logic.LogicObjects;
using Microsoft.AspNetCore.Mvc;
using Models.DataModels;

namespace GeoChatting.Controllers
{
    public class ShopController : Controller
    {
        List<Product> products = new List<Product>();

        private readonly IProductLogic productLogic;
        private readonly IReviewLogic reviewLogic;
        private readonly ICartLogic cartLogic;

        public ShopController(IProductContext productContext, ICartContext cartContext, IReviewContext reviewContext)
        {
            productLogic = new ProductLogic(productContext);
            cartLogic = new CartLogic(cartContext);
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

        public IActionResult Payment()
        {
            return View();
        }
    }
}