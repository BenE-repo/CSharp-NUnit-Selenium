using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Microsoft.Extensions.Configuration;
using NUnit_Selenium.POMs;
using NUnit_Selenium.Utils;

namespace NUnit_Selenium.Tests
{
    public class Tests_ADS
    {
        private IWebDriver driver;
        private IConfigurationRoot config;
        private Credentials credentials;
        private string baseURL;

        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}