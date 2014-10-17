using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using LOGGER = Logger.Logger;

namespace Selenium.PageObjects
{
    /* The PageObjectBase class.  This class is to represent anything that applies to all pages
    * of the site to be tested.  The example here shows that you can pass in by default a
    * Title from the driver to ensure that the correct page is loaded, but the intention 
    * of this is to provide a base class so that codereplication is kept to a minimum
    */
    public class PageObjectBase
    {
        private IWebDriver Driver { get; set; }
        private static readonly By Title = By.XPath("//title");
        private const string LOGSTRING = "TestDetails";

        public PageObjectBase(IWebDriver driver)
        {
            Driver = driver;
        }

        public void Visit(string url, string expectedTitle)
        {
            var rootUrl = new Uri(url);
            LOGGER.GetLogger(LOGSTRING).LogMessage(string.Format("GoUrl: {0}", url));
            Driver.Navigate().GoToUrl(rootUrl);
            if (!Driver.Title.Contains(expectedTitle))
            {
                const string errMsg =
                    "PageObjectBase: We're not on the expected page.";
                LOGGER.GetLogger(LOGSTRING).LogError(errMsg);
                LOGGER.GetLogger(LOGSTRING).LogInfo(string.Format("Expected: {0}", expectedTitle));
                LOGGER.GetLogger(LOGSTRING).LogInfo(string.Format("Actual: {0}", Driver.Title));
                throw new NoSuchWindowException(errMsg);
            }
        }

        public string GetTitle()
        {
            var title = GetInnerHtml(Title);
            LOGGER.GetLogger(LOGSTRING).LogMessage(string.Format("Title: {0}", title));
            return title;
        }

        public string GetCurrentUrl()
        {
            return Driver.Url;
        }

        public IWebElement Find(By by)
        {
            return Driver.FindElement(by);
        }

        public void Click(By by)
        {
            LOGGER.GetLogger(LOGSTRING).LogMessage(string.Format("Click: {0}", by));

            var element = Find(by);
            var actions = new Actions(Driver);
            actions.MoveToElement(element).Click().Perform();
        }

        public void Close()
        {
            Driver.Close();
        }

        public void SendKeys(By by, String inputText)
        {
            LOGGER.GetLogger(LOGSTRING).LogMessage(string.Format("SndKy: {0}", inputText));
            LOGGER.GetLogger(LOGSTRING).LogMessage(string.Format("Elmnt: {0}", by));
            Find(by).Clear();
            Find(by).SendKeys(inputText);
        }

        public void SelectByText(By by, string optionText)
        {
            LOGGER.GetLogger(LOGSTRING).LogMessage(string.Format("Selct: {0}", optionText));
            LOGGER.GetLogger(LOGSTRING).LogMessage(string.Format("Elmnt: {0}", by));
            var select = new SelectElement(Find(by));
            select.SelectByText(optionText);
        }

        public string GetInnerHtml(By by)
        {
            var innerHtml = Find(by).GetAttribute("innerHTML");
            return innerHtml;
        }
    }
}