using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs.Organisations
{
    class Vacancies_Summary : MasterPOM
    {
        private const string _urlEnd = "Vacancies/Default/Summary";
        private string _vacID;

        readonly public Button AddProvision;
        readonly public Table Table;

        public Vacancies_Summary(IWebDriver driver, string baseURL, string vacID) : base(driver, baseURL)
        {
            _vacID = vacID;
            pageURL = baseURL + _urlEnd + "/" + _vacID;
            AddProvision = new(driver, By.XPath("//a[contains(@data-ajax-url, 'ToolComponents/Provisions/CreateWizard/')]"));
            Table = new(driver, By.Id("provisions-list"));
        }
    }
}
