using NUnit_Selenium.POMs;
using NUnit_Selenium.Utils;
using NUnit.Framework.Internal;
using NUnit_Selenium.Tests.Base;

namespace NUnit_Selenium.Tests.LoginPages
{
    public class Tests_LoginPage : NUnit_Baseclass
    {
        public Credentials Credentials;

        [SetUp]
        public void CreateCredentials()
        {
            // Can't use the PICSTest credentials here as we want to fiddle with them
            Credentials = new Credentials();
        }

        [Test]
        [Parallelizable]
        public void NoCredentials()
        {
            LoginPage loginPage = new(test.driver, test.baseURL);

            loginPage.GotoPage();
            loginPage.Click_Login();

            Assert.Multiple(() =>
            {
                Assert.That(loginPage.IsUsernameErrorVisible(), Is.True, "Username validation message visibility");
                Assert.That(loginPage.IsPasswordErrorVisible(), Is.True, "Password validation message visibility");
            });
        }

        [Test]
        [Parallelizable]
        public void NoUsername()
        {
            LoginPage loginPage = new(test.driver, test.baseURL);

            loginPage.GotoPage();
            loginPage.Enter_Password(Credentials.password_valid);
            loginPage.Click_Login();
            Assert.Multiple(() =>
            {
                Assert.That(loginPage.IsUsernameErrorVisible(), Is.True, "Username validation message visibility");
                Assert.That(loginPage.IsPasswordErrorVisible(), Is.False, "Password validation message visibility");
            });
        }

        [Test]
        [Parallelizable]
        public void NoPassword()
        {
            LoginPage loginPage = new(test.driver, test.baseURL);

            loginPage.GotoPage();
            loginPage.Enter_Username(Credentials.username_valid);
            loginPage.Click_Login();
            Assert.Multiple(() =>
            {
                Assert.That(loginPage.IsUsernameErrorVisible(), Is.False, "Username validation message visibility");
                Assert.That(loginPage.IsPasswordErrorVisible(), Is.True, "Password validation message visibility");
            });
        }

        [Test]
        [Parallelizable]
        public void ValidCredentials()
        {
            LoginPage loginPage = new(test.driver, test.baseURL);
            Workspace workSpace = new(test.driver, test.baseURL);

            loginPage.GotoPage();
            loginPage.Enter_Username(Credentials.username_valid);
            loginPage.Enter_Password(Credentials.password_valid);
            loginPage.Click_Login();

            Assert.That(workSpace.IsCorrectURL(), Is.True, $"Is {workSpace.pageURL} loaded?");
        }

        [Test]
        [Parallelizable]
        public void SuspendedCredentials()
        {
            LoginPage loginPage = new(test.driver, test.baseURL);

            loginPage.GotoPage();
            loginPage.Enter_Username(Credentials.username_invalid);
            loginPage.Enter_Password(Credentials.password_invalid);
            loginPage.Click_Login();

            Assert.That(loginPage.GetValidationMessages(), Is.EqualTo("Username or Password invalid"));
        }

        [Test]
        [Parallelizable]
        public void ForgotPasswordExistingUser()
        {
            LoginPage loginPage = new(test.driver, test.baseURL);
            ForgotPassword forgotPassword = new(test.driver, test.baseURL);

            loginPage.GotoPage();
            loginPage.Click_ForgotPassword();
            forgotPassword.EnterEmail(Credentials.valid_existing_email);
            forgotPassword.ClickSubmit();
            List<string> validationMessages = forgotPassword.GetMessages();

            Assert.Multiple(() =>
            {
                Assert.That(validationMessages, Has.Count.EqualTo(2), "Number of messages");
                Assert.That(validationMessages[0],
                    Is.EqualTo("If the email address is linked to a valid user account, a password reset link has been emailed to you."));
                Assert.That(validationMessages[1],
                    Is.EqualTo("Please click the link in the email to reset your password."));
            });
        }

        [Test]
        [Parallelizable]
        public void ForgotPasswordNonExistantUser()
        {
            LoginPage loginPage = new(test.driver, test.baseURL);
            ForgotPassword forgotPassword = new(test.driver, test.baseURL);

            loginPage.GotoPage();
            loginPage.Click_ForgotPassword();
            forgotPassword.EnterEmail(Credentials.valid_nonexistant_email);
            forgotPassword.ClickSubmit();
            List<string> validationMessages = forgotPassword.GetMessages();

            Assert.Multiple(() =>
            {
                Assert.That(validationMessages, Has.Count.EqualTo(2), "Number of messages");
                Assert.That(validationMessages[0],
                    Is.EqualTo("If the email address is linked to a valid user account, a password reset link has been emailed to you."));
                Assert.That(validationMessages[1],
                    Is.EqualTo("Please click the link in the email to reset your password."));
            });
        }

        [Test]
        [Parallelizable]
        public void ForgotPasswordInvalidEmailAddress()
        {
            LoginPage loginPage = new(test.driver, test.baseURL);
            ForgotPassword forgotPassword = new(test.driver, test.baseURL);

            loginPage.GotoPage();
            loginPage.Click_ForgotPassword();
            forgotPassword.EnterEmail(Credentials.invalid_email);
            forgotPassword.ClickSubmit();

            Assert.That(forgotPassword.EmailErrorVisible(), Is.True, "Invalid email error message visible");
        }
    }
}