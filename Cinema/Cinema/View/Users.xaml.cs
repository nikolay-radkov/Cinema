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
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : UserControl
    {
        private List<int> usersToChange = new List<int>();

        public Users()
        {
            this.InitializeComponent();
            this.LoadUsers();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            MainWindow mainWindow = StartWindow.GetMainWindow(this);
            mainWindow.adminWindow.Visibility = Visibility.Visible;
        }

        private void LoadUsers()
        {
            List<User> users = AdminQuery.GetUsers();

            foreach (var user in users)
            {
                StackPanel horizontalUserPanel = new StackPanel();
                horizontalUserPanel.Orientation = Orientation.Horizontal;

                CheckBox checkBox = new CheckBox();
                checkBox.Uid = user.Id.ToString();
                checkBox.Checked += this.checkBox_Checked;
                checkBox.Unchecked += this.checkBox_Unchecked;
                checkBox.Effect = this.btnDeleteIcon.Effect;
                horizontalUserPanel.Children.Add(checkBox);
                
                TextBlock textBlock = new TextBlock();
                textBlock.Text = string.Format("Username: {0}; Email: {1}; Admin: {2}", user.Username, user.Email, user.IsAdmin);
                textBlock.FontSize = 20;
                textBlock.Foreground = Brushes.Black;
                textBlock.Width = 720;
                textBlock.Background = Brushes.Gray;
                textBlock.Margin = new Thickness(0, 0, 0, 10);
                textBlock.Effect = this.btnDeleteIcon.Effect;
                horizontalUserPanel.Children.Add(textBlock);

                Button btnRemoveUser = new Button();
                btnRemoveUser.VerticalAlignment = VerticalAlignment.Top;
                btnRemoveUser.BorderBrush = Brushes.Transparent;
                btnRemoveUser.Uid = user.Id.ToString();
                btnRemoveUser.Background = this.btnDeleteIcon.Background;
                btnRemoveUser.Width = 40;
                btnRemoveUser.Height = 40;
                btnRemoveUser.Effect = this.btnDeleteIcon.Effect;
                btnRemoveUser.Margin = new Thickness(0, 0, 0, 10);
                btnRemoveUser.Click += this.btnRemoveUser_Click;
                horizontalUserPanel.Children.Add(btnRemoveUser);

                this.usersContainer.Children.Add(horizontalUserPanel);
            }
        }

        private void btnRemoveUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult reuslt = MessageBox.Show("Are you sure you want to delete this user?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (reuslt == MessageBoxResult.Yes)
            {
                Button button = sender as Button;
                if (AdminQuery.DeleteUser(int.Parse(button.Uid)))
                    {
                        MessageBox.Show("You successfully deleted user!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    this.usersContainer.Children.Clear();
                    this.LoadUsers();
            }
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            this.usersToChange.Remove(int.Parse(checkBox.Uid));
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            this.usersToChange.Add(int.Parse(checkBox.Uid));
        }

        private void btnMakeAdmin_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult reuslt = MessageBox.Show("Are you sure you want to make these users admins?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (reuslt == MessageBoxResult.Yes)
            {
                foreach (var user in this.usersToChange)
                {
                    if (AdminQuery.MakeAdmin(user))
                    {
                        MessageBox.Show("You successfully make the user admin.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    this.usersContainer.Children.Clear();
                    this.LoadUsers();
                }

                this.usersToChange.Clear();
            }
        }

        private void btnMakeUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult reuslt = MessageBox.Show("Are you sure you want to make these admins just users?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (reuslt == MessageBoxResult.Yes)
            {
                foreach (var admin in this.usersToChange)
                {
                    if (AdminQuery.MakeUser(admin))
                    {
                        MessageBox.Show("You successfully make the admin just a user.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    this.usersContainer.Children.Clear();
                    this.LoadUsers();
                }

                this.usersToChange.Clear();
            }
        }
    }
}
