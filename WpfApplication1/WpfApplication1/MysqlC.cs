﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;

namespace WpfApplication1
{
    class MysqlC
    {
        //private ILog logger = LogManager.GetLogger(nameof(MysqlConnector));

        //KOSTA KANO MIA ALLAGI STON KODIKA
        //LOGIKA THA PREPEI NA TIN DEIS
        //

            /// kai ego to idio

        private string username;
        private string password;
        private string hostname;
        private string port = "3306";
        private string database;

        private MySqlConnection connection;


        public void CostasFunction()
        {
            //Costas edw egrapse
            //dsgadsgsg/gasag

            //sadgasgasdgasgasgasg

            //σςδφ
            //ασφασφ/ασδφ
            //σδφασδφασφασδφ
        }




        public  MysqlC(string username, string password, string hostname, string port, string database)
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
        }

        public void initializeConnection()
        {
            //Logger.Setup();
           // logger.Debug("initializeConnection()");

            connection = new MySqlConnection("Server = " + username + "; Port = " + port + "; Database = " + database + "; Uid = " + username + "; Pwd = " + password + ";");
        }
        public void openMysqlConnection()
        {
            //logger.Debug("openDatabase()");

            try
            {
                connection.Open();
            }
            catch (MySqlException e)
            {
                //logger.Error("OPEN_CONNECTION_FAILED: ", e);
                throw e;
            }


        }
        public void closeMysqlConnection()
        {
            //logger.Debug("closeDatabase()");

            try
            {
                connection.Close();
            }
            catch (MySqlException e)
            {
                //logger.Error("CLOSE_CONNECTION_FAILED: ", e);
                throw e;

            }

        }

        public void getRow(String username)
        {
            try
            {
                //Debug.WriteLine("Start");

                //MySqlCommand command = new MySqlCommand("select username from account where username=@username;", connection);
                MySqlCommand command = new MySqlCommand("select * from user", connection);
                command.Parameters.AddWithValue("username", username);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    String user = reader.GetString(0);
                    MessageBox.Show("The calculations are complete "+user, "My Application");
                    //Debug.WriteLine(user);
                }

            }
            catch (Exception e)
            {
                //Debug.WriteLine(e);

            }



        }

        //Ayti i allagi einai gia to merge
        public string Messagge
        {
       
            get { return hostname; }

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
    }
}
