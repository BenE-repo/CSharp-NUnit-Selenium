using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs.Courses
{
    class CourseType_Create : MasterPOM
    {
        private const string urlEnd = "Courses/Configuration";

        public readonly EditField Code;
        public readonly EditField Name;
        public readonly Button Save;
        //public readonly Button Search_Button;
        //public readonly Table Search_Grid;

        public CourseType_Create(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            Code = new(driver, By.Id("Code"));
            Name = new(driver, By.Id("Name"));
            Save = new(driver, By.XPath("//button/i[contains(@class,'fa-save')]"));
        }
    }
}
