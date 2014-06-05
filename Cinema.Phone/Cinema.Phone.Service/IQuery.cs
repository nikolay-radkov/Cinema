using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Cinema.Phone.Service
{
    [ServiceContract]
    public interface IQuery
    {
        [OperationContract]
        bool IsUserExist(User user);

        [OperationContract]
        User GetUser(User user);

        [OperationContract]
        bool IsEmailRegistered(string email);

        [OperationContract]
        bool HasAlreadyRegisteredUser(string username);

        [OperationContract]
        bool SignUpUser(string username, string password, string secondPassword, string email);

        [OperationContract]
        bool SignInUser(User user);

         [OperationContract]
        List<Movie> GetMovies();

        [OperationContract]
        Movie GetMovieInfo(string name);

        [OperationContract]
        List<Comment> GetMovieComments(string name);

        [OperationContract]
        void CloseConnection();

        [OperationContract]
        bool AddComment(string text, DateTime dateTime, int userId, int movieId);

        [OperationContract]
        bool RateMovie(int movieId, int number);

        [OperationContract]
        bool RefreshMovieRating(int movieId);

        [OperationContract]
        int CalculateRating(int movieId);

        [OperationContract]
        List<Schedule> GetMovieSchedules(int movie_id);

        [OperationContract]
        List<Seat> GetBookedSeats(int scheduleId);

        [OperationContract]
        List<Seat> BookPlace(List<Seat> seats);

        [OperationContract]
        DateTime GetScheduleTime(int scheduleId);
    }
}
