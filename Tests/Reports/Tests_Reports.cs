using NUnit_Selenium.Utils;
using NUnit.Framework.Internal;
using NUnit_Selenium.POMs.Organisations;
using NUnit_Selenium.POMs.Reports.ILR_Learners;
using NUnit_Selenium.Tests.Base;

namespace NUnit_Selenium.Tests.Reports
{
    public class ReportList
    {
        public static List<String> reports = new();
        public static AdvantageDB db = new("C:\\Data\\at\\pics.add"); // TODO: Make nicer
    }

    [SetUpFixture]
    public class Test_SetupTearDown
    {

        [OneTimeSetUp]
        public void Setup()
        {
            // Remove all reports that are currently in the db.
            ReportList.db.DeleteAllReports();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (ReportList.reports.Count > 0)
            {
                foreach (string report in ReportList.reports)
                {
                    bool _reportFinished = false;
                    int _iteration = 0;

                    // Check if the report has finished running yet, if not wait for 10 secs and try again until we hit 10 tries.
                    while (_reportFinished == false && _iteration < 10)
                    {
                        _reportFinished = ReportList.db.CheckReportFinished(report);
                        if (!_reportFinished) System.Threading.Thread.Sleep(10000);
                        _iteration++;
                    }

                    Assert.That(_reportFinished, Is.True, $"Report {report} has finished");
                }
            }
        }
    }

    [TestFixture]
    public class Tests_Reports : NUnit_Baseclass
    {
        // PD-1671 - ILR Learners Report
        [Test]
        [Parallelizable]
        public void ApplicantReport()
        {
            // Login and go to reports page
            PICSActions.Login(test);

            Reports_Home reports = new(test.driver, test.baseURL);
            reports.GotoPage("#templates");

            // FInd and open ILR Learners Report
            reports.Template_SearchField.SetText("Learner ILR");
            reports.Template_SearchButton.Click();
            reports.Template_Grid.SelectMenuItem_ByText("ILR Learners Report", 1, "Run");

            // Run Report
            ILRLearnersReport ILRreport = new(test.driver);
            ILRreport.Footer_RunNow.Click();

            // Need to sleep to allow the toast to pop up and disappear. TODO: Might be a better way to do this.
            Thread.Sleep(3000);

            // Flip over to 'available reports' and refresh
            reports.Tab_AvailableReports.Click();
            test.driver.Navigate().Refresh();

            // Assert that it's done SOMETHING. Actual checking to come later.
            Assert.That(reports.Available_Grid.FindRow("ILR Learners Report", 1), Is.True, "ILR Learner Report is present in the table");

            // If we get this far it's worked, so add the report to the list for checking later.
            ReportList.reports.Add("ILR Learners Report");
        }

        
    }
}