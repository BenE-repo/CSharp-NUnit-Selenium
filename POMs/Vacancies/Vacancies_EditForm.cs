using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;
using System.Xml.Linq;

namespace NUnit_Selenium.POMs
{
    class Vacancies_EditForm : MasterPOM
    {
        private const string urlEnd = "Vacancies/Default/Edit";

        readonly By _vacLink = By.XPath("//div[@class='modal-body']/input[@id='ID']");
        readonly By _modalContainerHidden = By.XPath("//div[@id='general-modal-container' and @aria-hidden='true']");

        public string VacID = string.Empty;
        readonly public EditField Postcode;
        readonly public Button Save;
        readonly public ValidationErrors Errors;

        public Vacancies_EditForm(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            Postcode = new(driver, By.Id("Postcode"));
            Save = new(driver, By.XPath("//button/i[contains(@class,'fa-save')]"));
            Errors = new(driver);
        }

        public string GetVacID()
        {
            VacID = driver.FindElement(_vacLink).GetAttribute("Value");
            return VacID;
        }

        public void WaitForDialogClose()
        {
            try
            {
                wait.Until(d => !driver.FindElement(_modalContainerHidden).Displayed);
            }
            catch
            {
                // This space intentionally left blank (The nice way of waiting for an element to not exist, with ExpectedConditions
                // is deprecated, and a third-party package which does similar is abandoned, so doing like this instead.)
            }
        }
    }
}
