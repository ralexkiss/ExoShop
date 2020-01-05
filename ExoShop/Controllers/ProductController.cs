using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Interfaces.Contexts;
using Interfaces.Logic;
using Logic.LogicObjects;
using ExoShop.Models;
using Models.DataModels;

namespace ExoShop.Controllers
{
    public class ProductController : Controller
    {
        List<Product> list = new List<Product>();
        private readonly IProductLogic productLogic;

        public ProductController(IProductContext context)
        {
            productLogic = new ProductLogic(context);
            list = productLogic.GetAll();
        }

        public IActionResult Index()
        {
            ViewBag.Products = list;
            return View();
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        public ActionResult EditProduct(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(ProductViewModel model)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int id)
        {   
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    productLogic.Insert(new Product
                    {
                        Name = model.Name,
                        Description = model.Description,
                        ImageURL = model.ImageURL,
                        Price = model.Price,

                    });
                    return RedirectToAction("Index", "Product");
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return View();
        }
    }
}