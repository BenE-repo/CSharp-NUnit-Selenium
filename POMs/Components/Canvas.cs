using NUnit_Selenium.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Collections;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace NUnit_Selenium.POMs.Components
{
    internal class Canvas
    {
        readonly private IWebDriver _driver;
        readonly private By _element;
        readonly private WebDriverWait _wait;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"> Parent of the button for the top-level action menu</param>
        public Canvas(IWebDriver driver, By element)
        {
            _driver = driver;
            _element = element;
            _wait = new(_driver, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Make a mark on the canvas
        /// </summary>
        public void Draw()
        {
            IWebElement e = _driver.FindElement(_element);
            
            // Appears to be a bug (possibly only with Chromium) which affects dragging the mouse, so this won't work properly, It still makes a mark on the canvas, which
            // is enough for a signature, so leaving the code as is for now.
            new Actions(_driver)
                .MoveToElement(e)
                .ClickAndHold(e)
                .MoveByOffset(50, 50)
                .Release()
                .ClickAndHold()
                .Release()
                .Perform();     
        }
    }
}
