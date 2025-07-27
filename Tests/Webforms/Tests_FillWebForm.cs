using NUnit_Selenium.Utils;
using NUnit.Framework.Internal;
using NUnit_Selenium.Tests.Base;
using NUnit_Selenium.POMs.Organisations;

namespace NUnit_Selenium.Tests.Webforms
{
    [TestFixture]
    public class Tests_FillWebForm : NUnit_Baseclass
    {
        // PD-1829 - Fill Sample Form
        [Test]
        // [Parallelizable]
        public void FillAndSignSimpleForm()
        {
            // Login and go to Forms List
            PICSActions.Login(test);

            Forms_Default FormsList = new(test.driver, test.baseURL);
            FormsList.GotoPage();

            // Create Form
            FormsList.ActionMenu.SelectAction("Create WebForm");
            
            Forms_CreateFormInstance Create = new(test.driver, test.baseURL);
            Create.Form.SelectOption("Form One");
            Create.Owner.SelectOption("Ben Test");
            Create.Save.Click();

            // Fill & Sign from the Summary (summary opens automatically after submitting)
            Forms_Summary Summary = new(test.driver, test.baseURL);
            Summary.FillAndSign.Click();

            Forms_FormOneFillAndSign FormOne = new(test.driver, test.baseURL);
            String _surname = "Surname" + DateTime.Now.ToString("yyyyMMddHHmm"); // Just in case it's run at 23:59:59 for some reason...
            FormOne.Surname.SetText(_surname);
            FormOne.Firstname.SetText("Firstname");
            FormOne.LearnerSig.Click();

            Forms_Signature Sig = new(test.driver, test.baseURL);
            Sig.SignatureBox.Draw();
            Sig.Save.Click();

            FormOne.Action.SelectAction("Submit Form");

            // Check Status
            FormsList.Search_FormTitle.SetText(_surname);
            FormsList.SearchButton.Click();
            Assert.Multiple(() =>
            {
                Assert.That(FormsList.Table.CountRows(), Is.EqualTo(1), "Only One Result in Table");
                Assert.That(FormsList.Table.GetColumnText(3), Is.EqualTo("Submitted"), "Form in Submitted Status");
            });
        }

        /// <summary>
        /// PD-1908 - Fill, sign and check more complicated learner webform
        /// </summary>
        [Test]
        public void FillAndSignFullForm()
        {
            // Login and go to Forms List
            PICSActions.Login(test);

            Forms_Default FormsList = new(test.driver, test.baseURL);
            FormsList.GotoPage();

            // Create Form
            FormsList.ActionMenu.SelectAction("Create WebForm");

            Forms_CreateFormInstance Create = new(test.driver, test.baseURL);
            Create.Form.SelectOption("Form One");
            Create.Owner.SelectOption("Ben Test");
            Create.Save.Click();

            // Fill & Sign from the Summary (summary opens automatically after submitting)
            Forms_Summary Summary = new(test.driver, test.baseURL);
            Summary.FillAndSign.Click();

            Forms_FormOneFillAndSign FormOne = new(test.driver, test.baseURL);
            String _surname = "Surname" + DateTime.Now.ToString("yyyyMMddHHmm"); // Just in case it's run at 23:59:59 for some reason...
            FormOne.Surname.SetText(_surname);
            FormOne.Firstname.SetText("Firstname");
            FormOne.TextField.SetText("TextField Text");
            FormOne.EmailField.SetText("aa@aa.com");
            FormOne.NumberField.SetText("1");
            FormOne.DateField.SetText("01/01/2000");
            FormOne.TimeField.SetText("11:59");
            FormOne.CheckboxField.Click();
            FormOne.SelectField.SelectOption("opt2");
            FormOne.InitialField.Click();
            FormOne.InitialField.Sign();

            // Sign the form - TODO: change this to be signature component instead
            FormOne.LearnerSig.Click();
            Forms_Signature Sig = new(test.driver, test.baseURL);
            Sig.SignatureBox.Draw();
            Sig.Save.Click();

            FormOne.Action.SelectAction("Submit Form");

            // Check Status
            FormsList.Search_FormTitle.SetText(_surname);
            FormsList.SearchButton.Click();
            Assert.Multiple(() =>
            {
                Assert.That(FormsList.Table.CountRows(), Is.EqualTo(1), "Only One Result in Table");
                Assert.That(FormsList.Table.GetColumnText(3), Is.EqualTo("Submitted"), "Form in Submitted Status");
            });
        }
    }   
}