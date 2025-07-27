using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Xml.Linq;

namespace NUnit_Selenium.POMs
{
    public abstract class MasterPOM
    {
        public IWebDriver driver;
        public WebDriverWait wait;
        public string pageURL;

        public MasterPOM(IWebDriver driver, string baseURL = "")
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
            pageURL = string.Empty;
        }

        public void GotoPage()
        {
            driver.Navigate().GoToUrl(pageURL);
            //assert is the right place
        }

        public void GotoPage(string tab)
        {
            //Goto the page, adding the string passed in to the end of the url. Created for navigating to tabs in summarys etc.

            //Just in case we forget to add the hash at the beginning
            if (tab[0].ToString() != "#") { tab = "#" + tab; }

            driver.Navigate().GoToUrl(pageURL + tab);
            wait.Until(d => driver.Url == pageURL + tab);
        }

        public void RefreshPage()
        {
            driver.Navigate().Refresh();
        }

        public bool IsCorrectURL()
        {
            if (driver.Url == pageURL)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
