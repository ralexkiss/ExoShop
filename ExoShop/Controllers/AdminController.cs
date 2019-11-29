using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExoShop.Models;
using Interfaces.Contexts;
using Interfaces.Logic;
using Logic.LogicObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DataModels;

namespace GeoChatting.Controllers
{
    public class AdminController : Controller
    {

        private readonly IProductLogic productLogic;

        public AdminController(IProductContext context)
        {
            productLogic = new ProductLogic(context);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(ProductViewModel model)
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
                        category = model.Category

                    });
                    return RedirectToAction("Index", "Admin");
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