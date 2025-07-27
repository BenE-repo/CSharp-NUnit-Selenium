using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs
{
    class Orgs_CreateForm : MasterPOM
    {
        private const string urlEnd = "Organisations/Default/Create";

        readonly public EditField Name;
        readonly public EditField Postcode;
        readonly public ButtonWithDropDown Save;
        readonly public ValidationErrors errors;

        public Orgs_CreateForm(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            Name = new(driver, By.Id("Name"));
            Postcode = new(driver, By.Id("Postcode"));
            Save = new(driver, By.XPath("//button/i[contains(@class,'fa-save')]"));
            errors = new(driver);
        }
    }
}
