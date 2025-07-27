using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using NUnit_Selenium.POMs.Components;

namespace NUnit_Selenium.POMs.Reports.Base
{
    abstract class Reports_Base : MasterPOM
    {
        public readonly Button Footer_RunNow;

        public Reports_Base(IWebDriver driver) : base(driver)
        {
            Footer_RunNow = new(driver, By.XPath("//div[@class='modal-footer']/div/div/button[contains(.,'Run Now')]/i"));
        }
    }
}
