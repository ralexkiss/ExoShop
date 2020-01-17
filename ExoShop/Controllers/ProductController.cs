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
using Exceptions.Product;

namespace ExoShop.Controllers
{
    public class ProductController : Controller
    {
        List<Product> products = new List<Product>();
        private readonly IProductLogic productLogic;
        private readonly IReviewLogic reviewLogic;

        public ProductController(IProductContext productContext, IReviewContext reviewContext)
        {
            productLogic = new ProductLogic(productContext);
            reviewLogic = new ReviewLogic(reviewContext);

            products = productLogic.GetAll();
        }

        public IActionResult Info(int id)
        {
            Product product = products.Find(foundProduct => foundProduct.ID == id);
            if (product == null)
            {
                return RedirectToAction("Index", "Shop");
            }
            ViewBag.Reviews = reviewLogic.GetAllByProduct(product);
            ViewBag.Product = product;
            return View();
        }

        public IActionResult ProductPanel()
        {
            User loggedInUser = HttpContext.Session.GetUser();
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
            User loggedInUser = HttpContext.Session.GetUser();
            if (loggedInUser.IsAdmin)
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
                    catch (UpdatingProductFailedException)
                    {
                        throw new UpdatingProductFailedException();
                    }
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int id)
        {
            User loggedInUser = HttpContext.Session.GetUser();
            if (loggedInUser.IsAdmin)
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
                catch (RemovingProductFailedException)
                {
                    throw new RemovingProductFailedException();
                }
            }
            return RedirectToAction("ProductPanel", "Product");
        }

        public ActionResult AddProduct()
        {
            User loggedInUser = HttpContext.Session.GetUser();
            if (loggedInUser.IsAdmin)
            {
                return View();
            }
            return RedirectToAction("ProductPanel", "Product");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(ProductViewModel model)
        {
            User loggedInUser = HttpContext.Session.GetUser();
            if (loggedInUser.IsAdmin)
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
                        return RedirectToAction("ProductPanel", "Product");
                    }
                    catch (AddingProductFailedException)
                    {
                        throw new AddingProductFailedException();
                    }
                }
            }
            return View();
        }
    }
}