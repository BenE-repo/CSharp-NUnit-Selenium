using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;
using System.ComponentModel;

namespace NUnit_Selenium.POMs.Generic
{
    class Site_Edit_Modal : MasterPOM
    {
        private const string urlEnd = "";

        public readonly Button AllSitesToggle;
        public readonly ComboList SitesList;
        public readonly Button Save;

        public Site_Edit_Modal(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            AllSitesToggle = new(driver, By.XPath("//input[@id='NoRestrictions']/following-sibling::span"));
            SitesList = new(driver, By.XPath("//table/tbody"), 2);
            Save = new(driver, By.XPath("//button/i[contains(@class,'fa-save')]"));
        }
    }
}
