using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs.Organisations
{
    class Orgs_OfficerLink : MasterPOM
    {
        private const string _urlEnd = "Organisations/Default/Summary";

        readonly public SingleSelect Officer;
        readonly public Button Save;

        public Orgs_OfficerLink(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + _urlEnd;
            Officer = new(driver, By.Id("select2-Officer_Value-container"), By.Id("select2-Officer_Value-results"), true);
            Save = new(driver, By.XPath("//button/i[contains(@class,'fa-save')]"));
        }
    }
}
