namespace Cinema.Phone.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using Cinema.Phone.ExecuteQueryService;
    using Cinema.Phone.Model;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;

    public partial class SignUp : PhoneApplicationPage
    {
        public SignUp()
        {
            this.InitializeComponent();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            SingletonQuery.QueryClient.SignUpUserCompleted += new EventHandler<SignUpUserCompletedEventArgs>(this.Service_SignUpUser);
        
            SingletonQuery.QueryClient.SignUpUserAsync(txtUsername.Text, txtPassword.Password, txtSecondPassword.Password, txtEmail.Text);
        }

        void Service_SignUpUser(object sender, SignUpUserCompletedEventArgs e)
        {
            SingletonQuery.QueryClient.SignUpUserCompleted -= this.Service_SignUpUser;
     
            if (e.Error == null)
            {
                if (e.Result)
                {
                    NavigationService.Navigate(new Uri("/View/MainPage.xaml", UriKind.Relative));
                    MessageBox.Show("The registration is completed successfully.", "Information", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Error while registering.", "Error", MessageBoxButton.OK);
                }
            }
        }
    }
}