using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quastionnaire.Model
{
    public class CurrentUserInfo
    {

        //Connection
        public static string HOSTNAME = "127.0.0.1";
        public static string PORT = "3306";
        public static string DATABASE = "questionaire";


        //When the user Logged in we use username-password for the connection
        public static int ID = 1;
        public static string USERNAME = "questionaire";
        public static string PASSWORD = "6979480030";
        public static Account CURENT_ACCOUNT = new Account() {Id = ID, Username = USERNAME, Password = PASSWORD };

        //Temp Username - Password only For Register
        public static string TEMP_REGISTER_USERNAME = "root";
        public static string TEMP_REGISTER_PASSWORD = "123456";

    }
}
