namespace DoorChecker.Models
{
    public class MainViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ErrorText { get; set; }

        public MainViewModel()
        {
            Username = string.Empty;
            Password = string.Empty;
            ErrorText = string.Empty;
        }

        public bool Validate()
        {
            if (string.IsNullOrEmpty(Username))
            {
                ErrorText = "Username is required";
                return false;
            }

            if (string.IsNullOrEmpty(Password))
            {
                ErrorText = "Password is required";
                return false;
            }

            if (Password.Length < 5 || Password != "12345")
            {
                ErrorText = "Invalid username/password";
                return false;
            }

            return true;
        }
    }
}
