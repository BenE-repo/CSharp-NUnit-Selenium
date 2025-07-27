using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;
using System.ComponentModel;

namespace NUnit_Selenium.POMs.Courses
{
    class CourseType_Summary : MasterPOM
    {
        private const string urlEnd = "Courses/ConfigCourseType/Summary";

        public readonly ActionsMenu Actions;

        public CourseType_Summary(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;
        }
    }
}
