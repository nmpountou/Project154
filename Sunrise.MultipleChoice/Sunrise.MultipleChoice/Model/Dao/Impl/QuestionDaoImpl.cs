using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunrise.MultipleChoice.Model;
using MySql.Data.MySqlClient;
using Sunrise.MultipleChoice.Data;
using System.Diagnostics;
using Quastionnaire.Model.Dao.Interface;
using log4net;

namespace Quastionnaire.Model.Dao.Impl
{

    class QuestionDaoImpl : IQuestionDao
    {
        private ILog logger = LogManager.GetLogger(nameof(QuestionDaoImpl));


        public List<Question> findQuestion(string usernameF,Subject subject, Department department)
        {
            logger.Debug("findQuestion()");

            List<Question> questionList = new List<Question>();
            string query = "";

            if(usernameF != "all")
                query = "select Q.id as question_id,Q.question,Q.create_date,A.id as user_id,Q.level_range,A.username from question Q " +
                           "inner join account A on A.id = Q.account_id " +
                           "inner join subject_department SD on Q.subject_department_id = SD.id where SD.subject_id = " + subject.Id + " and SD.department_id = " + department.Id + " and A.username = '" + usernameF + "'; ";
            else
                query = "select Q.id as question_id,Q.question,Q.create_date,A.id as user_id,Q.level_range,A.username from question Q " +
                          "inner join account A on A.id = Q.account_id " +
                          "inner join subject_department SD on Q.subject_department_id = SD.id where SD.subject_id = " + subject.Id + " and SD.department_id = " + department.Id + " ; ";


            MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
               CurrentUserInfo.PASSWORD,
               CurrentUserInfo.HOSTNAME,
               CurrentUserInfo.PORT,
               CurrentUserInfo.DATABASE);

            mysql.initializeConnection();
            mysql.openMysqlConnection();

            //Question Fields
            int question_id;
            string question_descr;
            int level_question;
            DateTime date_question;

            //Account Fields
            int account_id;
            string username;

            //Questions
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
                            date_question = reader.GetDateTime(2).Date;

                            Debug.Write("NOWWWWWWWWWW");
                            Debug.Write(date_question.ToString("MM/dd/yyyy"));

                            account_id = reader.GetInt32(3);
                            level_question = reader.GetInt32(4);
                            username = reader.GetString(5);

                            questionList.Add(new Question() { Id = question_id, Question_descr = question_descr, Account = new Account() { Username = username }, Subject = subject, Department = department, Level = level_question, Date = date_question });
                        }
                    }
                } // reader closed and disposed up here

            } // command disposed here
            mysql.closeMysqlConnection();


            //Answer of Above Questions

            if (questionList.Any())
            {
                mysql.openMysqlConnection();
                setAnsersForIndividualQuestion(questionList, mysql);
                mysql.closeMysqlConnection();
            }
         

            //Debug
            foreach (Question question in questionList)
            {
                Debug.WriteLine(question.Question_descr);
                foreach (Answer answer in question.AnswerList)
                    Debug.WriteLine(answer.Answer_descr);
            }





            return questionList;

        }

        public void setAnsersForIndividualQuestion(List<Question> questionList, MysqlConnector mysql)
        {

            if (questionList == null)
                return;

            List<Answer> answerList = new List<Answer>();

            string query;
            //Answer Fields
            int answer_id;
            string answer_descr;
            DateTime date_answer;
            bool correct_answer;


            foreach (Question question in questionList)
            {
                query = "select id as answer_id,answer,create_date,correct from answer A inner join question_answer QA on A.id = QA.answer_id and QA.question_id=" + question.Id + ";";
                using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                answer_id = reader.GetInt32(0);
                                answer_descr = reader.GetString(1);
                                date_answer = reader.GetDateTime(2).Date;
                                correct_answer = (reader.GetInt32(3) == 0 ? false : true);

                                answerList.Add(new Answer() { Id = answer_id, Answer_descr = answer_descr, Date = date_answer, Correct = correct_answer, Account = question.Account });
                            }
                            question.AnswerList = answerList;
                            answerList = new List<Answer>();
                        }
                    } // reader closed and disposed up here

                } // command disposed here

            }


        }


        public List<Question> findQuestion(Account account, Subject subject, Department department)
        {
            throw new NotImplementedException();
        }

        public void saveQuestion(Question question)
        {

            string query = "select SD.id from subject_department SD where SD.subject_id=" + question.Subject.Id + " and SD.department_id=" + question.Department.Id + " ";

            MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
                CurrentUserInfo.PASSWORD,
                CurrentUserInfo.HOSTNAME,
                CurrentUserInfo.PORT,
                CurrentUserInfo.DATABASE);

            mysql.initializeConnection();
            mysql.openMysqlConnection();

            long subjectdepartment_id = -1;

            using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader != null)
                        while (reader.Read())
                        {
                            subjectdepartment_id = reader.GetInt32(0);
                        }
                }

            }


            string formatForMySql = question.Date.ToString("yyyy-MM-dd");




            query = "insert into question(question,level_range,create_date,account_id,subject_department_id) values('" + question.Question_descr + "'," + question.Level + ",'" + formatForMySql + "'," + question.Account.Id + "," + subjectdepartment_id + ")";
            long new_questionId;

            using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
            {
                cmd.ExecuteNonQuery();
                new_questionId = cmd.LastInsertedId;
            }

            question.Id = (int)new_questionId;

            mysql.closeMysqlConnection();

        }
        public void updateQuestion(Question question)
        {
            string query = "select SD.id from subject_department SD where SD.subject_id=" + question.Subject.Id + " and SD.department_id=" + question.Department.Id + " ";

            MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
                CurrentUserInfo.PASSWORD,
                CurrentUserInfo.HOSTNAME,
                CurrentUserInfo.PORT,
                CurrentUserInfo.DATABASE);

            mysql.initializeConnection();
            mysql.openMysqlConnection();

            int subjectdepartment_id = -1;

            using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader != null)
                        while (reader.Read())
                        {
                            subjectdepartment_id = reader.GetInt32(0);
                        }
                }
            }


            string formatForMySql = question.Date.ToString("yyyy-MM-dd");

            query = "update question set question = '" + question.Question_descr + "',level_range=" + question.Level + ",create_date='" + formatForMySql + "',subject_department_id = " + subjectdepartment_id + " where id = " + question.Id + "";

            using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
                cmd.ExecuteNonQuery();

            mysql.closeMysqlConnection();

        }
        public void deleteQuestion(Question question)
        {
            logger.Debug("deleteQuestion()");

            MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
             CurrentUserInfo.PASSWORD,
             CurrentUserInfo.HOSTNAME,
             CurrentUserInfo.PORT,
             CurrentUserInfo.DATABASE);

            string query = "delete from question where id = " + question.Id + ";";
            string answersIDs = "";
            foreach (Answer answer in question.AnswerList)
                answersIDs += answer.Id.ToString() + ",";

            if (!String.IsNullOrEmpty(answersIDs))
                answersIDs = answersIDs.Substring(0, answersIDs.Length - 1);

            mysql.initializeConnection();
            mysql.openMysqlConnection();

            using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
                cmd.ExecuteNonQuery();

            query = "delete from answer where id in (" + answersIDs + ");";

            if (!String.IsNullOrEmpty(answersIDs))
                using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
                    cmd.ExecuteNonQuery();


            mysql.closeMysqlConnection();

            logger.Info("Question With ID " + question.Id + " Deleted");
        }



    }
}
