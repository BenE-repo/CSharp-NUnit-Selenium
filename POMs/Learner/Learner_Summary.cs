using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs.Organisations
{
    class Learner_Summary : MasterPOM
    {
        private const string _urlEnd = "Learner/Default/Summary";
        private string _learnerID;
        
        readonly public ActionsMenu ActionMenu;

        public Learner_Summary(IWebDriver driver, string baseURL, string learnerID) : base(driver, baseURL)
        {
            _learnerID = learnerID;
            pageURL = baseURL + _urlEnd + "/" + _learnerID;
            ActionMenu = new(driver);
        }
    }
}
