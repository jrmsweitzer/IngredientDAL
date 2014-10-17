using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.PageObjects
{
    class YahooHomePage : PageObjectBase
    {
        private IWebDriver Driver { get; set; }
         
        public YahooHomePage(IWebDriver driver) : base(driver)
        {
            Driver = driver;
            Visit("https://www.yahoo.com/", "Yahoo");
        }

        private static readonly By Horoscope = By.XPath("//p[contains(@class,'horoscope-blurb')]");

        public string getHoroscope()
        {
            return GetInnerHtml(Horoscope);
        }
    }
}
