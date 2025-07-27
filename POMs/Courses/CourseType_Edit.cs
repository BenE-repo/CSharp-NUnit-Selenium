using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;
using System.ComponentModel;

namespace NUnit_Selenium.POMs.Courses
{
    class CourseType_Edit : MasterPOM
    {
        private const string urlEnd = "Courses/Configuration";

        public readonly Form EditForm;
        public readonly EditField Code;
        public readonly EditField Name;
        public readonly SingleSelect Status;
        public readonly Button Save;

        public CourseType_Edit(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            EditForm = new(driver, By.XPath("//form[contains(@action, '/pics/Courses/ConfigCourseType/Edit/')]"));
            Code = new(driver, By.Id("Code"));
            Name = new(driver, By.Id("Name"));
            Status = new(driver, "Status");
            Save = new(driver, By.XPath("//button/i[contains(@class,'fa-save')]"));
        }
    }
}
