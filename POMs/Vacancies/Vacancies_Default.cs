using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs.Organisations
{
    class Vacancies_Default : MasterPOM
    {
        private const string urlEnd = "Vacancies/Default";

        public readonly Button CreateBtn;

        public Vacancies_Default(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            CreateBtn = new(driver, By.XPath("//a[@data-ajax-url='/pics/Vacancies/Default/Create']"));
        }
    }
}
