namespace Cinema.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class User
    {
        private int id;
        private string username;
        private string password;
        private string email;
        private bool isAdmin;

        public User(string username, string password, string email, bool isAdmin)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.IsAdmin = isAdmin;
        }

        public User(string username, string password)
            : this(username, password, string.Empty, false)
        {
        }

        public User()
            : this(string.Empty, string.Empty, string.Empty, false)
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

        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                this.password = value;
            }
        }

        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                this.email = value;
            }
        }

        public bool IsAdmin
        {
            get
            {
                return this.isAdmin;
            }

            set
            {
                this.isAdmin = value;
            }
        }
    }
}
