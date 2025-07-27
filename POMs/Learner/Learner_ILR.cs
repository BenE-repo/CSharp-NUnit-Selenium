using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs.Organisations
{
    class Learner_ILR : MasterPOM
    {
        private const string _urlEnd = "Learner/ILR/Summary";
        private string _learnerID;
        

        readonly public ActionsMenu ActionMenu;
        readonly public Button Edit_Programme;
        readonly public Button PlannedHoursTab;

        public Learner_ILR(IWebDriver driver, string baseURL, string learnerID) : base(driver, baseURL)
        {
            _learnerID = learnerID;
            pageURL = baseURL + _urlEnd + "/" + _learnerID;
            ActionMenu = new(driver);
            Edit_Programme = new(driver, By.XPath("//a[contains(@data-ajax-url,'/pics/Learner/ILR/EditLearner/')]"));
            PlannedHoursTab = new(driver, By.XPath("//a[@href='#planned-hours']/i"));
        }
    }
}
