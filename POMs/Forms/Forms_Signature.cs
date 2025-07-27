using Microsoft.VisualBasic;
using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs.Organisations
{
    class Forms_Signature : MasterPOM
    {
        private const string _urlEnd = "Forms/Forms/Summary";

        readonly public Canvas SignatureBox;
        readonly public Button Save;

        public Forms_Signature(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + _urlEnd;

            SignatureBox = new(driver, By.XPath("//canvas[contains(@class,'panel')]"));
            Save = new(driver, SelConst.Button_Save);
        }
    }
}
