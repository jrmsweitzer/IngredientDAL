using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumCrawler
{
    public class Driver : IWebDriver
    {
        private readonly IWebDriver _driver;
        public Driver() 
        {
            // This is grabbing the driver from the folder SeleniumDrivers in
            // the project root.
            _driver = new ChromeDriver("../../../SeleniumDrivers/");
        }

        public void Quit()
        {
            _driver.Quit();
        }

        public INavigation Navigate()
        {
            return _driver.Navigate();
        }

        public IWebElement FindElement(By by)
        {
            return _driver.FindElement(by);
        }

        public void Close()
        {
            _driver.Close();
        }

        public string CurrentWindowHandle
        {
            get { return _driver.CurrentWindowHandle; }
        }

        public IOptions Manage()
        {
            return _driver.Manage();
        }

        public string PageSource
        {
            get { return _driver.PageSource; }
        }

        public ITargetLocator SwitchTo()
        {
            return _driver.SwitchTo();
        }

        string IWebDriver.Title
        {
            get { return _driver.Title; }
        }

        public string Url
        {
            get { return _driver.Url; }
            set { _driver.Url = value; }
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<string> WindowHandles
        {
            get { return _driver.WindowHandles; }
        }


        public System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return _driver.FindElements(by);
        }

        public void Dispose()
        {
            _driver.Dispose();
        }
    }
}
