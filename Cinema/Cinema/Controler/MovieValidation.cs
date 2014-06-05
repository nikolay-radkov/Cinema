namespace Cinema.Controler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using Cinema.Model;

    public class MovieValidation
    {
        public static bool IsValidMovieInput(Movie movie)
        {
            bool result = true;

            if (!IsValidName(movie.Name))
            {
                result = false;
            }

            if (!IsValidDescription(movie.Description))
            {
                result = false;
            }

            if (!IsValidGenre(movie.Genre))
            {
                result = false;
            }

            if (!IsValidActors(movie.Actors))
            {
                result = false;
            }

            if (!IsValidDirector(movie.Director))
            {
                result = false;
            }

            if (!IsValidPoster(movie.Poster))
            {
                result = false;
            }

            if (!IsValidTrailer(movie.TrailerUrl))
            {
                result = false;
            }

            if (!IsValidYear(movie.Year))
            {
                result = false;
            }

            if (!IsValidDuration(movie.Duration))
            {
                result = false;
            }

            return result;
        }

        private static bool IsValidDuration(int duration)
        {
            if (duration < 0)
            {
                MessageBox.Show("The duration can't be neagtive!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private static bool IsValidYear(int year)
        {
            if (year < 0)
            {
                MessageBox.Show("The year can't be neagtive!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private static bool IsValidTrailer(string trailer)
        {
            if (string.IsNullOrWhiteSpace(trailer))
            {
                MessageBox.Show("The trailer can't be white space!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (trailer.Length < 8)
            {
                MessageBox.Show("The trailer length must be at least 6 characters!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private static bool IsValidPoster(string poster)
        {
            if (string.IsNullOrWhiteSpace(poster))
            {
                MessageBox.Show("The poster can't be white space!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (poster.Length < 5)
            {
                MessageBox.Show("The poster length must be at least 6 characters!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private static bool IsValidDirector(string diretor)
        {
            if (string.IsNullOrWhiteSpace(diretor))
            {
                MessageBox.Show("The diretor can't be white space!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (diretor.Length < 5)
            {
                MessageBox.Show("The diretor length must be at least 6 characters!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private static bool IsValidActors(string actors)
        {
            if (string.IsNullOrWhiteSpace(actors))
            {
                MessageBox.Show("The actors can't be white space!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (actors.Length < 5)
            {
                MessageBox.Show("The actors length must be at least 6 characters!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private static bool IsValidGenre(string genre)
        {
            if (string.IsNullOrWhiteSpace(genre))
            {
                MessageBox.Show("The genre can't be white space!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (genre.Length < 3)
            {
                MessageBox.Show("The genre length must be at least 6 characters!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("The name can't be white space!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (name.Length < 3)
            {
                MessageBox.Show("The name length must be at least 6 characters!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private static bool IsValidDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("The description can't be white space!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (description.Length < 3)
            {
                MessageBox.Show("The description length must be at least 6 characters!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
