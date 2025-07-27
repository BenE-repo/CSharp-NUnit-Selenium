using NUnit_Selenium.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections;
using System.Collections.ObjectModel;
using static System.Net.Mime.MediaTypeNames;

namespace NUnit_Selenium.POMs.Components
{
    internal class Button
    {
        readonly private IWebDriver _driver;
        readonly private By _element;
        readonly private WebDriverWait _wait;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public Button(IWebDriver driver, By element)
        {
            _driver = driver;
            _element = element;
            _wait = new(_driver, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Click the button
        /// </summary>
        public void Click()
        {
            PICSActions.ScrollIntoView(_driver, _driver.FindElement(_element));
            _wait.Until(d => _driver.FindElement(_element).Displayed);
            _driver.FindElement(_element).Click();
        }
    }
}
