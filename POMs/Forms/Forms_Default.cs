using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs.Organisations
{
    class Forms_Default : MasterPOM
    {
        private const string _urlEnd = "Forms/Forms";

        readonly public ActionsMenu ActionMenu;
        readonly public Table_v2 Table;
        readonly public EditField Search_FormTitle;
        readonly public Button SearchButton;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="baseURL"></param>
        public Forms_Default(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + _urlEnd;
            ActionMenu = new(driver);
            Table = new(driver, By.XPath("//table[@id='formsTable']/tbody"), 5);
            Search_FormTitle = new(driver, By.Id("Text"));
            SearchButton = new(driver, SelConst.Button_Search);
        }
    }
}
