using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs.Officers
{
    class Officers_Default : MasterPOM
    {
        private const string urlEnd = "Officers/Default";

        public readonly Button CreateBtn;
        public readonly EditField Search_Name;
        public readonly Button Search_Button;
        public readonly Table Search_Grid;

        public Officers_Default(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            CreateBtn = new(driver, By.XPath("//a[@data-ajax-url='/pics/Officers/Default/Create']"));
            Search_Name = new(driver, By.Id("Text"));
            Search_Button = new(driver, By.XPath("//button/i[contains(@class,'fa-search')]"));
            Search_Grid = new(driver, By.XPath("//div[contains(@class,'container-fluid')]"), "Complex");
        }
    }
}
