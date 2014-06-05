using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using MySql.Data.MySqlClient;
using System.Text;

namespace Cinema.Phone.Service
{    
    public class MySqlConnector
    {
        private string server;
        private string database;
        private string uid;
        private string password;
        private string connectionString;
        private MySqlConnection connection;

        public MySqlConnector()
        {
            this.server = "sql3.freesqldatabase.com ";
            this.database = "sql340414 ";
            this.uid = "sql340414 ";
            this.password = "uA7*wR6%";
            this.connectionString = this.CreateConnectionString();
            this.Connection = new MySqlConnection(this.connectionString);
        }

        public MySqlConnection Connection
        {
            get
            {
                return this.connection;
            }

            set
            {
                this.connection = value;
            }
        }

        private string CreateConnectionString()
        {
            StringBuilder connectionStringBuilder = new StringBuilder();

            connectionStringBuilder.Append("SERVER=");
            connectionStringBuilder.Append(this.server);
            connectionStringBuilder.Append("; DATABASE=");
            connectionStringBuilder.Append(this.database);
            connectionStringBuilder.Append("; UID=");
            connectionStringBuilder.Append(this.uid);
            connectionStringBuilder.Append("; PASSWORD=");
            connectionStringBuilder.Append(this.password);
            connectionStringBuilder.Append("; Port=3306");
            connectionStringBuilder.Append("; CHARSET=utf8");

            return connectionStringBuilder.ToString();
        }
    }
}
