using OpenQA.Selenium;

namespace NUnit_Selenium.POMs
{
    class ForgotPassword : MasterPOM
    {
        private const string urlEnd = "Authenticate/Account/ForgotPassword";

        public ForgotPassword(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;
        }

        By _email = By.Id("Email");
        By _submit = By.Id("kt_login_signin_submit");
        By _sentEmailText = By.XPath("//p[contains(@class, 'text-muted font-weight-bold')]");
        By _emailError = By.Id("Email-error");

        public void EnterEmail(string email)
        {
            driver.FindElement(_email).SendKeys(email);
        }

        public void ClickSubmit()
        {
            driver.FindElement(_submit).Click();
        }

        public List<string> GetMessages()
        {
            IReadOnlyCollection<IWebElement> messages = driver.FindElements(_sentEmailText);
            List<string> messagesList = new List<string>();
            foreach (IWebElement item in messages)
            {
                messagesList.Add(item.Text);
            }
            return messagesList;
        }

        public bool EmailErrorVisible()
        {
            try
            {
                Assert.That(driver.FindElement(_emailError).Displayed, Is.True);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
