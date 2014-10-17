using OpenQA.Selenium;
using Selenium.Annotations;

namespace Selenium.PageObjects
{
    public class GoogleResultsPage : PageObjectBase
    {
        private IWebDriver Driver { get; set; }

        public GoogleResultsPage(IWebDriver driver) : base(driver)
        {
            Driver = driver;
        }
    }
}
