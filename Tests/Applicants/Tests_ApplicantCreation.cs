using NUnit_Selenium.POMs;
using NUnit_Selenium.Tests.Base;

namespace NUnit_Selenium.Tests.Applicants
{
    public class Tests_ApplicantCreation : NUnit_Baseclass
    {
        // PD-1378 - Empty First Page
        [Test]
        [Parallelizable]
        public void EmptyApplicantInitialForm()
        {
            LoginPage loginPage = new(test.driver, test.baseURL);
            Workspace workSpace = new(test.driver, test.baseURL);
            Applicants_Default appDefault = new(test.driver, test.baseURL);
            Applicants_CreateForm appCreateForm = new(test.driver, test.baseURL);

            loginPage.GotoPage();
            loginPage.Enter_Username(test.credentials.username_valid);
            loginPage.Enter_Password(test.credentials.password_valid);
            loginPage.Click_Login();

            Assert.That(workSpace.IsCorrectURL(), Is.True, $"Is {workSpace.pageURL} loaded?");

            appDefault.GotoPage();
            appDefault.CreateApplicant_Click();

            appCreateForm.Click_Save();

            Assert.Multiple(() =>
            {
                Assert.That(appCreateForm.IsFirstnamesErrorVisible(), Is.True, "Firstnames validation message visibility");
                Assert.That(appCreateForm.IsSurnamErrorVisible(), Is.True, "Surname validation message visibility");
            });
        }

        // PD-1379 - Empty Second Page
        [Test]
        [Parallelizable]
        public void EmptyApplicantPostCreateEditForm()
        {
            LoginPage loginPage = new(test.driver, test.baseURL);
            loginPage.GotoPage();
            loginPage.Enter_Username(test.credentials.username_valid);
            loginPage.Enter_Password(test.credentials.password_valid);
            loginPage.Click_Login();

            Workspace workSpace = new(test.driver, test.baseURL);
            Assert.That(workSpace.IsCorrectURL(), Is.True, $"Is {workSpace.pageURL} loaded?");

            Applicants_Default appDefault = new(test.driver, test.baseURL);
            appDefault.GotoPage();
            appDefault.CreateApplicant_Click();

            Applicants_CreateForm appCreateForm = new(test.driver, test.baseURL);
            appCreateForm.Firstnames.SetText("Aaron");
            appCreateForm.Surname.SetText("Aaronson");
            appCreateForm.Click_Save();

            Applicants_EditForm appEditForm = new(test.driver, test.baseURL);
            Assert.That(appEditForm.IsFormLoaded(), Is.True, "Applicant Edit Form Loaded");

            appEditForm.FirstNames.ClearText();
            appEditForm.Surname.ClearText();

            appEditForm.Click_Save();

            Assert.Multiple(() =>
            {
                Assert.That(appEditForm.IsFirstnamesErrorVisible(), Is.True, "Firstnames validation message visibility");
                Assert.That(appEditForm.IsSurnamErrorVisible(), Is.True, "Surname validation message visibility");
            });
        }

        // PD-1300 - Full Valid Entry
        [Test]
        [Parallelizable]
        public void FullValidEntry()
        {
            LoginPage loginPage = new(test.driver, test.baseURL);
            Workspace workSpace = new(test.driver, test.baseURL);
            Applicants_Default appDefault = new(test.driver, test.baseURL);
            Applicants_CreateForm appCreateForm = new(test.driver, test.baseURL);
            Applicants_EditForm appEditForm = new(test.driver, test.baseURL);

            //Login
            loginPage.GotoPage();
            loginPage.Enter_Username(test.credentials.username_valid);
            loginPage.Enter_Password(test.credentials.password_valid);
            loginPage.Click_Login();

            Assert.That(workSpace.IsCorrectURL(), Is.True, $"Is {workSpace.pageURL} loaded?");

            //Create Applicant - Start
            appDefault.GotoPage();
            appDefault.CreateApplicant_Click();

            //Create Applicant - First Form
            appCreateForm.Firstnames.SetText("Aaron");
            appCreateForm.Surname.SetText("Aaronson");
            appCreateForm.Click_Save();

            Assert.That(appEditForm.IsFormLoaded(), Is.True, "Applicant Edit Form Loaded");

            //Create Applicant - Edit Form
            appEditForm.PrefFirstName.SetText("Bob");
            appEditForm.Title.SelectOption("Mr");
            appEditForm.Sex.SelectOption("Other");
            appEditForm.DOB.SetText($"01/01/2000");
            appEditForm.NIN.SetText("AB112233A");
            appEditForm.Ethnicity.SelectOption("32: Irish");
            appEditForm.ILRDisab.SelectOption("Learner considers themself to have a learning difficulty and/or disability and/or health problem");
            List<string> DisabOptions = new() { "04: Vision impairment", "05: Hearing impairment" };
            appEditForm.Disab.SelectOption(DisabOptions);
            appEditForm.PrimaryDisab.SelectOption("05: Hearing impairment");
            appEditForm.FavSciFi.SetText("Blakes 7");
            appEditForm.SelectSelDIG();

            Assert.Multiple(() =>
            {
                //TODO: Shove asserts into the classes???
                Assert.That(appEditForm.FirstNames.GetText(), Is.EqualTo("Aaron"), "Firstnames text");
                Assert.That(appEditForm.PrefFirstName.GetText(), Is.EqualTo("Bob"), "Preferred First Name text");
                Assert.That(appEditForm.Surname.GetText(), Is.EqualTo("Aaronson"), "Surname text");
                Assert.That(appEditForm.Title.GetSelectedValue(), Is.EqualTo("Mr"), "Title option");
                Assert.That(appEditForm.Sex.GetSelectedValue(), Is.EqualTo("Other"), "Sex option");
                Assert.That(appEditForm.DOB.GetText(), Is.EqualTo($"01/01/2000"), "DOB Text");
                Assert.That(appEditForm.NIN.GetText(), Is.EqualTo("AB112233A"), "NIN Text");
                Assert.That(appEditForm.Ethnicity.GetSelectedValue(), Is.EqualTo("32: Irish"), "Ethnicity Option");
                Assert.That(appEditForm.ILRDisab.GetSelectedValue(), Is.EqualTo("Learner considers themself to have a learning difficulty and/or disability and/or health problem"), "ILR Disability Option");
                CollectionAssert.AreEqual(appEditForm.Disab.GetSelectedValues(), DisabOptions, "Disability Option Equality");
                Assert.That(appEditForm.PrimaryDisab.GetSelectedValue(), Is.EqualTo("05: Hearing impairment"), "Primary Disability option");
                Assert.That(appEditForm.FavSciFi.GetText(), Is.EqualTo("Blakes 7"), "Favourite Sci Fi Value");
            });

            string applicantID = appEditForm.GetApplicantID(); //For use when searching for the new applicant
            appEditForm.Click_Save();

            Assert.That(appEditForm.GetNumberOfErrors(), Is.EqualTo(0), "Number of Validation Errors");

            //Open Applicant
            Applicant_Summary appSummary = new(test.driver, test.baseURL, applicantID);
            appSummary.GotoPage();
            appSummary.ActionMenu.SelectAction("Edit");

            Assert.Multiple(() =>
            {
                Assert.That(appEditForm.FirstNames.GetText(), Is.EqualTo("Aaron"), "Firstnames text");
                Assert.That(appEditForm.PrefFirstName.GetText(), Is.EqualTo("Bob"), "Preferred First Name text");
                Assert.That(appEditForm.Surname.GetText(), Is.EqualTo("Aaronson"), "Surname text");
                Assert.That(appEditForm.Title.GetSelectedValue(), Is.EqualTo("Mr"), "Title option");
                Assert.That(appEditForm.Sex.GetSelectedValue(), Is.EqualTo("Other"), "Sex option");
                Assert.That(appEditForm.DOB.GetText(), Is.EqualTo($"01/01/2000"), "DOB Text");
                Assert.That(appEditForm.NIN.GetText(), Is.EqualTo("AB112233A"), "NIN Text");
                Assert.That(appEditForm.Ethnicity.GetSelectedValue(), Is.EqualTo("32: Irish"), "Ethnicity Option");
                Assert.That(appEditForm.ILRDisab.GetSelectedValue(), Is.EqualTo("Learner considers themself to have a learning difficulty and/or disability and/or health problem"), "ILR Disability Option");
                CollectionAssert.AreEqual(appEditForm.Disab.GetSelectedValues(), DisabOptions, "Disability Option Equality");
                Assert.That(appEditForm.PrimaryDisab.GetSelectedValue(), Is.EqualTo("05: Hearing impairment"), "Primary Disability option");
                Assert.That(appEditForm.FavSciFi.GetText(), Is.EqualTo("Blakes 7"), "Favourite Sci Fi Value");
            });
        }
    }
}