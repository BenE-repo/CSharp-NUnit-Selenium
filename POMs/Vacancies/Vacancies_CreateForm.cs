using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs
{
    class Vacancies_CreateForm : MasterPOM
    {
        private const string urlEnd = "Vacancies/Default/Create";

        readonly public EditField Title;
        readonly public ButtonWithDropDown Save;
        readonly public ValidationErrors errors;

        public Vacancies_CreateForm(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            Title = new(driver, By.XPath("//div[@class='col-9']/input[@id='Title']"));
            Save = new(driver, By.XPath("//button/i[contains(@class,'fa-save')]"));
            errors = new(driver);
        }
    }
}
