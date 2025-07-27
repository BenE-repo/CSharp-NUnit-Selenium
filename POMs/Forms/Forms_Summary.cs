using Microsoft.VisualBasic;
using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs.Organisations
{
    class Forms_Summary : MasterPOM
    {
        private const string _urlEnd = "Forms/Forms/Summary";

        readonly public Button FillAndSign;

        public Forms_Summary(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + _urlEnd;

            FillAndSign = new(driver, By.XPath("//a[contains(@href,'FillAndSign')]"));
        }
    }
}
