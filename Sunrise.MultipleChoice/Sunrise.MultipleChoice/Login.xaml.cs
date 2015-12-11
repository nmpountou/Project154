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

//Thread for Language Culture
using System.Globalization;
using System.Threading;
using MySql.Data.MySqlClient;

namespace Sunrise.MultipleChoice
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private MysqlC mysqlc;
        public Login()
        {
            //Language default start
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            Sunrise.MultipleChoice.Data.Logger.Setup();
            InitializeComponent();
        }

        private void Login_exit_button_Click(object sender, RoutedEventArgs e)
        {
                      
        }

        private void Login_button_Click(object sender, RoutedEventArgs e)
        {
            if(Login_username_textBox.Text.Equals(""))
            {
                Login_image_error_user_name.Visibility = Visibility.Visible;
            }
            else if (Login_passwordBox.Password.Equals(""))
            {
                Login_image_error_password.Visibility = Visibility.Visible;
            }
            else
            {
                //Graph of processing.
                Login_Process.Visibility = Visibility.Visible;
                //Graph of errors
                Login_image_error_user_name.Visibility = Visibility.Hidden;
                Login_image_error_password.Visibility = Visibility.Hidden;

                //BY DEFALYT CONNECTION!!!
                mysqlc = new MysqlC( Login_username_textBox.Text, Login_passwordBox.Password);

                //Connection as root from local machine
                //mysqlc = new MysqlC("root", "123456", "localhost", "questionnairex");
                mysqlc.initializeConnection();

                if (mysqlc.Return_Connection() != null)
                {
                    //Stop the graphics.               
                    Login_Process.Visibility = Visibility.Hidden;

                    //Retrive the connection string
                    //WORKS
                    //String connection = (new Model.RetriveStringConnection()).get_sc();
                    //MessageBox.Show(connection);
                    //Close the form.               
                    this.Login_exit_button_Click(sender, e);
                }
            }
        }

        private void Login_button_new_Account_Click(object sender, RoutedEventArgs e)
        {
            //Forgot_Password frp = new Forgot_Password();
            //frp.Show();
            Register reg = new Register();
            reg.Show();

            //System.Uri resourceLocater = new System.Uri("Register.xaml",System.UriKind.Relative);
            //System.Windows.Application.LoadComponent(this, resourceLocater);
        }
    }
}
