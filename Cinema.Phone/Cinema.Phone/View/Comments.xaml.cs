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

    public partial class Comments : PhoneApplicationPage
    {
        public Comments()
        {
            this.InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
           this.FillCommentsInformation(SingletonMovie.Movie.Name);
        }

        private void FillCommentsInformation(string name)
        {
            SingletonQuery.QueryClient.GetMovieCommentsCompleted += new EventHandler<GetMovieCommentsCompletedEventArgs>(this.Service_FillInformation);
            SingletonQuery.QueryClient.GetMovieCommentsAsync(name);
        }

        void Service_FillInformation(object sender, GetMovieCommentsCompletedEventArgs e)
        {
            SingletonQuery.QueryClient.GetMovieCommentsCompleted -= this.Service_FillInformation;

            if (e.Error == null)
            {
                ObservableCollection<Comment> comments = e.Result;

                if (comments.Count == 0)
                {
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = "There are no comments to this movie.";
                    textBlock.Foreground = new SolidColorBrush(Colors.Yellow);
                    this.commentsPanel.Children.Add(textBlock);
                }
                else
                {
                    foreach (var comment in comments)
                    {
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

                        TextBlock label = new TextBlock();
                        label.Text = string.Format("commented on {0} by {1}", comment.Date, comment.Username);
                        label.FontSize = 14;
                        label.Foreground = new SolidColorBrush(Colors.Yellow);
                        label.HorizontalAlignment = HorizontalAlignment.Right;

                        TextBlock textBlock = new TextBlock();
                        textBlock.Text = comment.Text;
                        textBlock.FontSize = 20;
                        textBlock.Foreground = new SolidColorBrush(Colors.Yellow);
                        textBlock.Width = 430;
                        textBlock.TextWrapping = TextWrapping.Wrap;

                        sp.Margin = new Thickness(0, 0, 0, 20);
                        sp.Background = linearGradient;

                        sp.Children.Add(textBlock);
                        sp.Children.Add(label);

                        this.commentsPanel.Children.Add(sp);
                    }
                }
            }
        }

        private void btnComment_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.txtComment.Text))
            {
                SingletonQuery.QueryClient.AddCommentCompleted += new EventHandler<AddCommentCompletedEventArgs>(this.Service_AddComment);
                SingletonQuery.QueryClient.AddCommentAsync(this.txtComment.Text, DateTime.Now,
                    SingletonUser.User.Id, SingletonMovie.Movie.Id);
            }
            else
            {
                MessageBox.Show("You must first add a comment!", "Error", MessageBoxButton.OK);
            }
        }

        void Service_AddComment(object sender, AddCommentCompletedEventArgs e)
        {
            SingletonQuery.QueryClient.AddCommentCompleted -= this.Service_AddComment;
            if (e.Error == null)
            {
                if (e.Result)
                {
                    MessageBox.Show("You successfully commented the movie.", "Information", MessageBoxButton.OK);
                    this.commentsPanel.Children.Clear();
                    this.FillCommentsInformation(SingletonMovie.Movie.Name);
                    this.txtComment.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("Error while commenting!", "Error", MessageBoxButton.OK);
                }
            }
        }
    }
}