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
        }
        private void Registration_submit_button_Click(object sender, RoutedEventArgs e)
        {
            int avaliable = Registration_get_first_avaliabe_id();
            logger.Debug("First id: "+avaliable);
            String msg = "";
            Boolean validate = false;
            if (check_empty_textboxes())
            {
                msg += "Empty fields in registration form.\n ";
                validate = true;
            }
            if (check_validation_email())
            {
                msg += "Non validate email.\n";
                validate = true;
            }
            if (check_unique_username())
            {
                msg += "Must select a unique username.\n";
                validate = true;
            }
            if (check_unique_email())
            {
                msg += "Must select a unique email.\n";
                validate = true;
            }
            if (check_password_similarity())
            {
                msg += "Passwords must be identical.\n";
                validate = true;
            }
            if (check_password_validation())
            {
                msg += "PASSWORD Must contain at least one:\n";
                msg += "lower case letter, one upper case letter\n";
                msg+= "one digit and one special character.\n";
                msg += "Valid special characters are –   @#$%^&+=";
                validate = true;
            }
            if (validate == true)
            {
                Registration_error_label.IsEnabled = true;
                Registration_error_label.Content = msg;
            }
            else
            {
                Registration_error_label.IsEnabled = false;
                int id = Registration_get_first_avaliabe_id();
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
            //Initialize the Connection, using default values of a user.
            //Create the temp Connection String
            MysqlC init_con = new MysqlC();
            //init_con.initializeConnection();
            MySqlConnection con = new MySqlConnection(init_con.temp_connection_string());
            String query = "SELECT email FROM user_information WHERE email = \"" + Registration_email_textBox.Text + "\";";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader;
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    validation = true;
                    logger.Debug("Found email!!!");
                }
            }
            catch (Exception ex)
            {
                logger.Error("RETRIVE_MAIL_FAILED: ", ex);
            }
            finally
            {
                con.Close();
            }

            return validation;
        }

        public Boolean check_unique_username()
        {
            Boolean validation = false;
            //Initialize the Connection, using default values of a user.
            //Create the temp Connection String
            MysqlC init_con = new MysqlC();
            //init_con.initializeConnection();
            MySqlConnection con = new MySqlConnection(init_con.temp_connection_string());
            String query = "SELECT username FROM account WHERE username = \"" + Registration_username_textBox.Text + "\";";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader;
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    validation = true;
                    logger.Debug("Found username!!!");
                }
            }
            catch (Exception ex)
            {
                logger.Error("RETRIVE_USERNAME_FAILED: ", ex);
            }
            finally
            {
                con.Close();
            }

            return validation;
        }

        private Boolean check_validation_email()
        {
            Boolean validation = true;
            //Regex for making sure Email is valid
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(Registration_email_textBox.Text);
            if (!match.Success)
            {
                Registration_image_error_email.Visibility = Visibility.Visible;
            }
            else
            {
                validation = false;
            }
            return validation;
        }
        private Boolean check_empty_textboxes()
        {
            Boolean validated = false;
            if (Registration_name_textBox.Text.Equals(""))
            {
                Registration_image_error_name.Visibility = Visibility.Visible;
                validated = true;
            }
            else
            {
                Registration_image_error_name.Visibility = Visibility.Hidden;
            }
            if (Registration_lastname_textBox.Text.Equals(""))
            {
                Registration_image_error_last_name.Visibility = Visibility.Visible;
                validated = true;
            }
            else
            {
                Registration_image_error_last_name.Visibility = Visibility.Hidden;
            }
            if (Registration_email_textBox.Text.Equals(""))
            {
                Registration_image_error_email.Visibility = Visibility.Visible;
                validated = true;
            }
            else
            {
                Registration_image_error_email.Visibility = Visibility.Hidden;
            }
            if (Registration_comboBox_country.SelectedIndex == -1)
            {
                Registration_image_error_country.Visibility = Visibility.Visible;
                validated = true;
            }
            else
            {
                Registration_image_error_country.Visibility = Visibility.Hidden;
            }
            if (Registration_comboBox_city.SelectedIndex == -1)
            {
                Registration_image_error_city.Visibility = Visibility.Visible;
                validated = true;
            }
            else
            {
                Registration_image_error_city.Visibility = Visibility.Hidden;
            }
            if (Registration_textBox_street.Text.Equals(""))
            {
                Registration_image_error_street.Visibility = Visibility.Visible;
                validated = true;
            }
            else
            {
                Registration_image_error_street.Visibility = Visibility.Hidden;
            }
            if (Registration_username_textBox.Text.Equals(""))
            {
                Registration_image_error_username.Visibility = Visibility.Visible;
                validated = true;
            }
            else
            {
                Registration_image_error_username.Visibility = Visibility.Hidden;
            }
            if (Registration_passwordBox.Password.Equals(""))
            {
                Registration_image_error_password.Visibility = Visibility.Visible;
                validated = true;
            }
            else
            {
                Registration_image_error_password.Visibility = Visibility.Hidden;
            }
            //Check the reverse.
            if (Registration_passwordBox_Copy.Password.Equals(""))
            {
                Registration_image_error_password.Visibility = Visibility.Visible;
            }
            else if (Registration_passwordBox_Copy.Password.Equals(Registration_passwordBox.Password))
            {
                Registration_image_error_password.Visibility = Visibility.Hidden;
            }
            else
            {
                Registration_image_error_password.Visibility = Visibility.Visible;
                validated = true;
            }
            return validated;
        }
        private Boolean check_password_similarity()
        {
            Boolean validate = true;
            if (Registration_passwordBox_Copy.Password.Equals(Registration_passwordBox.Password))
            {
                validate = false;
            }
            return validate;
        }
        /// <summary>
        /// Must be at least 6 characters
        // Must contain at least one one lower case letter, one upper case letter, one digit and one special character
        // Valid special characters are –   @#$%^&+=
        /// </summary>
        /// <returns></returns>
        private Boolean check_password_validation()
        {
            Boolean validation = true;
            Regex regex = new Regex(@"^.*(?=.{6,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$");
            Match match = regex.Match(Registration_passwordBox.Password);
            if (!match.Success)
            {
                Registration_image_error_email.Visibility = Visibility.Visible;
            }
            else
            {
                validation = false;
            }

            return validation;
        }
        private void Registration_comboBox_city_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Registration_comboBox_country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String cities;
            Registration_comboBox_city.Items.Clear();
            if (Registration_comboBox_country.SelectedIndex != -1)
            {
                Registration_comboBox_city.IsEnabled = true;
                int country_id = Registration_comboBox_country_getid(Registration_comboBox_country.SelectedItem.ToString());
                //Initialize the Connection, using default values of a user.
                //Create the temp Connection String
                MysqlC init_con = new MysqlC();
                //init_con.initializeConnection();
                MySqlConnection con = new MySqlConnection(init_con.temp_connection_string());
                String query = "SELECT city FROM city WHERE country_id = " + country_id + ";";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader;
                try
                {
                    con.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cities = reader.GetString("city");
                        //logger.Debug("Find cities for country id: " + country_id+" cities: "+cities);
                        Registration_comboBox_city.Items.Add(cities);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("RETRIVE_CITY_FAILED: ", ex);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        private int Registration_comboBox_country_getid(string country)
        {
            int country_id=0;
            country = Registration_comboBox_country.SelectedItem.ToString();
            logger.Debug("RETRIVE_COUNTRY: " + country);
            //Initialize the Connection, using default values of a user.
            //Create the temp Connection String
            MysqlC init_con = new MysqlC();
            //init_con.initializeConnection();
            MySqlConnection con = new MySqlConnection(init_con.temp_connection_string());
            String query = "SELECT id FROM country WHERE country = \"" + country + "\";";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader;
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                     country_id = reader.GetInt16("id");
                     //Registration_comboBox_country.Items.Add(country_id);
                     logger.Debug("Find cities for country id: " + country_id);
                     //Registration_comboBox_country.Items.Add(country_id);
                }
            }
            catch (Exception ex)
            {
               logger.Error("RETRIVE_CITY_FAILED: ", ex);
            }
            finally
            {
                con.Close();     
            }
            return country_id;
        }
        private int Registration_get_first_avaliabe_id()
        {
            int availiable=0;
            Boolean validation = false;
            //Initialize the Connection, using default values of a user.
            //Create the temp Connection String
            MysqlC init_con = new MysqlC();
            //init_con.initializeConnection();
            MySqlConnection con = new MySqlConnection(init_con.temp_connection_string());
            string query = "SELECT u.id + 1 AS FirstAvailableId " +
                "FROM account u LEFT JOIN account u1 ON u1.id = u.id + 1 " +
                "WHERE u1.id IS NULL ORDER BY u.id   LIMIT 0, 1";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader;
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    availiable = reader.GetInt16("FirstAvailableId");
                    logger.Debug("Found first avaliable id!!!"+availiable);
                }
            }
            catch (Exception ex)
            {
                logger.Error("RETRIVE_AVAILIABLE_ID_FAILED: ", ex);
            }
            finally
            {
                con.Close();
            }
            return availiable;
        }
    }
}