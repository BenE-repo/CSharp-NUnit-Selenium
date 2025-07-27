using NUnit_Selenium.POMs;
using NUnit_Selenium.Tests.Base;

namespace NUnit_Selenium.Tests.Applicants
{
    public class Tests_ApplicantSignUp : NUnit_Baseclass
    {
        [Test]
        [Parallelizable]
        public void FullyEnteredSignUpForm()
        {
            SignupForm_ATForm1 suForm = new(test.driver, test.baseURL);
            ThankYouPage tyPage = new(test.driver, test.baseURL);

            suForm.GotoPage();

            suForm.Title.SelectOption("Miss");
            suForm.Firstnames.SetText("AtAPP_" + DateTime.Now.ToString("yyMMddHHmm"));
            suForm.Surname.SetText("Test");
            suForm.Address1.SetText("Add1");
            suForm.Address2.SetText("Add2");
            suForm.Address3.SetText("Add3");
            suForm.Address4.SetText("Add4");
            suForm.Postcode.SetText("NR1 1AA");
            suForm.Sex.SelectOption("Female");
            suForm.DOB.SetText($"01/01/2000");
            suForm.Telephone.SetText("01603 123456");
            suForm.Qualplan.SelectOption("Classroom Plan 1");
            suForm.AvailableToStart.SetText(DateTime.Now.ToString("dd/MM/yyyy"));
            suForm.FavSciFi.SetText("Babylon 5");
            suForm.SelectSelDIG();
            suForm.SelectSelSNIFF();
            suForm.ClickSubmit();

            Assert.That(tyPage.IsThankYouTextVisible(), Is.True, "Thank you text visible");
        }

        [Test]
        [Parallelizable]
        public void EmptySignUpForm()
        {
            SignupForm_ATForm1 suForm = new(test.driver, test.baseURL);

            suForm.GotoPage();

            suForm.ClickSubmit();

            Assert.That(suForm.CountErrorMessages(), Is.EqualTo(9), "Number of error messages");
        }
    }
}