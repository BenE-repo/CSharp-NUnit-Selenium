using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs.Organisations
{
    class Vacancies_AddProvision : MasterPOM
    {
        private const string _urlEnd = "Vacancies/Default/Summary";

        readonly public SingleSelect ProvisionType;
        readonly public Button Next;
        readonly public SingleSelect SelectProvision;
        //readonly public SingleSelect Officer;
        readonly public Button Save;

        public Vacancies_AddProvision(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + _urlEnd;
            ProvisionType = new(driver, "ProvisionType");
            Next = new(driver, By.XPath("//button[contains(@class,'btn-primary')]/i"));
            SelectProvision = new(driver, "ProvisionLocal");
            Save = new(driver, By.XPath("//button/i[contains(@class,'fa-save')]"));
        }
    }
}
