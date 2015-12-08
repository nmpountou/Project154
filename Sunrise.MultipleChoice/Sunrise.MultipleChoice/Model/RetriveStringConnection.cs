using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Namespace for reading the Sunrise.vhost.exe config
using System.Configuration;
using log4net;

namespace Sunrise.MultipleChoice.Model
{
    class RetriveStringConnection
    {
        private ILog logger = LogManager.GetLogger(nameof(RetriveStringConnection));

        public String get_sc()
        {
            ConnectionStringsSection csSection = null;
            ConnectionStringSettingsCollection csCollection = null;
            try
            {
                //Refresh the file.
                ConfigurationManager.RefreshSection("ConnectionStrings");
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
                logger.Debug("REFRESH THE STRING_CONNECTION");
                
                // Get the connection string collection.
                csSection = config.ConnectionStrings;
                csCollection = csSection.ConnectionStrings;
                logger.Debug("GET THE STRING_CONNECTION");

            }
            catch (ConfigurationErrorsException ex)
            {
                logger.Error("RETRIVE_STRING_CONNECTION_FAILED: ", ex);
                return null;
            } 
            if (csSection.ConnectionStrings["mysql"].ToString() != null)
            {
                logger.Debug("RETURN THE STRING_CONNECTION");
                return csSection.ConnectionStrings["mysql"].ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
