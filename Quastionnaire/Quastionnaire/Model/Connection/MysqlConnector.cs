using log4net;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise.MultipleChoice.Data
{
    public class MysqlConnector
    {
        private ILog logger = LogManager.GetLogger(nameof(MysqlConnector));

        private string username;
        private string password;
        private string hostname;
        private string port = "3306";
        private string database;

        private MySqlConnection connection;

        public MysqlConnector(string username, string password, string hostname, string port, string database)
        {
            this.username = username;
            this.password = password;
            this.hostname = hostname;
            this.port = port;
            this.database = database;
        }
        public MysqlConnector(string username, string password, string hostname, string database)
        {
            this.username = username;
            this.password = password;
            this.hostname = hostname;
            this.database = database;
        }

        public void initializeConnection()
        {
            Logger.Setup();
            logger.Debug("initializeConnection()");

            connection = new MySqlConnection("Server = " + hostname + "; Port = " + port + "; Database = " + database + "; Uid = " + username + "; Pwd = " + password + ";");
        }
        public void openMysqlConnection()
        {
            logger.Debug("openDatabase()");
            try
            {
                connection.Open();
            }
            catch (MySqlException e)
            {
                logger.Error("OPEN_CONNECTION_FAILED: ", e);
                throw e;
            }
        }
        public void closeMysqlConnection()
        {
            logger.Debug("closeDatabase()");

            try
            {
                connection.Close();
            }
            catch (MySqlException e)
            {
                logger.Error("CLOSE_CONNECTION_FAILED: ", e);
                throw e;

            }

        }

        public void getRow(String username)
        {
            try
            {
                Debug.WriteLine("Start");

                MySqlCommand command = new MySqlCommand("select username from account where username=@username;", connection);
                command.Parameters.AddWithValue("username", username);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    String user = reader.GetString(0);
                    Debug.WriteLine(user);
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);

            }

        }

        public string Username
        {
            set { username = value; }
            get { return username; }
        }
        public string Password
        {
            set { password = value; }
            get { return password; }
        }
        public string Hostname
        {
            set { hostname = value; }
            get { return hostname; }
        }
        public string Port
        {
            set { port = value; }
            get { return port; }
        }
        public string Database
        {
            set { database = value; }
            get { return database; }
        }

        public MySqlConnection MysqlConnection
        {
            set { connection = value; }
            get { return connection; }
        }
    }
}
