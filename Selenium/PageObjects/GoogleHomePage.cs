using OpenQA.Selenium;
using System.Configuration;

namespace Selenium.PageObjects
{
    public class GoogleHomePage : PageObjectBase
    {
        private IWebDriver Driver { get; set; }
        
        private static readonly By SearchDialog = By.Name("q");
        private static readonly By SearchButton = By.Name("btnG");
        private static readonly string _testUrl = ConfigurationManager.AppSettings["testUrl"];

        public GoogleHomePage(IWebDriver driver) : base(driver)
        {
            Driver = driver;
            Visit(_testUrl, "Google");
        }

        /* Here are two example methods of using the newly generated objects with friendlier
         *  names so that it clearly identifies the task being completed
         * Note that there is no validation on these, so if they are to fail there will 
         * be no friendly output provided other than the failure.
         */
        public GoogleHomePage EnterSearchText(string text)
        {
            SendKeys(SearchDialog, text);
            return this;
        }
        public GoogleResultsPage Search()
        {
            Click(SearchButton);
            return new GoogleResultsPage(Driver);
        }
    }
}
