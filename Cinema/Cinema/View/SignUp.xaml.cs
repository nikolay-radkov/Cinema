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

    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : UserControl
    {
        public SignUp()
        {
            this.InitializeComponent();
        }

        private void ClearFields()
        {
            this.txtEmail.Text = string.Empty;
            this.txtPassword.Password = string.Empty;
            this.txtSecondPassword.Password = string.Empty;
            this.txtUsername.Text = string.Empty;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            MainWindow mainWindow = StartWindow.GetMainWindow(this);
            mainWindow.startingUserControl.Visibility = Visibility.Visible;
            this.ClearFields();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (Query.SignUpUser(txtUsername.Text, txtPassword.Password, txtSecondPassword.Password, txtEmail.Text))
            {
                MessageBox.Show("The registration is completed successfully.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            this.btnSignUp_Click(sender, e);
        }
    }
}
