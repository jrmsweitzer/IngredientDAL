using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumCrawler;

namespace UnitTests
{
    [TestClass]
    public class SeleniumTests
    {
        private readonly Driver _driver = new Driver();

        [TestMethod]
        public void SeleniumWorks()
        {
            _driver.Navigate().GoToUrl("http://www.google.com");
            var query = _driver.FindElement(By.Name("q"));
            query.SendKeys("Selenium");
            query.Submit();
        }

        [TestCleanup]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
