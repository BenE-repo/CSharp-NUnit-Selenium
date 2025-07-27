using OpenQA.Selenium;

namespace NUnit_Selenium.POMs
{
    class Applicants_Default : MasterPOM
    {
        private const string urlEnd = "Applicants/Default";

        public Applicants_Default(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;
        }

        readonly By _CreateApplicant = By.XPath("//a[@data-ajax-url='/pics/Applicants/Default/Create']");

        public void CreateApplicant_Click()
        {
            driver.FindElement(_CreateApplicant).Click();
        }
    }
}
