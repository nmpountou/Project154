using Quastionnaire.Model;
using Sunrise.MultipleChoice.Model;
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
    public partial class QuestionForm : Page
    {
        public QuestionForm()
        {
            InitializeComponent();

            Subject subject = new Subject();
            subject.Id = 1;
            subject.Subject_descr = "Mathimatika";


            Department dep = new Department();
            dep.Id = 1;
            dep.Level = 1;
            dep.Department_descr = "Prwth";

            Account account = new Account();
            account.Username = "Omega";

            List<Question> items = new List<Question>();
            items.Add(new Question() { Id = 1, Level = 2 , Account = account, Date = new DateTime(), Question_descr = "Erwthsh1",Subject= subject, Department = dep  });
            items.Add(new Question() { Id = 1, Level = 2, Account = account, Date = new DateTime(), Question_descr = "Erwthsh2", Subject = subject, Department = dep });
            items.Add(new Question() { Id = 1, Level = 2, Account = account, Date = new DateTime(), Question_descr = "Erwthsh3", Subject = subject, Department = dep });
            items.Add(new Question() { Id = 1, Level = 2, Account = account, Date = new DateTime(), Question_descr = "Erwthsh4", Subject = subject, Department = dep });
            lvQuestion.ItemsSource = items;


            List<Answer> answers = new List<Answer>();
            answers.Add(new Answer() { Id = 1,Date = new DateTime(),Answer_descr ="Answer 1"  });
            answers.Add(new Answer() { Id = 1, Date = new DateTime(), Answer_descr = "Answer 2" });
            answers.Add(new Answer() { Id = 1, Date = new DateTime(), Answer_descr = "Answer 3" });
            answers.Add(new Answer() { Id = 1, Date = new DateTime(), Answer_descr = "Answer 4" });
            lvAnswer.ItemsSource = items;



        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }


        private void btSave_Question_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btEdit_Question_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btDelete_Question_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btClear_Question_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btSave_Answer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btEdit_Answer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btDelete_Answer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btClear_Answer_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }

      
    }
}
