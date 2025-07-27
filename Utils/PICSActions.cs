using NUnit_Selenium.POMs;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit_Selenium.Utils
{
    //Library of commonly used sequences of Selenium actions
    public class PICSActions
    {
        public static void Login(PICSTest test)
        {
            LoginPage loginPage = new(test.driver, test.baseURL);
            Workspace workSpace = new(test.driver, test.baseURL);

            loginPage.GotoPage();
            loginPage.Enter_Username(test.credentials.username_valid);
            loginPage.Enter_Password(test.credentials.password_valid);
            loginPage.Click_Login();

            Assert.That(workSpace.IsCorrectURL(), Is.True, $"Is {workSpace.pageURL} loaded?");
        }

        public static void ScrollIntoView(IWebDriver driver, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(false);", element);
        }
    }
}
