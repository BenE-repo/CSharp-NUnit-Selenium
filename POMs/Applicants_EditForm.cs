using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;
using System.ComponentModel;
using System.Xml.Linq;

namespace NUnit_Selenium.POMs
{
    class Applicants_EditForm : MasterPOM
    {
        private const string urlEnd = "Applicants/Default";

        readonly By _FormTitle = By.XPath("//h5[text()[contains(.,'Edit Applicant')]]");
        readonly By _ApplicantLink = By.XPath("//div[@id='general-modal-content']/form"); //so we can get the applicant ID from it

        public string ApplicantID = string.Empty;
        readonly public EditField FirstNames;
        readonly public EditField PrefFirstName;
        readonly public EditField Surname;
        readonly public SingleSelect Title;
        readonly public SingleSelect Sex;
        readonly public EditField DOB;
        readonly public EditField NIN;
        readonly public SingleSelect Ethnicity;
        readonly public SingleSelect ILRDisab;
        readonly public MultiSelect Disab;
        readonly public SingleSelect PrimaryDisab;
        readonly public EditField FavSciFi;
        
        readonly By SelectionDIG = By.XPath($"//input[@id='UserDefinedForms_APPSI_00002_4']/parent::label");

        readonly By _SaveButton = By.XPath("//button/i[contains(@class,'fa-save')]");

        readonly By _ErrorList = By.XPath("//div[contains(@class,'validation-summary-errors')]/ul/li");
        readonly By _FirstnamesError = By.Id("Firstnames-error");
        readonly By _SurnameError = By.Id("Surname-error");

        public Applicants_EditForm(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;
            
            FirstNames = new(driver, By.Id("Firstnames"));
            PrefFirstName = new(driver, By.Id("PreferredFirstName"));
            Surname = new(driver, By.Id("Surname"));
            Title = new(driver, "Title");
            Sex = new(driver, "Sex");
            DOB = new(driver, By.Id("DateOfBirth"));
            NIN = new(driver, By.Id("NINumber"));
            Ethnicity = new(driver, "Ethnicity");
            ILRDisab = new(driver, "ILRDisability");
            Disab = new(driver, By.XPath("//select[@id='LLDDs']/following-sibling::span[contains(@class,'select2-container')]"),
                        By.Id("select2-LLDDs-results"));
            PrimaryDisab = new(driver, "PrimaryLLDD");
            FavSciFi = new(driver, By.Id("UserDefinedForms_APPSI_00001"));
        }

        public bool IsFormLoaded()
        {
            try
            {
                Assert.That(driver.FindElement(_FormTitle).Displayed, Is.True);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public string GetApplicantID()
        {
            ApplicantID = driver.FindElement(_ApplicantLink).GetAttribute("action").Split("/").Last();
            return ApplicantID;
        }

        public void SelectSelDIG()
        {
            IWebElement selected = driver.FindElement(SelectionDIG);
            selected.Click();

            Assert.That(selected.FindElement(By.XPath($".//input")).Selected, Is.True, "Digging Holes selected");
        }

        public void Click_Save()
        {
            driver.FindElement(_SaveButton).Click();
        }

        public int GetNumberOfErrors()
        {
            int count = driver.FindElements(_ErrorList).Count();
            return count;
        }

        public bool IsFirstnamesErrorVisible()
        {
            try
            {
                Assert.That(driver.FindElement(_FirstnamesError).Displayed, Is.True);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsSurnamErrorVisible()
        {
            try
            {
                Assert.That(driver.FindElement(_SurnameError).Displayed, Is.True);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
