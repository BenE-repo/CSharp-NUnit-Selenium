using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs.Officers
{
    class Officers_EditForm : MasterPOM
    {
        private const string urlEnd = "Officers/Default/Edit";

        readonly By _orgLink = By.XPath("//div[@class='modal-body']/input[@id='ID']");

        public string OrgID = string.Empty;
        readonly public EditField Name;
        readonly public EditField Postcode;
        readonly public Button PostcodeSearch;
        readonly public SingleSelect AddressSelect;
        readonly public SingleSelect AddressDropdown;
        readonly public EditField Address1;
        readonly public EditField Address4;
        readonly public EditField Address5;
        readonly public SingleSelect Ward;
        readonly public MultiSelect AllowedContactMethods;
        readonly public SingleSelect PreferredContactMethod;
        readonly public Button Save;
        readonly public ValidationErrors Errors;

        public Officers_EditForm(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            Name = new(driver, By.Id("Name"));
            Postcode = new(driver, By.Id("Postcode"));
            PostcodeSearch = new(driver, By.XPath("//a[contains(@class,'postcode-search')]"));
            AddressSelect = new(driver, "postcode-addresses");
            AddressDropdown = new(driver, "postcode-addresses");
            Address1 = new(driver, By.Id("Address1"));
            Address4 = new(driver, By.Id("Address4"));
            Address5 = new(driver, By.Id("Address5"));
            Ward = new(driver, By.Id("select2-Ward_Value-container"), By.Id("select2-Ward_Value-results"), true);
            AllowedContactMethods = new(driver, By.XPath("//select[@id='ContactMethods_AllowedMethods']/following-sibling::span[contains(@class,'select2-container')]"),
                                                By.Id("select2-ContactMethods_AllowedMethods-results"));
            PreferredContactMethod = new(driver, "ContactMethods_PreferredMethod");
            Save = new(driver, By.XPath("//button/i[contains(@class,'fa-save')]"));
            Errors = new(driver);
        }

        public string GetOrgID()
        {
            OrgID = driver.FindElement(_orgLink).GetAttribute("Value");
            return OrgID;
        }
    }
}
