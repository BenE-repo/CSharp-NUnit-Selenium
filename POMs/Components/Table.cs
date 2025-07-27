using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data.Common;
using static System.Net.Mime.MediaTypeNames;

namespace NUnit_Selenium.POMs.Components
{
    internal class Table
    {
        readonly private IWebDriver _driver;
        readonly private By _element;
        readonly private string _tableType;
        readonly private WebDriverWait _wait;
        public ReadOnlyCollection<IWebElement>? Rows;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public Table(IWebDriver driver, By element)
        {
            _driver = driver;
            _element = element;
            _tableType = "Simple";
            _wait = new(_driver, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        /// <param name="tableType"></param>
        public Table(IWebDriver driver, By element, string tableType)
        {
            _driver = driver;
            _element = element;
            _wait = new(_driver, TimeSpan.FromSeconds(30));
            _tableType = tableType; //Simple or Complex. Complex is grid with multi-select and Icons.

        }

        public void PopulateRows()
        {
            if (_tableType == "Complex")
            {
                _wait.Until(d => _driver.FindElement(By.XPath(".//tbody/tr")).Displayed);
                Rows = _driver.FindElements(By.XPath(".//tbody/tr"));
            }
            else
            {
                _wait.Until(d => _driver.FindElement(By.XPath(".//div/div/table/tbody/tr")).Displayed);
                Rows = _driver.FindElement(_element).FindElements(By.XPath(".//div/div/table/tbody/tr"));
            }
            
        }

        public bool FindRow(string text, int column, bool a_Tag_Needed)
        {
            PopulateRows();

            if (Rows != null)
            {
                foreach (IWebElement row in Rows)
                {
                    //TODO find a nicer way to do this. Some grids have an <a> tag beneath the <td>
                    if (a_Tag_Needed)
                    {
                        _wait.Until(d => _driver.FindElement(By.XPath($".//td[{column}]/a")).Displayed);
                        if (row.FindElement(By.XPath($".//td[{column}]/a")).Text == text)
                        {
                            return true;
                        }
                    }
                    if (!a_Tag_Needed)
                    {
                        _wait.Until(d => _driver.FindElement(By.XPath($".//td[{column}]")).Displayed);
                        if (row.FindElement(By.XPath($".//td[{column}]")).Text == text)
                        {
                            return true;
                        }
                    }
                }
            }
            

            return false;
        }

        public int CountRows()
        {
            PopulateRows();
            if (Rows != null)
            {
                return Rows.Count();
            }

            return 0;
            
        }

        public void SelectMenuItem(string rowText, int column, string menuItem)
        {
            PopulateRows();
            if (Rows != null)
            {
                foreach (IWebElement row in Rows)
                {
                    _wait.Until(d => _driver.FindElement(By.XPath($".//td[{column}]/a")).Displayed);
                    if (row.FindElement(By.XPath($".//td[{column}]/a")).Text == rowText)
                    {
                        _wait.Until(d => _driver.FindElement(By.XPath(".//td[4]/div/div/a/i")).Displayed);
                        row.FindElement(By.XPath(".//td[4]/div/div/a/i")).Click();
                        _wait.Until(d => _driver.FindElement(By.XPath(".//td[4]/div/div/div/a/i[contains(text(), 'Delete')]")).Displayed);
                        row.FindElement(By.XPath(".//td[4]/div/div/div/a/i[contains(text(), 'Delete')]")).Click();
                    }
                }
            }
            
        }
    }
}
