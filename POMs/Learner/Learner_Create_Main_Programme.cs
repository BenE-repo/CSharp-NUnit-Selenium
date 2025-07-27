using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs
{
    class Learner_Create_Main_Programme : MasterPOM
    {
        private const string urlEnd = "Learner/Default/Create";

        readonly public EditField PlannedHours2024;
        readonly public SingleSelect DelLocSource;
        readonly public Button Next;
        readonly public ValidationErrors errors;

        public Learner_Create_Main_Programme(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            PlannedHours2024 = new(driver, By.Id("ILRLearnerPlannedLearningHours2024"));
            DelLocSource = new(driver, "DeliveryLocationSource");
            Next = new(driver, By.XPath("//div[@id='ProgrammeDetails']/form/div[@class='wizard-footer']/div/div/button/i[contains(@class,'fa-chevron-right')]"));
            errors = new(driver);
        }
    }
}
