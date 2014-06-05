using Cinema.Phone.ExecuteQueryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Phone.Model
{
    public class SingletonMovie
    {

        private static Movie movie = null;

        public static Movie Movie
        {
            get
            {
                if (movie == null)
                {
                    movie = new Movie();
                }

                return SingletonMovie.movie;
            }
        }

        private SingletonMovie()
        {
        }
    }
}
