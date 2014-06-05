namespace Cinema.View
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Reflection;
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
    /// Interaction logic for SeatBooking.xaml
    /// </summary>
    public partial class SeatBooking : UserControl
    {
        private const int ROWS = 11;
        private const int POSITIONS = 11;
        private List<Seat> userSeats = new List<Seat>();

        public SeatBooking()
        {
            this.InitializeComponent();
        }

        public void PlaceSeats(int scheduleId)
        {
            Seat[,] seats = new Seat[ROWS, POSITIONS];
            seats = Query.GetBookedSeats(scheduleId, seats);
            this.txtScheduleId.Text = scheduleId.ToString();

            for (int row = 1; row < ROWS; row++)
            {
                for (int position = 1; position < POSITIONS; position++)
                {
                    int currentRow = 11 - row;

                    Button btnSeat = new Button();
                    btnSeat.Uid = string.Format("row{0}position{1}", row, position);
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
                Label lblRow = new Label();
                lblRow.Foreground = Brushes.Yellow;
                lblRow.HorizontalAlignment = HorizontalAlignment.Center;
                lblRow.VerticalAlignment = VerticalAlignment.Center;
                lblRow.Content = index;
                Grid.SetRow(lblRow, ROWS - index);
                Grid.SetColumn(lblRow, 0);
                this.seatsContainer.Children.Add(lblRow);
              
                Label lblPosition = new Label();
                lblPosition.Foreground = Brushes.Yellow;
                lblPosition.HorizontalAlignment = HorizontalAlignment.Center;
                lblPosition.Content = index;
                Grid.SetRow(lblPosition, 0);
                Grid.SetColumn(lblPosition, index);
                this.seatsContainer.Children.Add(lblPosition);
            }

            TextBlock caption = new TextBlock();
            caption.Foreground = Brushes.Yellow;
            caption.Text = "Cols Rows";
            caption.TextWrapping = TextWrapping.Wrap;

            Grid.SetRow(caption, 0);
            Grid.SetColumn(caption, 0);

            this.seatsContainer.Children.Add(caption);
        }

        private void btnSeat_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string[] ticketPlace = button.Uid.Split(new string[] { "row", "position" },
                StringSplitOptions.RemoveEmptyEntries);
            int row = int.Parse(ticketPlace[0]);
            int position = int.Parse(ticketPlace[1]);
            MainWindow mainWindow = StartWindow.GetMainWindow(this);
            int scheduleId = int.Parse(this.txtScheduleId.Text);
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
                this.userSeats.Add(new Seat(row, position, scheduleId, mainWindow.User.Id, mainWindow.User.Email));
                            button.Background = bookedSeat.Background;
            }
        }

        private void btnBookTicket_Click(object sender, EventArgs e)
        {
            if (this.userSeats.Count > 0)
            {
                foreach (var userSeat in this.userSeats)
                {
                    if (Query.BookPlace(userSeat.UserId, userSeat.ScheduleId, userSeat.Row, userSeat.Position))
                    {
                        MessageBox.Show(string.Format("You successfully booked the seat at row {0} and position {1}",
                            userSeat.Row, userSeat.Position), "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }

                this.SendMessage();
                this.seatsContainer.Children.Clear();
                this.PlaceSeats(int.Parse(txtScheduleId.Text));
            }
            else
            {
                MessageBox.Show("No seats are choosen!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SendMessage()
        {
            string to = this.userSeats[0].Email;
            string from = "tu.cinema.notification@gmail.com";
            string subject = "Message for your order";
            string body = string.Empty;
            string seats = ":";
            DateTime time = Query.GetScheduleTime(this.userSeats[0].ScheduleId);

            using (StreamReader sr = new StreamReader(
                Assembly.GetExecutingAssembly().GetManifestResourceStream(
                Assembly.GetExecutingAssembly().GetName().Name + ".Resourses.email_template.txt")))
            {
                body = sr.ReadToEnd();
            }

            foreach (var seat in this.userSeats)
            {
                seats += string.Format("\nrow {0} position {1}", seat.Row, seat.Position);
            }

            body = body.Replace(":", seats);
            body = body.Replace("-", "- " + time);

            MailMessage message = new MailMessage(from, to, subject, body);
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("tu.cinema.notification@gmail.com", "cinema50");
            smtp.Host = "smtp.gmail.com";
            try
            {
                smtp.Send(message);
                MessageBox.Show("An email message was send to you.", "Information",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot send email to you!", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            this.userSeats.Clear();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.userSeats.Clear();
            this.Visibility = Visibility.Hidden;
            MainWindow mainWindow = StartWindow.GetMainWindow(this);
            mainWindow.movieInformation.Visibility = Visibility.Visible;
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            this.btnBookTicket_Click(sender, e);
        }
    }
}
