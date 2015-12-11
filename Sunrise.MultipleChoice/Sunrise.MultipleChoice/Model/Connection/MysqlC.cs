using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Create a class to manage the Mysql connection with the database.
/// </summary>
using log4net;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Threading;

namespace Sunrise.MultipleChoice
{
    class MysqlC
    {
        //Logger
        private ILog logger = LogManager.GetLogger(nameof(MysqlC));
        //Create connectionString
        private string username;
        private string password;
        private string hostname = Quastionnaire.Model.CurrentUserInfo.HOSTNAME;
        private string port = Quastionnaire.Model.CurrentUserInfo.PORT;
        private string database = Quastionnaire.Model.CurrentUserInfo.DATABASE;

        private MySqlConnection connection;
        public MysqlC()
        {
            this.username = Quastionnaire.Model.CurrentUserInfo.USERNAME;
            this.password = Quastionnaire.Model.CurrentUserInfo.PASSWORD;
            this.hostname = Quastionnaire.Model.CurrentUserInfo.HOSTNAME;
            this.port = Quastionnaire.Model.CurrentUserInfo.PORT;
            this.database = Quastionnaire.Model.CurrentUserInfo.DATABASE;
        }
        public MysqlC(string username,string password)
        {
            this.username = username;
            this.password = password;
        }
        public MysqlC(string username, string password, string hostname, string port, string database)
        {
            this.username = username;
            this.password = password;
            this.hostname = hostname;
            this.port = port;
            this.database = database;
        }
        public MysqlC(string username, string password, string hostname, string database)
        {
            this.username = username;
            this.password = password;
            this.hostname = hostname;
            this.database = database;
            this.port = "3306";
        }

        public void initializeConnection()
        {
            //CHECK WHAT NEED FOR INITIALIZATION
            //Sunrise.MultipleChoice.Data.Logger.Setup();
            
            //logger.Setup();

            logger.Debug("initializeConnection()");

            //String give root access locally.
            //string cs = @"server=localhost;userid=root;password=123456;database=questionnairex";

            //string for connection with the external database. 
            string cs = @"server=" + hostname.Trim() + ";userid=" + username.Trim() + ";password=" + password + ";database=" + database.Trim();
            //log.Debug(cs);

            connection = null;
            try
            {

                connection = new MySqlConnection(cs);
                connection.Open();
                logger.Debug("OpenDatabase Connection()");
                logger.Debug("MySQL version : {0}" + connection.ServerVersion.ToString());
                
                //MUST DONE
                change_app_config_file(cs);
                
            }
            catch (MySqlException e)
            {
                logger.Error("OPEN_CONNECTION_FAILED: ", e);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
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
            }
        }
        public void change_app_config_file(String cs)
        {
            try
            {
                //ConfigurationUserLevel.PerUserRoaming
                System.Configuration.Configuration config =
                        ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None);

                // Clear the connection strings collection.
                ConnectionStringsSection csSection =
                    config.ConnectionStrings;
                ConnectionStringSettingsCollection csCollection =
                 csSection.ConnectionStrings;

                ConfigurationManager.RefreshSection("ConnectionStrings");
                //csSection.ConnectionStrings.
                csSection.ConnectionStrings.Add(
                    new ConnectionStringSettings("mysql",
                        cs, "MySql.Data.MySqlClient"));

                config.Save(ConfigurationSaveMode.Full);
                // Save the configuration file.
            }
            catch (ConfigurationErrorsException e)
            {
                logger.Error("INITIALIZATION_CONFIGURETION_FILE_FAILED: ", e);
            }
        }
        public string Username
        {
            set { username = value; }
            get { return username; }
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
        public MySqlConnection Return_Connection()
        {
            return connection;
        }
        public String temp_connection_string()
        {
            string cs = @"server=" + hostname.Trim() + ";userid=" + username.Trim() + ";password=" + password + ";database=" + database.Trim();
            return cs;
        }
        //public string Password
        //{
        //    set { password = value; }
        //    get { return password; }
        //}


    }
}
