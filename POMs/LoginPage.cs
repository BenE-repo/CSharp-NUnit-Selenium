using OpenQA.Selenium;

namespace NUnit_Selenium.POMs
{
    class LoginPage : MasterPOM
    {
        private const string urlEnd = "Authenticate/Account/Login";

        public LoginPage(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;
        }

        By _username = By.Id("UserName");
        By _password = By.Id("Password");
        By _Login = By.Id("kt_login_signin_submit");
        By _ForgotPassword = By.Id("kt_login_forgot");
        By _UsernameError = By.Id("UserName-error");
        By _PasswordError = By.Id("Password-error");
        By _validationSummary = By.XPath("//*[contains(@class, 'validation-summary-errors')]/ul/li");

        public void Enter_Username(string username)
        {
            driver.FindElement(_username).SendKeys(username);
        }

        public void Enter_Password(string password)
        {
            driver.FindElement(_password).SendKeys(password);
        }

        public void Click_Login()
        {
            driver.FindElement(_Login).Click();
        }

        public void Click_ForgotPassword()
        {
            driver.FindElement(_ForgotPassword).Click();
        }

        public bool IsUsernameErrorVisible()
        {
            try
            {
                Assert.That(driver.FindElement(_UsernameError).Displayed, Is.True);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsPasswordErrorVisible()
        {
            try
            {
                Assert.That(driver.FindElement(_PasswordError).Displayed, Is.True);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public string GetValidationMessages()
        {
            if (_validationSummary != null)
            {
                return driver.FindElement(_validationSummary).Text;
            }
            else
            {
                return "";
            }
        }
    }
}
