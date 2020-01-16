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
        List<Product> products = new List<Product>();
        private readonly IProductLogic productLogic;

        public ProductController(IProductContext context)
        {
            productLogic = new ProductLogic(context);
            products = productLogic.GetAll();
        }

        public IActionResult Index()
        {
            ViewBag.Products = products;
            return View();
        }

        public ActionResult EditProduct(int id)
        {
            ViewBag.Product = products.Find(product => product.ID == id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(int id, ProductViewModel model)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                Product product = new Product
                {
                    ID = id
                };
                productLogic.RemoveProduct(product);
                return RedirectToAction("Index", "Product");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Product");
            }
        }

        public ActionResult AddProduct()
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
                    productLogic.AddProduct(new Product
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