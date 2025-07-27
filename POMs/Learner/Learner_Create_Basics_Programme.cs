using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs
{
    class Learner_Create_Basics_Programme : MasterPOM
    {
        private const string urlEnd = "Learner/Default/Create";

        readonly public EditField StartDate;
        readonly public SingleSelect QualPlan;
        readonly public ButtonWithDropDown Next;
        readonly public ValidationErrors errors;

        public Learner_Create_Basics_Programme(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            StartDate = new(driver, By.Id("ProgrammeStart"));
            QualPlan = new(driver, By.Id("select2-PlanCode_Value-container"), By.Id("select2-PlanCode_Value-results"), true);
            // :eyeroll:
            Next = new(driver, By.XPath("//div[@id='SelectProgramme']/form/div[@class='wizard-footer']/div/div/button/i[contains(@class,'fa-chevron-right')]"));
            errors = new(driver);
        }
    }
}
