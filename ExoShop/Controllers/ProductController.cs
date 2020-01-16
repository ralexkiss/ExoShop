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

        public IActionResult Index(int id)
        {
            ViewBag.Products = products;
            return View();
        }

        public IActionResult ProductPanel()
        {
            User loggedInUser = HttpContext.Session.GetObject<User>("loggedInUser");
            if (loggedInUser.IsAdmin)
            {
                ViewBag.Products = products;
                return View();
            }
            return RedirectToAction("Index", "Shop");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(int id, ProductViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    productLogic.EditProduct(new Product
                    {
                        ID = id,
                        Name = model.Name,
                        Description = model.Description,
                        ImageURL = model.ImageURL,
                        Price = model.Price,

                    });
                    return RedirectToAction("ProductPanel", "Product");
                }
                catch(Exception)
                {
                    return RedirectToAction("ProductPanel", "Product");
                }
            }
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
                return RedirectToAction("ProductPanel", "Product");
            }
            catch (Exception)
            {
                return RedirectToAction("ProductPanel", "Product");
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