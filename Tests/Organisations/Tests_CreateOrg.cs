using NUnit_Selenium.Utils;
using NUnit.Framework.Internal;
using NUnit_Selenium.POMs.Organisations;
using NUnit_Selenium.POMs;
using NUnit_Selenium.Tests.Base;

namespace NUnit_Selenium.Tests.Organisations
{
    public class Tests_CreateOrg : NUnit_Baseclass
    {
        // PD-1413 - Create with Empty Form
        [Test]
        [Parallelizable]
        public void EmptyOrgInitialForm()
        {
            Orgs_Default orgs_Default = new(test.driver, test.baseURL);
            Orgs_CreateForm createForm = new(test.driver, test.baseURL);

            PICSActions.Login(test);
            orgs_Default.GotoPage();
            orgs_Default.CreateBtn.Click();

            createForm.Save.ClickDefault();

            Assert.Multiple(() =>
            {
                Assert.That(createForm.errors.Count(), Is.EqualTo(1), "Number of validation errors");
                Assert.That(createForm.errors.FindError("Name"), Is.True, "Validation error for Name present");
            });

        }

        // PD-1414 - Create with Reasonable Data
        [Test]
        [Parallelizable]
        public void FullCreate()
        {
            Orgs_Default orgs_Default = new(test.driver, test.baseURL);
            Orgs_CreateForm createForm = new(test.driver, test.baseURL);
            Orgs_EditForm editForm = new(test.driver, test.baseURL);

            PICSActions.Login(test);
            orgs_Default.GotoPage();
            orgs_Default.CreateBtn.Click();

            createForm.Name.SetText("TestOrg");
            createForm.Postcode.SetText("NR2 4JS");
            //Sometimes we try to click save before postcode has been set, so wait until it is.
            //TODO still dodgy sometimes... Lookup causing issues maybe?
            test.wait.Until(d => createForm.Postcode.GetText() == "NR1 1AA");
            createForm.Save.ClickDefault();

            Assert.Multiple(() =>
            {
                Assert.That(createForm.errors.Count(), Is.EqualTo(0), "Number of validation errors");
                Assert.That(editForm.Name.GetText(), Is.EqualTo("TestOrg"), "Edit form name correct on load");
                Assert.That(editForm.Postcode.GetText(), Is.EqualTo("NR1 1AA"), "Edit form postcode correct on load");
            });

            editForm.PostcodeSearch.Click();

            Assert.That(editForm.AddressSelect.IsVisible(), Is.True, "Address dropdown visibility");

            editForm.AddressDropdown.SelectOption("1 The Street, Norwich, Norfolk");
            List<string> AllowedContactOptions = new() { "SMS/Text", "Telephone" };
            editForm.AllowedContactMethods.SelectOption(AllowedContactOptions);
            editForm.PreferredContactMethod.SelectOption("Telephone");
            System.Threading.Thread.Sleep(2000);

            Assert.Multiple(() =>
            {
                Assert.That(editForm.Address1.GetText(), Is.EqualTo("1 The Street"), "Address1 Value");
                Assert.That(editForm.Address2.GetText(), Is.EqualTo("Norwich"), "Address2 Value");
                Assert.That(editForm.Address3.GetText(), Is.EqualTo("Norfolk"), "Address3 Value");
                CollectionAssert.AreEqual(editForm.AllowedContactMethods.GetSelectedValues(), AllowedContactOptions, "Allowed Contact Options Equality");
                Assert.That(editForm.PreferredContactMethod.GetSelectedValue(), Is.EqualTo("Telephone"), "Preferred Contact Value");
            });

            string orgId = editForm.GetOrgID();
            editForm.Save.Click();

            Assert.That(createForm.errors.Count(), Is.EqualTo(0), "Number of validation errors");

            Orgs_Summary summary = new(test.driver, test.baseURL, orgId);
            summary.GotoPage();
            summary.ActionMenu.SelectAction("Edit");


            Assert.Multiple(() =>
            {
                Assert.That(editForm.Name.GetText(), Is.EqualTo("TestOrg"), "Edit form name correct on load");
                Assert.That(editForm.Postcode.GetText(), Is.EqualTo("NR1 1AA"), "Edit form postcode correct on load");
                Assert.That(editForm.Address1.GetText(), Is.EqualTo("1 The Street"), "Address1 Value");
                Assert.That(editForm.Address2.GetText(), Is.EqualTo("Norwich"), "Address3 Value");
                Assert.That(editForm.Address3.GetText(), Is.EqualTo("Norfolk"), "Address4 Value");
                Assert.That(editForm.Ward.GetSelectedValue(), Is.EqualTo("Wensum"), "Ward Value");
                CollectionAssert.AreEqual(editForm.AllowedContactMethods.GetSelectedValues(), AllowedContactOptions, "Allowed Contact Options Equality");
                Assert.That(editForm.PreferredContactMethod.GetSelectedValue(), Is.EqualTo("Telephone"), "Preferred Contact Value");
            });
        }

        // PD-1415 - Edit Applicant w./ Officer
        [Test]
        [Parallelizable]
        public void AddContact()
        {
            Orgs_Default orgs_Default = new(test.driver, test.baseURL);
            Orgs_CreateForm createForm = new(test.driver, test.baseURL);
            Orgs_EditForm editForm = new(test.driver, test.baseURL);

            PICSActions.Login(test);
            orgs_Default.GotoPage();
            orgs_Default.CreateBtn.Click();

            createForm.Name.SetText("TestOrgEdits");
            createForm.Save.ClickDefault();

            string orgId = editForm.GetOrgID();
            editForm.Save.Click();

            Orgs_Summary summary = new(test.driver, test.baseURL, orgId);

            summary.GotoPage("#officers-list");

            summary.SubActionMenu.SelectAction("Link Officer");
            System.Threading.Thread.Sleep(2000);

            Orgs_OfficerLink officerLink = new(test.driver, test.baseURL);
            officerLink.Officer.SelectOption("Link Contact");
            officerLink.Save.Click();

            Assert.That(summary.Table.FindRow("Link Contact", 1, true), Is.True, "Linked officer in grid");
        }
    }
}