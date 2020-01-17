using Data.Contexts;
using Interfaces.Logic;
using Logic.LogicObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace UnitTest
{
    [TestClass]
    public class UnitTests
    {
        private readonly IUserLogic userLogic = new UserLogic(new UserSqlContext());

        Stopwatch stopWatch = new Stopwatch();

        [ClassInitialize]
        public void InitializeTests()
        {
            stopWatch.Start();
        }

        #region Positive Unit & Integration Tests
        #endregion


        #region Negative Unit & Integration Tests
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