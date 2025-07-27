using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs.Organisations
{
    class Orgs_Default : MasterPOM
    {
        private const string urlEnd = "Organisations/Default";

        public readonly Button CreateBtn;
        public readonly EditField Search_Name;
        public readonly Button Search_Button;
        public readonly Table Search_Grid;

        public Orgs_Default(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            CreateBtn = new(driver, By.XPath("//a[@data-ajax-url='/pics/Organisations/Default/Create']"));
            Search_Name = new(driver, By.Id("Text"));
            Search_Button = new(driver, By.XPath("//button/i[contains(@class,'fa-search')]"));
            Search_Grid = new(driver, By.XPath("//div[contains(@class,'container-fluid')]"), "Complex");
        }
    }
}
