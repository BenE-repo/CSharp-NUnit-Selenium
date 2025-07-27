using Microsoft.VisualBasic;
using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs.Organisations
{
    class Forms_FormOneFillAndSign : MasterPOM
    {
        private const string _urlEnd = "Forms/Forms";

        readonly public EditField Surname;
        readonly public EditField Firstname;
        readonly public EditField TextField;
        readonly public EditField EmailField;
        readonly public EditField NumberField;
        readonly public EditField DateField;
        readonly public EditField TimeField;
        readonly public Button CheckboxField;
        readonly public SingleSelect SelectField;
        readonly public Signature InitialField;

        readonly public Button LearnerSig;
        readonly public ActionsMenu Action;

        public Forms_FormOneFillAndSign(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + _urlEnd;
            Surname = new(driver, By.Id("SURNAME"));
            Firstname = new(driver, By.Id("FIRSTNAME"));
            TextField = new(driver, By.Id("text_ovbx8wd5yf40"));
            EmailField = new(driver, By.Id("email_ynhkxopicc00"));
            NumberField = new(driver, By.Id("number_bmnuygw4q140"));
            DateField = new(driver, By.Id("date_2znhsnsbd940"));
            TimeField = new(driver, By.Id("time_7eebpiukmwk0"));
            CheckboxField = new(driver, By.Id("checkbox_r503vbqaoy80"));
            SelectField = new(driver, By.Id("singledropdown_da2oqrb27s00"), true);
            InitialField = new(driver, By.Id("initial_k2t3o1omg680"));

            LearnerSig = new(driver, By.Id("Sig_Lrnr"));
            Action = new(driver);
        }
    }
}
