using NUnit.Framework;
using Selenium.PageObjects;
using System.Collections.Generic;

namespace Selenium.Tests
{
    [TestFixture]
    public class BingBot : TestBase
    {
        public Dictionary<string, string> credentials =
            new Dictionary<string, string>();

        [TestFixtureSetUp]
        public void SetUp()
        {
            credentials.Add("thumbell72@outlook.com", "Thisisapasswor");
            credentials.Add("Youttle91@outlook.com", "Thisisapasswor");
            credentials.Add("Livill56@outlook.com", "Thisisapasswor");
        }

        [Test]
        public void GetBingPoints()
        {
            foreach (var user in credentials)
            {
                string username = user.Key;
                string password = user.Value;

                var bingPage = new BingPage(WebDriver);
                bingPage.SignInAndGoToSearchPage(username, password)
                    .SearchRandomQueries(40);

                bingPage.LogOut();
            }
        }
    }
}
