using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Namespace for reading the Questionnaire.vhost.exe config
using System.Configuration;
/// <summary>
/// At this class we take the connection string the user creates earlier during login.
/// We have done a static method. Don't need the creation of an instance of class.
/// Just getting the Connection String.
/// </summary>
namespace Quastionnaire
{
    class RetriveStringConnection
    {
        public static String get_sc()
        {
            ConnectionStringsSection csSection=null;
            ConnectionStringSettingsCollection csCollection=null;
            try {
                //Refresh the file.
                ConfigurationManager.RefreshSection("ConnectionStrings");
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
                // Get the connection strings collection.
                csSection = config.ConnectionStrings;
                csCollection = csSection.ConnectionStrings;
            }
            catch (ConfigurationErrorsException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return null;
            }
            if (csSection.ConnectionStrings["mysql"].ToString() != null)
            {
                return csSection.ConnectionStrings["mysql"].ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
