using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs.Organisations
{
    class Reports_Home : MasterPOM
    {
        private const string _urlEnd = "Reports/Home";

        readonly public EditField Template_SearchField;
        readonly public Button Template_SearchButton;
        readonly public Table_v2 Template_Grid;
        readonly public Table_v2 Available_Grid;
        readonly public Button Tab_AvailableReports;

        public Reports_Home(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + _urlEnd;

            Template_SearchField = new(driver, By.XPath("//div[@id='templates']/div/div/div/div/form/div/div/input[@id='Text']"));
            Template_SearchButton = new(driver, By.XPath("//div[@id='templates']/div/div/div/div/form/div/div/button[@aria-label='Search']/i"));
            Template_Grid = new(driver, By.XPath("//table[@id='downloadsTable']/tbody"),2);
            Available_Grid = new(driver, By.XPath("//table[@id='availableReportsTable']/tbody"),11);
            Tab_AvailableReports = new(driver, By.XPath("//a[@href = '#availablereports']"));
        }
    }
}
