﻿using Quastionnaire.Model;
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
        private List<Question> questionData;

        public QuestionForm()
        {
            InitializeComponent();

            Logger.Setup();

            setMsgLabelToHidden();
            initializeComboBoxInfo();


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

            cbCorrect_Answer.Items.Add("No");
            cbCorrect_Answer.Items.Add("Yes");

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

            lvQuestion.ItemsSource = null;
            lvAnswer.ItemsSource = null;

            string subject = cbSubject_search.Text;
            string department = cbDepartment_search.Text;

            int subject_index = cbSubject_search.SelectedIndex;
            int department_index = cbDepartment_search.SelectedIndex;

            if (checkSearchForNullInput(subject, department))
                return;

            questionData = loadQuestionData(CurrentUserInfo.CURENT_ACCOUNT, subjectList[subject_index], subjectList[subject_index].DepList[department_index]);
            lvQuestion.ItemsSource = questionData;

        }

        private bool checkSearchForNullInput(string subject, string department)
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
       
        //Table OnClick
        private void lvQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
            cbCorrect_Answer.Text = answer.Correct.ToString();

            lvAnswer.SelectedItem = null;

        }

        //Question ToolBAr
        private void btSave_Question_Click(object sender, RoutedEventArgs e)
        {

            string level = tbLevel_Question.Text;
            DateTime date = new DateTime();
            string question_descr = tbQuestion_Description.Text;

            string subject = cbSubject_Question.Text;
            string department = cbDepartment_Question.Text;
            int subjectID = cbSubject_Question.SelectedIndex;
            int departmentID = cbDepartment_Question.SelectedIndex;

            if (checkQuestionForNullInput(level, question_descr, subject, department))
                return;
            if (checkQuestionForNullInput(level, question_descr, subject, department))
                return;

            Question question = new Question() { Subject = subjectList[subjectID], Department = subjectList[subjectID].DepList[departmentID], Question_descr = question_descr, Date = date, Level = Int32.Parse(level)};

            IQuestionDao questionDao = new QuestionDaoImpl();
            questionDao.saveQuestion(question);

            MessageBox.Show("Question Saved", "Confirmation");

        }
        private void btEdit_Question_Click(object sender, RoutedEventArgs e)
        {

            if (selected_Question == null)
            {
                MessageBox.Show("Select Question First", "Confirmation");
                return;
            }

            string level = tbLevel_Question.Text;
            DateTime date = new DateTime();
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
            int subject_index = cbSubject_search.SelectedIndex;
            int department_index = cbDepartment_search.SelectedIndex;

            List<Question> questionData = loadQuestionData(CurrentUserInfo.CURENT_ACCOUNT, subjectList[subject_index], subjectList[subject_index].DepList[department_index]);
            lvQuestion.ItemsSource = questionData;
            lvQuestion.ItemsSource = null;


            MessageBox.Show("Question Updated", "Confirmation");
        }
        private void btDelete_Question_Click(object sender, RoutedEventArgs e)
        {

            if (selected_Question == null)
            {
                MessageBox.Show("Select Question First", "Confirmation");
                return;
            }

            IQuestionDao questionDao = new QuestionDaoImpl();
            questionDao.deleteQuestion(selected_Question);

            int subject_index = cbSubject_search.SelectedIndex;
            int department_index = cbDepartment_search.SelectedIndex;

            List<Question> questionData = loadQuestionData(CurrentUserInfo.CURENT_ACCOUNT, subjectList[subject_index], subjectList[subject_index].DepList[department_index]);
            lvQuestion.ItemsSource = questionData;
            lvQuestion.ItemsSource = null;

            MessageBox.Show("Question Deleted", "Confirmation");

        }
        private void btClear_Question_Click(object sender, RoutedEventArgs e)
        {
            clearQuestionWidgets();
        }
        private void cbSubject_Question_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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

        private List<Question> loadQuestionData(Account account, Subject subject, Department department)
        {
            List<Question> questionList;
            IQuestionDao questionDao = new QuestionDaoImpl();

            questionList = questionDao.findQuestion(subject, department);

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
            Answer answer = new Answer() { Answer_descr = answer_descr, Account = CurrentUserInfo.CURENT_ACCOUNT, Date = new DateTime(), Correct = corrext_answer };

            IAnswerDao answerDao = new AnswerDaoImpl();
            answerDao.saveAnswer(answer,selected_Question.Id);

            selected_Question.AnswerList.Add(answer);
            lvAnswer.ItemsSource = null;
            lvAnswer.ItemsSource = selected_Question.AnswerList;

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

        private bool checkAnswerForNullInput(string answer,string correct)
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }

       
    }
}
