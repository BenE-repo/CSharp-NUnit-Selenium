using NUnit_Selenium.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace NUnit_Selenium.POMs.Components
{
    internal class ComboList
    {
        readonly private IWebDriver _driver;
        readonly private By _baseElement;
        readonly private WebDriverWait _wait;
        private ReadOnlyCollection<IWebElement>? _comboItems;
        readonly private int _indexColumn;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element">pointer to tbody element</param>
        /// <param name="indexColumn">Column to be used to search for items</param>
        public ComboList(IWebDriver driver, By element, int indexColumn)
        {
            _driver = driver;
            _baseElement = element;
            _comboItems = null;
            _indexColumn = indexColumn;


            _wait = new(_driver, TimeSpan.FromSeconds(5));
        }

        /// <summary>
        /// Goes through the list of items in the combolist and adds them to an object for future use
        /// </summary>
        private void PopulateComboItems()
        {
            // Just exit if there are already items in the list
            if (_comboItems != null)
            {
                return;
            }

            _comboItems = _driver.FindElement(_baseElement).FindElements(By.XPath(".//tr"));

        }

        /// <summary>
        /// Select item from list
        /// </summary>
        /// <param name="searchCode">Code of item to select</param>
        public void SelectItem(string searchCode)
        {
            PopulateComboItems();

            if (_comboItems != null)
            {
                foreach (IWebElement item in _comboItems)
                {
                    System.Diagnostics.Debug.WriteLine("loop");
                    System.Threading.Thread.Sleep(500);
                    if (item.FindElement(By.XPath($".//td[{_indexColumn}]/span")).Text == searchCode)
                    {
                        System.Diagnostics.Debug.WriteLine("in if");
                        item.FindElement(By.XPath($".//td[1]")).Click();
                        // No way to tell in the DOM if an item has been selected so have to sleep to give it time to work. Bleugh.
                        System.Threading.Thread.Sleep(1000);
                        break;
                    }
                }
            }
        }
    }
}
