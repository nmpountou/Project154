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
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.FormClosed += new FormClosedEventHandler(form1_FormClosed);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //Draw a line between new questionnaire and recent.
            System.Drawing.Graphics graphics = this.CreateGraphics();
            //Color.FromArgb(128, 128, 128, 0)
            Pen pen = new Pen(Color.SlateBlue, 8.0F);
            e.Graphics.DrawLine(pen, 470, 10, 470, 200);
            pen.Dispose();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.form1_FormClosed(sender, e);
        }
        void form1_FormClosed(object sender, EventArgs e)
        {
            // here you can do anything
            this.Dispose();
            // we will close the application
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
//label1.Text = RetriveStringConnection.get_sc();
//Get the info from current thread for the selection of the language. 
//By itself it works by taking the current language form.
//MessageBox.Show(String.Format("Current Thread Culture {0}  / UI Culture: {1}",
//            Thread.CurrentThread.CurrentCulture.Name,
//            Thread.CurrentThread.CurrentUICulture.Name));
