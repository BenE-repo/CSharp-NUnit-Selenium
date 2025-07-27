using OpenQA.Selenium;

namespace NUnit_Selenium.POMs
{
    class ThankYouPage : MasterPOM
    {
        private const string urlEnd = "Guest/SignUp/Complete/";

        public ThankYouPage(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;
        }

        By _thankYouText = By.XPath($"//div[contains(.,'Thank You')]");
        
        public bool IsThankYouTextVisible()
        {
            try
            {
                Assert.That(driver.FindElement(_thankYouText).Displayed, Is.True);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
