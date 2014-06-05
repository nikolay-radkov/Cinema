namespace Cinema.Phone.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using Cinema.Phone.Model;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;
    using MyToolkit.Multimedia;

    public partial class Trailer : PhoneApplicationPage
    {
        public Trailer()
        {
            this.InitializeComponent();
        }

        private async void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            var url = await YouTube.GetVideoUriAsync(
                SingletonMovie.Movie.TrailerUrl.Substring(SingletonMovie.Movie.TrailerUrl.IndexOf('=') + 1),
                YouTubeQuality.Quality720P);

            if (url != null)
            {
                this.player.Source = url.Uri;
                this.player.Play();
            }
            else
            {
                MessageBox.Show("The video cannot be played at this time!", "Error", MessageBoxButton.OK);
            }
        }        
    }
}