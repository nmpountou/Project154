using MySql.Data.MySqlClient;
using Sunrise.MultipleChoice.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quastionnaire.Model.Dao.Impl
{
    class QuestionaireDaoImpl : IQuestionaireDao
    {
        public void deleteQuestionaire(Questionaire questionaire)
        {
            throw new NotImplementedException();
        }

        public void deleteQuestionaire(int question_id)
        {
            String query = "select * from questionaire";

            MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
                CurrentUserInfo.PASSWORD,
                CurrentUserInfo.HOSTNAME,
                CurrentUserInfo.PORT,
                CurrentUserInfo.DATABASE);

            mysql.initializeConnection();
            mysql.openMysqlConnection();

            MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection);
            MySqlDataReader dr = cmd.ExecuteReader();

            //Execute

            mysql.closeMysqlConnection();
        }

        public List<Questionaire> findQuestionare(Account account)
        {
            throw new NotImplementedException();
        }

        public void saveQuestionaire(Questionaire questionaire)
        {
            throw new NotImplementedException();
        }

        public void updateQuestionaire(Questionaire questionaire)
        {
            throw new NotImplementedException();
        }
    }
}
