using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Log4net
//using log4net;
//using log4net.Config;

using MySql.Data.MySqlClient;
/// <summary>
/// Set the language pack
/// </summary>
using System.Globalization;
using System.Threading;

namespace Quastionnaire
{
    public partial class Form1 : Form
    {
        //log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //In case of use Netframwork above 4.6 you can write the file name. 
        //private static readonly log4net.ILog log = LogHelper.Getlogger();
        private MysqlC mysqlc;

        public Form1()
        {
            //Language default start
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mysqlc = null;
            comboBox1.Items.Add("English");
            comboBox1.Items.Add("Ελληνικά");
            comboBox1.SelectedIndex = 0;
            this.FormClosed += new FormClosedEventHandler(form1_FormClosed);
        }

        private void button1_Click(object sender, EventArgs e)
        {
         //Forget password form.

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            //Rectangle for textbox1 & textbox2 (customizing).
            System.Drawing.Graphics graphics = this.CreateGraphics();
            System.Drawing.Rectangle rectangle1 = new System.Drawing.Rectangle(282, 245, 280, 30);
            graphics.DrawRectangle(System.Drawing.Pens.DeepSkyBlue, rectangle1);
            System.Drawing.Rectangle rectangle2 = new System.Drawing.Rectangle(282, 284, 280, 30);
            graphics.DrawRectangle(System.Drawing.Pens.DeepSkyBlue, rectangle2);
            base.OnPaint(e);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox4.Visible = true;
            //Thread th = new Thread(new ThreadStart(WorkThread));
            //th.Start();

            // DELETE THIS CHANGES
            textBox1.Text = " ";
            textBox2.Text = " ";

            //log.Debug("Click to do connection");
            if (textBox1.Text.Equals(""))
            {
                pictureBox2.Visible = true;

            }
            else if (textBox2.Text.Equals(""))
            {
                pictureBox3.Visible = true;
            }
            else
            {
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                //mysqlc = new MysqlC(textBox1.Text, textBox2.Text, "localhost", "questionnairex");

                //BY DEFALYT CONNECTION!!!
                mysqlc = new MysqlC("root", "123456", "localhost", "questionnairex");
                mysqlc.initializeConnection();
                if (mysqlc.Return_Connection()!=null)
                {
                    //Stop the graphics.               
                    pictureBox4.Visible = false;

                    System.Windows.Forms.MessageBox.Show("Connection Succeeded!");
                    //Add a new event to close the window form
                    this.form1_FormClosed(sender,e);
                }
            }
        }

        //Start the thread for the graphics.
        //private void WorkThread()
        //{

        //    // we have no params, so just call the method
        //    UpdateTextBox();
        //}
        //private void UpdateTextBox()
        //{
        //    // do we need to switch threads?
        //    try {
        //        if (InvokeRequired)
        //        {
        //            // slightly different now, as we dont need params
        //            // we can just use MethodInvoker
        //            MethodInvoker method = new MethodInvoker(UpdateTextBox);
        //            Invoke(method);
        //            return;
        //        }

        //        // as we didn't take a param, we just update
        //        //textBox1.Text += "Hello world!\r\n";
        //        pictureBox4.Visible = true;
        //    }
        //    catch (ThreadAbortException e)
        //    {
        //        pictureBox4.Visible = false;
        //        Thread.ResetAbort();
        //    }
        //}



    void form1_FormClosed(object sender, EventArgs e)
        {
            // here you can do anything
            pictureBox4.Visible = false;
            this.Dispose();
            if (mysqlc != null)
            {
                mysqlc.closeMysqlConnection();
            }
            // we will close the application
            //Application.Exit();
        }

        //EXIT BUTTON
        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Questionnaire",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            == DialogResult.Yes)
            {
                if (mysqlc != null)
                {
                    mysqlc.closeMysqlConnection();
                }
                this.Dispose();
                Application.Exit();
            }
        }


        public MySqlConnection return_connection()
        {
            if (mysqlc!=null)
            {
                return mysqlc.Return_Connection();
            }
            else
            {
                return null;
            }
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "English")
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                ChangeLanguage("en");
            }
            else
            {
                //Change the language
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("el");
                ChangeLanguage("el");
            }
        }
        private void ChangeLanguage(string lang)
        {
            foreach (Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
                resources.ApplyResources(c, c.Name, new CultureInfo(lang));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Hide the main form.
            //When login is done we re-open the form with this.Visible.True
            // the user can now login to the program. 
            this.Hide();
            Form4 fr4 = new Form4();
            fr4.ShowDialog();
        }
    }
}
