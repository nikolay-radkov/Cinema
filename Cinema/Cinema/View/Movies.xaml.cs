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
    using System.Windows.Media.Effects;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using Cinema.Controler;
    using Cinema.Model;

    /// <summary>
    /// Interaction logic for Movies.xaml
    /// </summary>
    public partial class Movies : UserControl
    {
        private bool isAdmin = false;

        public Movies()
        {
            this.InitializeComponent();
            this.LoadMovies();
        }

        public void InsertAddButton()
        {
            MainWindow mainWindow = StartWindow.GetMainWindow(this);
            this.isAdmin = mainWindow.User.IsAdmin;

            if (this.isAdmin)
            {
                StackPanel addMoviePannel = new StackPanel();
                addMoviePannel.Margin = new Thickness(5, 0, 5, 15);
                addMoviePannel.Width = 188;

                Button btnAddMovie = new Button();
                btnAddMovie.Background = this.txtHiddenShadow.Background;
                btnAddMovie.Height = 220;
                btnAddMovie.BorderBrush = Brushes.Transparent;
                btnAddMovie.Click += this.add_movie;

                TextBlock txtAddMovie = new TextBlock();
                txtAddMovie.Text = "Add movie";
                txtAddMovie.Effect = this.txtHiddenShadow.Effect;
                txtAddMovie.Foreground = Brushes.White;
                txtAddMovie.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                txtAddMovie.Foreground = Brushes.Yellow;
                txtAddMovie.FontSize = 15;
                txtAddMovie.TextWrapping = TextWrapping.Wrap;
                txtAddMovie.TextAlignment = TextAlignment.Center;
                txtAddMovie.FontWeight = FontWeights.Bold;

                addMoviePannel.Children.Add(btnAddMovie);
                addMoviePannel.Children.Add(txtAddMovie);

                this.moviesContainer.Children.Insert(0, addMoviePannel);
            }
        }

        public void LoadMovies()
        {
            List<Movie> currentMovies = Query.GetMovies();

            foreach (var movie in currentMovies)
            {
                StackPanel infoContainer = new StackPanel();
                infoContainer.Margin = new Thickness(5, 0, 5, 15);
                infoContainer.Width = 188;
           
                Button btn = new Button();
                btn.BorderBrush = Brushes.Transparent;
                BitmapImage imageFromWeb = new BitmapImage(new Uri("http://tu-cinema.net78.net/posters/" + movie.Poster));

                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = imageFromWeb;
                btn.Uid = movie.Name;
                btn.Background = imageBrush;
                btn.Height = 220;
                btn.Click += this.poster_Click;

                TextBlock title = new TextBlock();
                title.Effect = this.txtHiddenShadow.Effect;
                title.Text = movie.Name;
                title.Foreground = Brushes.White;
                title.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                title.FontSize = 15;
                title.TextWrapping = TextWrapping.Wrap;
                title.TextAlignment = TextAlignment.Center;
                title.FontWeight = FontWeights.Bold;

                infoContainer.Children.Add(btn);
                infoContainer.Children.Add(title);
                this.moviesContainer.Children.Add(infoContainer);
            }

            StackPanel backPanel = new StackPanel();

            Button btnBack = new Button();
            btnBack.Background = this.btnHiddenBackground.Background;
            btnBack.BorderBrush = Brushes.Transparent;
            btnBack.Click += this.btnBack_Click;
            btnBack.Width = 185;
            btnBack.Height = 220;
            backPanel.Children.Add(btnBack);

            TextBlock txtBack = new TextBlock();
            txtBack.Effect = this.txtHiddenShadow.Effect;
            txtBack.Text = "Log out";
            txtBack.Foreground = Brushes.White;
            txtBack.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            txtBack.FontSize = 15;
            txtBack.TextWrapping = TextWrapping.Wrap;
            txtBack.TextAlignment = TextAlignment.Center;
            txtBack.FontWeight = FontWeights.Bold;
            backPanel.Children.Add(txtBack);

            this.moviesContainer.Children.Add(backPanel);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            this.moviesContainer.Children.RemoveAt(0);
            MainWindow mainWindow = StartWindow.GetMainWindow(this);
            mainWindow.startingUserControl.Visibility = Visibility.Visible;
            mainWindow.User = new User();
        }

        private void add_movie(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            MainWindow mainWindow = StartWindow.GetMainWindow(this);
            mainWindow.addMovie.Visibility = Visibility.Visible;
        }

        private void poster_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            MainWindow mainWindow = StartWindow.GetMainWindow(this);
            mainWindow.movieInformation.FillInformation((sender as Button).Uid);
            mainWindow.movieInformation.Visibility = Visibility.Visible;
        }
    }
}
