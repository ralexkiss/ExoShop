using Data.Contexts;
using Exceptions.Review;
using Exceptions.User;
using Interfaces.Logic;
using Logic.LogicObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.DataModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class UnitTests
    {
        private readonly IUserLogic userLogic = new UserLogic(new UserSqlContext());
        private readonly IUserLogic userLogicTest = new UserLogic(new UserTestContext());
        private readonly IProductLogic productLogic = new ProductLogic(new ProductSqlContext());
        private readonly IReviewLogic reviewLogic = new ReviewLogic(new ReviewSqlContext());
        private readonly ICartLogic cartLogicTest = new CartLogic(new CartTestContext());

        Stopwatch stopWatch = new Stopwatch();

        [ClassInitialize]
        public void InitializeTests()
        {
            stopWatch.Start();
        }

        #region Negative Unit & Integration Tests
        [TestMethod]
        [ExpectedException(typeof(AddingReviewFailedException))]
        public void AddReviewMessageTooLong()
        {
            Review review = new Review()
            {
                Message = "Zd1CqA1DsmOMnAKoGUJPfdbWxosd2X1eFAjKs6ULsJlyvttyYA9",
                Stars = 4,
            };
            reviewLogic.AddReview(review);
        }

        [TestMethod]
        [ExpectedException(typeof(AddingReviewFailedException))]
        public void AddReviewTooManyStars()
        {
            Review review = new Review()
            {
                Message = "Zd1CqA1DsmOMnAKoGUJPfdbWxosd2X1eFAjKs6ULsJlyvyYA9",
                Stars = 8,
            };
            reviewLogic.AddReview(review);
        }

        [TestMethod]
        [ExpectedException(typeof(RegistrationFailedException))]
        public void RegisterInvalidEmail()
        {
            User user = new User()
            {
                Email = "Test",
                Name = "Testy Test",
                Password = "Test123",
                IsAdmin = false
            };
            userLogicTest.Register(user);
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationFailedException))]
        public void LoginInvalidEmail()
        {
            userLogicTest.Login("Test", "Test123");
        }
        #endregion

        #region Positive Unit Tests

        [TestMethod]
        public void UnitRegister()
        {
            User user = new User()
            {
                Email = "Test@gmail.com",
                Name = "Testy Test",
                Password = "Test123",
                IsAdmin = false         
            };
            userLogicTest.Register(user);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UnitLogin()
        {
            User user = userLogicTest.Login("Test@gmail.com", "Test123");
            Assert.IsTrue(user != null);
        }


        [TestMethod]
        public void UnitAddToCart()
        {
            User user = new User()
            {
                Email = "Test@gmail.com",
                Name = "Testy Test",
                Password = "Test123",
                IsAdmin = false
            };
            Product product = new Product()
            {
                Name = "Nike Shirt",
                Description = "Hele mooie Nike Shirt",
                ImageURL = "https://i.ya-webdesign.com/images/nike-swoosh-png-white-6.png",
                Price = 49.99,
            };
            cartLogicTest.AddToCart(product, user);
            Assert.IsTrue(cartLogicTest.GetCartByUser(user).Any());
        }
        #endregion

        #region Positive Integration Tests
        [TestMethod]
        public void IntegrationRegister()
        {
            userLogic.Register(new User()
            {
                Email = "Test@gmail.com",
                Name = "Testy Test",
                Password = "Test123",
            });
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void IntegrationLogin()
        {
            userLogic.Login("Test@gmail.com", "Test123");
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void IntegrationAddProduct()
        {
            Product product = new Product()
            {
                Name = "Nike Shirt",
                Description = "Hele mooie Nike Shirt",
                ImageURL = "https://i.ya-webdesign.com/images/nike-swoosh-png-white-6.png",
                Price = 49.99,
            };
            productLogic.AddProduct(product);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void IntegrationGetAllProducts()
        {
            List<Product> products = productLogic.GetAll();
            Assert.IsTrue(products.Any());
        }
        #endregion

        [ClassCleanup]
        public void Finish()
        {
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("Runtime: " + elapsedTime);
            Assert.IsTrue(true);
        }
    }
}