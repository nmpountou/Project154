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
            {
                cmd.ExecuteNonQuery();
            }


        }

        public List<Answer> findAnswer(Account account, Question question)
        {
            throw new NotImplementedException();
        }

        public void saveAnswer(Answer answer,int question_id)
        {

            string formatForMySql = answer.Date.ToString("yyyy-MM-dd HH:mm");

            string query = "insert into answer(answer,create_date,correct,account_id) values('" + answer.Answer_descr + "','" + formatForMySql + "'," + (answer.Correct?1:0) + "," + answer.Account.Id + ")";

            MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
             CurrentUserInfo.PASSWORD,
             CurrentUserInfo.HOSTNAME,
             CurrentUserInfo.PORT,
             CurrentUserInfo.DATABASE);

            mysql.initializeConnection();
            mysql.openMysqlConnection();

            long new_answer_id;

            using (MySqlCommand cmd = new MySqlCommand(query,mysql.MysqlConnection))
            {
                cmd.ExecuteNonQuery();
                new_answer_id = cmd.LastInsertedId;

            }
            query = "insert into question_answer(question_id,answer_id) values("+question_id+","+ new_answer_id + ")";

            logger.Info("Answer iD " + new_answer_id);
            logger.Info("Question ID " + question_id);

            using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
            {
                cmd.ExecuteNonQuery();

            }
            answer.Id = (int)new_answer_id;

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

            string query = "update answer set answer='"+answer.Answer_descr+"',create_date='"+ formatForMySql + "',correct="+(answer.Correct?1:0)+",account_id="+answer.Account.Id+" where id="+answer.Id+"";

            using (MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection))
            {
                cmd.ExecuteNonQuery();

            }

            mysql.closeMysqlConnection();

        }
    }
}
