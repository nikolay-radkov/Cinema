namespace Cinema.Controler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using Cinema.Model;
    using MySql.Data.MySqlClient;

    public class AdminQuery
    {
        public static bool AddMovie(Movie movie)
        {
            bool result = false;

            if (MovieValidation.IsValidMovieInput(movie))
            {
                Query.queryToExecute = string.Format(
                    "INSERT INTO movies (name, description, genre, year, duration, actors, director, poster, trailer_url) " +
                    "VALUES (\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", \"{7}\", \"{8}\");",
                    movie.Name, movie.Description, movie.Genre, movie.Year, movie.Duration, movie.Actors, movie.Director, movie.Poster, movie.TrailerUrl);

                int queryResult = Query.queryExecutor.ExecuteNonQuery(Query.queryToExecute);

                if (queryResult == 1)
                {
                    result = true;
                }
                else
                {
                    MessageBox.Show("Error while adding the movie!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return result;
        }

        public static bool AddScreening(Schedule schedule)
        {
            bool result = false;
            string formatForMySql = schedule.Date.ToString("yyyy-MM-dd HH:mm:ss");

            Query.queryToExecute = string.Format(
                        "INSERT INTO schedule (movie_id, time, price) VALUES (\"{0}\", \"{1}\", \"{2}\");",
                        schedule.MovieId, formatForMySql, schedule.Price);
            int queryResult = Query.queryExecutor.ExecuteNonQuery(Query.queryToExecute);

            if (queryResult == 1)
            {
                result = true;
            }
            else
            {
                MessageBox.Show("Error while adding screening!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return result;
        }

        public static bool RemoveScreening(int id)
        {
            bool result = false;

            Query.queryToExecute = string.Format(
                        "DELETE FROM schedule WHERE id = {0};", id);
            int queryResult = Query.queryExecutor.ExecuteNonQuery(Query.queryToExecute);

            if (queryResult == 1)
            {
                result = true;
            }
            else
            {
                MessageBox.Show("Error while removing screening!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return result;
        }

        public static bool RemoveComment(int id)
        {
            bool result = false;

            Query.queryToExecute = string.Format(
                        "DELETE FROM comments WHERE id = {0};", id);
            int queryResult = Query.queryExecutor.ExecuteNonQuery(Query.queryToExecute);

            if (queryResult == 1)
            {
                result = true;
            }
            else
            {
                MessageBox.Show("Error while removing comment!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return result;
        }

        public static bool RemoveMovie(int id)
        {
            bool result = false;

            Query.queryToExecute = string.Format(
                        "DELETE FROM movies WHERE id = {0};", id);
            int queryResult = Query.queryExecutor.ExecuteNonQuery(Query.queryToExecute);

            if (queryResult == 1)
            {
                result = true;
            }
            else
            {
                MessageBox.Show("Error while removing movie!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return result;
        }

        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();

            Query.queryToExecute = string.Format("SELECT * FROM users;");
            MySqlDataReader reader = Query.queryExecutor.ExecuteQuery(Query.queryToExecute);

            if (reader.HasRows)
            {               
                while (reader.Read())
                {
                    User user = new User();

                    user.Id = reader.GetInt32(0);
                    user.Username = reader.GetString(1);
                    user.Email = reader.GetString(3);
                    user.IsAdmin = reader.GetBoolean(4);

                    users.Add(user);
                }
            }

            reader.Close();
            return users;
        }

        public static bool MakeAdmin(int id)
        {
            bool result = false;

            Query.queryToExecute = string.Format(
                        "UPDATE users SET is_admin = {0} WHERE id = {1};", true, id);
            int queryResult = Query.queryExecutor.ExecuteNonQuery(Query.queryToExecute);

            if (queryResult == 1)
            {
                result = true;
            }
            else
            {
                MessageBox.Show("Error while updating user status!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return result;
        }

        public static bool MakeUser(int id)
        {
            bool result = false;

            Query.queryToExecute = string.Format(
                        "UPDATE users SET is_admin = {0} WHERE id = {1};", false, id);
            int queryResult = Query.queryExecutor.ExecuteNonQuery(Query.queryToExecute);

            if (queryResult == 1)
            {
                result = true;
            }
            else
            {
                MessageBox.Show("Error while updating user status!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return result;
        }

        public static bool DeleteUser(int id)
        {
            bool result = false;

            Query.queryToExecute = string.Format(
                        "DELETE FROM users WHERE id = {0};", id);
            int queryResult = Query.queryExecutor.ExecuteNonQuery(Query.queryToExecute);

            if (queryResult == 1)
            {
                result = true;
            }
            else
            {
                MessageBox.Show("Error while removing user!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return result;
        }
    }
}
