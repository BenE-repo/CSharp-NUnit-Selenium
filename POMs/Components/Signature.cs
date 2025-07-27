using NUnit_Selenium.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections;
using System.Collections.ObjectModel;
using System.Drawing.Text;
using static System.Net.Mime.MediaTypeNames;

namespace NUnit_Selenium.POMs.Components
{
    internal class Signature
    {
        readonly private IWebDriver _driver;
        readonly private By _element;
        readonly private WebDriverWait _wait;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element">Element to be clicked on that opens the signature panel thing</param>
        public Signature(IWebDriver driver, By element)
        {
            _driver = driver;
            _element = element;
            _wait = new(_driver, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Click the element specified in the constructor
        /// </summary>
        public void Click()
        {
            PICSActions.ScrollIntoView(_driver, _driver.FindElement(_element));
            _wait.Until(d => _driver.FindElement(_element).Displayed);
            _driver.FindElement(_element).Click();
        }

        /// <summary>
        /// Makes a mark on the 'canvas' and saves
        /// </summary>
        public void Sign()
        {
            Canvas SigBox = new(_driver, By.XPath("//canvas[contains(@class,'panel')]"));
            SigBox.Draw();

            Button Save = new(_driver, SelConst.Button_Save);
            Save.Click();
        }
    }
}
