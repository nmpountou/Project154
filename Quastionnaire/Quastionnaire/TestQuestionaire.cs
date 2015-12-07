using log4net;
using MySql.Data.MySqlClient;
using Sunrise.MultipleChoice.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quastionnaire
{


    public partial class TestQuestionaire : Form
    {
    private ILog logger = LogManager.GetLogger(nameof(MysqlConnector));

        public TestQuestionaire()
        {
            InitializeComponent();
        }

        private void TestQuestionaire_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("Working");
            logger.Warn("LoooL");

            Logger.Setup();




          
        }
    }
}
