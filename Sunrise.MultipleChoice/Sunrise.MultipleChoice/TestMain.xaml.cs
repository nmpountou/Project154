using Quastionnaire.Model;
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
    /// Interaction logic for TestMain.xaml
    /// </summary>
    public partial class TestMain : Page
    {
        public TestMain()
        {
            InitializeComponent();

            Account account = new Account();
            account.Username = "Omega";

            List<Question> items = new List<Question>();
            items.Add(new Question() { Id = 1, Account = account, Correct_Answer_ID = 3 , Correct_Department_ID = 1 , Date = new DateTime() , Question_des = "Erwthsh1", Level = 2 } );
            items.Add(new Question() { Id = 1, Account = account, Correct_Answer_ID = 3, Correct_Department_ID = 1, Date = new DateTime(), Question_des = "Erwthsh1", Level = 2 });
            items.Add(new Question() { Id = 1, Account = account, Correct_Answer_ID = 3, Correct_Department_ID = 1, Date = new DateTime(), Question_des = "Erwthsh1", Level = 2 });
            items.Add(new Question() { Id = 1, Account = account, Correct_Answer_ID = 3, Correct_Department_ID = 1, Date = new DateTime(), Question_des = "Erwthsh1", Level = 2 });
            listView1.ItemsSource = items;


        }
    }
}
