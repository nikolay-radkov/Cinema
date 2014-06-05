namespace Cinema.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Comment
    {
        private int id;
        private string username;
        private string text;
        private string movie;
        private DateTime date;

        public Comment(string username, int id, string text, string movie, DateTime date)
        {
            this.Username = username;
            this.Id = id;
            this.Text = text;
            this.Movie = movie;
            this.Date = date;
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

        public string Username
        {
            get
            {
                return this.username;
            }

            set
            {
                this.username = value;
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
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

        public string Movie
        {
            get
            {
                return this.movie;
            }

            set
            {
                this.movie = value;
            }
        }
    }
}
