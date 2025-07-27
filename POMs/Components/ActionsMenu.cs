using NUnit_Selenium.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace NUnit_Selenium.POMs.Components
{
    internal class ActionsMenu
    {
        readonly private IWebDriver _driver;
        readonly private By _element;
        readonly private WebDriverWait _wait;

        /// <summary>
        /// Constructor. Assumes ActionsMenu has an id of 'mainActionsButton'
        /// </summary>
        /// <param name="driver"></param>
        public ActionsMenu(IWebDriver driver)
        {
            _driver = driver;
            _element = By.XPath("//button[@id='mainActionsButton']/..");
            _wait = new(_driver, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Constructor. Specify the element.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"> Parent of the button for the top-level action menu</param>
        public ActionsMenu(IWebDriver driver, By element)
        {
            _driver = driver;
            _element = element;
            _wait = new(_driver, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Select an action from the menu
        /// </summary>
        /// <param name="action">The visible text of the action</param>
        public void SelectAction(string action)
        {
            PICSActions.ScrollIntoView(_driver, _driver.FindElement(_element));
            _wait.Until(d => _driver.FindElement(_element).FindElement(By.XPath(".//button")).Displayed);
            _driver.FindElement(_element).FindElement(By.XPath(".//button")).Click();

            _wait.Until(d => _driver.FindElement(_element).FindElement(By.XPath($".//div/ul/li/a/span[contains(text(),'{action}')]")).Displayed);
            _driver.FindElement(_element).FindElement(By.XPath($".//div/ul/li/a/span[contains(text(),'{action}')]")).Click();
        }
    }
}
