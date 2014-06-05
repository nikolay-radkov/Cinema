namespace Cinema.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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

        public Movie(string name, string description, string genre, int year,
           int duration, string actors, string director, string poster,
           string trailerUrl)
            : this(0, name, description, genre, year, duration, actors, director, poster, trailerUrl, 0, 0)
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
