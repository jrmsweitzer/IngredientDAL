using System.Configuration;
using System.Diagnostics;
using NUnit.Framework;
using LOGGER = Logger.Logger;
using System.IO;
using OpenQA.Selenium;
using System;

namespace Selenium.Tests
{
    public class TestBase : SeleniumDriver
    {
        private Stopwatch _suiteStopwatch;
        private Stopwatch _testStopwatch;
        private string screenshotDir = ConfigurationManager.AppSettings["screenshotDirectory"];

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            LOGGER.GetLogger(LOGSTRING).LogStartTestSuite();
            _suiteStopwatch = Stopwatch.StartNew();
        }

        [SetUp]
        public void TestSetUp()
        {
            LOGGER.GetLogger(LOGSTRING).LogStartTest(TestContext.CurrentContext.Test.Name);
            _testStopwatch = Stopwatch.StartNew();
        }

        [TearDown]
        public void TestTearDown()
        {
            var context = TestContext.CurrentContext;
            if (context.Result.Status == TestStatus.Passed)
            {
                LOGGER.GetLogger(LOGSTRING).LogPass(context.Test.Name);
            }
            else
            {
                LOGGER.GetLogger(LOGSTRING).LogFail(context.Test.Name);
                TakeScreenShot();
            }
            _testStopwatch.Stop();
            LOGGER.GetLogger(LOGSTRING).LogTime("Elapsed Time", _testStopwatch.Elapsed);
            WebDriver.Quit();
            WebDriver = null;
        }

        private void TakeScreenShot()
        {
            CreateScreenshotDirectory();
            var ss = ((ITakesScreenshot)WebDriver).GetScreenshot();
            var sslocation = string.Format(@"{0}{1}_{2}.jpeg",
                    screenshotDir,
                    DateTime.Now.ToString("yyyy-MM-dd"),
                    TestContext.CurrentContext.Test.Name);
            ss.SaveAsFile(sslocation, System.Drawing.Imaging.ImageFormat.Jpeg);
            LOGGER.GetLogger(LOGSTRING).LogInfo(string.Format("Screenshot saved at {0}", sslocation));
        }

        private void CreateScreenshotDirectory()
        {
            if (!Directory.Exists(screenshotDir))
            {
                Directory.CreateDirectory(screenshotDir);
            }
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            LOGGER.GetLogger(LOGSTRING).LogFinishTestSuite();
            _suiteStopwatch.Stop();
            LOGGER.GetLogger(LOGSTRING).LogTime("Total Time", _suiteStopwatch.Elapsed);
        }
    }
}
