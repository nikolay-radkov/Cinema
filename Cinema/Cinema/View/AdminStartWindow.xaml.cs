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
    /// Interaction logic for AdminStartWindow.xaml
    /// </summary>
    public partial class AdminStartWindow : UserControl
    {
        public AdminStartWindow()
        {
            this.InitializeComponent();
        }

        private void btnMovies_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            MainWindow mainWindow = StartWindow.GetMainWindow(this);
            mainWindow.movies.Visibility = Visibility.Visible;
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            MainWindow mainWindow = StartWindow.GetMainWindow(this);
            mainWindow.users.Visibility = Visibility.Visible;
        }
    }
}
