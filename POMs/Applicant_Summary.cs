using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs
{
    class Applicant_Summary : MasterPOM
    {
        private const string _urlEnd = "Applicants/Default/Summary";
        private string _applicantID;

        readonly public ActionsMenu ActionMenu;

        public Applicant_Summary(IWebDriver driver, string baseURL, string applicantID) : base(driver, baseURL)
        {
            _applicantID = applicantID;
            pageURL = baseURL + _urlEnd + "/" + _applicantID;
            ActionMenu = new(driver);
        }
    }
}
