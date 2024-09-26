using System.Text.RegularExpressions;

namespace DoAnNoSQL.ModelView
{
    public class RegisterModel
    {
        public string email { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;

        public string? username { get; set; } = string.Empty;

        public bool IsValid()
        {
            if (String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(password) || string.IsNullOrEmpty(username) )
            {
                return false;
            }

            if (IsValidEmail(email))
            {
                return false;
            }
            return true;
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^([\w-\.]+)@([\w-]+\.)+([\w-]{2,4})$");
        }
    }
}
