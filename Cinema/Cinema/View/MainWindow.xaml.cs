namespace Cinema
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
    using Cinema.View;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User user;

        public MainWindow()
        {
            this.InitializeComponent();
            this.Closed += new EventHandler(this.MainWindow_Closed);
            this.User = new User();
        }

        public User User
        {
            get
            {
                return this.user;
            }

            set
            {
                this.user = value;
            }
        }
    
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Query.CloseConnection();
            this.Close();
        }
    }
}
