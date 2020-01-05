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

        public ShopController(IProductContext context)
        {
            productLogic = new ProductLogic(context);
            products = productLogic.GetAll();
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
    }
}