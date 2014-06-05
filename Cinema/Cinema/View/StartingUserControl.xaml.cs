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
    /// Interaction logic for StartingUserControl.xaml
    /// </summary>
    public partial class StartingUserControl : UserControl
    {
        public StartingUserControl()
        {
            this.InitializeComponent();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            MainWindow mainWindow = StartWindow.GetMainWindow(this);
            mainWindow.signIn.Visibility = Visibility.Visible;
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            MainWindow mainWindow = StartWindow.GetMainWindow(this);
            mainWindow.signUp.Visibility = Visibility.Visible;
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            this.btnSignIn_Click(sender, e);
        }
    }
}
