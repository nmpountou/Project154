using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Create a class to manage the Mysql connection with the database.
/// </summary>

//Nikos dsfgdfgdfgds
using MySql.Data.MySqlClient;
using System.Configuration;


namespace Quastionnaire
{
    class MysqlC
    {
        public void costas1() {

            //sdfdsfdsf
            //sdfsdfds
            //sdfsdf
            //dsfdasfdasfas
            //xcvxzvxczvxzcz

         }
        public void costas2() { }
        public void costas3() { }
        public void costas4() { }
        public void costas5() {
            //private static readonly log4net.ILog log = LogHelper.Getlogger();
        }

        public void costas6()
        {

        }
        private string username;
        private string password;
        private string hostname;
        private string port;
        private string database;

        private MySqlConnection connection;

        //Log4bet Debugger
        //private static readonly log4net.ILog log = LogHelper.Getlogger();

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
            //log.Debug("initializeConnection()");
            //String give root access locally.
            //string cs = @"server=localhost;userid=root;password=123456;database=questionnairex";

            string cs = @"server=" + hostname.Trim() + ";userid="+username.Trim()+";password="+password+";database="+database.Trim();
            //log.Debug(cs);
            connection = null;
            try
            {
                
                connection = new MySqlConnection(cs);
                connection.Open();
                //log.Debug("MySQL version : {0}" + connection.ServerVersion.ToString());
                change_app_config_file(cs);
            }
            catch (MySqlException ex)
            {
                //log.Debug("Error: {0}" + ex.ToString());
                System.Windows.Forms.MessageBox.Show(ex.ToString());
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
            //log.Debug("closeDatabase()");

            try
            {
                connection.Close();
            }
            catch (MySqlException e)
            {
                //log.Debug("CLOSE_CONNECTION_FAILED: "+e.ToString());

                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
        }
        public void change_app_config_file(String cs)
        {

            ConfigurationManager.RefreshSection("ConnectionStrings");
            
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
        public string Password
        {
            set { password = value; }
            get { return password; }
        }
        public void help()
        {

        }
        public void help2()
        {

        }
        public void help3()
        {

        }
        public void help4()
        {

        }

    }
}
