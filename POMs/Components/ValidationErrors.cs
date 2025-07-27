using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace NUnit_Selenium.POMs.Components
{
    internal class ValidationErrors
    {
        //Class for the validation error elements on the page. So far it seems like they can all be identified in the same way
        //so this should work without much faff...
        readonly private IWebDriver _driver;
        readonly private By _elements;
        readonly private WebDriverWait _wait;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="driver"></param>
        public ValidationErrors(IWebDriver driver)
        {
            _driver = driver;
            _elements = By.XPath("//span[contains(@class,'field-validation-error')]");
            _wait = new(_driver, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Returns the number of elements
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            try
            {
                _wait.Until(d => _driver.FindElement(_elements).Displayed);
                return _driver.FindElements(_elements).Count();
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Returns true if the form has a validation with the passed in text
        /// </summary>
        /// <param name="data_valmsg_for"></param>
        /// <returns></returns>
        public bool FindError(string data_valmsg_for)
        {
            try
            {
                return _driver.FindElement(_elements).FindElement(By.XPath($"//span[@data-valmsg-for='{data_valmsg_for}']")).Displayed;
            }
            catch 
            {
                return false;
            }
        }
    }
}
