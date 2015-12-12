using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quastionnaire.Model.Dao.Interface
{
    interface IAnswerDao
    {
        List<Answer> findAnswer(Account account, Question question);
        void saveAnswer(Answer answer,int question_id);
        void deleteAnswer(Answer answer);
        void updateAnswer(Answer answer);
        
    }
}
