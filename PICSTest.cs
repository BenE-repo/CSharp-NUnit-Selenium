using Microsoft.Extensions.Configuration;
using NUnit_Selenium.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace NUnit_Selenium
{
    public class PICSTest
    {
        public IWebDriver driver;
        public IConfigurationRoot config { get; set; }
        public Credentials credentials { get; set; }
        public string baseURL { get; set; }
        public WebDriverWait wait { get; set; }
        public AdvantageDB db { get; set; }

        public PICSTest()
        {
            // Chrome - will want to do others at some point.

            ChromeOptions options = new();
            options.AddUserProfilePreference("autofill.profile_enabled", false);

            string path = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName;
            driver = new ChromeDriver(path + @"\Drivers\", options);

            // Set Wait Time
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Maximise window size
            driver.Manage().Window.Maximize();

            // Config
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", true, true);
            config = builder.Build();

            // Wait
            wait = new(driver, TimeSpan.FromSeconds(5));

            baseURL = config["PICSWeb_URL"]!;

            // Get Credentials
            credentials = new Credentials();

            // Set up conenction to DB. TODO: shove data location in appsettings or something.
            db = new("C:\\Data\\at\\pics.add");
        }

    }
}
