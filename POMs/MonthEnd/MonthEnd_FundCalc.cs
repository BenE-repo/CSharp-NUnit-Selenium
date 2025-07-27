using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using NUnit_Selenium.POMs.Components;

namespace NUnit_Selenium.POMs.MonthEnd
{
    class MonthEnd_FundCalc : MasterPOM
    {
        private const string urlEnd = "MonthEnd/Funding";

        readonly public ActionsMenu ActionMenu;
        public readonly Table_v2 PeriodGrid;
        public readonly Button RefinalModal_Yes;

        public MonthEnd_FundCalc(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            ActionMenu = new(driver);
            PeriodGrid = new(driver, By.XPath("//table/tbody"), 8);
            RefinalModal_Yes = new(driver, By.XPath("//div[contains(@class, 'sweet-alert')]/div[@class = 'sa-button-container']/div[@class = 'sa-confirm-button-container']/button"));
        }
    }
}
