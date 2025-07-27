using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs.Officers
{
    class Officers_Summary : MasterPOM
    {
        private const string _urlEnd = "Officers/Default/Summary";

        readonly public ActionsMenu ActionMenu;
        readonly public ActionsMenu SubActionMenu;
        readonly public Table Table;
        readonly public Button SitesTab;
        readonly public Button Sites_Edit;
        // readonly public Table Sites_Grid;
        readonly public Table_v2 Sites_Grid;

        public Officers_Summary(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + _urlEnd + "/";
            ActionMenu = new(driver);
            SubActionMenu = new(driver, By.XPath("//div[contains(@class, 'pull-right')]/div/button[@id='mainActionsButton']/.."));
            Table = new(driver, By.Id("LinkedOfficersInner"));
            SitesTab = new(driver, By.XPath("//a[@href='#sites-list']"));
            Sites_Edit = new(driver, By.XPath("//a[contains(@data-ajax-url,'/Officers/Sites/Edit/')]"));
            Sites_Grid = new(driver, By.XPath("//table[@class='table']/tbody"), 1);
        }
    }
}
