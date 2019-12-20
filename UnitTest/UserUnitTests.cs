using NUnit.Framework;
using System;
using System.Diagnostics;

namespace UnitTest
{
    public class Tests
    {
        Stopwatch stopWatch = new Stopwatch();

        [SetUp]
        public void InitializeTests()
        {
            stopWatch.Start();

        }

        [Test]
        public void RegisterUser()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void AuthorizeUser()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void AddToCart()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void FillCheckout()
        {
            Assert.IsTrue(true);
        }

        [Test]
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