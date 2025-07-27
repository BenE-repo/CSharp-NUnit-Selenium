using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs.Courses
{
    class Courses_Config : MasterPOM
    {
        private const string urlEnd = "Courses/Configuration";

        public readonly Button CourseTypes_Create;
        public readonly Table_v2 CourseTypeGrid;
        public readonly Alert DeleteDialog;

        public Courses_Config(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            CourseTypes_Create = new(driver, By.XPath("//a[@data-ajax-url='/pics/Courses/ConfigCourseType/Create']"));
            CourseTypeGrid = new(driver, By.XPath("//div[@id='coursetypes']/div/div/table/tbody"), 5);
            DeleteDialog = new(driver);
        }
    }
}
