using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace NUnit_Selenium.POMs.Officers
{
    class Officers_CreateForm : MasterPOM
    {
        private const string urlEnd = "Officers/Default/Create";

        readonly public EditField Firstnames;
        readonly public EditField Surname;
        readonly public MultiSelect Roles;
        readonly public ButtonWithDropDown Save;
        readonly By _FirstnamesError = By.Id("Firstnames-error");
        readonly By _SurnameError = By.Id("Surname-error");
        readonly By _RolesError = By.Id("Roles-error");
        readonly By _PotentialMatches = By.Id("potential-matches");

        public Officers_CreateForm(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            Firstnames = new(driver, By.Id("Firstnames"));
            Surname = new(driver, By.Id("Surname"));
            Roles = new(driver, By.XPath("//div[@class='col-9']/span[contains(@class,'select2-container')]"), By.XPath("//span[contains(@class,'select2-results')]"));
            Save = new(driver, By.XPath("//button/i[contains(@class,'fa-save')]"));
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

        public bool IsSurnameErrorVisible()
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

        public bool IsRolesErrorVisible()
        {
            try
            {
                Assert.That(driver.FindElement(_RolesError).Displayed, Is.True);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void WaitUntilFinishedPotentialMatchSearch()
        {
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(".//div/div[contains(@class,'alert-help')]")));
        }
    }
}
