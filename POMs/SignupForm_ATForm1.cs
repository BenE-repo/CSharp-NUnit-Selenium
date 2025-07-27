using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs
{
    class SignupForm_ATForm1 : MasterPOM
    {
        private const string urlEnd = "Guest/SignUp/Applicant/e5e36e5c35b3434abff8b22755ac951c";

        readonly public SingleSelect Title;
        readonly public EditField Firstnames;
        readonly public EditField Surname;
        readonly public EditField Address1;
        readonly public EditField Address2;
        readonly public EditField Address3;
        readonly public EditField Address4;
        readonly public EditField Postcode;
        readonly public SingleSelect Sex;
        readonly public EditField DOB;
        readonly public EditField Telephone;
        readonly public SingleSelect Qualplan;
        readonly public EditField AvailableToStart;
        readonly public EditField FavSciFi;
        //TODO Probably want to change these ones to dedicated objects.
        By _selDIG = By.XPath($"//input[@id='UserDefinedForms_APPSI_00002_4']/parent::label");
        By _selSNIFF = By.XPath($"//input[@id='UserDefinedForms_APPSI_00002_5']/parent::label");
        By _submitBtn = By.Id("application_form_submit");
        By _validationErrors = By.XPath($"//div[contains(@class,'validation-summary-errors')]/ul/li");

        public SignupForm_ATForm1(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            Title = new(driver, "Title");
            Firstnames = new(driver, By.Id("Forename"));
            Surname = new(driver, By.Id("Surname"));
            Address1 = new(driver, By.Id("Address1"));
            Address2 = new(driver, By.Id("Address2"));
            Address3 = new(driver, By.Id("Address3"));
            Address4 = new(driver, By.Id("Address4"));
            Postcode = new(driver, By.Id("Postcode"));
            Sex = new(driver, "Sex");
            DOB = new(driver, By.Id("DateOfBirth"));
            Telephone = new(driver, By.Id("Telephone"));
            Qualplan = new(driver, By.Id("select2-QualPlan_Value-container"), By.Id("select2-QualPlan_Value-results"), true);
            AvailableToStart = new(driver, By.Id("CanStart"));
            FavSciFi = new(driver, By.Id("UserDefinedForms_APPSI_00001"));
        }

        public void SelectSelDIG()
        {
            IWebElement selected = driver.FindElement(_selDIG);
            selected.Click();

            Assert.That(selected.FindElement(By.XPath($".//input")).Selected, Is.True, "Digging Holes selected");
        }

        public void SelectSelSNIFF()
        {
            IWebElement selected = driver.FindElement(_selSNIFF);
            selected.Click();

            Assert.That(selected.FindElement(By.XPath($".//input")).Selected, Is.True, "Sniffing Things selected");
        }

        public void ClickSubmit()
        {
            driver.FindElement(_submitBtn).Click();
        }

        public int CountErrorMessages()
        {
            //Can't easily check individual errors totally due to presence of generic messages for some fields
            //so just check that we're getting the number of errors we're expecting to.
            return driver.FindElements(_validationErrors).Count();
        }
    }
}
