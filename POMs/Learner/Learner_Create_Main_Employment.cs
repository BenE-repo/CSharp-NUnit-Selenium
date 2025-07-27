using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs
{
    class Learner_Create_Main_Employment : MasterPOM
    {
        private const string urlEnd = "Learner/Default/Create";

        readonly public SingleSelect EmploymentStatus;
        readonly public SingleSelect UnemploymentLength;
        readonly public SingleSelect SmallEmployer;
        readonly public Button Next;
        readonly public ValidationErrors errors;

        public Learner_Create_Main_Employment(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            EmploymentStatus = new(driver, "PriorEmploymentStatus");
            UnemploymentLength = new(driver, "PriorLengthOfUnemployment");
            SmallEmployer = new(driver, "PriorSmallEmployerIndicator"); 
            Next = new(driver, By.XPath("//div[@id='Employment']/form/div[@class='wizard-footer']/div/div/button/i[contains(@class,'fa-chevron-right')]"));
            errors = new(driver);
        }
    }
}
