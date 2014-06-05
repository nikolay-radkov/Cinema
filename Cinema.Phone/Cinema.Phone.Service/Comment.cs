using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Cinema.Phone.Service
{
    [DataContract]
    public class Comment
    {
        private string username;
        private string text;
        private string movie;
        private DateTime date;

        public Comment(string username, string text, string movie, DateTime date)
        {
            this.Username = username;
            this.Text = text;
            this.Movie = movie;
            this.Date = date;
        }

        [DataMember]
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

        [DataMember]
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

        [DataMember]
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

        [DataMember]
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