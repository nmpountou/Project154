using MySql.Data.MySqlClient;
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
        private Question selected_Question;
        private Answer selected_Answer;
        private Questionaire selected_Questionaire;
        private Question selected_Questionaire_Question;
        private List<Question> questionData;

        public enum UsernameType { Undefined,ALL, MINE,OTHER };
        private UsernameType usernameType = UsernameType.Undefined;

        public QuestionForm()
        {
            InitializeComponent();

            Logger.Setup();

            setMsgLabelToHidden();
            initializeComboBoxInfo();
            initializeQuestionaireData();

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

            cbCorrect_Answer.Items.Add("False");
            cbCorrect_Answer.Items.Add("True");


            cbUsernameType_search.Items.Add("ALL");
            cbUsernameType_search.Items.Add("MINE");
            cbUsernameType_search.Items.Add("OTHER");


        }
        private void initializeQuestionaireData()
        {
            IQuestionaireDao dao = new QuestionaireDaoImpl();
            List<Questionaire> listQuestionaire = dao.findQuestionare(CurrentUserInfo.CURENT_ACCOUNT);

            lvQuestionaire.ItemsSource = listQuestionaire;
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
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            lblSubject_search_msg.Visibility = Visibility.Hidden;
            lblDepartment_search_msg.Visibility = Visibility.Hidden;
            lblUsername_search_msg.Visibility = Visibility.Hidden;
            cbUsernameType_search_msg.Visibility = Visibility.Hidden;

            lvQuestion.ItemsSource = null;
            lvAnswer.ItemsSource = null;

            string subject = cbSubject_search.Text;
            string department = cbDepartment_search.Text;
            string username = "";

            int subject_index = cbSubject_search.SelectedIndex;
            int department_index = cbDepartment_search.SelectedIndex;

            //Check Username Type Search Engine
            switch (usernameType)
            {
                case UsernameType.MINE:
                    username = CurrentUserInfo.USERNAME;
                    break;
                case UsernameType.OTHER:

                    username = tbUsername_search.Text;
                  
                    break;
                case UsernameType.ALL:
                    username = "all";
                    break;
            }

            //Check Empty Fields
            if (checkSearchForNullInput(subject, department,username))
            {
                MessageBox.Show("Empty Fields", "Confirmation");
                return;
            }
            //Check if Searched Username Existt
            if(usernameType == UsernameType.OTHER)
                if (!usernameExist(username))
                {
                    MessageBox.Show("Username Dosent Exist");
                    return;
                }

            questionData = loadQuestionData(username, subjectList[subject_index], subjectList[subject_index].DepList[department_index]);
            lvQuestion.ItemsSource = questionData;


            MessageBox.Show("Questions Retrieved", "Confirmation");



        }

        private void cbUsernameType_search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int choice = cbUsernameType_search.SelectedIndex;

            switch (choice)
            {
                case 0:
                    usernameType = UsernameType.ALL;
                    tbUsername_search.IsEnabled = false;
                    tbUsername_search.Text = "";
                    break;
                case 1:
                    usernameType = UsernameType.MINE;
                    tbUsername_search.IsEnabled = false;
                    tbUsername_search.Text = "";
                    break;
                case 2:
                    usernameType = UsernameType.OTHER;
                    tbUsername_search.IsEnabled = true;
                    tbUsername_search.Text = "";
                    break;
            }

        }

        private bool usernameExist(string username)
        {
            bool usernameExist = false;

            MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
             CurrentUserInfo.PASSWORD,
             CurrentUserInfo.HOSTNAME,
             CurrentUserInfo.PORT,
             CurrentUserInfo.DATABASE);

            string query = "select username from account where username = '"+ username +"'";

            mysql.initializeConnection();
            mysql.openMysqlConnection();

            using (MySqlCommand cmd = new MySqlCommand(query,mysql.MysqlConnection))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                    if (reader.HasRows)
                        usernameExist = true;

             mysql.closeMysqlConnection();

            return usernameExist;
        }

        private bool checkSearchForNullInput(string subject, string department,string username)
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
            if (String.IsNullOrEmpty(username) && usernameType == UsernameType.OTHER)
            {
                lblUsername_search_msg.Visibility = Visibility.Visible;
                lblUsername_search_msg.Foreground = Brushes.Red;
                lblUsername_search_msg.Content = "Empty";
                emptyField = true;
            }
            if (usernameType == UsernameType.Undefined)
            {
                cbUsernameType_search_msg.Visibility = Visibility.Visible;
                cbUsernameType_search_msg.Foreground = Brushes.Red;
                cbUsernameType_search_msg.Content = "Empty";
                emptyField = true;
            }


            return emptyField;

        }

        //Table OnClick
        private void lvQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            lblLevel_Question_msg.Visibility = Visibility.Hidden;
            lblQuestion_Description_msg.Visibility = Visibility.Hidden;
            lblSubject_Question_msg.Visibility = Visibility.Hidden;
            lblDepartment_Question_msg.Visibility = Visibility.Hidden;

            Question question = (Question)lvQuestion.SelectedItem;

            if (question == null)
                return;

            selected_Question = question;

            Debug.WriteLine("Sos " + selected_Question.Question_descr);


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

        }
        private void lvAnswer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            lblAnswer_Description_msg.Visibility = Visibility.Hidden;
            lblCorrect_Answer_msg.Visibility = Visibility.Hidden;

            Answer answer = (Answer)lvAnswer.SelectedItem;

            if (answer == null)
                return;

            selected_Answer = answer;

            tbOwner_Answer.Text = answer.Account.Username.ToString();
            tbDate_Answer.Text = answer.Date.ToString();
            tbAnswer_Description.Text = answer.Answer_descr.ToString();
            cbCorrect_Answer.Text = answer.Correct.ToString();


        }
        private void lvQuestionaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Questionaire questionaire = (Questionaire)lvQuestionaire.SelectedItem;

            if (questionaire == null)
                return;

            selected_Questionaire = questionaire;

            lvQuestionaire_Question.ItemsSource = selected_Questionaire.QuestionList;

        }
        private void lvQuestionaire_Question_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Question question = (Question)lvQuestionaire_Question.SelectedItem;

            if (question == null)
                return;

            selected_Questionaire_Question = question;

        }

        //Question ToolBAr
        private void btSave_Question_Click(object sender, RoutedEventArgs e)
        {
            lblLevel_Question_msg.Visibility = Visibility.Hidden;
            lblQuestion_Description_msg.Visibility = Visibility.Hidden;
            lblSubject_Question_msg.Visibility = Visibility.Hidden;
            lblDepartment_Question_msg.Visibility = Visibility.Hidden;


            string level = tbLevel_Question.Text;
            DateTime date = DateTime.Now;
            string question_descr = tbQuestion_Description.Text;

            string subject = cbSubject_Question.Text;
            string department = cbDepartment_Question.Text;
            int subjectID = cbSubject_Question.SelectedIndex;
            int departmentID = cbDepartment_Question.SelectedIndex;

            if (checkQuestionForNullInput(level, question_descr, subject, department))
            {
                MessageBox.Show("Empty", "Confirmation");
                return;
            }
            if (checkQuestionForNullInput(level, question_descr, subject, department))
                return;


            Question question = new Question() { Subject = subjectList[subjectID], Department = subjectList[subjectID].DepList[departmentID], Question_descr = question_descr, Date = date, Level = Int32.Parse(level) };
            question.Account = CurrentUserInfo.CURENT_ACCOUNT;

            IQuestionDao questionDao = new QuestionDaoImpl();
            questionDao.saveQuestion(question);

            if (departmentID == cbDepartment_search.SelectedIndex && subjectID == cbSubject_search.SelectedIndex)
            {
                questionData.Add(question);
                lvQuestion.ItemsSource = null;
                lvQuestion.ItemsSource = questionData;
            }
              

            MessageBox.Show("Question Saved", "Confirmation");

        }
        private void btEdit_Question_Click(object sender, RoutedEventArgs e)
        {
            lblLevel_Question_msg.Visibility = Visibility.Hidden;
            lblQuestion_Description_msg.Visibility = Visibility.Hidden;
            lblSubject_Question_msg.Visibility = Visibility.Hidden;
            lblDepartment_Question_msg.Visibility = Visibility.Hidden;


            if (selected_Question == null)
            {
                MessageBox.Show("Select Question First", "Confirmation");
                return;
            }

            string level = tbLevel_Question.Text;
            DateTime date = DateTime.Now;
            string question_descr = tbQuestion_Description.Text;

            string subject = cbSubject_Question.Text;
            string department = cbDepartment_Question.Text;
            int subjectID = cbSubject_Question.SelectedIndex;
            int departmentID = cbDepartment_Question.SelectedIndex;

            if (checkQuestionForNullInput(level, question_descr, subject, department))
                return;
            if (checkQuestionForNullInput(level, question_descr, subject, department))
                return;

            Question question = new Question() { Subject = subjectList[subjectID], Department = subjectList[subjectID].DepList[departmentID], Question_descr = question_descr, Date = date, Level = Int32.Parse(level) };

            question.Id = selected_Question.Id;
            question.Account = CurrentUserInfo.CURENT_ACCOUNT;

            IQuestionDao questionDao = new QuestionDaoImpl();
            questionDao.updateQuestion(question);

            //Refresh
            questionData.Remove(selected_Question);
            questionData.Add(question);
            lvQuestion.ItemsSource = null;
            lvQuestion.ItemsSource = questionData;

            MessageBox.Show("Question Updated", "Confirmation");
        }
        private void btDelete_Question_Click(object sender, RoutedEventArgs e)
        {

            lblLevel_Question_msg.Visibility = Visibility.Hidden;
            lblQuestion_Description_msg.Visibility = Visibility.Hidden;
            lblSubject_Question_msg.Visibility = Visibility.Hidden;
            lblDepartment_Question_msg.Visibility = Visibility.Hidden;

            if (selected_Question == null)
            {
                MessageBox.Show("Select Question First", "Confirmation");
                return;
            }

            IQuestionDao questionDao = new QuestionDaoImpl();
            questionDao.deleteQuestion(selected_Question);

            //Refresh
            questionData.Remove(selected_Question);
            lvQuestion.ItemsSource = null;
            lvQuestion.ItemsSource = questionData;

            lvAnswer.ItemsSource = null;

            clearQuestionWidgets();
            clearAnswerWidgets();

            MessageBox.Show("Question Deleted", "Confirmation");

        }
        private void btClear_Question_Click(object sender, RoutedEventArgs e)
        {
            lblLevel_Question_msg.Visibility = Visibility.Hidden;
            lblQuestion_Description_msg.Visibility = Visibility.Hidden;
            lblSubject_Question_msg.Visibility = Visibility.Hidden;
            lblDepartment_Question_msg.Visibility = Visibility.Hidden;

            clearQuestionWidgets();
        }
        private void cbSubject_Question_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblLevel_Question_msg.Visibility = Visibility.Hidden;
            lblQuestion_Description_msg.Visibility = Visibility.Hidden;
            lblSubject_Question_msg.Visibility = Visibility.Hidden;
            lblDepartment_Question_msg.Visibility = Visibility.Hidden;

            cbDepartment_Question.Items.Clear();

            int index = cbSubject_Question.SelectedIndex;
            List<Department> listDep = subjectList[index].DepList;
            foreach (Department dep in listDep)
                cbDepartment_Question.Items.Add(dep.Department_descr);

        }

        private bool checkQuestionForNullInput(string level, string question_descr, string subject, string department)
        {
            bool emptyField = false;

            if (String.IsNullOrEmpty(level))
            {
                lblLevel_Question_msg.Visibility = Visibility.Visible;
                lblLevel_Question_msg.Foreground = Brushes.Red;
                lblLevel_Question_msg.Content = "Empty";
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
        private bool checkQuestionForValidInput(string level, string question_descr, string subject, string department)
        {

            bool emptyField = false;

            if (String.IsNullOrEmpty(level))
            {
                lblLevel_Question_msg.Visibility = Visibility.Visible;
                lblLevel_Question_msg.Foreground = Brushes.Red;
                lblLevel_Question_msg.Content = "Empty";
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

        private List<Question> loadQuestionData(string username, Subject subject, Department department)
        {
            List<Question> questionList;
            IQuestionDao questionDao = new QuestionDaoImpl();

            questionList = questionDao.findQuestion(username,subject, department);

            return questionList;
        }
        private void clearQuestionWidgets()
        {
            tbLevel_Question.Clear();
            tbOwner_Question.Clear();
            tbDate_Question.Clear();
            tbQuestion_Description.Clear();

            selected_Question = null;

        }

        //Answer ToolBAr
        private void btSave_Answer_Click(object sender, RoutedEventArgs e)
        {

            lblAnswer_Description_msg.Visibility = Visibility.Hidden;
            lblCorrect_Answer_msg.Visibility = Visibility.Hidden;

            if (selected_Question == null)
            {
                MessageBox.Show("Select Question First", "Confirmation");
                return;
            }

            string answer_descr = tbAnswer_Description.Text;
            string correct = cbCorrect_Answer.Text;
            bool corrext_answer;
            int index_correct_answer = cbCorrect_Answer.SelectedIndex;

            if (checkAnswerForNullInput(answer_descr, correct))
            {
                MessageBox.Show("Empty", "Confirmation");
                return;
            }

            corrext_answer = index_correct_answer == 0 ? false : true;
            Answer answer = new Answer() { Answer_descr = answer_descr, Account = CurrentUserInfo.CURENT_ACCOUNT, Date = DateTime.Now, Correct = corrext_answer };

            IAnswerDao answerDao = new AnswerDaoImpl();
            answerDao.saveAnswer(answer, selected_Question.Id);

            if (selected_Question.AnswerList == null)
                selected_Question.AnswerList = new List<Answer>();

            selected_Question.AnswerList.Add(answer);
            lvAnswer.ItemsSource = null;
            lvAnswer.ItemsSource = selected_Question.AnswerList;

            MessageBox.Show("Answer Saved", "Confirmation");
        }
        private void btEdit_Answer_Click(object sender, RoutedEventArgs e)
        {
            lblAnswer_Description_msg.Visibility = Visibility.Hidden;
            lblCorrect_Answer_msg.Visibility = Visibility.Hidden;

            if (selected_Question == null)
            {
                MessageBox.Show("Select Question First", "Confirmation");
                return;
            }
            if (selected_Answer == null)
            {
                MessageBox.Show("Select Answer First", "Confirmation");
                return;
            }

            string answer_descr = tbAnswer_Description.Text;
            string correct = cbCorrect_Answer.Text;
            bool corrext_answer;
            int index_correct_answer = cbCorrect_Answer.SelectedIndex;

            if (checkAnswerForNullInput(answer_descr, correct))
            {
                MessageBox.Show("Empty", "Confirmation");
                return;
            }

            corrext_answer = index_correct_answer == 0 ? false : true;
            Answer answer = new Answer() { Answer_descr = answer_descr, Account = CurrentUserInfo.CURENT_ACCOUNT, Date = DateTime.Now, Correct = corrext_answer };

            answer.Id = selected_Answer.Id;
            answer.Account = CurrentUserInfo.CURENT_ACCOUNT;

            IAnswerDao answerDao = new AnswerDaoImpl();
            answerDao.updateAnswer(answer);

            lvAnswer.ItemsSource = null;

            selected_Question.AnswerList.Remove(selected_Answer);
            selected_Question.AnswerList.Add(answer);
            lvAnswer.ItemsSource = null;
            lvAnswer.ItemsSource = selected_Question.AnswerList;

            MessageBox.Show("Answer Edited", "Confirmation");

        }
        private void btDelete_Answer_Click(object sender, RoutedEventArgs e)
        {
            lblAnswer_Description_msg.Visibility = Visibility.Hidden;
            lblCorrect_Answer_msg.Visibility = Visibility.Hidden;

            if (selected_Question == null)
            {
                MessageBox.Show("Select Question First", "Confirmation");
                return;
            }
            if (selected_Answer == null)
            {
                MessageBox.Show("Select Answer First", "Confirmation");
                return;
            }

            IAnswerDao answerDao = new AnswerDaoImpl();
            answerDao.deleteAnswer(selected_Answer);


            selected_Question.AnswerList.Remove(selected_Answer);
            lvAnswer.ItemsSource = null;
            lvAnswer.ItemsSource = selected_Question.AnswerList;

            clearAnswerWidgets();

            MessageBox.Show("Answer Deleted", "Confirmation");
        }
        private void btClear_Answer_Click(object sender, RoutedEventArgs e)
        {
            clearAnswerWidgets();
        }

        private bool checkAnswerForNullInput(string answer, string correct)
        {
            bool emptyField = false;

            if (String.IsNullOrEmpty(answer))
            {
                lblAnswer_Description_msg.Visibility = Visibility.Visible;
                lblAnswer_Description_msg.Foreground = Brushes.Red;
                lblAnswer_Description_msg.Content = "Empty";
                emptyField = true;
            }
            if (String.IsNullOrEmpty(correct))
            {
                lblCorrect_Answer_msg.Visibility = Visibility.Visible;
                lblCorrect_Answer_msg.Foreground = Brushes.Red;
                lblCorrect_Answer_msg.Content = "Empty";
                emptyField = true;
            }

            return emptyField;
        }

        private void clearAnswerWidgets()
        {
            lblAnswer_Description_msg.Visibility = Visibility.Hidden;
            lblCorrect_Answer_msg.Visibility = Visibility.Hidden;

            tbAnswer_Description.Clear();
            tbDate_Answer.Clear();
            tbOwner_Answer.Clear();

        }

        //Questionaire
        private void btRefresh_Questionaire_Click(object sender, RoutedEventArgs e)
        {
            IQuestionaireDao dao = new QuestionaireDaoImpl();
            List<Questionaire> listQuestionaire = dao.findQuestionare(CurrentUserInfo.CURENT_ACCOUNT);

            lvQuestionaire.ItemsSource = listQuestionaire;

        }
        private void btnAddQuestionToQuestionaire_Click(object sender, RoutedEventArgs e)
        {

            if (selected_Questionaire == null)
            {
                MessageBox.Show("Select Questionaire First", "Confirmation");
                return;
            }

            if (selected_Question == null)
            {
                MessageBox.Show("Select Question First", "Confirmation");
                return;
            }

            selected_Questionaire.QuestionList.Add(selected_Question);

            IQuestionaireDao dao = new QuestionaireDaoImpl();
            dao.addQuestionToQuestionaire(selected_Questionaire, selected_Question);

            lvQuestionaire_Question.ItemsSource = null;
            lvQuestionaire_Question.ItemsSource = selected_Questionaire.QuestionList;

            MessageBox.Show("Question Added", "Confirmation");
        }
        private void btnRemoveQuestionFromQuestionaire_Click(object sender, RoutedEventArgs e)
        {
            if (selected_Questionaire == null)
            {
                MessageBox.Show("Select Questionaire First", "Confirmation");
                return;
            }

            if (selected_Questionaire_Question == null)
            {
                MessageBox.Show("Select Questionaire Question First", "Confirmation");
                return;
            }

            selected_Questionaire.QuestionList.Remove(selected_Questionaire_Question);

            IQuestionaireDao dao = new QuestionaireDaoImpl();
            dao.removeQuestionFromQuestionaire(selected_Questionaire, selected_Questionaire_Question);

            lvQuestionaire_Question.ItemsSource = null;
            lvQuestionaire_Question.ItemsSource = selected_Questionaire.QuestionList;

            MessageBox.Show("Question Removed", "Confirmation");
        }

        private void setMsgLabelToHidden()
        {
            lblSubject_search_msg.Visibility = Visibility.Hidden;
            lblDepartment_search_msg.Visibility = Visibility.Hidden;
            cbUsernameType_search_msg.Visibility = Visibility.Hidden;
            lblUsername_search_msg.Visibility = Visibility.Hidden;

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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }

    }
}
