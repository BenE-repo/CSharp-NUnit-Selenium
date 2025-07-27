using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;
using System.Security.Cryptography;

namespace NUnit_Selenium.POMs
{
    class Learner_Create_Main_Learner : MasterPOM
    {
        private const string urlEnd = "Learner/Default/Create";
        private By _learnerID = By.Id("ID");

        readonly public SingleSelect Ethnicity;
        readonly public EditField Postcode;
        readonly public EditField Address1;
        readonly public SingleSelect PriorAttainment;
        readonly public SingleSelect Disability;
        readonly public Button Next;
        readonly public ValidationErrors errors;

        public Learner_Create_Main_Learner(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            Ethnicity = new(driver, "Ethnicity");
            Postcode = new(driver, By.Id("Postcode"));
            Address1 = new(driver, By.Id("Address1"));
            PriorAttainment = new(driver, "PriorAttainment");
            Disability = new(driver, "DisabilitySelfAssessed");
            Next = new(driver, By.XPath("//div[@id='LearnerDetails']/form/div[@class='wizard-footer']/div/div/button/i[contains(@class,'fa-chevron-right')]"));
            errors = new(driver);
        }

        public string GetLearnerID()
        {
            string id = driver.FindElement(_learnerID).GetAttribute("Value");
            return id;
        }
    }
}
