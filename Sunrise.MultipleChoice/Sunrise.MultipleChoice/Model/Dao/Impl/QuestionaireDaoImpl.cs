using log4net;
using MySql.Data.MySqlClient;
using Quastionnaire.Model.Dao.Interface;
using Sunrise.MultipleChoice.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunrise.MultipleChoice.Model;

namespace Quastionnaire.Model.Dao.Impl
{
    class QuestionaireDaoImpl : IQuestionaireDao
    {

        private ILog logger = LogManager.GetLogger(nameof(QuestionaireDaoImpl));

        public void addQuestionToQuestionaire(Questionaire questionaire, Question question)
        {

            MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
            CurrentUserInfo.PASSWORD,
            CurrentUserInfo.HOSTNAME,
            CurrentUserInfo.PORT,
            CurrentUserInfo.DATABASE);

            mysql.initializeConnection();
            mysql.openMysqlConnection();

            string query = "insert into questionaire_question(questionaire_id,question_id) values(" + questionaire.id + "," + question.Id + ")";

            using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
            {
                cmd.ExecuteNonQuery();
            }

            mysql.closeMysqlConnection();


            logger.Debug("Question ID ::" + question.Id + " Add To Questionare Wih Id ::" + questionaire.id);


        }
        public void removeQuestionFromQuestionaire(Questionaire questionaire, Question question)
        {

            logger.Debug("removeQuestionFromQuestionaire()");

            MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
            CurrentUserInfo.PASSWORD,
            CurrentUserInfo.HOSTNAME,
            CurrentUserInfo.PORT,
            CurrentUserInfo.DATABASE);

            mysql.initializeConnection();
            mysql.openMysqlConnection();


            string query = "delete from questionaire_question where question_id=" + question.Id + " and questionaire_id = " + questionaire.id + "";

            using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
            {
                cmd.ExecuteNonQuery();
            }

            mysql.closeMysqlConnection();

            logger.Debug("Question ID ::" +question.Id+ " Removed From Questionare Wih Id ::" + questionaire.id);

        }

        public void deleteQuestionaire(Questionaire questionaire)
        {

            MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
              CurrentUserInfo.PASSWORD,
              CurrentUserInfo.HOSTNAME,
              CurrentUserInfo.PORT,
              CurrentUserInfo.DATABASE);

            mysql.initializeConnection();
            mysql.openMysqlConnection();

            string query = "delete from questionaire where id = " + questionaire.id + "";

            using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
            {
                cmd.ExecuteNonQuery();

            }

            logger.Info("Questionare With ID :: " + questionaire.id + " Deleted");

        }
        public List<Questionaire> findQuestionare(Account account)
        {
            List<Questionaire> ListQuestionaire = new List<Questionaire>();
            List<Question> questionList = new List<Question>();

            MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
             CurrentUserInfo.PASSWORD,
             CurrentUserInfo.HOSTNAME,
             CurrentUserInfo.PORT,
             CurrentUserInfo.DATABASE);

            mysql.initializeConnection();
            mysql.openMysqlConnection();

            string query = "select QR.id,QR.questionaire,QR.create_date,QR.account_id from questionaire QR where QR.account_id=" + account.Id;
            int questionaire_id;
            string questionaire_desc;
            DateTime questionaire_date;
            int questionaire_account_id;

            using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader != null)
                        while (reader.Read())
                        {

                            questionaire_id = reader.GetInt32(0);
                            questionaire_desc = reader.GetString(1);
                            questionaire_date = reader.GetDateTime(2);
                            questionaire_account_id = reader.GetInt32(3);

                            ListQuestionaire.Add(new Questionaire() { id = questionaire_id, Questionaire_descr = questionaire_desc, Date = questionaire_date, Account = new Account() { Id = questionaire_account_id } });
                        }
                }
            }


            //Question Fields
            int question_id;
            string question_descr;
            int level_question;
            DateTime date_question;

            //Department - Subject
            string department;
            string subject;
            int department_id;
            int subject_id;

            //Account Fields
            int account_id;
            string username;

            foreach (Questionaire questionaire in ListQuestionaire)
            {

                questionaire.QuestionList = questionList;

                query = " select Q.id as question_id,Q.question,Q.create_date,A.id as user_id,Q.level_range,A.username,S.subject,D.department,S.id,D.id from question Q " +
                         " inner join account A on A.id = Q.account_id " +
                         " inner join questionaire_question QQ on QQ.questionaire_id="+questionaire.id + " and QQ.question_id= Q.id " +  
                         " inner join subject_department SD on Q.subject_department_id = SD.id " +
                         " inner join subject S on S.id=SD.subject_id " +
                         " inner join department D on D.id = SD.department_id " +
                         "where  Q.account_id = " + account.Id + "";


                using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
                {

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                question_id = reader.GetInt32(0);
                                question_descr = reader.GetString(1);
                                date_question = reader.GetDateTime(2);
                                account_id = reader.GetInt32(3);
                                level_question = reader.GetInt32(4);
                                username = reader.GetString(5);
                                subject = reader.GetString(6);
                                department = reader.GetString(7);
                                subject_id = reader.GetInt32(8);
                                department_id = reader.GetInt32(9);

                                questionList.Add(new Question() { Id = question_id, Question_descr = question_descr, Account = new Account() {Id = account_id, Username = username }, Subject = new Subject() { Id=subject_id,Subject_descr=subject} , Department = new Department() { Id=department_id,Department_descr=department}, Level = level_question, Date = date_question });
                                questionaire.QuestionList=questionList;
                            }
                        }
                    } // reader closed and disposed up here
                }
            }




            QuestionDaoImpl question = new QuestionDaoImpl();

            foreach (Questionaire questionaire in ListQuestionaire)
                question.setAnsersForIndividualQuestion(questionaire.QuestionList, mysql);


            mysql.closeMysqlConnection();

            return ListQuestionaire;

        }

        public void saveQuestionaire(Questionaire questionaire)
        {


        }
        public void updateQuestionaire(Questionaire questionaire)
        {


        } 
    }
}
