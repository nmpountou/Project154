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

// Using assemblies to retrive the object mysql connection.
using MySql.Data.MySqlClient;
using log4net;
//Regural Expression
using System.Text.RegularExpressions;

namespace Sunrise.MultipleChoice
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        //Logger
        private ILog logger = LogManager.GetLogger(nameof(Register));

        public Register()
        {
            InitializeComponent();
            fill_combobox_country();
            //Must sellect country first.
            Registration_comboBox_city.IsEnabled = false;
            //Sunrise.MultipleChoice.Data.Logger.Setup();
        }
        private void Registration_submit_button_Click(object sender, RoutedEventArgs e)
        {
            String msg = null;
            Boolean validate = false;
            //logger.Debug("asdad");
            Registration_comboBox_city.IsEnabled = true;
            if (check_empty_textboxes())
            {
                msg += "Empty fields in registration form. ";

                logger.Debug("FIND empty textboxes!");
            }
            if (check_validation_email())
            {
                msg += "Non validate email. ";

            }
            if (check_unique_username())
            {
                msg += "Must select a unique username. ";

            }
            if (check_unique_email())
            {
                msg += "Must select a unique email. ";
            }
            if(msg==null)
            {
                validate = true;
                //Registration_error_label.
            }
            else
            {
                Registration_error_label.IsEnabled = true;
                Registration_error_label.Content = msg;
            }
        }
        private void fill_combobox_country()
        {
            //Initialize the Connection, using default values of a user.
            //Create the temp Connection String
            MysqlC init_con = new MysqlC();
            //init_con.initializeConnection();

            MySqlConnection con = new MySqlConnection(init_con.temp_connection_string()); 
            String query = "SELECT * FROM country;";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader;
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string country_name = reader.GetString("country");
                    Registration_comboBox_country.Items.Add(country_name);
                }
            }
            catch (Exception e)
            {
                logger.Error("RETRIVE_COUNTRY_FAILED: ", e);
            }
            finally
            {
                con.Close();
            }
        }

        public Boolean check_unique_email()
        {
            Boolean validation = false;

            return true;
        }

        public Boolean check_unique_username()
        {
            Boolean validation = false;

            return true;
        }

        private Boolean check_validation_email()
        {
            Boolean validation = false;
            //Regex for making sure Email is valid
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(Registration_email_textBox.Text);
            if (!match.Success)
            {
                Registration_image_error_email.Visibility = Visibility.Visible;
            }
            else
            {
                validation = true;
            }
            return validation;
        }
        private Boolean check_empty_textboxes()
        {
            Boolean validated = true;
            if (Registration_name_textBox.Text.Equals(""))
            {
                Registration_image_error_name.Visibility = Visibility.Visible;
            }
            else if (Registration_lastname_textBox.Text.Equals(""))
            {
                Registration_image_error_name.Visibility = Visibility.Hidden;
                Registration_image_error_last_name.Visibility = Visibility.Visible;
            }
            else if (Registration_email_textBox.Text.Equals(""))
            {
                Registration_image_error_last_name.Visibility = Visibility.Hidden;
                Registration_image_error_email.Visibility = Visibility.Visible;
            }
            else if (Registration_comboBox_country.SelectedIndex == -1)
            {
                Registration_image_error_email.Visibility = Visibility.Hidden;
                Registration_image_error_country.Visibility = Visibility.Visible;
            }
            else if (Registration_comboBox_city.SelectedIndex == -1)
            {
                Registration_image_error_country.Visibility = Visibility.Hidden;
                Registration_image_error_city.Visibility = Visibility.Visible;
            }
            else if (Registration_textBox_street.Text.Equals(""))
            {
                Registration_image_error_city.Visibility = Visibility.Hidden;
                Registration_image_error_street.Visibility = Visibility.Visible;
            }
            else if (Registration_username_textBox.Text.Equals(""))
            {
                Registration_image_error_street.Visibility = Visibility.Hidden;
                Registration_image_error_username.Visibility = Visibility.Visible;
            }
            else if (Registration_passwordBox.Password.Equals(""))
            {
                Registration_image_error_username.Visibility = Visibility.Hidden;
                Registration_image_error_password.Visibility = Visibility.Visible;
            }
            else
            {
                Registration_image_error_password.Visibility = Visibility.Hidden;
                validated = false;
                //Graph of processing.
                //Login_Process.Visibility = Visibility.Visible;
                //Graph of errors
                //Login_image_error_user_name.Visibility = Visibility.Hidden;
                //Login_image_error_password.Visibility = Visibility.Hidden;

            }
            return validated;

        }

        private void Registration_comboBox_city_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Registration_comboBox_country.SelectedIndex != -1)
            {

            }
        }

        private void Registration_comboBox_country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Registration_comboBox_country.SelectedIndex != -1)
            {
                Registration_comboBox_city.IsEnabled = true;
            }
        }
    }
}
