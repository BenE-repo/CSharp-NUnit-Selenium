using NUnit_Selenium.Utils;
using NUnit.Framework.Internal;
using NUnit_Selenium.POMs.Courses;
using NUnit_Selenium.Tests.Base;
using NUnit_Selenium.POMs.Generic;
using NuGet.Frameworks;

namespace NUnit_Selenium.Tests.Courses
{
    public class Tests_CoursesBD : NUnit_Baseclass
    {

        // PD-1707 - Add Course Type
        [Test]
        public void AddCourseType()
        {
            // Login
            PICSActions.Login(test);

            // Delete course type in case it still exists from a previous run
            test.db.DeleteCourseType("CSType1");

            // Goto /Courses/Configuration#coursetypes and click Create
            Courses_Config config = new(test.driver, test.baseURL);
            config.GotoPage("#coursetypes");
            config.CourseTypes_Create.Click();

            // Enter details and save
            CourseType_Create create = new(test.driver, test.baseURL);
            create.Code.SetText("CSType1");
            create.Name.SetText("Course Type 1");
            create.Save.Click();

            // Check edit form and save
            CourseType_Edit edit = new(test.driver, test.baseURL);
            Assert.That(edit.EditForm.FormIsLoaded(), Is.True, "Edit Form Existence");

            // Delete Course Type
            test.db.DeleteCourseType("CSType1");

        }

        // PD-1708 - Edit Course Type
        [Test]
        public void EditCourseType()
        {
            // Delete course type we're going to edit if it's already there as we won't know what state it's in
            test.db.DeleteCourseType("CSTEDIT");

            // Directly add course type into DB
            string courseID = test.db.AddCourseType("CSTEDIT");

            // Login and go to courses->config->course type
            PICSActions.Login(test);
            Courses_Config config = new(test.driver, test.baseURL);
            config.GotoPage("#coursetypes");

            // click summary, add sites
            config.CourseTypeGrid.SelectMenuItem_ByRecordID(courseID, "Summary");
            CourseType_Summary summary = new(test.driver, test.baseURL);
            // TODO: get softlabel for sites from DB
            summary.Actions.SelectAction("Edit Sites");
            Site_Edit_Modal site = new(test.driver, test.baseURL);
            site.AllSitesToggle.Click();
            site.SitesList.SelectItem("SITEB");
            site.Save.Click();
            // Need to sleep while it saves. TODO: find a better way to do this.
            System.Threading.Thread.Sleep(1000);

            // Check DB for correctness
            Assert.That(test.db.CheckCourseTypeSite(courseID, "SITEB"), Is.True, "SITEB has been added to Course Type");
        }

        // PD-1709 - Delete Course Type
        [Test]
        public void DeleteCourseType()
        {
            // Delete course type we're going to edit if it's already there as we won't know what state it's in
            test.db.DeleteCourseType("CSTDEL");

            // Directly add course type into DB
            string courseID = test.db.AddCourseType("CSTDEL");

            // Login and go to courses->config->course type
            PICSActions.Login(test);
            Courses_Config config = new(test.driver, test.baseURL);
            config.GotoPage("#coursetypes");

            // Delete Course Type manually
            config.CourseTypeGrid.SelectMenuItem_ByRecordID(courseID, "Delete");
            config.DeleteDialog.ClickButton("Yes");
            System.Threading.Thread.Sleep(1000);

            // Check DB for correctness
            Assert.That(test.db.CheckCourseTypeExists("CSTDEL"), Is.False, "Course Type has been deleted");
        }
    }
}