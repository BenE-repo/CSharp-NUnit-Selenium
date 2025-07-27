using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace NUnit_Selenium.POMs.Components
{
    internal class Alert
    {
        readonly private IWebDriver _driver;
        readonly private By _element;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="driver"></param>
        public Alert(IWebDriver driver)
        {
            _driver = driver;
            _element = By.XPath("//div[contains(@class,'sweet-alert')]");
        }

        /// <summary>
        /// Click a button on an alert. 
        /// </summary>
        /// <param name="text">Visible text of the button to be clicked</param>
        public void ClickButton(string text)
        {
            ReadOnlyCollection<IWebElement> elements = _driver.FindElement(_element).FindElements(By.XPath(".//button"));

            foreach (IWebElement element in elements)
            {
                if (element.Text == text)
                {
                    element.Click();
                }
            }
        }
    }
}
