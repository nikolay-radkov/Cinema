namespace Cinema.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using MySql.Data.MySqlClient;

    public class QueryExecutor
    {
        private MySqlConnector connector;
        private MySqlCommand command;

        public QueryExecutor()
        {
            this.connector = new MySqlConnector();
            this.command = new MySqlCommand();
            this.command.Connection = this.connector.Connection;

            try
            {
                this.connector.Connection.Open();
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
            }
        }

        public MySqlDataReader ExecuteQuery(string commandText)
        {
            this.command.CommandText = commandText;

            MySqlDataReader reader = null;

            try
            {
                reader = this.command.ExecuteReader();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return reader;
        }

        public int ExecuteNonQuery(string commandText)
        {
            this.command.CommandText = commandText;

            int result = 0;

            try
            {
                result = this.command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return result;
        }

        public void CloseConnection()
        {
            this.connector.Connection.Clone();
        }
    }
}
