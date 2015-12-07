using log4net;
using Sunrise.MultipleChoice.Data;
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

namespace Sunrise.MultipleChoice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Page
    {
        private ILog logger = LogManager.GetLogger(nameof(MainWindow));


        public MainWindow()
        {
            InitializeComponent();


        }


        private void login_Click(object sender, RoutedEventArgs e)
        {

            String name = nameTB.Text;


        }
    }
}
