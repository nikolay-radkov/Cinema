using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Cinema.Phone.Service
{
    [DataContract]
    public class Movie
    {
        private int id;
        private string name;
        private string description;
        private string genre;
        private int year;
        private int duration;
        private string actors;
        private string director;
        private string poster;
        private string trailerUrl;
        private float raiting;
        private int comments;

        public Movie(int id, string name, string description, string genre, int year,
            int duration, string actors, string director, string poster,
            string trailerUrl, float raiting, int comments)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Genre = genre;
            this.Year = year;
            this.Duration = duration;
            this.Actors = actors;
            this.Director = director;
            this.Poster = poster;
            this.TrailerUrl = trailerUrl;
            this.Raiting = raiting;
            this.Comments = comments;
        }

        public Movie(int id, string name, string description, string genre, int year,
           int duration, string actors, string director, string poster,
           string trailerUrl, float raiting)
            : this(id, name, description, genre, year, duration, actors, director, poster, trailerUrl, raiting, 0)
        {
        }

        public Movie() { }

        [DataMember]
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

        [DataMember]
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }

        [DataMember]
        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                this.description = value;
            }
        }

        [DataMember]
        public string Genre
        {
            get
            {
                return this.genre;
            }

            set
            {
                this.genre = value;
            }
        }

        [DataMember]
        public int Year
        {
            get
            {
                return this.year;
            }

            set
            {
                this.year = value;
            }
        }

        [DataMember]
        public int Duration
        {
            get
            {
                return this.duration;
            }

            set
            {
                this.duration = value;
            }
        }

        [DataMember]
        public string Actors
        {
            get
            {
                return this.actors;
            }

            set
            {
                this.actors = value;
            }
        }

        [DataMember]
        public string Director
        {
            get
            {
                return this.director;
            }

            set
            {
                this.director = value;
            }
        }

        [DataMember]
        public string Poster
        {
            get
            {
                return this.poster;
            }

            set
            {
                this.poster = value;
            }
        }

        [DataMember]
        public string TrailerUrl
        {
            get
            {
                return this.trailerUrl;
            }

            set
            {
                this.trailerUrl = value;
            }
        }

        [DataMember]
        public float Raiting
        {
            get
            {
                return this.raiting;
            }

            set
            {
                this.raiting = value;
            }
        }

        [DataMember]
        public int Comments
        {
            get
            {
                return this.comments;
            }

            set
            {
                this.comments = value;
            }
        }
    }
}
