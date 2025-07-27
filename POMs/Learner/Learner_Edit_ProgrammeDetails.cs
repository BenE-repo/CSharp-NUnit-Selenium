using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs
{
    class Learner_Edit_ProgrammeDetails : MasterPOM
    {
        private const string urlEnd = "Learner/ilr/Edit";

        readonly public EditField Firstnames;
        readonly public EditField Surname;
        readonly public SingleSelect Ethnicity;
        readonly public EditField DateOfBirth;
        readonly public EditField Postcode;
        readonly public EditField Address1;
        readonly public SingleSelect Disability;
        readonly public Button Save;
        readonly public ValidationErrors Errors;

        public Learner_Edit_ProgrammeDetails(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            Firstnames = new(driver, By.Id("Firstnames"));
            Surname = new(driver, By.Id("Surname"));
            Ethnicity = new(driver, "Ethnicity");
            DateOfBirth = new(driver, By.Id("DateOfBirth"));
            Postcode = new(driver, By.Id("Postcode"));
            Address1 = new(driver, By.Id("Address1"));
            Disability = new(driver, "DisabilitySelfAssessed");
            Save = new(driver, By.XPath("//button/i[contains(@class,'fa-save')]"));
            Errors = new(driver);
        }
    }
}
