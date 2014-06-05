namespace Cinema.Phone.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using Cinema.Phone.Resources;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;

    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/SignIn.xaml", UriKind.Relative));
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/SignUp.xaml", UriKind.Relative));
        }
    }
}