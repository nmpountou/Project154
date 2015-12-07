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
        public void deleteQuestionaire(int question_id)
        {
            String query = "select * from questionaire";

            MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
                CurrentUserInfo.PASSWORD,
                CurrentUserInfo.HOSTNAME,
                CurrentUserInfo.PORT,
                CurrentUserInfo.DATABASE);

            mysql.openMysqlConnection();

            MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection);
            MySqlDataReader dr = cmd.ExecuteReader();

            //Execute

            mysql.closeMysqlConnection();
        }

        public void deleteQuestionaire(Questionaire question)
        {
            throw new NotImplementedException();
        }

        public void saveQuestionaire(Questionaire question)
        {
            throw new NotImplementedException();
        }

        public void updateQuestionaire(int question_id)
        {
            throw new NotImplementedException();
        }

        public void updateQuestionaire(Questionaire question)
        {
            throw new NotImplementedException();
        }
    }
}
