namespace Cinema.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using Cinema.Controler;
    using Cinema.Model;

    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : UserControl
    {
        public SignIn()
        {
            this.InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            MainWindow mainWindow = StartWindow.GetMainWindow(this);
            mainWindow.startingUserControl.Visibility = Visibility.Visible;
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            User user = new User(txtUsername.Text, txtPassword.Password);

            if (Query.SignInUser(user))
            {
                this.Visibility = Visibility.Hidden;
                MainWindow mainWindow = StartWindow.GetMainWindow(this);
                mainWindow.User = user;
                mainWindow.movies.InsertAddButton();

                this.txtUsername.Text = string.Empty;
                this.txtPassword.Password = string.Empty;

                if (mainWindow.User.IsAdmin)
                {
                    mainWindow.adminWindow.Visibility = Visibility.Visible;
                }
                else
                {
                    mainWindow.movies.Visibility = Visibility.Visible;
                }
            }
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            this.btnLogIn_Click(sender, e);
        }
    }
}
