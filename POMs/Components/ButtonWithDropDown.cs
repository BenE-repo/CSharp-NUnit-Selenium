using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace NUnit_Selenium.POMs.Components
{
    internal class ButtonWithDropDown
    {
        readonly private IWebDriver _driver;
        readonly private By _mainElement;
        readonly private By _dropdownButton;
        readonly private By _dropdownOptions;
        readonly private WebDriverWait _wait;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="mainElement">The button part of the component</param>
        public ButtonWithDropDown(IWebDriver driver, By mainElement)
        {
            _driver = driver;
            _mainElement = mainElement;
            _dropdownButton = By.XPath(".//../../button[contains(@class,'dropdown-toggle')]");
            _dropdownOptions = By.XPath(".//../../div[contains(@class,'dropdown-menu')]/div/button");
            _wait = new(_driver, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Click the button
        /// </summary>
        public void ClickDefault()
        {
            _wait.Until(d => _driver.FindElement(_mainElement).Displayed);
            _driver.FindElement(_mainElement).Click();
        }

        /// <summary>
        /// Select option from the dropdown
        /// </summary>
        /// <param name="option">Visible text of option to select</param>
        public void ClickOption(string option)
        {
            ReadOnlyCollection<IWebElement> _options = _driver.FindElement(_mainElement).FindElements(_dropdownOptions);

            foreach (IWebElement button in _options)
            {
                if (button.GetAttribute("value") ==  option)
                {
                    _wait.Until(d => _driver.FindElement(_mainElement).FindElement(_dropdownButton).Displayed);
                    _driver.FindElement(_mainElement).FindElement(_dropdownButton).Click();
                    _wait.Until(d => button.Displayed);
                    button.Click();
                    break;
                }
            }
        }
    }
}
