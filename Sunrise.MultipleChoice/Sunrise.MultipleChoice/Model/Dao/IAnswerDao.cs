using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quastionnaire.Model.Dao
{
    interface IAnswerDao
    {
        List<Answer> findAnswer(Account account, Question question);
        void saveAnswer(Answer answer);
        void deleteAnswer(Answer answer);
        void updateAnswer(Answer answer);
        
    }
}
