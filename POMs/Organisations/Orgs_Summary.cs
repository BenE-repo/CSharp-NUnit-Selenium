using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs.Organisations
{
    class Orgs_Summary : MasterPOM
    {
        private const string _urlEnd = "Organisations/Default/Summary";
        private string _orgID;
        

        readonly public ActionsMenu ActionMenu;
        readonly public ActionsMenu SubActionMenu;
        readonly public Table_v2 Table;

        public Orgs_Summary(IWebDriver driver, string baseURL, string orgID) : base(driver, baseURL)
        {
            _orgID = orgID;
            pageURL = baseURL + _urlEnd + "/" + _orgID;
            ActionMenu = new(driver);
            SubActionMenu = new(driver, By.XPath("//div[contains(@class, 'pull-right')]/div/button[@id='mainActionsButton']/.."));
            Table = new(driver, By.XPath("//div[@id = 'LinkedOfficersInner']/div/div/table/tbody"), 1);
        }
    }
}
