using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace NUnit_Selenium.POMs.Components
{
    internal class MultiSelect
    //Bleugh. The selectable items don't exist until the container has been clicked on, and are different elements to the container
    //so have to click on the container first then select the option, rather than doing it nicely.
    {
        readonly private IWebDriver _driver;
        readonly private By _container;
        readonly private By _options;
        readonly private WebDriverWait _wait;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="container"></param>
        /// <param name="options"></param>
        public MultiSelect(IWebDriver driver, By container, By options)
        {
            _driver = driver;
            _container = container;
            _options = options;
            _wait = new(_driver, TimeSpan.FromSeconds(30));
        }
        
        /// <summary>
        /// Select Option
        /// </summary>
        /// <param name="options"></param>
        public void SelectOption(List<String> options)
        {
            //Click the container so the list opens
            _wait.Until(d => _driver.FindElement(_container).Displayed);
            _driver.FindElement(_container).Click();

            //Select the options
            foreach (String option in options)
            {
                _wait.Until(d => _driver.FindElement(_options).FindElement(By.XPath($"//li[contains(.,'{option}')]")).Displayed);
                _driver.FindElement(_options).FindElement(By.XPath($"//li[contains(.,'{option}')]")).Click();
            }

            //Click the contianer again to close it (can cause unpredictable behaviour with Selenium if left open)
            _wait.Until(d => _driver.FindElement(_container).Displayed);
            _driver.FindElement(_container).Click();
        }

        /// <summary>
        /// Returns a list of the values selected
        /// </summary>
        /// <returns></returns>
        public List<string> GetSelectedValues()
        {
            List<string> result = new();
            _wait.Until(d => _driver.FindElement(_container).FindElement(By.XPath(".//li[@class='select2-selection__choice']")).Displayed);
            ReadOnlyCollection<IWebElement> selectedValues = _driver.FindElement(_container).FindElements(By.XPath(".//li[@class='select2-selection__choice']"));

            foreach (IWebElement value in selectedValues)
            {
                result.Add(value.GetAttribute("title"));
            }

            return result;
        }
    }
}
