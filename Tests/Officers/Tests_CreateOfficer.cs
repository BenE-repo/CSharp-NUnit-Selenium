using NUnit_Selenium.Utils;
using NUnit.Framework.Internal;
using NUnit_Selenium.POMs.Officers;
using NUnit_Selenium.Tests.Base;

namespace NUnit_Selenium.Tests.Officers
{
    public class Tests_CreateOfficer : NUnit_Baseclass
    {
        // PD-1418 - Create with Empty Form
        [Test]
        [Parallelizable]
        public void EmptyOfficerInitialForm()
        {
            Officers_Default officers_Default = new(test.driver, test.baseURL);
            Officers_CreateForm createForm = new(test.driver, test.baseURL);

            PICSActions.Login(test);
            officers_Default.GotoPage();
            officers_Default.CreateBtn.Click();

            createForm.Save.ClickDefault();

            Assert.Multiple(() =>
            {
                Assert.That(createForm.IsFirstnamesErrorVisible(), Is.True, "Firstname Error Visible");
                Assert.That(createForm.IsSurnameErrorVisible(), Is.True, "Surname Error Visible");
                Assert.That(createForm.IsRolesErrorVisible(), Is.True, "Role Error Visible");
            });
        }

        // PD-1419 - Create with Reasonable Data
        [Test]
        [Parallelizable]
        public void OfficerCreate()
        {
            Officers_Default officers_Default = new(test.driver, test.baseURL);
            Officers_CreateForm createForm = new(test.driver, test.baseURL);
            Officers_EditForm editForm = new(test.driver, test.baseURL);

            PICSActions.Login(test);
            officers_Default.GotoPage();
            officers_Default.CreateBtn.Click();

            createForm.Firstnames.SetText("Officer");
            createForm.WaitUntilFinishedPotentialMatchSearch();
            createForm.Surname.SetText("Test");

            List<string> SelectedRoles = new() { "Main Adviser" };
            createForm.Roles.SelectOption(SelectedRoles);

            createForm.Save.ClickDefault();

            Assert.Multiple(() =>
            {
                Assert.That(createForm.IsFirstnamesErrorVisible(), Is.False, "Firstname Error Visible");
                Assert.That(createForm.IsSurnameErrorVisible(), Is.False, "Surname Error Visible");
                Assert.That(createForm.IsRolesErrorVisible(), Is.False, "Roles Error Visible");
            });
        }

        // PD-1420 - Edit Officer
        [Test]
        [Parallelizable]
        public void EditOfficer()
        {
            PICSActions.Login(test);

            Officers_Default officers_Default = new(test.driver, test.baseURL);
            officers_Default.GotoPage();
            officers_Default.CreateBtn.Click();

            System.Threading.Thread.Sleep(2000);
            Officers_CreateForm createForm = new(test.driver, test.baseURL);
            createForm.Firstnames.SetText("Officer");
            createForm.WaitUntilFinishedPotentialMatchSearch();
            createForm.Surname.SetText("Test");

            List<string> SelectedRoles = new() { "Main Adviser" };
            createForm.Roles.SelectOption(SelectedRoles);

            createForm.Save.ClickOption("Summary");

            Officers_Summary summary = new(test.driver, test.baseURL);
            summary.SitesTab.Click();
            summary.Sites_Edit.Click();

            System.Threading.Thread.Sleep(2000);
            Officers_Site_Edit siteEdit = new(test.driver, test.baseURL);
            siteEdit.ToggleToggle();
            System.Threading.Thread.Sleep(1000);
            siteEdit.SelectSite("SITEA");
            System.Threading.Thread.Sleep(1000);
            siteEdit.Save.Click();
            System.Threading.Thread.Sleep(1000);

            // It was asserting before the grid had loaded, even when waiting for the page to report that it
            // had finished loading, so doing it this ugly way instead. Keeping the assert there for consistancy
            test.wait.Until(d => summary.Sites_Grid.FindRow("SITEA", 1));
            Assert.That(summary.Sites_Grid.FindRow("SITEA", 1), Is.True, "Site in summary grid");
        }
    }
}