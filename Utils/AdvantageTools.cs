using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvantageProvider;

namespace NUnit_Selenium.Utils
{
    // Collection of methods to perform actions directly on ADS tables. There's not going to be many, so one method per function with no concerns about reusability.
    class AdvantageTools
    {
        private static void ADSConnect()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var connection = new AdsConnection($"Data Source=");
        }
    }
}
