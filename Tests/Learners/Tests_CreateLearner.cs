using NUnit_Selenium.Utils;
using NUnit.Framework.Internal;
using NUnit_Selenium.POMs.Organisations;
using NUnit_Selenium.POMs;
using NUnit_Selenium.Tests.Base;

namespace NUnit_Selenium.Tests.Learners
{
    public class Tests_CreateLearner : NUnit_Baseclass
    {
        // PD-1619 - Create Classroom Learner
        [Test]
        [Parallelizable]
        public void CreateClassroomLearner()
        {
            // Login
            PICSActions.Login(test);

            // Check for existence of qualplan
            Assert.That(test.db.QualPlan_CheckExistence("CLS1"), Is.True, "Qualplan Exists in db");

            // Goto Learners/Default; Create learner
            Learner_Default def = new(test.driver, test.baseURL);
            def.GotoPage();
            def.CreateBtn.Click();

            // Basic wizard - Learner
            Learner_Create_Basics_Learner basicsLearner = new(test.driver, test.baseURL);
            basicsLearner.FirstNames.SetText("ATLearnerCLS");
            basicsLearner.Surname.SetText("Test");
            basicsLearner.Sex.SelectOption("Male");
            basicsLearner.DOB.SetText("01/01/2000");
            basicsLearner.Next.ClickDefault();

            // Basic wizard - Programme
            Learner_Create_Basics_Programme basicsProg = new(test.driver, test.baseURL);
            basicsProg.StartDate.SetText(DateTime.Now.ToString("dd/MM/yyyy"));
            basicsProg.QualPlan.SelectOption("Classroom Plan 1");
            basicsProg.Next.ClickDefault();

            // Main wizard - Learner
            Learner_Create_Main_Learner mainLearner = new(test.driver, test.baseURL);
            // Earliest we can get the learner ID for use later (if needed)
            string learnerID = mainLearner.GetLearnerID();
            mainLearner.Ethnicity.SelectOption("31: English / Welsh / Scottish / Northern Irish / British");
            mainLearner.Postcode.SetText("NR1 1AA");
            mainLearner.Address1.SetText("1 The Street");
            mainLearner.PriorAttainment.SelectOption("1: Entry level");
            mainLearner.Disability.SelectOption("Learner does not consider themself to have a learning difficulty and/or disability and/or health problem");
            mainLearner.Next.Click();

            // Main wizard - Employment
            Learner_Create_Main_Employment mainEmp = new(test.driver, test.baseURL);
            mainEmp.EmploymentStatus.SelectOption("11: Not in paid employment and looking for work");
            mainEmp.UnemploymentLength.SelectOption("01: Learner has been unemployed for less than 6 months");
            mainEmp.Next.Click();

            // Main wizard - Programme
            Learner_Create_Main_Programme mainProg = new(test.driver, test.baseURL);
            mainProg.PlannedHours2024.SetText("400");
            mainProg.DelLocSource.SelectOption("Home Postcode");
            mainProg.Next.Click();

            // Main wizard - Additional
            Learner_Create_Main_Additional mainAdd = new(test.driver, test.baseURL);
            mainAdd.Complete.Click();
            Thread.Sleep(2000);

            // Main wizard - Completed
            Learner_Create_Main_Completed mainComp = new(test.driver, test.baseURL);
            mainComp.Summary.Click();

            // Check personal details
            Learner_Summary summary = new(test.driver, test.baseURL, learnerID);
            Thread.Sleep(2000);
            summary.ActionMenu.SelectAction("Edit Learner Details");

            Learner_Edit_LearnerDetails learnerDetails = new(test.driver, test.baseURL);

            Assert.Multiple(() =>
            {
                Assert.That(learnerDetails.Firstnames.GetText(), Is.EqualTo("ATLearnerCLS"), "Firstnames equality");
                Assert.That(learnerDetails.Surname.GetText(), Is.EqualTo("Test"), "Surname equality");
                Assert.That(learnerDetails.Ethnicity.GetSelectedValue(), Is.EqualTo("31: English / Welsh / Scottish / Northern Irish / British"), "Ethnicity equality");
                Assert.That(learnerDetails.DateOfBirth.GetText(), Is.EqualTo("01/01/2000"), "DOB equality");
                Assert.That(learnerDetails.Postcode.GetText(), Is.EqualTo("NR1 1AA"), "Postcode equality");
                Assert.That(learnerDetails.Address1.GetText(), Is.EqualTo("1 The Street"), "Address1 equality");
            });

            learnerDetails.Save.Click();
            Thread.Sleep(2000);

            // Check ILR Programme
            Learner_ILR ilr = new(test.driver, test.baseURL, learnerID);
            ilr.GotoPage();
            ilr.Edit_Programme.Click();
            Learner_Edit_ProgrammeDetails prog = new(test.driver, test.baseURL);

            Assert.That(prog.Disability.GetSelectedValue(), Is.EqualTo("Learner does not consider themself to have a learning difficulty and/or disability and/or health problem"), "Disability equality");

            prog.Save.Click();
            Thread.Sleep(2000);
        }

        // PD-1620 - Create Apprentice Learner
        [Test]
        [Parallelizable]
        public void CreateApprenticeLearner()
        {
            // Login
            PICSActions.Login(test);

            // Check for existence of qualplan
            Assert.That(test.db.QualPlan_CheckExistence("L2AUTO"), Is.True, "Qualplan Exists in db");

            // Goto Learners/Default; Create learner
            Learner_Default def = new(test.driver, test.baseURL);
            def.GotoPage();
            def.CreateBtn.Click();

            // Basic wizard - Learner
            Learner_Create_Basics_Learner basicsLearner = new(test.driver, test.baseURL);
            basicsLearner.FirstNames.SetText("ATLearnerApp");
            basicsLearner.Surname.SetText("Test");
            basicsLearner.Sex.SelectOption("Male");
            basicsLearner.DOB.SetText("01/01/2000");
            basicsLearner.Next.ClickDefault();

            // Basic wizard - Programme
            Learner_Create_Basics_Programme basicsProg = new(test.driver, test.baseURL);
            basicsProg.StartDate.SetText(DateTime.Now.ToString("dd/MM/yyyy"));
            basicsProg.QualPlan.SelectOption("Level 2 Standard: Automotive");
            basicsProg.Next.ClickDefault();

            // Main wizard - Learner
            Learner_Create_Main_Learner mainLearner = new(test.driver, test.baseURL);
            // Earliest we can get the learner ID for use later (if needed)
            string learnerID = mainLearner.GetLearnerID();
            mainLearner.Ethnicity.SelectOption("31: English / Welsh / Scottish / Northern Irish / British");
            mainLearner.Postcode.SetText("NR1 1AA");
            mainLearner.Address1.SetText("1 The Street");
            mainLearner.PriorAttainment.SelectOption("1: Entry level");
            mainLearner.Disability.SelectOption("Learner does not consider themself to have a learning difficulty and/or disability and/or health problem");
            mainLearner.Next.Click();

            // Main wizard - Employment
            Learner_Create_Main_Employment mainEmp = new(test.driver, test.baseURL);
            mainEmp.SmallEmployer.SelectOption("00: Not Small Employer");
            mainEmp.Next.Click();

            // Main wizard - Programme
            Learner_Create_Main_Programme mainProg = new(test.driver, test.baseURL);
            mainProg.Next.Click();

            // Main wizard - Apprenticeship Prices
            Learner_Create_Main_AppPrices appPrices = new(test.driver, test.baseURL);
            appPrices.Next.Click();

            // Main wizard - Additional
            Learner_Create_Main_Additional mainAdd = new(test.driver, test.baseURL);
            mainAdd.Complete.Click();
            Thread.Sleep(2000);

            // Main wizard - Completed
            Learner_Create_Main_Completed mainComp = new(test.driver, test.baseURL);
            mainComp.Summary.Click();

            // Check personal details
            Learner_Summary summary = new(test.driver, test.baseURL, learnerID);
            Thread.Sleep(2000);
            summary.ActionMenu.SelectAction("Edit Learner Details");

            Learner_Edit_LearnerDetails learnerDetails = new(test.driver, test.baseURL);

            Assert.Multiple(() =>
            {
                Assert.That(learnerDetails.Firstnames.GetText(), Is.EqualTo("ATLearnerApp"), "Firstnames equality");
                Assert.That(learnerDetails.Surname.GetText(), Is.EqualTo("Test"), "Surname equality");
                Assert.That(learnerDetails.Ethnicity.GetSelectedValue(), Is.EqualTo("31: English / Welsh / Scottish / Northern Irish / British"), "Ethnicity equality");
                Assert.That(learnerDetails.DateOfBirth.GetText(), Is.EqualTo("01/01/2000"), "DOB equality");
                Assert.That(learnerDetails.Postcode.GetText(), Is.EqualTo("NR1 1AA"), "Postcode equality");
                Assert.That(learnerDetails.Address1.GetText(), Is.EqualTo("1 The Street"), "Address1 equality");
            });

            learnerDetails.Save.Click();
            Thread.Sleep(2000);

            // Check ILR Programme
            Learner_ILR ilr = new(test.driver, test.baseURL, learnerID);
            ilr.GotoPage();
            ilr.Edit_Programme.Click();
            Learner_Edit_ProgrammeDetails prog = new(test.driver, test.baseURL);

            Assert.That(prog.Disability.GetSelectedValue(), Is.EqualTo("Learner does not consider themself to have a learning difficulty and/or disability and/or health problem"), "Disability equality");

            prog.Save.Click();
            Thread.Sleep(2000);
        }

        // PD-1621 - Edit Classroom Learner
        [Test]
        [Parallelizable]
        public void EditClassroomLearner()
        {
            // Get appropriate learner ID from the DB
            string? learnerID = test.db.GetClassroomLearnerID();
            // Ugly. TODO: method to directly insert learner into db for tests like this
            Assert.That(learnerID, Is.Not.Null, "Sorry, need to create the learner first.");

            // Login
            PICSActions.Login(test);

            // Add NIN
            Learner_Summary summary = new(test.driver, test.baseURL, learnerID);
            summary.GotoPage();
            Thread.Sleep(2000);
            summary.ActionMenu.SelectAction("Edit Learner Details");

            Learner_Edit_LearnerDetails learnerDetails = new(test.driver, test.baseURL);
            learnerDetails.NIN.SetText("JG112233A");

            learnerDetails.Save.Click();

            System.Threading.Thread.Sleep(2000);
            Assert.That(test.db.CheckDB(learnerID, "TRAINEE", "NI_NO", "JG112233A"), "NIN Equality");
        }

        // PD-1622 - Edit Apprentice Learner
        [Test]
        [Parallelizable]
        public void EditApprenticeLearner()
        {
            // Get appropriate learner ID from the DB
            string? learnerID = test.db.GetApprenticeLearnerID();
            // Ugly. TODO: method to directly insert learner into db for tests like this
            Assert.That(learnerID, Is.Not.Null, "Sorry, need to create the learner first.");

            // Login
            PICSActions.Login(test);

            // Add NIN
            Learner_Summary summary = new(test.driver, test.baseURL, learnerID);
            summary.GotoPage();
            Thread.Sleep(2000);
            summary.ActionMenu.SelectAction("Edit Learner Details");

            Learner_Edit_LearnerDetails learnerDetails = new(test.driver, test.baseURL);
            learnerDetails.NIN.SetText("FD112233B");

            learnerDetails.Save.Click();

            Assert.That(test.db.CheckDB(learnerID, "TRAINEE", "NI_NO", "FD112233B"), "NIN Equality");
        }

        // PD-1623 - Delete Classroom Learner
        // Don't want to run in parallel as the learner needs to exist
        [Test]
        public void DeleteClassroomLearner()
        {
            // Get appropriate learner ID from the DB
            string? learnerID = test.db.GetClassroomLearnerID();
            // Ugly. TODO: method to directly insert learner into db for tests like this
            Assert.That(learnerID, Is.Not.Null, "Sorry, need to create the learner first.");

            // Login
            PICSActions.Login(test);

            // Search for Learner
            Learner_Default learnerDefault = new(test.driver, test.baseURL);
            learnerDefault.GotoPage();
            learnerDefault.Search_Name.SetText(learnerID);
            learnerDefault.Search_Status.SelectOption("All");
            learnerDefault.Search_Button.Click();

            // Delete them
            System.Threading.Thread.Sleep(2000);
            learnerDefault.LearnerTable.SelectMenuItem_ByText("ATLearnerCLS Test", 2, "Delete", true);
            System.Threading.Thread.Sleep(2000);
            learnerDefault.DeleteDialog.ClickButton("Yes");
            System.Threading.Thread.Sleep(2000);

            //TODO: Need a wait here
            Assert.That(test.db.CheckLearnerExistence(learnerID), Is.False, "Learner Exists");
        }

        // PD-1624 - Delete Apprentice Learner 
        // Don't want to run in parallel as the learner needs to exist
        [Test]
        public void DeleteApprenticeLearner()
        {
            // Get appropriate learner ID from the DB
            string? learnerID = test.db.GetApprenticeLearnerID();
            // Ugly. TODO: method to directly insert learner into db for tests like this
            Assert.That(learnerID, Is.Not.Null, "Sorry, need to create the learner first.");

            // Login
            PICSActions.Login(test);

            // Search for Learner
            Learner_Default learnerDefault = new(test.driver, test.baseURL);
            learnerDefault.GotoPage();
            learnerDefault.Search_Name.SetText(learnerID);
            learnerDefault.Search_Status.SelectOption("All");
            learnerDefault.Search_Button.Click();

            // Delete them
            System.Threading.Thread.Sleep(2000);
            learnerDefault.LearnerTable.SelectMenuItem_ByText("ATLearnerApp Test", 2, "Delete", true);
            System.Threading.Thread.Sleep(2000);
            learnerDefault.DeleteDialog.ClickButton("Yes");
            System.Threading.Thread.Sleep(2000);

            //TODO: Need a wait here
            Assert.That(test.db.CheckLearnerExistence(learnerID), Is.False, "Learner Exists");
        }
    }
}