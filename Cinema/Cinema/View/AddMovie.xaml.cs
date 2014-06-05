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
    /// Interaction logic for AddMovie.xaml
    /// </summary>
    public partial class AddMovie : UserControl
    {
        private Microsoft.Win32.OpenFileDialog dlg;
        private bool isUploaded = false;
        private string poster = string.Empty;

        public AddMovie()
        {
            this.InitializeComponent();
        }

        private void btnAddMovie_Click(object sender, RoutedEventArgs e)
        {
            if (!this.isUploaded)
            {
                MessageBox.Show("First, you must upload the image!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int year = 0;

                if (!int.TryParse(this.txtYear.Text, out year))
                {
                    MessageBox.Show("Your input for year is not a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int duration = 0;

                if (!int.TryParse(this.txtDuration.Text, out duration))
                {
                    MessageBox.Show("Your input for duration is not a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Movie movie = new Movie(this.txtName.Text, this.txtDescription.Text, this.txtGenre.Text, year,
                    duration, this.txtActors.Text, this.txtDirector.Text, this.poster, this.txtTrailer.Text);

                if (AdminQuery.AddMovie(movie))
                {
                    MessageBox.Show("Movie was added successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow mainWindow = StartWindow.GetMainWindow(this);
                    mainWindow.movies.moviesContainer.Children.Clear();
                    mainWindow.movies.LoadMovies();
                    mainWindow.movies.InsertAddButton();

                    mainWindow.movieInformation.FillInformation(movie.Name);
                    this.Visibility = Visibility.Hidden;
                    mainWindow.movieInformation.Visibility = Visibility.Visible;
                    this.ClearFields();
                }
            }
        }

        private void ClearFields()
        {
            this.dlg = null;
            this.isUploaded = false;
            this.txtActors.Text = string.Empty;
            this.txtDescription.Text = string.Empty;
            this.txtDirector.Text = string.Empty;
            this.txtDuration.Text = string.Empty;
            this.txtGenre.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtTrailer.Text = string.Empty;
            this.txtYear.Text = string.Empty;
            this.poster = string.Empty;
            this.imgPoster.Source = null;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            this.dlg = new Microsoft.Win32.OpenFileDialog();
            this.dlg.DefaultExt = ".png";
            this.dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            Nullable<bool> result = this.dlg.ShowDialog();

            if (result == true)
            {
                this.imgPoster.Source = new BitmapImage(new Uri(this.dlg.FileName));
            }
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            if (this.dlg != null)
            {
                if (Controler.ImageManipulation.UploadFile(this.dlg.FileName))
                {
                    MessageBox.Show("Image was uploaded successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.poster = this.dlg.FileName.Substring(this.dlg.FileName.LastIndexOf('\\'));
                    this.isUploaded = true;
                }
                else
                {
                    MessageBox.Show("Cannot upload the file!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Choose image first!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.ClearFields();

            MainWindow mainWindow = StartWindow.GetMainWindow(this);
            this.Visibility = Visibility.Hidden;
            mainWindow.movies.Visibility = Visibility.Visible;
        }
    }
}
