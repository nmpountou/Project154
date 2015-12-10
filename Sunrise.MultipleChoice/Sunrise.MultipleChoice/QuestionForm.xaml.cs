using Quastionnaire.Model;
using Quastionnaire.Model.Dao;
using Quastionnaire.Model.Dao.Impl;
using Quastionnaire.Model.Dao.Interface;
using Sunrise.MultipleChoice.Data;
using Sunrise.MultipleChoice.Model;
using Sunrise.MultipleChoice.Model.Dao;
using Sunrise.MultipleChoice.Model.Dao.Impl;
using Sunrise.MultipleChoice.Model.Dao.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
        private List<Subject> subjectList;

        public QuestionForm()
        {
            InitializeComponent();

            Logger.Setup();

            setMsgLabelToHidden();
            initializeComboBoxInfo();

            //Subject subject = new Subject();
            //subject.Id = 1;
            //subject.Subject_descr = "Mathimatika";


            //Department dep = new Department();
            //dep.Id = 1;
            //dep.Level = 1;
            //dep.Department_descr = "Prwth";

            //Account account = new Account();
            //account.Username = "Omega";

            //List<Answer> answer = new List<Answer>();
            //answer.Add(new Answer() { Id = 1, Date = new DateTime(), Answer_descr = "Answer 1", Account = account });
            //answer.Add(new Answer() { Id = 1, Date = new DateTime(), Answer_descr = "Answer 2", Account = account });
            //answer.Add(new Answer() { Id = 1, Date = new DateTime(), Answer_descr = "Answer 3", Account = account });
            //answer.Add(new Answer() { Id = 1, Date = new DateTime(), Answer_descr = "Answer 4", Account = account });

            //List<Question> question = new List<Question>();
            //question.Add(new Question() { Id = 1, Level = 2, Account = account, Date = new DateTime(), Question_descr = "Erwrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrthsh1", Subject = subject, Department = dep , AnswerList = answer });
            //question.Add(new Question() { Id = 1, Level = 2, Account = account, Date = new DateTime(), Question_descr = "Erwthsh2", Subject = subject, Department = dep , AnswerList = answer });
            //question.Add(new Question() { Id = 1, Level = 2, Account = account, Date = new DateTime(), Question_descr = "Erwthsh3", Subject = subject, Department = dep });
            //question.Add(new Question() { Id = 1, Level = 2, Account = account, Date = new DateTime(), Question_descr = "Erwthsh4", Subject = subject, Department = dep });

            //lvQuestion.ItemsSource = question;

        }

        //Initialize
        private void initializeComboBoxInfo()
        {
            ISubjectDao subjectDao = new SubjectDaoImpl();
            subjectList = subjectDao.findSubject();

            foreach (Subject subject in subjectList)
            {
                cbSubject_search.Items.Add(subject.Subject_descr);
                cbSubject_Question.Items.Add(subject.Subject_descr);
            }


        }
       
        //Table OnClick
        private void lvQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Question question = (Question)lvQuestion.SelectedItem;

            if (question == null)
                return;

            // tbLevel_Question.Text;
            // tbOwner_Question.Text;
            // tbDate_Question.Text;
            // tbQuestion_Description.Text;
            // cbSubject_Question.Text;
            //cbDepartment_Question.Text;

            tbLevel_Question.Text = question.Level.ToString();
            tbOwner_Question.Text = question.Account.Username.ToString();
            tbDate_Question.Text = question.Date.ToString();
            tbQuestion_Description.Text = question.Question_descr.ToString();
            cbSubject_Question.Text = question.Subject.Subject_descr.ToString();
            cbDepartment_Question.Text = question.Department.Department_descr.ToString();

            //Load Answer Table
            lvAnswer.ItemsSource = question.AnswerList;
            lvQuestion.SelectedItem = null;

        }
        private void lvAnswer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Answer answer = (Answer)lvAnswer.SelectedItem;

            if (answer == null)
                return;

            tbOwner_Answer.Text = answer.Account.Username.ToString();
            tbDate_Answer.Text = answer.Date.ToString();
            tbAnswer_Description.Text = answer.Answer_descr.ToString();

            lvAnswer.SelectedItem = null;

        }

        //Question ToolBAr
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            lvQuestion.ItemsSource = null;
            lvAnswer.ItemsSource = null;

            string subject = cbSubject_search.Text;
            string department = cbDepartment_search.Text;

            int subject_index = cbSubject_search.SelectedIndex;
            int department_index = cbDepartment_search.SelectedIndex;

            if (checkBeforeSearchForNullInput(subject, department))
                return;

            List<Question> questionData = loadQuestionData(CurrentUserInfo.CURENT_ACCOUNT,subjectList[subject_index],subjectList[subject_index].DepList[department_index]);
            lvQuestion.ItemsSource = questionData;

        }
        private void btSave_Question_Click(object sender, RoutedEventArgs e)
        {

            string level = tbLevel_Question.Text;
            string owner = tbOwner_Question.Text;
            string date = tbDate_Question.Text;
            string question_descr = tbQuestion_Description.Text;
            string subject = cbSubject_Question.Text;
            string department = cbDepartment_Question.Text;

            if (checkQuestionForNullInput(level, owner, date, question_descr, subject, department))
                return;

            if (checkQuestionForNullInput(level, owner, date, question_descr, subject, department))
                return;



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
        private void cbSubject_Question_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbDepartment_Question.Items.Clear();

            int index = cbSubject_Question.SelectedIndex;
            List<Department> listDep = subjectList[index].DepList;
            foreach (Department dep in listDep)
                cbDepartment_Question.Items.Add(dep.Department_descr);

        }

        //Answer ToolBAr
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

        //Search

        private void cbSubject_search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            cbDepartment_search.Items.Clear();

            int index = cbSubject_search.SelectedIndex;
            List<Department> listDep = subjectList[index].DepList;
            foreach (Department dep in listDep)
                cbDepartment_search.Items.Add(dep.Department_descr);

        }

        //Common
        private List<Question> loadQuestionData(Account account, Subject subject, Department department)
        {
            List<Question> questionList;
            IQuestionDao questionDao = new QuestionDaoImpl();

            questionList = questionDao.findQuestion(subject,department);

            return questionList;
        }


        private void setMsgLabelToHidden()
        {
            lblSubject_search_msg.Visibility = Visibility.Hidden;
            lblDepartment_search_msg.Visibility = Visibility.Hidden;

            lblLevel_Question_msg.Visibility = Visibility.Hidden;
            lblOwner_Question_msg.Visibility = Visibility.Hidden;
            lblDate_Question_msg.Visibility = Visibility.Hidden;
            lblSubject_Question_msg.Visibility = Visibility.Hidden;
            lblDepartment_Question_msg.Visibility = Visibility.Hidden;
            lblQuestion_Description_msg.Visibility = Visibility.Hidden;

            lblOwner_Answer_msg.Visibility = Visibility.Hidden;
            lblDate_Answer_msg.Visibility = Visibility.Hidden;
            lblAnswer_Description_msg.Visibility = Visibility.Hidden;

        }
        private bool checkBeforeSearchForNullInput(string subject, string department)
        {
            bool emptyField = false;

            if (String.IsNullOrEmpty(subject))
            {
                lblSubject_search_msg.Visibility = Visibility.Visible;
                lblSubject_search_msg.Foreground = Brushes.Red;
                lblSubject_search_msg.Content = "Empty";
                emptyField = true;
            }
            if (String.IsNullOrEmpty(department))
            {
                lblDepartment_search_msg.Visibility = Visibility.Visible;
                lblDepartment_search_msg.Foreground = Brushes.Red;
                lblDepartment_search_msg.Content = "Empty";
                emptyField = true;
            }

            return emptyField;

        }
        private bool checkQuestionForNullInput(string level, string owner, string date, string question_descr, string subject, string department)
        {

            bool emptyField = false;

            if (String.IsNullOrEmpty(level))
            {
                lblLevel_Question_msg.Visibility = Visibility.Visible;
                lblLevel_Question_msg.Foreground = Brushes.Red;
                lblLevel_Question_msg.Content = "Empty";
                emptyField = true;
            }
            if (String.IsNullOrEmpty(owner))
            {
                lblOwner_Question_msg.Visibility = Visibility.Visible;
                lblOwner_Question_msg.Foreground = Brushes.Red;
                lblOwner_Question_msg.Content = "Empty";
                emptyField = true;
            }
            if (String.IsNullOrEmpty(date))
            {
                lblDate_Question_msg.Visibility = Visibility.Visible;
                lblDate_Question_msg.Foreground = Brushes.Red;
                lblDate_Question_msg.Content = "Empty";
                emptyField = true;
            }
            if (String.IsNullOrEmpty(question_descr))
            {
                lblQuestion_Description_msg.Visibility = Visibility.Visible;
                lblQuestion_Description_msg.Foreground = Brushes.Red;
                lblQuestion_Description_msg.Content = "Empty";
                emptyField = true;
            }
            if (String.IsNullOrEmpty(subject))
            {
                lblSubject_Question_msg.Visibility = Visibility.Visible;
                lblSubject_Question_msg.Foreground = Brushes.Red;
                lblSubject_Question_msg.Content = "Empty";
                emptyField = true;
            }
            if (String.IsNullOrEmpty(department))
            {
                lblDepartment_Question_msg.Visibility = Visibility.Visible;
                lblDepartment_Question_msg.Foreground = Brushes.Red;
                lblDepartment_Question_msg.Content = "Empty";
                emptyField = true;
            }



            return emptyField;
        }
        private bool checkQuestionForValidInput(string level, string owner, string date, string question_descr, string subject, string department)
        {

            bool emptyField = false;

            if (String.IsNullOrEmpty(level))
            {
                lblLevel_Question_msg.Visibility = Visibility.Visible;
                lblLevel_Question_msg.Foreground = Brushes.Red;
                lblLevel_Question_msg.Content = "Empty";
                emptyField = true;
            }
            if (String.IsNullOrEmpty(owner))
            {
                lblOwner_Question_msg.Visibility = Visibility.Visible;
                lblOwner_Question_msg.Foreground = Brushes.Red;
                lblOwner_Question_msg.Content = "Empty";
                emptyField = true;
            }
            if (String.IsNullOrEmpty(date))
            {
                lblDate_Question_msg.Visibility = Visibility.Visible;
                lblDate_Question_msg.Foreground = Brushes.Red;
                lblDate_Question_msg.Content = "Empty";
                emptyField = true;
            }
            if (String.IsNullOrEmpty(question_descr))
            {
                lblQuestion_Description_msg.Visibility = Visibility.Visible;
                lblQuestion_Description_msg.Foreground = Brushes.Red;
                lblQuestion_Description_msg.Content = "Empty";
                emptyField = true;
            }

            return emptyField;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }

       
    }
}
