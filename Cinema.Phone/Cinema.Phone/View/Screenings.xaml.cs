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
    using System.Windows.Navigation;
    using Cinema.Phone.ExecuteQueryService;
    using Cinema.Phone.Model;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;

    public partial class Screenings : PhoneApplicationPage
    {
        public Screenings()
        {
            this.InitializeComponent();
            this.FillSchedule(SingletonMovie.Movie.Id);
        }

        private void FillSchedule(int id)
        {
            SingletonQuery.QueryClient.GetMovieSchedulesCompleted += new EventHandler<GetMovieSchedulesCompletedEventArgs>(this.Service_GetMovieSchedule);
            SingletonQuery.QueryClient.GetMovieSchedulesAsync(id);
        }

        void Service_GetMovieSchedule(object sender, GetMovieSchedulesCompletedEventArgs e)
        {
            SingletonQuery.QueryClient.GetMovieSchedulesCompleted -= this.Service_GetMovieSchedule;

            if (e.Error == null)
            {
                ObservableCollection<Schedule> movieSchedule = e.Result;

                if (movieSchedule.Count == 0)
                {
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = "There are no projections for this movie.";
                    textBlock.FontSize = 15;
                    textBlock.TextAlignment = TextAlignment.Center;
                    textBlock.Foreground = new SolidColorBrush(Colors.Yellow);
                    this.schedulePanel.Children.Add(textBlock);
                }
                else
                {
                    foreach (var projection in movieSchedule)
                    {
                        Button button = new Button();
                        button.Content = string.Format("{0} price: {1} $", projection.Date.ToString(), projection.Price);
                        button.Tag = projection.Id;
                        button.FontSize = 20;
                        button.Foreground = new SolidColorBrush(Colors.Black);
                        button.Width = 700;
                        button.Background = new SolidColorBrush(Colors.Gray);
                        button.Margin = new Thickness(0, 0, 0, 10);
                        button.Click += this.btnSeat_Click;

                        this.schedulePanel.Children.Add(button);
                    }
                }
            }
        }

        private void btnSeat_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            SingletonSchedule.Schedule.Id = int.Parse(button.Tag.ToString());
            NavigationService.Navigate(new Uri("/View/SeatBooking.xaml", UriKind.Relative));
        }
    }
}