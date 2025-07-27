using NUnit_Selenium.Utils;
using NUnit.Framework.Internal;
using NUnit_Selenium.POMs.Organisations;
using NUnit_Selenium.POMs.MonthEnd;
using NUnit_Selenium.Tests.Base;

namespace NUnit_Selenium.Tests.MonthEnd
{

    [TestFixture]
    public class Tests_Reports : NUnit_Baseclass
    {
        [TearDown]
        public void CheckFinalled()
        {
            int _iteration = 0;
            bool _finishedFinalling = false;
            AdvantageDB db = new("C:\\Data\\at\\pics.add");

            while (_iteration < 10 && _finishedFinalling == false)
            {
                _finishedFinalling = db.PeriodFinalled();
                _iteration++;
            }
            Assert.That(_finishedFinalling, Is.True, "Period finished finalling");
        }

        // PD-1795 - Basic Run
        [Test]
        public void BasicRun()
        {
            // Login and go to Month End -> Funding Calculator
            PICSActions.Login(test);

            MonthEnd_FundCalc FundCalc = new(test.driver, test.baseURL);
            FundCalc.GotoPage();

            // The number of rows in the grid indicates the most recently drafted/finalled period (3 rows means 3 periods drafted/finalled etc.)
            int numOfPeriods = FundCalc.PeriodGrid.CountRows();

            // Assuming the most recent period is always finalled. Data is controlled so it's a reasonable assumption.
            FundCalc.PeriodGrid.SelectMenuItem_ByText(numOfPeriods.ToString(), 2, "Re-Final");
            FundCalc.RefinalModal_Yes.Click();
            // Give it a few seconds...
            System.Threading.Thread.Sleep(200);
        }
    }
}