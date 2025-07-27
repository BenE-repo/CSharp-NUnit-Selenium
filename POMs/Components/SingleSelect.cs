using NUnit_Selenium.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Collections;
using System.Collections.ObjectModel;

namespace NUnit_Selenium.POMs.Components
{
    internal class SingleSelect
    //Bleugh. The selectable items don't exist until the container has been clicked on, and are different elements to the container
    //so have to click on the container first then select the option, rather than doing it nicely.
    {
        readonly private IWebDriver _driver;
        readonly private By _container;
        readonly private By? _results;
        readonly private WebDriverWait _wait;
        readonly private Boolean _hasID; // Indicates the results also displays the code of the entry (as it changes the XPath needed to get the value)
        readonly private Boolean _isActuallyASelect = false; // Bodge to avoid having a different component. In a few instances we actually use a normal select
                                                             // so this bool with represent that.

        /// <summary>
        /// Constructor for when the container node and the results node have different names. Specify each individually
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="container">Path to the container node</param>
        /// <param name="results">Path to the results node</param>
        public SingleSelect(IWebDriver driver, By container, By results, Boolean hasID = false)
        {
            _driver = driver;
            _container = container;
            _results = results;
            _wait = new(_driver, TimeSpan.FromSeconds(30));
            _hasID = hasID;
        }

        /// <summary>
        /// Constructor for when the container and results nodes have the same ID
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="ID"></param>
        /// <param name="hasID"></param>
        public SingleSelect(IWebDriver driver, String ID, Boolean hasID = false)
        {
            _driver = driver;
            _container = By.Id($"select2-{ID}-container");
            _results = By.Id($"select2-{ID}-results");
            _wait = new(_driver, TimeSpan.FromSeconds(30));
            _hasID = hasID;
        }

        /// <summary>
        /// Constructor for those odd times when a single select is actually a select.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="container"></param>
        public SingleSelect(IWebDriver driver, By container, Boolean isSelect)
        {
            _driver = driver;
            _container = container;
            _wait = new(_driver, TimeSpan.FromSeconds(30));
            _isActuallyASelect = isSelect;
        }

        /// <summary>
        /// Select Option
        /// </summary>
        /// <param name="option">Visible text of option to be selected</param>
        public void SelectOption(String option)
        {
            if (_isActuallyASelect)
            {
                IWebElement _element = _driver.FindElement(_container);
                PICSActions.ScrollIntoView(_driver, _element);
                SelectElement _select = new(_element);
                _select.SelectByText(option);
                
            }
            else
            {
                PICSActions.ScrollIntoView(_driver, _driver.FindElement(_container));
                _driver.FindElement(_container).Click();
                if (_hasID)
                {
                    //Is the component with the ID/Code
                    _wait.Until(d => _driver.FindElement(_results).FindElement(By.XPath($".//li/span/div[contains(.,'{option}')]/span")).Displayed);
                    _driver.FindElement(_results).FindElement(By.XPath($".//li/span/div[contains(.,'{option}')]/span")).Click();
                }
                else
                {
                    //Isn't the component with the ID/Code
                    _wait.Until(d => _driver.FindElement(_results).FindElement(By.XPath($".//li[contains(text(), '{option}')]")).Displayed);
                    _driver.FindElement(_results).FindElement(By.XPath($".//li[contains(text(), '{option}')]")).Click();
                }
            }
        }

        /// <summary>
        /// Returns the value selected
        /// </summary>
        /// <returns></returns>
        public string GetSelectedValue()
        {
            PICSActions.ScrollIntoView(_driver, _driver.FindElement(_container));
            _wait.Until(d => _driver.FindElement(_container).Displayed);
            IWebElement selectedElement = _driver.FindElement(_container);
            if (_hasID)
            {
                return selectedElement.GetAttribute("title");
            }
            else
            {
                return selectedElement.Text;
            }
            
        }

        /// <summary>
        /// Checks if the component is visible
        /// </summary>
        /// <returns></returns>
        public bool IsVisible()
        {
            try
            {
                PICSActions.ScrollIntoView(_driver, _driver.FindElement(_container));
                return true;
            }
            catch
            { 
                return false;
            }   
        }
    }
}
