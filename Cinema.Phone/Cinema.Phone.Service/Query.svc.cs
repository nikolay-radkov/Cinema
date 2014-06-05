using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MySql.Data.MySqlClient;

namespace Cinema.Phone.Service
{
    public class Query : IQuery
    {
        public static QueryExecutor queryExecutor = new QueryExecutor();
        public static string queryToExecute = string.Empty;

        public bool IsUserExist(User user)
        {
            bool result = false;

            queryToExecute = string.Format(
                "SELECT * FROM users WHERE user_name=\"{0}\" AND password=\"{1}\";", user.Username, user.Password);
            MySqlDataReader reader = queryExecutor.ExecuteQuery(queryToExecute);

            if (reader.HasRows)
            {
                result = true;

                while (reader.Read())
                {
                    user.Id = reader.GetInt32(0);
                    user.Email = reader.GetString(3);
                    user.IsAdmin = reader.GetBoolean(4);
                }
            }

            reader.Close();
            return result;
        }

        public User GetUser(User user)
        {
            User result = new User();

            queryToExecute = string.Format(
                "SELECT * FROM users WHERE user_name=\"{0}\" AND password=\"{1}\";", user.Username, user.Password);
            MySqlDataReader reader = queryExecutor.ExecuteQuery(queryToExecute);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Id = reader.GetInt32(0);
                    result.Username = reader.GetString(1);
                    result.Password = reader.GetString(2);
                    result.Email = reader.GetString(3);
                    result.IsAdmin = reader.GetBoolean(4);
                }
            }

            reader.Close();
            return result;
        }

        public bool IsEmailRegistered(string email)
        {
            bool result = false;

            queryToExecute = string.Format(
                "SELECT * FROM users WHERE email=\"{0}\";",
                email);
            MySqlDataReader reader = queryExecutor.ExecuteQuery(queryToExecute);

            if (reader.HasRows)
            {
                result = true;
            }

            reader.Close();
            return result;
        }

        public bool HasAlreadyRegisteredUser(string username)
        {
            bool result = false;

            queryToExecute = string.Format(
                "SELECT * FROM users WHERE user_name=\"{0}\";",
                username);
            MySqlDataReader reader = queryExecutor.ExecuteQuery(queryToExecute);

            if (reader.HasRows)
            {
                result = true;
            }

            reader.Close();
            return result;
        }

        public bool SignUpUser(string username, string password, string secondPassword, string email)
        {
            bool result = false;

            if (InputValidation.IsValidSignUpInput(username, password, secondPassword, email))
            {
                if (!this.HasAlreadyRegisteredUser(username) || !this.IsEmailRegistered(email))
                {
                    queryToExecute = string.Format(
                        "INSERT INTO users (user_name, password, email) VALUES (\"{0}\", \"{1}\", \"{2}\");", username, password, email);
                    int queryResult = queryExecutor.ExecuteNonQuery(queryToExecute);

                    if (queryResult == 1)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        public bool SignInUser(User user)
        {
            bool result = false;

            if (InputValidation.IsValidSignInInput(user.Username, user.Password))
            {
                if (this.IsUserExist(user))
                {
                    result = true;
                }
            }

            return result;
        }

        public List<Movie> GetMovies()
        {
            List<Movie> currentMovies = new List<Movie>();

            queryToExecute = "SELECT * FROM movies;";
            MySqlDataReader reader = queryExecutor.ExecuteQuery(queryToExecute);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Movie movie = new Movie(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetInt32(4),
                    reader.GetInt32(5),
                    reader.GetString(6),
                    reader.GetString(7),
                    reader.GetString(8),
                    reader.GetString(9),
                    reader.GetFloat(10));

                    currentMovies.Add(movie);
                }
            }

            reader.Close();
            return currentMovies;
        }

        public Movie GetMovieInfo(string name)
        {
            Movie movie = null;

            queryToExecute = string.Format("SELECT movies.id, movies.name, movies.description, " +
            "movies.genre, movies.year, movies.duration, movies.actors, movies.director, " +
            "movies.poster, movies.trailer_url, movies.rating, COUNT(comments.id)" +
            "FROM movies LEFT JOIN comments ON movies.id = comments.movie_id WHERE name = \"{0}\";", name);

            MySqlDataReader reader = queryExecutor.ExecuteQuery(queryToExecute);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    movie = new Movie(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetInt32(4),
                        reader.GetInt32(5),
                        reader.GetString(6),
                        reader.GetString(7),
                        reader.GetString(8),
                        reader.GetString(9),
                        reader.GetFloat(10),
                        reader.GetInt32(11));
                }
            }

            reader.Close();
            return movie;
        }

        public List<Comment> GetMovieComments(string name)
        {
            List<Comment> comments = new List<Comment>();

            queryToExecute = string.Format("SELECT users.user_name, comments.text, movies.name, comments.date  " +
            "FROM movies LEFT JOIN comments ON movies.id = comments.movie_id JOIN users " +
            "ON users.id = comments.user_id WHERE movies.name = \"{0}\" ORDER BY comments.date;", name);

            MySqlDataReader reader = queryExecutor.ExecuteQuery(queryToExecute);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var date = reader.GetMySqlDateTime(3);
                    DateTime datetime = DateTime.Parse(date.ToString());

                    Comment comment = new Comment(
                       reader.GetString(0),
                       reader.GetString(1),
                       reader.GetString(2),
                       datetime);
                    comments.Add(comment);
                }
            }

            reader.Close();
            return comments;
        }

        public void CloseConnection()
        {
            queryExecutor.CloseConnection();
        }

        public bool AddComment(string text, DateTime dateTime, int userId, int movieId)
        {
            bool result = false;

            string formatForMySql = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

            queryToExecute = string.Format(
                        "INSERT INTO comments (user_id, movie_id, text, date) VALUES (\"{0}\", \"{1}\", \"{2}\", \"{3}\");", userId, movieId, text, formatForMySql);
            int queryResult = queryExecutor.ExecuteNonQuery(queryToExecute);

            if (queryResult == 1)
            {
                result = true;
            }

            return result;
        }

        public bool RateMovie(int movieId, int number)
        {
            bool result = false;
            queryToExecute = string.Format("INSERT INTO rating (movie_id, number) VALUES (\"{0}\", \"{1}\");", movieId, number);
            int queryResult = queryExecutor.ExecuteNonQuery(queryToExecute);

            if (queryResult == 1)
            {
                if (this.RefreshMovieRating(movieId))
                {
                    result = true;
                }
            }

            return result;
        }

        public bool RefreshMovieRating(int movieId)
        {
            bool result = false;
            int rating = this.CalculateRating(movieId);

            queryToExecute = string.Format(
                "UPDATE movies SET rating = {0} WHERE id = {1};",  rating, movieId);
            int queryResult = queryExecutor.ExecuteNonQuery(queryToExecute);

            if (queryResult == 1)
            {
                result = true;
            }

            return result;
        }

        public int CalculateRating(int movieId)
        {
            int result = 0;

            queryToExecute = string.Format(
                "SELECT AVG(number) FROM rating WHERE movie_id={0};",
                movieId);

            MySqlDataReader reader = queryExecutor.ExecuteQuery(queryToExecute);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result = reader.GetInt32(0);
                }
            }

            reader.Close();
            return result;
        }

        public List<Schedule> GetMovieSchedules(int movie_id)
        {
            List<Schedule> schedules = new List<Schedule>();

            queryToExecute = string.Format("SELECT id, time, price FROM schedule WHERE movie_id = \"{0}\" ORDER BY time;", movie_id);

            MySqlDataReader reader = queryExecutor.ExecuteQuery(queryToExecute);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var date = reader.GetMySqlDateTime(1);
                    DateTime datetime = DateTime.Parse(date.ToString());

                    Schedule schedule = new Schedule(
                       reader.GetInt32(0),
                       datetime,
                       reader.GetFloat(2));
                    schedules.Add(schedule);
                }
            }

            reader.Close();
            return schedules;
        }

        public List<Seat> GetBookedSeats(int scheduleId)
        {
            List<Seat> seats = new List<Seat>();
            queryToExecute = string.Format("SELECT id, row, position FROM tickets WHERE schedule_id = \"{0}\";", scheduleId);

            MySqlDataReader reader = queryExecutor.ExecuteQuery(queryToExecute);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Seat seat = new Seat(
                       reader.GetInt32(0),
                       reader.GetInt32(1),
                       reader.GetInt32(2));

                    seats.Add(seat);
                }
            }

            reader.Close();
            return seats;
        }

        public List<Seat> BookPlace(List<Seat> seats)
        {
            foreach (var seat in seats)
            {
                queryToExecute = string.Format(
                  "INSERT INTO tickets (user_id, schedule_id, row, position) VALUES (\"{0}\", \"{1}\", \"{2}\", \"{3}\");", seat.UserId, seat.ScheduleId, seat.Row, seat.Position);

                int queryResult = queryExecutor.ExecuteNonQuery(queryToExecute);

                if (queryResult != 1)
                {
                    seats.Remove(seat);
                }
            }

            return seats;
        }

        public DateTime GetScheduleTime(int scheduleId)
        {
            DateTime scheduleTime = new DateTime();

            queryToExecute = string.Format("SELECT time FROM schedule WHERE id = \"{0}\"", scheduleId);

            MySqlDataReader reader = queryExecutor.ExecuteQuery(queryToExecute);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var date = reader.GetMySqlDateTime(0);
                    scheduleTime = DateTime.Parse(date.ToString());
                }
            }

            reader.Close();
            return scheduleTime;
        }
    }
}
