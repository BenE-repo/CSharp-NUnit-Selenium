using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

// Second attempt at implementing a table. Will keep the old one for now, until the existing objects can be updated.
namespace NUnit_Selenium.POMs.Components
{
    internal class Table_v2
    {
        readonly private IWebDriver _driver;
        readonly private By _element;
        readonly private int _menuColumn;
        readonly private WebDriverWait _wait;
        public ReadOnlyCollection<IWebElement>? Rows;

        /// <summary>
        /// Initialises a new instance of Table_v2
        /// </summary>
        /// <param name="driver">Reference to IWebDriver</param>
        /// <param name="element">Reference to 'By' that points to the tbody of the table</param>
        /// <param name="menuColumn">The index of the td element of the tables menu</param>
        public Table_v2(IWebDriver driver, By element, int menuColumn)
        {
            _driver = driver;
            _element = element;
            _wait = new(_driver, TimeSpan.FromSeconds(30));
            _menuColumn = menuColumn;
        }

        /// <summary>
        /// Populates the Rows object with IWebElements for each visible row in the grid
        /// </summary>
        private void PopulateRows()
        {
            System.Threading.Thread.Sleep(1000);
            _wait.Until(d => _driver.FindElement(_element).Displayed);
            Rows = _driver.FindElement(_element).FindElements(By.XPath(".//tr"));
        }

        /// <summary>
        /// Reports if row specified exists in table
        /// </summary>
        /// <param name="text">Text to search for in cell</param>
        /// <param name="column">the column (td) to search in</param>
        /// <param name="isLink">true if the column/text being searched is a link to another record</param>
        /// <returns></returns>
        public bool FindRow(string text, int column, bool isLink = false)
        {
            PopulateRows();

            if (Rows != null)
            {
                // Clever or bodge? If the thing we're finding is a link then the xpath needs to be slightly different 
                // so set up a var which will add the extra node if necessary
                string extraXPathForLink = "";
                if (isLink) { extraXPathForLink = "/a"; }

                foreach (IWebElement row in Rows)
                {
                    _wait.Until(d => row.Displayed);
                    if (row.FindElement(By.XPath($".//td[{column}]{extraXPathForLink}")).Text == text)
                    {
                        return true;
                    }
                }
            }
            

            return false;
        }

        /// <summary>
        /// Returns number of visible rows in grid
        /// </summary>
        /// <returns></returns>
        public int CountRows()
        {
            PopulateRows();

            if (Rows != null)
            {
                return Rows.Count();
            }
            
            return 0;
        }

        /// <summary>
        /// Clicks on a meatball menu item for a given row in a grid
        /// </summary>
        /// <param name="rowText">The text used to search for the correct row</param>
        /// <param name="column">The column in the grid the search text appears in. NOT zero-indexed</param>
        /// <param name="menuItem">The menu item to click on</param>
        /// <param name="isLink">Whether the cell searched for is a link to another record</param>
        /// <exception cref="NoSuchElementException"></exception>
        public void SelectMenuItem_ByText(string rowText, int column, string menuItem, bool isLink = false)
        {
            PopulateRows();

            if (Rows != null)
            {
                foreach (IWebElement row in Rows)
                {
                    _wait.Until(d => row.Displayed);

                    // Finding a cell with a link to another record requires a slightly different XPath, hence isLink.
                    if (isLink)
                    {
                        if (row.FindElement(By.XPath($".//td[{column}]/a")).Text == rowText)
                        {
                            // Columns are for learner/default
                            _wait.Until(d => row.FindElement(By.XPath($".//td[{_menuColumn}]/div/div/a/i")).Displayed);
                            row.FindElement(By.XPath($".//td[{_menuColumn}]/div/div/a/i")).Click();

                            _wait.Until(d => row.FindElement(By.XPath($".//td[{_menuColumn}]/div/div/div/a[contains(.,'{menuItem}')]")).Displayed);
                            row.FindElement(By.XPath($".//td[{_menuColumn}]/div/div/div/a[contains(.,'{menuItem}')]")).Click();

                            return;
                        }
                    }
                    else
                    {
                        if (row.FindElement(By.XPath($".//td[{column}]")).Text == rowText)
                        {
                            // Columns are for learner/default
                            _wait.Until(d => row.FindElement(By.XPath($".//td[{_menuColumn}]/div/div/a/i")).Displayed);
                            row.FindElement(By.XPath($".//td[{_menuColumn}]/div/div/a/i")).Click();

                            _wait.Until(d => row.FindElement(By.XPath($".//td[{_menuColumn}]/div/div/div/a[contains(.,'{menuItem}')]")).Displayed);
                            row.FindElement(By.XPath($".//td[{_menuColumn}]/div/div/div/a[contains(.,'{menuItem}')]")).Click();

                            return;
                        }
                    }
                    
                }
            }

            // If we get this far in the method either the grid is empty or the record we're looking for doesn't exist, so error
            throw new NoSuchElementException();
            
        }

        /// <summary>
        /// Select a meatball menu item, finding the row by the ID of the record
        /// </summary>
        /// <param name="recordID"></param>
        /// <param name="menuItem"></param>
        /// <exception cref="NoSuchElementException"></exception>
        public void SelectMenuItem_ByRecordID(string recordID, string menuItem)
        {
            IWebElement rowToUse = _driver.FindElement(By.Id($"record_{recordID}"));
            if (rowToUse != null)
            {
                _wait.Until(d => rowToUse.FindElement(By.XPath($".//td[{_menuColumn}]/div/div/a/i")).Displayed);
                rowToUse.FindElement(By.XPath($".//td[{_menuColumn}]/div/div/a/i")).Click();

                _wait.Until(d => rowToUse.FindElement(By.XPath($".//td[{_menuColumn}]/div/div/div/a[contains(.,'{menuItem}')]")).Displayed);
                rowToUse.FindElement(By.XPath($".//td[{_menuColumn}]/div/div/div/a[contains(.,'{menuItem}')]")).Click();
            }
            else
            {
                throw new NoSuchElementException();
            }
        }

        /// <summary>
        /// Return the text of the given cell index for the grid
        /// </summary>
        /// <param name="ColumnIndex"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        public string GetColumnText(int ColumnIndex)
        {
            String _columnText = "";
            PopulateRows();

            if (Rows != null)
            {
                // Should only be needed for single rows for now, so error if there isn't.
                if (Rows.Count == 1)
                {
                    foreach (IWebElement row in Rows)
                    {
                        _columnText = row.FindElement(By.XPath($".//td[{ColumnIndex}]")).Text;
                    }
                }
                else
                {
                    throw new InvalidDataException();
                }
            }

            return _columnText;
        }
    }
}
