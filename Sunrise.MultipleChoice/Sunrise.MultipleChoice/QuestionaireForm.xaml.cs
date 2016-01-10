using log4net;
using MySql.Data.MySqlClient;
using Quastionnaire.Model;
using Quastionnaire.Model.Dao.Impl;
using Quastionnaire.Model.Dao.Interface;
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
    /// Interaction logic for QuestionaireForm.xaml
    /// </summary>
    public partial class QuestionaireForm : Page
    {
        private ILog logger = LogManager.GetLogger(nameof(QuestionaireForm));

        private Questionaire selected_Questionaire;
        private QuestionForm.UsernameType userNameTypeQuestionaire = QuestionForm.UsernameType.Undefined;
        private QuestionForm.DateType dateTypeQuestionaire = QuestionForm.DateType.Undefined;
        private List<Questionaire> listQuestionaire = new List<Questionaire>();

        public QuestionaireForm()
        {
            InitializeComponent();
            initializeComboBoxInfo();
            setMsgLabelToHidden();
        }
        private void initializeComboBoxInfo()
        {
            cbUsernameType_Questionairesearch.Items.Add("ALL");
            cbUsernameType_Questionairesearch.Items.Add("MINE");
            cbUsernameType_Questionairesearch.Items.Add("OTHER");

            cbDateType_Questionairesearch.Items.Add("ALL");
            cbDateType_Questionairesearch.Items.Add("OTHER");

        }
        private void setMsgLabelToHidden()
        {
            cbUsernameType_Questionairesearch_msg.Visibility = Visibility.Hidden;
            cbDateType_Questionairesearch_msg.Visibility = Visibility.Hidden;
            lblUsername_Questionairesearch_msg.Visibility = Visibility.Hidden;
            lblDate_Questionairesearch_msg.Visibility = Visibility.Hidden;
            lblOwner_Questionaire_msg.Visibility = Visibility.Hidden;
            lblDate_Questionaire_msg.Visibility = Visibility.Hidden;
            lblQuestionaire_Description_msg.Visibility = Visibility.Hidden;

            tbUsername_Questionairesearch.IsEnabled = false;
            dpDate_Questionairesearch.IsEnabled = false;
        }

        //Table
        private void lvQuestionaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Questionaire questionaire = (Questionaire)lvQuestionaire.SelectedItem;

            if (questionaire == null)
                return;

            selected_Questionaire = questionaire;

            tbOwner_Questionaire.Text = selected_Questionaire.Account.Username;
            tbDate_Questionaire.Text = selected_Questionaire.Date.ToString();
            tbQuestionaire_Description.Text = selected_Questionaire.Questionaire_descr;
        }

        //Search
        private void cbUsernameType_search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int choice = cbUsernameType_Questionairesearch.SelectedIndex;

            switch (choice)
            {
                case 0:
                    userNameTypeQuestionaire = QuestionForm.UsernameType.ALL;
                    tbUsername_Questionairesearch.IsEnabled = false;
                    tbUsername_Questionairesearch.Text = "";
                    break;
                case 1:
                    userNameTypeQuestionaire = QuestionForm.UsernameType.MINE;
                    tbUsername_Questionairesearch.IsEnabled = false;
                    tbUsername_Questionairesearch.Text = "";
                    break;
                case 2:
                    userNameTypeQuestionaire = QuestionForm.UsernameType.OTHER;
                    tbUsername_Questionairesearch.IsEnabled = true;
                    tbUsername_Questionairesearch.Text = "";
                    break;
            }

        }
        private void cbDateType_search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int choice = cbDateType_Questionairesearch.SelectedIndex;

            switch (choice)
            {
                case 0:
                    dateTypeQuestionaire = QuestionForm.DateType.ALL;
                    dpDate_Questionairesearch.IsEnabled = false;
                    break;
                case 1:
                    dateTypeQuestionaire = QuestionForm.DateType.OTHER;
                    dpDate_Questionairesearch.IsEnabled = true;
                    break;
            }

        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            cbUsernameType_Questionairesearch_msg.Visibility = Visibility.Hidden;
            cbDateType_Questionairesearch_msg.Visibility = Visibility.Hidden;
            lblUsername_Questionairesearch_msg.Visibility = Visibility.Hidden;
            lblDate_Questionairesearch_msg.Visibility = Visibility.Hidden;

            if (userNameTypeQuestionaire == QuestionForm.UsernameType.Undefined)
            {
                cbUsernameType_Questionairesearch_msg.Visibility = Visibility.Visible;
                cbUsernameType_Questionairesearch_msg.Foreground = Brushes.Red;
                cbUsernameType_Questionairesearch_msg.Content = "Empty";
                MessageBox.Show("UsernameType Empty");
                return;
            }
            if (dateTypeQuestionaire == QuestionForm.DateType.Undefined)
            {
                cbDateType_Questionairesearch_msg.Visibility = Visibility.Visible;
                cbDateType_Questionairesearch_msg.Foreground = Brushes.Red;
                cbDateType_Questionairesearch_msg.Content = "Empty";
                MessageBox.Show("DateType Empty");
                return;
            }

            string username = "";
            DateTime? date = new DateTime();

            switch (userNameTypeQuestionaire)
            {
                case QuestionForm.UsernameType.ALL:
                    break;
                case QuestionForm.UsernameType.MINE:
                    username = CurrentUserInfo.USERNAME;
                    break;
                case QuestionForm.UsernameType.OTHER:
                    username = tbUsername_Questionairesearch.Text;
                    if (String.IsNullOrEmpty(username))
                    {
                        lblUsername_Questionairesearch_msg.Visibility = Visibility.Visible;
                        lblUsername_Questionairesearch_msg.Foreground = Brushes.Red;
                        lblUsername_Questionairesearch_msg.Content = "Empty";
                        MessageBox.Show("Username Field Empty");
                        return;
                    }
                    //Check if Searched Username Existt
                    if (!usernameExist(username))
                    {
                        MessageBox.Show("Username Dosent Exist");
                        return;
                    }
                    break;

            }
            switch (dateTypeQuestionaire)
            {
                case QuestionForm.DateType.ALL:
                    break;
                case QuestionForm.DateType.OTHER:
                    date = dpDate_Questionairesearch.SelectedDate;
                    if (date == null)
                    {
                        lblDate_Questionairesearch_msg.Visibility = Visibility.Visible;
                        lblDate_Questionairesearch_msg.Foreground = Brushes.Red;
                        lblDate_Questionairesearch_msg.Content = "Empty";
                        MessageBox.Show("Date Field Null");
                        return;
                    }

                    break;
            }

            DateTime dateTime = DateTime.Parse(date.ToString());

            IQuestionaireDao dao = new QuestionaireDaoImpl();
            listQuestionaire = dao.findQuestionare(username, dateTime, userNameTypeQuestionaire, dateTypeQuestionaire);
            lvQuestionaire.ItemsSource = listQuestionaire;

            MessageBox.Show("Questionaire Retrieved", "Confirmation");

        }
        private bool usernameExist(string username)
        {
            bool usernameExist = false;

            MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
             CurrentUserInfo.PASSWORD,
             CurrentUserInfo.HOSTNAME,
             CurrentUserInfo.PORT,
             CurrentUserInfo.DATABASE);

            string query = "select username from account where username = '" + username + "'";

            mysql.initializeConnection();
            mysql.openMysqlConnection();

            using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
            using (MySqlDataReader reader = cmd.ExecuteReader())
                if (reader.HasRows)
                    usernameExist = true;

            mysql.closeMysqlConnection();

            return usernameExist;
        }

        //ToolBar
        private void btSave_Questionaire_Click(object sender, RoutedEventArgs e)
        {
            lblQuestionaire_Description_msg.Visibility = Visibility.Hidden;

            string description = tbQuestionaire_Description.Text;

            if (String.IsNullOrEmpty(description))
            {
                lblQuestionaire_Description_msg.Visibility = Visibility.Visible;
                lblQuestionaire_Description_msg.Foreground = Brushes.Red;
                lblQuestionaire_Description_msg.Content = "Empty";
                MessageBox.Show("Empty");
                return;
            }

            Questionaire questionaire = new Questionaire() {Date = DateTime.Now , Questionaire_descr = description , HasQuestions = false , Account  = CurrentUserInfo.CURENT_ACCOUNT};

            IQuestionaireDao questionaireDao = new QuestionaireDaoImpl();
            questionaireDao.saveQuestionaire(questionaire);

            listQuestionaire.Add(questionaire);
            lvQuestionaire.ItemsSource = null;
            lvQuestionaire.ItemsSource = listQuestionaire;

            selected_Questionaire = null;
            MessageBox.Show("Saved");

        }
        private void btEdit_Questionaire_Click(object sender, RoutedEventArgs e)
        {
            lblQuestionaire_Description_msg.Visibility = Visibility.Hidden;

            if (selected_Questionaire == null)
            {
                MessageBox.Show("Select Questionaire First", "Confirmation");
                return;
            }

            string description = tbQuestionaire_Description.Text;

            Questionaire questionaire = new Questionaire() { Id = selected_Questionaire.Id,Date = DateTime.Now, Questionaire_descr = description, Account = CurrentUserInfo.CURENT_ACCOUNT };

            IQuestionaireDao questionaireDao = new QuestionaireDaoImpl();
            questionaireDao.updateQuestionaire(questionaire);

            listQuestionaire.Remove(selected_Questionaire);
            listQuestionaire.Add(questionaire);
            lvQuestionaire.ItemsSource = null;
            lvQuestionaire.ItemsSource = listQuestionaire;

            selected_Questionaire = null;
            MessageBox.Show("Edited");

        }
        private void btDelete_Questionaire_Click(object sender, RoutedEventArgs e)
        {
            lblQuestionaire_Description_msg.Visibility = Visibility.Hidden;

            if (selected_Questionaire == null)
            {
                MessageBox.Show("Select Questionaire First", "Confirmation");
                return;
            }

            IQuestionaireDao questionaireDao = new QuestionaireDaoImpl();
            questionaireDao.deleteQuestionaire(selected_Questionaire);

            listQuestionaire.Remove(selected_Questionaire);
            lvQuestionaire.ItemsSource = null;
            lvQuestionaire.ItemsSource = listQuestionaire;

            selected_Questionaire = null;
            MessageBox.Show("Deleted");
        }
        private void btClear_Questionaire_Click(object sender, RoutedEventArgs e)
        {
            lblQuestionaire_Description_msg.Visibility = Visibility.Hidden;
            tbDate_Questionaire.Text = "";
            tbOwner_Questionaire.Text = "";
            tbQuestionaire_Description.Text = "";

            selected_Questionaire = null;

        }

        //Navigation
        private void mainMenuBt_Click(object sender, RoutedEventArgs e)
        {

            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri("MainMenu.xaml", UriKind.RelativeOrAbsolute));
            

        }
        private void questionBt_Click(object sender, RoutedEventArgs e)
        {

            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri("QuestionForm.xaml", UriKind.RelativeOrAbsolute));
            
        }
        private void export_Click(object sender, RoutedEventArgs e)
        {
            if (selected_Questionaire == null)
            {
                MessageBox.Show("Select Questionaire First", "Confirmation");
                return;
            }

            Export_to_Pdf wpdf = new Export_to_Pdf(selected_Questionaire);
            wpdf.Visibility = Visibility.Visible;
        }
    }
}
