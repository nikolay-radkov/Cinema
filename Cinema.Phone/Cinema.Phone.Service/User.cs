using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Cinema.Phone.Service
{
    [DataContract]
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

        [DataMember]
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

        [DataMember]
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