using NUnit_Selenium.POMs.Components;
using OpenQA.Selenium;

namespace NUnit_Selenium.POMs
{
    class Learner_Create_Main_Completed : MasterPOM
    {
        private const string urlEnd = "Learner/Default/Create";

        readonly public Button Summary;
        readonly public Button Close;

        public Learner_Create_Main_Completed(IWebDriver driver, string baseURL) : base(driver, baseURL)
        {
            pageURL = baseURL + urlEnd;

            Summary = new(driver, By.XPath("//div[@id='Completed']/div/div/div/a[contains(@href,'/pics/Learner/Default/Summary/')]/i"));
            Close = new(driver, By.XPath("//div[@id='Completed']/div/div/div/button"));
        }
    }
}
