using OpenQA.Selenium;
using NUnit_Selenium.POMs.Components;

namespace NUnit_Selenium.POMs
{
    class Learner_Default : MasterPOM
    {
        private const string urlEnd = "Learner/Default";

        public readonly Button CreateBtn;
        public readonly EditField Search_Name;
        public readonly SingleSelect Search_Status;
        public readonly Button Search_Button;
        public readonly Table_v2 LearnerTable;
        public readonly Alert DeleteDialog;

        public Learner_Default(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            CreateBtn = new(driver, By.XPath("//a[@data-ajax-url='/pics/Learner/Create/CreateWizard']"));
            Search_Name = new(driver, By.Id("Text"));
            Search_Status = new(driver, "InLearningStatus");
            Search_Button = new(driver, By.XPath("//button[@aria-label='Search']/i"));
            LearnerTable = new(driver, By.XPath("//tbody"), 6);
            DeleteDialog = new(driver);
        }
    }
}
