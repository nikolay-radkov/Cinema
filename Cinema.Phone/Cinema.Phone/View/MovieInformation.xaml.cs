namespace Cinema.Phone.View
{
    using System;
    using System.Collections.Generic;
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

    public partial class MovieInformation : PhoneApplicationPage
    {
        public MovieInformation()
        {
            this.InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            SingletonQuery.QueryClient.RateMovieCompleted += new EventHandler<RateMovieCompletedEventArgs>(this.Service_RateMovie);
            this.FillInformation(SingletonMovie.Movie.Name);
        }

        private void FillInformation(string name)
        {
            SingletonQuery.QueryClient.GetMovieInfoCompleted += new EventHandler<GetMovieInfoCompletedEventArgs>(this.Service_FillInformation);
            SingletonQuery.QueryClient.GetMovieInfoAsync(name);
        }

        private void Service_FillInformation(object sender, GetMovieInfoCompletedEventArgs e)
        {
            SingletonQuery.QueryClient.GetMovieInfoCompleted -= this.Service_FillInformation;

            if (e.Error == null)
            {
                Movie movie = e.Result;

                this.imgPoster.Source = new BitmapImage(new Uri("http://tu-cinema.net78.net/posters/" + movie.Poster));
                this.imgPoster.Visibility = Visibility.Collapsed;
                this.imgPoster.Visibility = Visibility.Visible;
                this.txtName.Text = movie.Name;
                this.txtGenre.Text = movie.Genre;
                this.txtDuration.Text = movie.Duration.ToString();
                this.txtYear.Text = movie.Year.ToString();
                this.txtDirector.Text = movie.Director;
                this.txtActors.Text = movie.Actors;
                this.txtDescription.Text = movie.Description;
                this.btnComments.Content = string.Format("comments({0})", movie.Comments);
                this.ratingBar.Value = (int)Math.Ceiling(movie.Raiting / 2);
                SingletonMovie.Movie.Id = movie.Id;
                SingletonMovie.Movie.TrailerUrl = movie.TrailerUrl;
            }
        }

        private void Service_RateMovie(object sender, RateMovieCompletedEventArgs e)
        {
            SingletonQuery.QueryClient.RateMovieCompleted -= this.Service_RateMovie;

            if (e.Error == null)
            {
                if (e.Result)
                {
                    MessageBox.Show("You successfully rate the movie!", "Information", MessageBoxButton.OK);
                    this.FillInformation(SingletonMovie.Movie.Name);
                }
                else
                {
                    MessageBox.Show("Error while rating the movie!", "Error", MessageBoxButton.OK);
                }
            }
        }

        private void btnTrailer_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/Trailer.xaml", UriKind.Relative));
        }

        private void btnComments_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/Comments.xaml", UriKind.Relative));
        }

        private void btnRate_Click(object sender, RoutedEventArgs e)
        {
            int rate = (int)Math.Ceiling(ratingBar.Value * 2);
            SingletonQuery.QueryClient.RateMovieAsync(SingletonMovie.Movie.Id, rate);
        }

        private void btnBookPlace_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/Screenings.xaml", UriKind.Relative));
        }
    }
}