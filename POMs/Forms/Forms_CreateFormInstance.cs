using Microsoft.VisualBasic;
using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs.Organisations
{
    class Forms_CreateFormInstance : MasterPOM
    {
        private const string _urlEnd = "Forms/Forms";

        readonly public SingleSelect Form;
        readonly public SingleSelect Owner;
        readonly public Button Save;

        public Forms_CreateFormInstance(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + _urlEnd;
            Form = new(driver, "Form");
            Owner = new(driver, By.XPath("//div[@class='modal-body']/div/div/span/span/span/span[@id='select2-Owner_Value-container']"),
                                By.Id("select2-Owner_Value-results"), true);
            Save = new(driver, SelConst.Button_Save);
        }
    }
}
