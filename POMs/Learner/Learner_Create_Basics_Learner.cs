using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs
{
    class Learner_Create_Basics_Learner : MasterPOM
    {
        private const string urlEnd = "Organisations/Default/Create";

        readonly public EditField FirstNames;
        readonly public EditField Surname;
        readonly public SingleSelect Sex;
        readonly public EditField DOB;
        readonly public ButtonWithDropDown Next;
        readonly public ValidationErrors errors;

        public Learner_Create_Basics_Learner(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            FirstNames = new(driver, By.Id("FirstNames"));
            Surname = new(driver, By.Id("Surname"));
            Sex = new(driver, "Sex");
            DOB = new(driver, By.Id("DateOfBirth"));
            Next = new(driver, By.XPath("//button/i[contains(@class,'fa fa-chevron-right')]"));
            errors = new(driver);
        }
    }
}
