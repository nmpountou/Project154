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

namespace Quastionnaire.Model.Dao.Impl
{
    class QuestionDaoImpl : IQuestionDao
    {
        public void deleteQuestion(Question question)
        {
            throw new NotImplementedException();
        }

        public List<Question> findQuestion(Subject subject, Department department)
        {

            List<Question> questionList = new List<Question>();
            List<Answer> answerList = new List<Answer>();

            string query = "select Q.id as question_id,Q.question,Q.create_date,Q.correct_answer_id,A.id as user_id,Q.level_range,A.username from question Q " +
                           "inner join account A on A.id = Q.account_id " +
                           "inner join subject_department SD on Q.subject_department_id = SD.id where SD.subject_id = " + subject.Id + " and SD.department_id = " + department.Id + "; ";

            MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
               CurrentUserInfo.PASSWORD,
               CurrentUserInfo.HOSTNAME,
               CurrentUserInfo.PORT,
               CurrentUserInfo.DATABASE);

            mysql.initializeConnection();
            mysql.openMysqlConnection();

            //Question Fields
            int question_id;
            int correct_answer_id;
            string question_descr;
            int level_question;
            DateTime date_question;

            //Account Fields
            int account_id;
            string username;

            //Answer Fields
            int answer_id;
            string answer_descr;
            DateTime date_answer;

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
                            date_question = reader.GetDateTime(2);
                            correct_answer_id = reader.GetInt32(3);
                            account_id = reader.GetInt32(4);
                            level_question = reader.GetInt32(5);
                            username = reader.GetString(6);

                            questionList.Add(new Question() { Id = question_id, Question_descr = question_descr, Account = new Account() { Username = username }, CorrectAnswer = new Answer() { Id = correct_answer_id }, Subject = subject, Department = department,Level = level_question,Date = date_question });
                        }
                    }
                } // reader closed and disposed up here

            } // command disposed here
            mysql.closeMysqlConnection();

            //Answer of Above Questions
            mysql.openMysqlConnection();

            foreach (Question question in questionList)
            {
                query = "select id as answer_id,answer,create_date from answer A inner join question_answer QA on A.id = QA.answer_id and QA.question_id=" + question.Id + ";";
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
                                date_answer = reader.GetDateTime(2);

                                answerList.Add(new Answer() { Id = answer_id, Answer_descr = answer_descr, Date = date_answer, Correct = question.CorrectAnswer.Id == answer_id ? true : false, Account = question.Account });
                            }
                            question.AnswerList = answerList;
                            answerList = new List<Answer>();
                        }
                    } // reader closed and disposed up here

                } // command disposed here

            }

            mysql.closeMysqlConnection();

            //Debug
            foreach (Question question in questionList)
            {
                Debug.WriteLine(question.Question_descr);
                foreach (Answer answer in question.AnswerList)
                    Debug.WriteLine(answer.Answer_descr);
            }



            return questionList;

        }

        public List<Question> findQuestion(Account account, Subject subject, Department department)
        {
            throw new NotImplementedException();
        }

        public void saveQuestion(Question question)
        {
            throw new NotImplementedException();
        }

        public void updateQuestion(Question question)
        {
            throw new NotImplementedException();
        }
    }
}
