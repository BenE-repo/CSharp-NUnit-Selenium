using NUnit_Selenium.Utils;
using NUnit.Framework.Internal;
using NUnit_Selenium.POMs.Organisations;
using NUnit_Selenium.POMs;
using NUnit_Selenium.Tests.Base;

namespace NUnit_Selenium.Tests.Vacancies
{
    public class Tests_CreateVacancies : NUnit_Baseclass
    {
        // PD-1571 - Create with Empty Form
        [Test]
        [Parallelizable]
        public void EmptyVacInitialForm()
        {
            PICSActions.Login(test);

            Vacancies_Default vacancies_Default = new(test.driver, test.baseURL);
            vacancies_Default.GotoPage();
            vacancies_Default.CreateBtn.Click();

            Vacancies_CreateForm createForm = new(test.driver, test.baseURL);
            createForm.Save.ClickDefault();

            Assert.Multiple(() =>
            {
                Assert.That(createForm.errors.Count(), Is.EqualTo(1), "Number of validation errors");
                Assert.That(createForm.errors.FindError("Title"), Is.True, "Validation error for Name present");
            });

        }

        // PD-1572 - Create with Valid Fields
        [Test]
        [Parallelizable]
        public void FullCreate()
        {
            PICSActions.Login(test);

            Vacancies_Default vacancies_Default = new(test.driver, test.baseURL);
            vacancies_Default.GotoPage();
            vacancies_Default.CreateBtn.Click();

            Vacancies_CreateForm createForm = new(test.driver, test.baseURL);
            createForm.Title.SetText("TestVac");
            createForm.Save.ClickDefault();

            Vacancies_EditForm editForm = new(test.driver, test.baseURL);
            editForm.Postcode.SetText("NR1 1AA");
            editForm.Save.Click();

            Assert.Multiple(() =>
            {
                Assert.That(createForm.errors.Count(), Is.EqualTo(0), "Number of validation errors");
            });
        }

        // PD-1573 - Create with Additional Edits
        [Test]
        [Parallelizable]
        public void AddProvision()
        {
            PICSActions.Login(test);

            Vacancies_Default vacancies_Default = new(test.driver, test.baseURL);
            vacancies_Default.GotoPage();
            vacancies_Default.CreateBtn.Click();

            Vacancies_CreateForm createForm = new(test.driver, test.baseURL);
            createForm.Title.SetText("EditVac");
            createForm.Save.ClickDefault();

            Vacancies_EditForm editForm = new(test.driver, test.baseURL);
            string vacId = editForm.GetVacID();

            editForm.Save.Click();
            editForm.WaitForDialogClose();

            Vacancies_Summary summary = new(test.driver, test.baseURL, vacId);
            summary.GotoPage("#provisions-list");
            summary.AddProvision.Click();

            Vacancies_AddProvision addProvision = new(test.driver, test.baseURL);
            addProvision.ProvisionType.SelectOption("SIC 2007");
            addProvision.Next.Click();
            addProvision.SelectProvision.SelectOption("A: AGRICULTURE, FORESTRY AND FISHING");
            addProvision.Save.Click();

            Assert.Multiple(() =>
            {
                Assert.That(summary.Table.FindRow("A", 2, false), Is.True, "Provision exists in Grid");
            });
        }
    }
}