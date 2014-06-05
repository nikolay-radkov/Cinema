namespace Cinema.Phone.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public static class InputValidation
    {
        public static bool IsValidSignUpInput(string username, string password, string secondPassword, string email)
        {
            bool result = true;

            if (!IsValidUsername(username))
            {
                result = false;
            }

            if (!IsValidPassword(password, secondPassword))
            {
                result = false;
            }

            if (!IsValidEmail(email))
            {
                result = false;
            }

            return result;
        }

        public static bool IsValidSignInInput(string username, string password)
        {
            bool result = true;

            if (!IsValidUsername(username))
            {
                result = false;
            }

            if (!IsValidPassword(password))
            {
                result = false;
            }

            return result;
        }

        private static bool IsValidUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return false;
            }

            if (username.Length < 5)
            {
                return false;
            }

            return true;
        }

        private static bool IsValidPassword(string password, string secondPassword)
        {
            if (password != secondPassword)
            {
                return false;
            }

            if (!IsValidPassword(password))
            {
                return false;
            }

            return true;
        }

        private static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            if (password.Length < 6)
            {
                return false;
            }

            return true;
        }

        private static bool IsValidEmail(string email)
        {
            string emailPattern = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
            bool isEmail = Regex.IsMatch(email, emailPattern);

            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            if (!isEmail)
            {
                return false;
            }

            return true;
        }
    }
}
