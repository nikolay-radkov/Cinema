namespace Cinema.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Schedule
    {
        private int id;
        private DateTime date;
        private float price;
        private int movieId;

        public Schedule(int id, DateTime date, float price, int movieId)
        {
            this.Id = id;
            this.Date = date;
            this.Price = price;
            this.MovieId = movieId;
        }

        public Schedule(int id, DateTime date, float price)
            : this(id, date, price, 0)
        {
        }

        public Schedule(DateTime date, float price, int movieId)
            : this(0, date, price, movieId)
        {
        }

        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return this.date;
            }

            set
            {
                this.date = value;
            }
        }

        public float Price
        {
            get
            {
                return this.price;
            }

            set
            {
                this.price = value;
            }
        }

        public int MovieId
        {
            get
            {
                return this.movieId;
            }

            set
            {
                this.movieId = value;
            }
        }
    }
}
