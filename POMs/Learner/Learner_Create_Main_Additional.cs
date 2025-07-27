using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs
{
    class Learner_Create_Main_Additional : MasterPOM
    {
        private const string urlEnd = "Learner/Default/Create";

        readonly public Button Complete;
        readonly public ValidationErrors errors;

        public Learner_Create_Main_Additional(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            Complete = new(driver, By.XPath("//div[@id='Additional']/form/div[@class='wizard-footer']/div/div/button/i[contains(@class,'fa-chevron-right')]"));
            errors = new(driver);
        }
    }
}
