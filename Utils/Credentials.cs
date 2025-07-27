using Newtonsoft.Json;

namespace NUnit_Selenium.Utils
{
    public class CredentialsFile
    {
        public string UsernameValid { get; set; } = string.Empty;
        public string PasswordValid { get; set; } = string.Empty;
        public string UsernameInvalid { get; set; } = string.Empty;
        public string PasswordInvalid { get; set; } = string.Empty;
        public string ValidExistingEmail { get; set; } = string.Empty;
        public string ValidNonExistantEmail { get; set; } = string.Empty;
        public string InvalidEmail { get; set; } = string.Empty;
    }

    public class Credentials
    {
        private string _credentialsFile = string.Empty;
        public string username_valid = string.Empty;
        public string password_valid = string.Empty;
        public string username_invalid = string.Empty;
        public string password_invalid = string.Empty;
        public string valid_existing_email = string.Empty;
        public string valid_nonexistant_email = string.Empty;
        public string invalid_email = string.Empty;

        public Credentials()
        {
            _credentialsFile = "Credentials.json";
            GetCredentials();
        }

        private void GetCredentials()
        {
            try
            {
                string file = File.ReadAllText(_credentialsFile);
                CredentialsFile credentialsFile = JsonConvert.DeserializeObject<CredentialsFile>(file)!;

                username_valid = credentialsFile.UsernameValid;
                password_valid = credentialsFile.PasswordValid;
                username_invalid = credentialsFile.UsernameInvalid;
                password_invalid = credentialsFile.PasswordInvalid;
                valid_existing_email = credentialsFile.ValidExistingEmail;
                valid_nonexistant_email = credentialsFile.ValidNonExistantEmail;
                invalid_email = credentialsFile.InvalidEmail;
            }
            catch
            {
                throw new FileNotFoundException(_credentialsFile + " can't be found or it's not in a format we expect or something. Have a look, it's probably obvious");
            }
        }
    }
}
