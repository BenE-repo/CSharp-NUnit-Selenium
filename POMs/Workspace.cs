using OpenQA.Selenium;

namespace NUnit_Selenium.POMs
{
    class Workspace : MasterPOM
    {
        private const string urlEnd = "Workspace/Home";

        public Workspace(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;
        }
    }
}
