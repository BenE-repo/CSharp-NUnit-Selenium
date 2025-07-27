using NUnit_Selenium.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace NUnit_Selenium.POMs.Components
{
    internal class EditField
    {
        readonly private IWebDriver _driver;
        readonly private By _element;
        readonly private WebDriverWait _wait;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public EditField(IWebDriver driver, By element)
        {
            _driver = driver;
            _element = element;
            _wait = new(_driver, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Set the text in the edit field
        /// </summary>
        /// <param name="text"></param>
        /// <param name="skipWaitForValue">set to true for fields where we don't display the value (e.g. passwords)</param>
        public void SetText(string text, bool skipWaitForValue = false)
        {
            _wait.Until(d => _driver.FindElement(_element).Displayed);
            PICSActions.ScrollIntoView(_driver, _driver.FindElement(_element));
            _driver.FindElement(_element).SendKeys(text);
            
            // We usually want to wait until the field has been entered until proceeding, but want to have
            // the option to skip the check for things like passwords.
            if (!skipWaitForValue)
            {
                _wait.Until(d => _driver.FindElement(_element).GetAttribute("value") == text);
            }
        }

        /// <summary>
        /// Returns the text in the field
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            _wait.Until(d => _driver.FindElement(_element).Displayed);
            return _driver.FindElement(_element).GetAttribute("value");
        }

        /// <summary>
        /// Clearns the text in the field
        /// </summary>
        public void ClearText()
        {
            _wait.Until(d => _driver.FindElement(_element).Displayed);
            _driver.FindElement(_element).Clear();
        }
    }
}
