using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace NUnit_Selenium.POMs.Officers
{
    class Officers_Site_Edit : MasterPOM
    {
        private const string urlEnd = "";

        readonly By _orgLink = By.XPath("//div[@class='modal-body']/input[@id='ID']");

        // TODO: Needs converting to updated model
        readonly By _allSitesToggle = By.XPath("//input[@id='NoRestrictions']/following-sibling::span");
        readonly By _sites_List = By.XPath("//div[@id='collapseSpecificSites']/div/table/tbody/tr");
        readonly public Button Save;


        public Officers_Site_Edit(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;
            Save = new(driver, By.XPath("//button/i[contains(@class,'fa-save')]"));
        }

        public void ToggleToggle()
        {
            driver.FindElement(_allSitesToggle).Click();
        }

        public void SelectSite(string site)
        {
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(30));            
            wait.Until(d => driver.FindElement(_sites_List).FindElement(By.XPath($".//td/input[@value='{site}']/preceding-sibling::label")).Displayed);
            driver.FindElement(_sites_List).FindElement(By.XPath($".//td/input[@value='{site}']/preceding-sibling::label")).Click();
        }
    }
}
