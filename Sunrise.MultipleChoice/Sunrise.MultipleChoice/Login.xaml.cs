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
using Sunrise.MultipleChoice.Localization;
using Quastionnaire.Model;
using Sunrise.MultipleChoice.Data;

namespace Sunrise.MultipleChoice
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        Register reg;
        public Login()
        {
            //Language default start
            //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("el");
            Sunrise.MultipleChoice.Data.Logger.Setup();
            InitializeComponent();
        }

        private void Login_exit_button_Click(object sender, RoutedEventArgs e)
        {
            var oldWindow = Application.Current.MainWindow;

            Export_to_Pdf wpdf = new Export_to_Pdf();
            wpdf.Visibility = Visibility.Visible;
            oldWindow.Close();
        }

        private void Login_button_Click(object sender, RoutedEventArgs e)
        {
            if (Login_username_textBox.Text.Equals(""))
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

                MysqlConnector connector = new MysqlConnector(Login_username_textBox.Text, Login_passwordBox.Password,CurrentUserInfo.HOSTNAME,CurrentUserInfo.DATABASE);
                
                connector.initializeConnection();
                try
                {
                    connector.openMysqlConnection();
                    //Stop the graphics.               
                    Login_Process.Visibility = Visibility.Hidden;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Authentication Failed");
                    return;
                }
                //Retrive the connection string
                //WORKS
                //String connection = (new Model.RetriveStringConnection()).get_sc();
                //Close the form.

                //take the username and the password from reg to make connection               
                //this.Login_username_textBox.Text = reg.Registration_username_textBox.Text;

               
                CurrentUserInfo.USERNAME = Login_username_textBox.Text;
                    CurrentUserInfo.PASSWORD = Login_passwordBox.Password.ToString();
                    CurrentUserInfo.ID = getUserID();
                    CurrentUserInfo.CURENT_ACCOUNT = new Account() { Id = CurrentUserInfo.ID, Username = CurrentUserInfo.USERNAME, Password = CurrentUserInfo.PASSWORD };

                NavigationService nav = NavigationService.GetNavigationService(this);
                nav.Navigate(new Uri("MainMenu.xaml", UriKind.RelativeOrAbsolute));

                //CALL the menu from Costas. pass the password and the username
                //this.Login_exit_button_Click(sender, e);

                MessageBox.Show("Authentication Succed");
              
               
            }
        }

        private int getUserID()
        {

            MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
                      CurrentUserInfo.PASSWORD,
                      CurrentUserInfo.HOSTNAME,
                      CurrentUserInfo.PORT,
                      CurrentUserInfo.DATABASE);

            mysql.initializeConnection();
            mysql.openMysqlConnection();

            string query = "select id from account where username = '" + CurrentUserInfo.USERNAME + "'";
            int userID = -1;

            using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            userID = reader.GetInt32(0);
                        }
                    }

                }
            }


            mysql.closeMysqlConnection();

            return userID;

        } 



        private void Login_button_new_Account_Click(object sender, RoutedEventArgs e)
        {


            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri("Register.xaml", UriKind.RelativeOrAbsolute));

            //System.Uri resourceLocater = new System.Uri("Register.xaml",System.UriKind.Relative);
            //System.Windows.Application.LoadComponent(this, resourceLocater);
        }

        private void Language_button_Click(object sender, RoutedEventArgs e)
        {
            CultureInfo ci;
            if (Login_Language_Button.Content.ToString() == "EN")
            {
                Login_Language_Button.Content = "GR";
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("el");
                ci = new CultureInfo("el");
            }
            else
            {
                Login_Language_Button.Content = "EN";
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                ci = new CultureInfo("en");
            }
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            Localization.LocalizationManager.UpdateResources();
            MessageBox.Show(Properties.Resources.Language_Selection);
        }
    }
}
