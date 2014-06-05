using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using MySql.Data.MySqlClient;

namespace Cinema.Phone.Service
{

    public class QueryExecutor
    {
        private MySqlConnector connector;
        private MySqlCommand command;

        public QueryExecutor()
        {
            this.connector = new MySqlConnector();
            this.command = new MySqlCommand();
            this.command.Connection = this.connector.Connection;
            this.connector.Connection.Open();     
        }

        public MySqlDataReader ExecuteQuery(string commandText)
        {
            this.command.CommandText = commandText;

            MySqlDataReader reader = null;
            reader = this.command.ExecuteReader();

            return reader;
        }

        public int ExecuteNonQuery(string commandText)
        {
            this.command.CommandText = commandText;

            int result = 0;
            result = this.command.ExecuteNonQuery();

            return result;
        }

        public void CloseConnection()
        {
            this.connector.Connection.Clone();
        }
    }
}
