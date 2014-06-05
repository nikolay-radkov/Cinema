namespace Cinema.Controler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows;

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
                MessageBox.Show("The username can't be white space!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (username.Length < 5)
            {
                MessageBox.Show("The username length must be at least 5 characters!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private static bool IsValidPassword(string password, string secondPassword)
        {
            if (password != secondPassword)
            {
                MessageBox.Show("The passwords are not equal!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("The password can't be white space!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("The password length must be at least 6 characters!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("The email can't be white space!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!isEmail)
            {
                MessageBox.Show("Invalid email!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
