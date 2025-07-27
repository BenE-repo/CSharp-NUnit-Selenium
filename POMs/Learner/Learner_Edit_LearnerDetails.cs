using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs
{
    class Learner_Edit_LearnerDetails : MasterPOM
    {
        private const string urlEnd = "Learner/Default/Edit";

        readonly public EditField Firstnames;
        readonly public EditField Surname;
        readonly public SingleSelect Ethnicity;
        readonly public EditField DateOfBirth;
        readonly public EditField NIN;
        readonly public EditField Postcode;
        readonly public EditField Address1;
        readonly public Button Save;
        readonly public ValidationErrors Errors;

        public Learner_Edit_LearnerDetails(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            Firstnames = new(driver, By.Id("Firstnames"));
            Surname = new(driver, By.Id("Surname"));
            Ethnicity = new(driver, "Ethnicity");
            DateOfBirth = new(driver, By.Id("DateOfBirth"));
            NIN = new(driver, By.Id("NINumber"));
            Postcode = new(driver, By.Id("Postcode"));
            Address1 = new(driver, By.Id("Address1"));
            Save = new(driver, By.XPath("//button/i[contains(@class,'fa-save')]"));
            Errors = new(driver);
        }
    }
}
