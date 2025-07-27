using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace NUnit_Selenium.POMs.Components
{
    internal class Form
    {
        readonly private IWebDriver _driver;
        readonly private By _element;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public Form(IWebDriver driver, By element)
        {
            _driver = driver;
            _element = element;
        }

        /// <summary>
        /// Checks if the form has finished loading
        /// </summary>
        /// <returns></returns>
        public bool FormIsLoaded()
        {
            try
            {
                IWebElement _form = _driver.FindElement(_element);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
