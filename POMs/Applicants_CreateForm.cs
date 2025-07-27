using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs
{
    class Applicants_CreateForm : MasterPOM
    {
        private const string urlEnd = "Applicants/Default";

        readonly public EditField Firstnames;
        readonly public EditField Surname;
        readonly By _Save = By.XPath("//button/i[contains(@class,'fa-save')]");
        readonly By _FirstnamesError = By.Id("Firstnames-error");
        readonly By _SurnameError = By.Id("Surname-error");

        public Applicants_CreateForm(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;
            Firstnames = new(driver, By.Id("Firstnames"));
            Surname = new(driver, By.Id("Surname"));
        }

        public void Click_Save()
        {
            driver.FindElement(_Save).Click();
        }

        public bool IsFirstnamesErrorVisible()
        {
            try
            {
                Assert.That(driver.FindElement(_FirstnamesError).Displayed, Is.True);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsSurnamErrorVisible()
        {
            try
            {
                Assert.That(driver.FindElement(_SurnameError).Displayed, Is.True);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
