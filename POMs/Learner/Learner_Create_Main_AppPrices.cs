using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs
{
    class Learner_Create_Main_AppPrices : MasterPOM
    {
        private const string urlEnd = "Learner/Default/Create";

        readonly public Button Next;
        readonly public ValidationErrors errors;

        public Learner_Create_Main_AppPrices(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            Next = new(driver, By.XPath("//div[@id='ApprenticeshipPrices']/form/div[@class='wizard-footer']/div/div/button/i[contains(@class,'fa-chevron-right')]"));
            errors = new(driver);
        }
    }
}
