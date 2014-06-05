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

    public partial class SignIn : PhoneApplicationPage
    {
        public SignIn()
        {
            this.InitializeComponent();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            SingletonUser.User.Username = txtUsername.Text;
            SingletonUser.User.Password = txtPassword.Password;

            SingletonQuery.QueryClient.GetUserCompleted += new EventHandler<GetUserCompletedEventArgs>(this.Service_GetUser);
            SingletonQuery.QueryClient.GetUserAsync(SingletonUser.User);
        }

        void Service_GetUser(object sender, GetUserCompletedEventArgs e)
        {
            SingletonQuery.QueryClient.GetUserCompleted -= this.Service_GetUser;

            if (e.Error == null)
            {
                if (e.Result.Id != 0)
                {
                    SingletonUser.User.Id = e.Result.Id;
                    SingletonUser.User.Email = e.Result.Email;
                    SingletonUser.User.IsAdmin = e.Result.IsAdmin;
                    NavigationService.Navigate(new Uri("/View/Movies.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("Incorrect username or password!", "Errr", MessageBoxButton.OK);
                }
            }
        }
    }
}