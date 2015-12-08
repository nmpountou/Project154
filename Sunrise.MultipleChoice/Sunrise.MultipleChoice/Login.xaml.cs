using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//Thread
using System.Globalization;
using System.Threading;

namespace Sunrise.MultipleChoice
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        //private MysqlC mysqlc;
        public Login()
        {
            //Language default start
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            InitializeComponent();
            //this.Ev += new FormClosedEventHandler(form1_FormClosed);
        }

        private void Login_exit_button_Click(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Login_Process.Visibility = Visibility.Visible;
            if(Login_username_textBox.Text.Equals(""))
            {
                Login_image_error_user_name.Visibility = Visibility.Visible;
                    // img.Visibility = Visibility.Visible;
            }
            else if (Login_passwordBox.Password.Equals(""))
            {
                Login_image_error_password.Visibility = Visibility.Visible;
            }
            else
            {
                Login_image_error_user_name.Visibility = Visibility.Hidden;
                Login_image_error_password.Visibility = Visibility.Hidden;

                //BY DEFALYT CONNECTION!!!

                //mysqlc = new MysqlC("root", "123456", "localhost", "questionnairex");
                // mysqlc.initializeConnection();
                //if (mysqlc.Return_Connection() != null)
                Boolean connection = true;
                if (connection)
                {
                    //Stop the graphics.               
                    Login_Process.Visibility = Visibility.Hidden;
                    //System.Windows.Forms.MessageBox.Show(Messages.Connection_str);

                    //Raise a new event to destroy the form.
                    
                }
            }

        }
    }
}
