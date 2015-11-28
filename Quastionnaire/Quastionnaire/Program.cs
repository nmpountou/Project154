using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

//log4net
//using log4net.Config;

//MysqlConnection
using MySql.Data.MySqlClient;

namespace Quastionnaire
{
    static class Program
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(Program.cs);
        //static System.Globalization.CultureInfo GetCulture()
        //{
            /* Code to retrieve the culture*/
            
        //}

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 fr1 = new Form1();
            Application.Run(fr1);

            Form2 fr2 = new Form2();
            Application.Run(fr2);



        }
    }
}
