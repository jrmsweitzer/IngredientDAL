using System;
using NUnit.Framework;
using Selenium.PageObjects;

namespace Selenium.Tests
{
    [TestFixture]
    public class UnitTest1 : TestBase
    {
        [Test]
        public void GetHoroscope()
        {
            var yahooPage = new YahooHomePage(WebDriver);

            string actHoroscope = yahooPage.getHoroscope();
            string expHoroscope = "It's time to clean up -- at least some aspect of your life requires a once-over. Fortunately, your amazing energy helps you to push things in the right direction while you have a great time!";

            Assert.AreEqual(actHoroscope, expHoroscope);
        }
    }
}
