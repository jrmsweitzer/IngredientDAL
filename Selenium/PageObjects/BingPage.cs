using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Selenium.PageObjects
{
    public class BingPage : PageObjectBase
    {
        private IWebDriver Driver { get; set; }
        private string BingURL = "http://www.bing.com/rewards/dashboard";

        private static readonly By LoginButton = By.
            XPath("//span[.='Microsoft account']/../span[.='Connect']");
        private static readonly By UsernameField = By.Name("login");
        private static readonly By PasswordField = By.Name("passwd");
        private static readonly By LoginToggle = By.Id("id_s");
        private static readonly By LoginButton2 = By.Id("idSIButton9");
        private static readonly By PCSearch = By.XPath("//span[.='PC search']");
        private static readonly By BingSearchBar = By.Id("sb_form_q");
        private static readonly By BingSearchButton = By.Id("sb_form_go");
        private static readonly By LogoutButton = By.XPath("//span[@class='id_link_text'][.='Sign out']");
        private static readonly By LogoutToggle = By.Id("id_n");

        public BingPage(IWebDriver driver) : base(driver)
        {
            Driver = driver;
        }

        public BingPage SignInAndGoToSearchPage(string username, string password)
        {
            Visit(BingURL, "Bing");
            Thread.Sleep(1000);
            Click(LoginToggle);
            Click(LoginButton);
            SendKeys(UsernameField, username);
            SendKeys(PasswordField, password);
            Click(LoginButton2);

            // Only 1 Window Handle should exist - the dashboard
            string winHandle = Driver.CurrentWindowHandle;

            Thread.Sleep(1000);
            Click(PCSearch);

            // Now 2 handles should appear - We want to go to the new handle
            foreach(string handle in Driver.WindowHandles)
            {
                if (!handle.Equals(winHandle))
                {
                    Driver.SwitchTo().Window(handle);
                    break;
                }
            }

            return new BingPage(Driver);
        }

        public BingPage SearchRandomQueries(int numQueries)
        {
            for (int queriesSearched = 0; queriesSearched < numQueries;
                queriesSearched++)
            {
                SendKeys(BingSearchBar, GetRandomSearchText());
                Click(BingSearchButton);
            }

            return this;
        }

        private string GetRandomSearchText()
        {
            string ret = "";
            Random rand = new Random();

            int randomCharacterAmount = rand.Next(3, 8);

            #region switch
            for (int count = 0; count < randomCharacterAmount; count++)
            {
                int currentChar = rand.Next(1, 26);
                switch (currentChar)
                {
                    case 1:
                        ret += "q";
                        break;
                    case 2:
                        ret += "w";
                        break;
                    case 3:
                        ret += "e";
                        break;
                    case 4:
                        ret += "r";
                        break;
                    case 5:
                        ret += "t";
                        break;
                    case 6:
                        ret += "y";
                        break;
                    case 7:
                        ret += "u";
                        break;
                    case 8:
                        ret += "i";
                        break;
                    case 9:
                        ret += "o";
                        break;
                    case 10:
                        ret += "p";
                        break;
                    case 11:
                        ret += "a";
                        break;
                    case 12:
                        ret += "s";
                        break;
                    case 13:
                        ret += "d";
                        break;
                    case 14:
                        ret += "f";
                        break;
                    case 15:
                        ret += "g";
                        break;
                    case 16:
                        ret += "h";
                        break;
                    case 17:
                        ret += "j";
                        break;
                    case 18:
                        ret += "k";
                        break;
                    case 19:
                        ret += "l";
                        break;
                    case 20:
                        ret += "z";
                        break;
                    case 21:
                        ret += "x";
                        break;
                    case 22:
                        ret += "c";
                        break;
                    case 23:
                        ret += "v";
                        break;
                    case 24:
                        ret += "b";
                        break;
                    case 25:
                        ret += "n";
                        break;
                    case 26:
                        ret += "m";
                        break;
                }
            }
            #endregion

            return ret;
        }

        internal void LogOut()
        {
            string dashHandle = "";
            foreach(string handle in Driver.WindowHandles)
            {
                if (!handle.Equals(Driver.CurrentWindowHandle))
                {
                    dashHandle = handle;
                }
            }
            Driver.Close();
            Driver.SwitchTo().Window(dashHandle);
            Click(LogoutToggle);
            Click(LogoutButton);
        }
    }
}
