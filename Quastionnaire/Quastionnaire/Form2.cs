using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Using Mysql Connection
using MySql.Data.MySqlClient;
using System.Configuration;
//Language Pack
using System.Globalization;
using System.Threading;

namespace Quastionnaire
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            label1.Text = RetriveStringConnection.get_sc();
            //Get the info from current thread for the selection of the language. 
            //By itself it works by taking the current language form.
            //MessageBox.Show(String.Format("Current Thread Culture {0}  / UI Culture: {1}",
            //            Thread.CurrentThread.CurrentCulture.Name,
            //            Thread.CurrentThread.CurrentUICulture.Name));
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Visible = true;
        }
    }
}