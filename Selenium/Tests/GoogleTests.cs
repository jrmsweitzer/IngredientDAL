using System.Threading;
using NUnit.Framework;
using Selenium.PageObjects;
using LOGGER = Logger.Logger;

namespace Selenium.Tests
{
    [TestFixture]
    public class GoogleTests : TestBase
    {
        [Test]
        public void GooglePageTitle()
        {
            /* The first task is to create the GoogleHomePage Object.  This
             * allows us to reference the Homepage and all of the defined 
             * elements as shown in the examples below
             */
            var homepage = new GoogleHomePage(WebDriver);

            /* Here is an example of using the new Elements to validate information
             * within the webpage using the pagefactory
             */
            Assert.AreEqual("Google", homepage.GetTitle());

            /* Next, we're going to take advantage of method chaining with our
             * PageObjects, while creating other objects in our tests. In this case,
             * we'll create a GoogleResultsPage Object.
             * 
             * With method chaining in the PageObject Pattern, our methods return
             * PageObjects. EnterSearchText(string text), returns a GoogleHomePage Object.
             * Search() is a method within the GoogleHomePage Class, so it can be chained
             * on to it. Search() then returns a GoogleResultsPage Object.
             */
            GoogleResultsPage searchPage = homepage.EnterSearchText("hello world").Search();
            /* The following sleep statement has only been placed in this code to demonstrate
             * the button push as there is no validation of the search results in place
             * so its not possible to actually see the result of the button click.
             */
            Thread.Sleep(5000);
            Assert.AreEqual("hello world - Google Search", searchPage.GetTitle());
        }

        [Test]
        public void TestScreenshot()
        {
            /* This will test the Screenshot capturing capabilities of our code.
             * When the test fails (which it will, since 1 is most definitely NOT
             * equal to 2), the TestBase class will take a screencap at the
             * current state of the app. In this case, it will simply take a 
             * screencap of the Google Home Page.             * 
             */
            var homepage = new GoogleHomePage(WebDriver);
            Assert.AreEqual(1, 2);
        }
    }
}
