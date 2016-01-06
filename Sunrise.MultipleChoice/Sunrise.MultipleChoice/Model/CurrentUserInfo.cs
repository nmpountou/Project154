using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quastionnaire.Model
{
    class CurrentUserInfo
    {
        public static Account CURENT_ACCOUNT = new Account() {Id = 1, Username = "questionaire", Password = "6979480030" };

        public static string HOSTNAME = "127.0.0.1";
        public static string PORT = "3306";
        public static string DATABASE = "questionaire";
        public static string USERNAME = "questionaire";
        public static string PASSWORD = "6979480030";

        public static string TEMP_REGISTER_USERNAME = "root";
        public static string TEMP_REGISTER_PASSWORD = "123456";
       

    }
}
