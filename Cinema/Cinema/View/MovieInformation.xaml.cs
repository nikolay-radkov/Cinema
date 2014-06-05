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
    /// Interaction logic for MovieInfromation.xaml
    /// </summary>
    public partial class MovieInformation : UserControl
    {
        public MovieInformation()
        {
            this.InitializeComponent();
        }

        public void FillInformation(string name)
        {
            Movie movie = Query.GetMovieInfo(name);

            this.imgPoster.Source = new BitmapImage(new Uri("http://tu-cinema.net78.net/posters/" + movie.Poster));
            this.txtName.Text = movie.Name;
            this.txtGenre.Text = movie.Genre;
            this.txtDuration.Text = movie.Duration.ToString();
            this.txtYear.Text = movie.Year.ToString();
            this.txtDirector.Text = movie.Director;
            this.txtActors.Text = movie.Actors;
            this.txtDescription.Text = movie.Description;
            this.txtTrailer.Text = movie.TrailerUrl;
            this.btnComments.Content = string.Format("comments({0})", movie.Comments);
            this.txtId.Text = movie.Id.ToString();
            this.ratingBar.RatingValue = (int)Math.Ceiling(movie.Raiting / 2);

            MainWindow mainWindow = StartWindow.GetMainWindow(this);

            if (mainWindow.User.IsAdmin)
            {
                this.btnDeleteMovie.Visibility = Visibility.Visible;
                this.btnDeleteMovie.Uid = movie.Id.ToString();
            }

            this.FillCommentsInformation(name);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            this.insertScheduleContainer.Visibility = Visibility.Hidden;
            this.commentsContainer.Visibility = Visibility.Hidden;
            this.scheduleContainer.Visibility = Visibility.Hidden;
            this.informationContainer.Visibility = Visibility.Visible;
            this.ClearFields();

            MainWindow mainWindow = StartWindow.GetMainWindow(this);
            mainWindow.movies.Visibility = Visibility.Visible;
        }

        private void ClearFields()
        {
            this.txtComment.Text = string.Empty;
            this.commentsPanel.Children.Clear();
            this.schedulePanel.Children.Clear();
        }

        private void btnTrailer_Click(object sender, RoutedEventArgs e)
        {
            TrailerWindow trailer = new TrailerWindow();
            trailer.media.Source = new Uri(txtTrailer.Text);
            trailer.Show();
        }

        private void btnComments_Click(object sender, RoutedEventArgs e)
        {
            this.ClearFields();
            this.FillCommentsInformation(this.txtName.Text);
            this.informationContainer.Visibility = Visibility.Hidden;
            this.commentsContainer.Visibility = Visibility.Visible;
        }

        private void btnInformation_Click(object sender, RoutedEventArgs e)
        {
            this.ClearFields();
            this.informationContainer.Visibility = Visibility.Visible;
            this.commentsContainer.Visibility = Visibility.Hidden;
        }

        private void FillCommentsInformation(string name)
        {
            List<Comment> comments = Query.GetMovieComments(name);

            if (comments.Count == 0)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = "There are no comments to this movie.";
                textBlock.Foreground = Brushes.Yellow;
                this.commentsPanel.Children.Add(textBlock);
            }
            else
            {
                foreach (var comment in comments)
                {
                    StackPanel horizontalSchedulePanel = new StackPanel();
                    horizontalSchedulePanel.Orientation = Orientation.Horizontal;

                    StackPanel sp = new StackPanel();
                    LinearGradientBrush linearGradient = new LinearGradientBrush();
                    linearGradient.StartPoint = new Point(0, 0);
                    linearGradient.EndPoint = new Point(0, 1);
                    linearGradient.Opacity = 0.8;

                    GradientStop whiteGS = new GradientStop();
                    whiteGS.Color = Colors.White;
                    whiteGS.Offset = 0.0;
                    linearGradient.GradientStops.Add(whiteGS);

                    GradientStop blackGS = new GradientStop();
                    blackGS.Color = Colors.Black;
                    blackGS.Offset = 0.2;
                    linearGradient.GradientStops.Add(blackGS);

                    Label label = new Label();
                    label.Content = string.Format("commented on {0} by {1}", comment.Date, comment.Username);
                    label.FontSize = 10;
                    label.Foreground = Brushes.Yellow;
                    label.HorizontalAlignment = HorizontalAlignment.Right;

                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = comment.Text;
                    textBlock.FontSize = 20;
                    textBlock.Foreground = Brushes.Yellow;
                    textBlock.Width = 450;
                    textBlock.TextWrapping = TextWrapping.Wrap;

                    sp.Margin = new Thickness(0, 0, 0, 20);
                    sp.Background = linearGradient;

                    sp.Children.Add(textBlock);
                    sp.Children.Add(label);

                    horizontalSchedulePanel.Children.Add(sp);

                    MainWindow mainWindow = StartWindow.GetMainWindow(this);

                    if (mainWindow.User.IsAdmin)
                    {
                        Button btnRemoveComment = new Button();
                        btnRemoveComment.VerticalAlignment = VerticalAlignment.Top;
                        btnRemoveComment.BorderBrush = Brushes.Transparent;
                        btnRemoveComment.Uid = comment.Id.ToString();
                        btnRemoveComment.Background = this.btnDeleteIcon.Background;
                        btnRemoveComment.Width = 40;
                        btnRemoveComment.Height = 40;
                        btnRemoveComment.Margin = new Thickness(0, 0, 0, 10);
                        btnRemoveComment.Click += this.btnRemoveComment_Click;
                        horizontalSchedulePanel.Children.Add(btnRemoveComment);
                    }

                    this.commentsPanel.Children.Add(horizontalSchedulePanel);
                }
            }
        }

        private void btnComment_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.txtComment.Text))
            {
                MainWindow mainWindow = StartWindow.GetMainWindow(this);
                int movieId = int.Parse(this.txtId.Text);
                DateTime date = DateTime.Now;

                if (Query.AddComment(this.txtComment.Text, date, mainWindow.User.Id, movieId))
                {
                    MessageBox.Show("You successfully commented the movie.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.ClearFields();
                    this.FillInformation(this.txtName.Text);
                }
            }
            else
            {
                MessageBox.Show("You must first add a comment!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRate_Click(object sender, RoutedEventArgs e)
        {
            if (Query.RateMovie(int.Parse(this.txtId.Text), this.ratingBar.RatingValue))
            {
                MessageBox.Show("You successfully rate the movie!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                this.FillInformation(this.txtName.Text);
            }
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            this.ClearFields();
            this.informationContainer.Visibility = Visibility.Visible;
            this.scheduleContainer.Visibility = Visibility.Hidden;
        }

        private void btnBookPlace_Click(object sender, RoutedEventArgs e)
        {
            this.FillSchedule();
            this.informationContainer.Visibility = Visibility.Hidden;
            this.scheduleContainer.Visibility = Visibility.Visible;
        }

        private void FillSchedule()
        {
            MainWindow mainWindow = StartWindow.GetMainWindow(this);

            if (mainWindow.User.IsAdmin)
            {
                this.InsertAddScreeningButton();
            }

            List<Schedule> movieSchedule = Query.GetMovieSchedules(int.Parse(this.txtId.Text));

            if (movieSchedule.Count == 0)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = "There are no projections for this movie.";
                textBlock.FontSize = 15;
                textBlock.TextAlignment = TextAlignment.Center;
                textBlock.Foreground = Brushes.Yellow;
                this.schedulePanel.Children.Add(textBlock);
            }
            else
            {
                foreach (var projection in movieSchedule)
                {
                    StackPanel horizontalSchedulePanel = new StackPanel();
                    horizontalSchedulePanel.Orientation = Orientation.Horizontal;

                    Button button = new Button();
                    button.Content = string.Format("{0} price: {1} $", projection.Date.ToString(), projection.Price);
                    button.Uid = projection.Id.ToString();
                    button.FontSize = 20;
                    button.Foreground = Brushes.Black;
                    button.Width = 465;
                    button.Background = Brushes.Gray;
                    button.Margin = new Thickness(0, 0, 0, 10);
                    button.Effect = ratingBar.Effect;
                    button.Click += this.btnSeat_Click;

                    horizontalSchedulePanel.Children.Add(button);

                    if (mainWindow.User.IsAdmin)
                    {
                        Button btnRemoveScreening = new Button();
                        btnRemoveScreening.BorderBrush = Brushes.Transparent;
                        btnRemoveScreening.Uid = projection.Id.ToString();
                        btnRemoveScreening.Background = this.btnDeleteIcon.Background;
                        btnRemoveScreening.Width = 30;
                        btnRemoveScreening.Height = 30;
                        btnRemoveScreening.Margin = new Thickness(0, 0, 0, 10);
                        btnRemoveScreening.Click += this.btnRemoveScreening_Click;
                        horizontalSchedulePanel.Children.Add(btnRemoveScreening);
                    }

                    this.schedulePanel.Children.Add(horizontalSchedulePanel);
                }
            }
        }

        private void btnRemoveScreening_Click(object sender, RoutedEventArgs e)
        {
             MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this screening?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

             if (result == MessageBoxResult.Yes)
             {
                 Button button = sender as Button;

                 if (AdminQuery.RemoveScreening(int.Parse(button.Uid)))
                 {
                     MessageBox.Show("You successfully removed the screening.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                     this.schedulePanel.Children.Clear();
                     this.insertScheduleContainer.Visibility = Visibility.Hidden;
                     this.btnBookPlace_Click(sender, e);
                 }
             }
        }

        private void btnRemoveComment_Click(object sender, RoutedEventArgs e)
        {
             MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this comment?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

             if (result == MessageBoxResult.Yes)
             {
                 Button button = sender as Button;

                 if (AdminQuery.RemoveComment(int.Parse(button.Uid)))
                 {
                     MessageBox.Show("You successfully removed the comment.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                     this.commentsPanel.Children.Clear();
                     this.FillInformation(this.txtName.Text);
                     this.btnComments_Click(sender, e);
                 }
             }
        }

        public void InsertAddScreeningButton()
        {
            MainWindow mainWindow = StartWindow.GetMainWindow(this);

            if (mainWindow.User.IsAdmin)
            {
                Button button = new Button();
                button.HorizontalAlignment = HorizontalAlignment.Left;
                button.Content = "Insert screening";
                button.FontSize = 20;
                button.Foreground = Brushes.Black;
                button.Width = 490;
                button.Background = Brushes.Yellow;
                button.Margin = new Thickness(0, 0, 0, 10);
                button.Effect = ratingBar.Effect;
                button.Click += this.btnAddScreening_Click;

                this.schedulePanel.Children.Insert(0, button);
            }
        }

        private void btnSeat_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            MainWindow mainWindow = StartWindow.GetMainWindow(this);
            mainWindow.seatBooking.PlaceSeats(int.Parse(button.Uid));
            this.Visibility = Visibility.Hidden;
            mainWindow.seatBooking.Visibility = Visibility.Visible;
        }

        private void btnAddScreening_Click(object sender, EventArgs e)
        {
            MainWindow mainWindow = StartWindow.GetMainWindow(this);
            mainWindow.movieInformation.scheduleContainer.Visibility = Visibility.Hidden;
            mainWindow.movieInformation.insertScheduleContainer.Visibility = Visibility.Visible;
        }

        private void btnAddNewScreening_Click(object sender, RoutedEventArgs e)
        {
            DateTime? date = this.txtNewDate.SelectedDate;
            int? hour = this.txtNewHour.Value;
            int? minute = this.txtNewMinute.Value;

            if (date == null)
            {
                MessageBox.Show("You must choose date!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (hour == null)
            {
                MessageBox.Show("You must choose hour!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (hour < 0 || 23 < hour)
            {
                MessageBox.Show("Hour must be between 0 and 23!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (minute == null)
            {
                MessageBox.Show("You must choose minute!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (minute < 0 || 59 < minute)
            {
                MessageBox.Show("Minute must be between 0 and 59!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime finalDateTime = date.Value.AddHours((double)hour);
            finalDateTime = finalDateTime.AddMinutes((double)minute);

            float price = 0;

            if (!float.TryParse(this.txtNewPrice.Text, out price))
            {
                MessageBox.Show("Price must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Schedule schedule = new Schedule(finalDateTime, price, int.Parse(this.txtId.Text));

            if (AdminQuery.AddScreening(schedule))
            {
                MessageBox.Show("You successfully added new screening to the movie.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                this.schedulePanel.Children.Clear();
                this.ClearInsertScheduleContainer();
                this.insertScheduleContainer.Visibility = Visibility.Hidden;
                this.btnBookPlace_Click(sender, e);
            }
        }

        private void ClearInsertScheduleContainer()
        {
            this.txtNewDate.SelectedDate = null;
            this.txtNewHour.Value = null;
            this.txtNewMinute.Value = null;
            this.txtNewPrice.Text = string.Empty;
        }

        private void btnScreenings_Click(object sender, RoutedEventArgs e)
        {
            this.ClearInsertFields();
            this.scheduleContainer.Visibility = Visibility.Visible;
            this.insertScheduleContainer.Visibility = Visibility.Hidden;
        }

        private void ClearInsertFields()
        {
            this.txtNewPrice.Text = string.Empty;
            this.txtNewHour.Value = null;
            this.txtNewMinute.Value = null;
            this.txtNewDate.SelectedDate = null;
        }

        private void btnDeleteMovie_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this movie?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (AdminQuery.RemoveMovie(int.Parse(this.btnDeleteMovie.Uid)))
                {
                    MainWindow mainWindow = StartWindow.GetMainWindow(this);
                    mainWindow.movies.moviesContainer.Children.Clear();
                    mainWindow.movies.LoadMovies();
                    mainWindow.movies.InsertAddButton();

                    MessageBox.Show("You successfully removed the movie.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                    this.ClearFields();
                    this.btnBack_Click(sender, e);
                }
            }
        }
    }
}
