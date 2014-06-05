namespace Cinema.Phone.View
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using Cinema.Phone.ExecuteQueryService;
    using Cinema.Phone.Model;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;

    public partial class Movies : PhoneApplicationPage
    {
        public Movies()
        {
            this.InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
             this.LoadMovies();
        }

        private void LoadMovies()
        {
            SingletonQuery.QueryClient.GetMoviesCompleted += new EventHandler<GetMoviesCompletedEventArgs>(this.Service_LoadMovies);
            SingletonQuery.QueryClient.GetMoviesAsync();
        }

        private void poster_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            SingletonMovie.Movie.Name = button.Tag.ToString();
            NavigationService.Navigate(new Uri("/View/MovieInformation.xaml", UriKind.Relative));
        }

        void Service_LoadMovies(object sender, GetMoviesCompletedEventArgs e)
        {
            SingletonQuery.QueryClient.GetMoviesCompleted -= this.Service_LoadMovies;

            if (e.Error == null)
            {
                ObservableCollection<Movie> currentMovies = e.Result;

                int row = 0;
                int col = 0;

                foreach (var movie in currentMovies)
                {
                    StackPanel infoContainer = new StackPanel();
                    infoContainer.Margin = new Thickness(0, 15, 0, 15);

                    Button btn = new Button();
                    Uri uri = new Uri("http://tu-cinema.net78.net/posters/" + movie.Poster, UriKind.Absolute);
                    BitmapImage imageFromWeb = new BitmapImage();

                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = imageFromWeb;

                    imageFromWeb.CreateOptions = BitmapCreateOptions.None;      
                    imageFromWeb.UriSource = uri;

                    btn.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    btn.Tag = movie.Name;
                    btn.Background = imageBrush;
                    btn.Height = 400;
                    btn.Width = 181;
                    btn.Click += this.poster_Click;
                    btn.Visibility = System.Windows.Visibility.Collapsed;
                    btn.Visibility = System.Windows.Visibility.Visible;

                    TextBlock title = new TextBlock();
                    title.Text = movie.Name;

                    title.Foreground = new SolidColorBrush(Colors.White);

                    title.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    title.FontSize = 15;
                    title.TextWrapping = TextWrapping.Wrap;
                    title.TextAlignment = TextAlignment.Center;
                    title.FontWeight = FontWeights.Bold;

                    infoContainer.Children.Add(btn);
                    infoContainer.Children.Add(title);

                    if (col == 4)
                    {
                        col = 0;
                        row++;
                    }

                    Grid.SetRow(infoContainer, row);
                    
                    Grid.SetColumn(infoContainer, col);

                    col++;

                    moviesContainer.Children.Add(infoContainer);
                }
            }
        }
    }
}