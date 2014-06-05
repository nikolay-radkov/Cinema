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

    public partial class SeatBooking : PhoneApplicationPage
    {
        private const int ROWS = 11;
        private const int POSITIONS = 11;
        private ObservableCollection<Seat> userSeats = new ObservableCollection<Seat>();

        public SeatBooking()
        {
            this.InitializeComponent();
            this.PlaceSeats();
        }

        private void PlaceSeats()
        {
            SingletonQuery.QueryClient.GetBookedSeatsCompleted += new EventHandler<GetBookedSeatsCompletedEventArgs>(this.Service_PlaceSeats);
            SingletonQuery.QueryClient.GetBookedSeatsAsync(SingletonSchedule.Schedule.Id);
        }

        private void Service_PlaceSeats(object sender, GetBookedSeatsCompletedEventArgs e)
        {
            SingletonQuery.QueryClient.GetBookedSeatsCompleted -= this.Service_PlaceSeats;

            if (e.Error == null)
            {
                Seat[,] seats = new Seat[ROWS, POSITIONS];
                ObservableCollection<Seat> gotBookedSeats = e.Result;

                foreach (var gotBookedSeat in gotBookedSeats)
                {
                    seats[gotBookedSeat.Row, gotBookedSeat.Position] = gotBookedSeat;
                }

                for (int row = 1; row < ROWS; row++)
                {
                    for (int position = 1; position < POSITIONS; position++)
                    {
                        int currentRow = 11 - row;

                        Button btnSeat = new Button();
                        btnSeat.Margin = new Thickness(-10, -10, -10, -10);
                        btnSeat.Tag = string.Format("row{0}position{1}", row, position);
                        btnSeat.Width = 90;
                        btnSeat.Height = 90;
                        Grid.SetRow(btnSeat, currentRow);
                        Grid.SetColumn(btnSeat, position);
                        btnSeat.BorderBrush = null;

                        if (seats[row, position] == null)
                        {
                            btnSeat.Background = freeSeat.Background;

                            btnSeat.Click += this.btnSeat_Click;
                        }
                        else
                        {
                            btnSeat.Background = bookedSeat.Background;
                        }

                        this.seatsContainer.Children.Add(btnSeat);
                    }
                }

                for (int index = 1; index < ROWS; index++)
                {
                    TextBlock lblRow = new TextBlock();
                    lblRow.Foreground = new SolidColorBrush(Colors.Yellow);
                    lblRow.HorizontalAlignment = HorizontalAlignment.Center;
                    lblRow.VerticalAlignment = VerticalAlignment.Center;
                    lblRow.Text = index.ToString();
                    Grid.SetRow(lblRow, ROWS - index);
                    Grid.SetColumn(lblRow, 0);
                    this.seatsContainer.Children.Add(lblRow);

                    TextBlock lblPosition = new TextBlock();
                    lblPosition.Foreground = new SolidColorBrush(Colors.Yellow);
                    lblPosition.HorizontalAlignment = HorizontalAlignment.Center;
                    lblPosition.Text = index.ToString();
                    Grid.SetRow(lblPosition, 0);
                    Grid.SetColumn(lblPosition, index);
                    this.seatsContainer.Children.Add(lblPosition);
                }

                TextBlock caption = new TextBlock();
                caption.Foreground = new SolidColorBrush(Colors.Yellow);
                caption.Text = "Cols Rows";
                caption.FontSize = 10;
                caption.TextWrapping = TextWrapping.Wrap;

                Grid.SetRow(caption, 0);
                Grid.SetColumn(caption, 0);

                this.seatsContainer.Children.Add(caption);
            }
        }

        private void btnSeat_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string[] ticketPlace = button.Tag.ToString().Split(new string[] { "row", "position" },
                StringSplitOptions.RemoveEmptyEntries);
            int row = int.Parse(ticketPlace[0]);
            int position = int.Parse(ticketPlace[1]);

            bool booked = false;
            int bookedSeatIndex = -1;

            for (int i = 0; i < this.userSeats.Count; i++)
            {
                if (this.userSeats[i].Row == row && this.userSeats[i].Position == position)
                {
                    booked = true;
                    bookedSeatIndex = i;
                    break;
                }
            }

            if (booked)
            {
                this.userSeats.RemoveAt(bookedSeatIndex);
                button.Background = freeSeat.Background;
            }
            else
            {
                Seat s = new Seat();
                s.Row = row;
                s.Position = position;
                s.ScheduleId = SingletonSchedule.Schedule.Id;
                s.UserId = SingletonUser.User.Id;
                s.Email = SingletonUser.User.Email;

                this.userSeats.Add(s);
                button.Background = bookedSeat.Background;
            }
        }

        private void btnBookTicket_Click(object sender, EventArgs e)
        {
            if (this.userSeats.Count > 0)
            {
                this.BookPlace();
            }
            else
            {
                MessageBox.Show("No seats are choosen!", "Error", MessageBoxButton.OK);
            }
        }

        private void BookPlace()
        {
            SingletonQuery.QueryClient.BookPlaceCompleted += new EventHandler<BookPlaceCompletedEventArgs>(this.Service_BookPlace);
            SingletonQuery.QueryClient.BookPlaceAsync(this.userSeats);
        }

        private void Service_BookPlace(object sender, BookPlaceCompletedEventArgs e)
        {
            SingletonQuery.QueryClient.BookPlaceCompleted -= this.Service_BookPlace;

            if (e.Error == null)
            {
                this.userSeats = e.Result;

                foreach (var seat in this.userSeats)
                {
                    MessageBox.Show(string.Format("You successfully booked the seat at row {0} and position {1}",
                           seat.Row, seat.Position), "Information", MessageBoxButton.OK);
                }

                this.userSeats.Clear();
                this.seatsContainer.Children.Clear();
                this.PlaceSeats();
            }
        }
    }
}