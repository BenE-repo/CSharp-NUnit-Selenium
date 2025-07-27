using OpenQA.Selenium;

namespace NUnit_Selenium.POMs.Components
{
    internal static class SelConst
    {
        public static readonly By Button_Save = By.XPath("//button/i[contains(@class,'fa-save')]");
        public static readonly By Button_Search = By.XPath("//button/i[contains(@class,'fa-search')]");
    }
}
