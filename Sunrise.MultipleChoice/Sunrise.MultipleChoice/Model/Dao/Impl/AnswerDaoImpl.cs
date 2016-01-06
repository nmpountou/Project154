using log4net;
using MySql.Data.MySqlClient;
using Quastionnaire.Model.Dao.Interface;
using Sunrise.MultipleChoice.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quastionnaire.Model.Dao.Impl
{
    class AnswerDaoImpl : IAnswerDao
    {

        private ILog logger = LogManager.GetLogger(nameof(QuestionDaoImpl));

        public void deleteAnswer(Answer answer)
        {

            logger.Debug("deleteAnswer()");

            MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
             CurrentUserInfo.PASSWORD,
             CurrentUserInfo.HOSTNAME,
             CurrentUserInfo.PORT,
             CurrentUserInfo.DATABASE);

            mysql.initializeConnection();
            mysql.openMysqlConnection();

            string query = "delete from answer where id = " + answer.Id + "";

            using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
                cmd.ExecuteNonQuery();

            mysql.closeMysqlConnection();
        }
        public void updateAnswer(Answer answer)
        {

            logger.Debug("updateAnswer()");

            MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
             CurrentUserInfo.PASSWORD,
             CurrentUserInfo.HOSTNAME,
             CurrentUserInfo.PORT,
             CurrentUserInfo.DATABASE);

            mysql.initializeConnection();
            mysql.openMysqlConnection();

            string formatForMySql = answer.Date.ToString("yyyy-MM-dd HH:mm");

            string query = "update answer set answer='" + answer.Answer_descr + "',create_date='" + formatForMySql + "',correct=" + (answer.Correct ? 1 : 0) + ",account_id=" + answer.Account.Id + " where id=" + answer.Id + "";

            using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
                cmd.ExecuteNonQuery();

            mysql.closeMysqlConnection();

        }
        public void saveAnswer(Answer answer, int question_id)
        {

            logger.Debug("saveAnswer()");

            string formatForMySql = answer.Date.ToString("yyyy-MM-dd HH:mm");

            string query = "insert into answer(answer,create_date,correct,account_id) values('" + answer.Answer_descr + "','" + formatForMySql + "'," + (answer.Correct ? 1 : 0) + "," + answer.Account.Id + ")";

            MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
             CurrentUserInfo.PASSWORD,
             CurrentUserInfo.HOSTNAME,
             CurrentUserInfo.PORT,
             CurrentUserInfo.DATABASE);

            mysql.initializeConnection();
            mysql.openMysqlConnection();

            long new_answer_id;

            using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
            {
                cmd.ExecuteNonQuery();
                new_answer_id = cmd.LastInsertedId;

            }
            query = "insert into question_answer(question_id,answer_id) values(" + question_id + "," + new_answer_id + ")";

            logger.Info("Answer iD " + new_answer_id);
            logger.Info("Question ID " + question_id);

            using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
                cmd.ExecuteNonQuery();

            answer.Id = (int)new_answer_id;

            mysql.closeMysqlConnection();

        }

        public List<Answer> findAnswer(int question_id)
        {

            logger.Debug("findAnswer()");

            string query = "select id,answer,correct,create_date,account_id from answer A "
                + " inner join question_answer QA on QA.answer_id = A.id where QA.question_id = " + question_id;

            List<Answer> listAnswer = new List<Answer>();
            Answer tempAnswer;
            int answer_id;
            string answer;
            bool correct;
            DateTime date_answer;
            int account_id;

            MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
               CurrentUserInfo.PASSWORD,
               CurrentUserInfo.HOSTNAME,
               CurrentUserInfo.PORT,
               CurrentUserInfo.DATABASE);

            mysql.initializeConnection();
            mysql.openMysqlConnection();

            using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
            using (MySqlDataReader reader = cmd.ExecuteReader())
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        answer_id = reader.GetInt32(0);
                        answer = reader.GetString(1);
                        correct = reader.GetInt32(2) == 1 ? true : false;
                        date_answer = reader.GetDateTime(3);
                        account_id = reader.GetInt32(4);

                        tempAnswer = new Answer() { Id = answer_id, Answer_descr = answer, Correct = correct, Date = date_answer, Account = new Account() { Id = account_id } };
                        listAnswer.Add(tempAnswer);
                    }

            mysql.closeMysqlConnection();


            return listAnswer;
        }

    }
}
