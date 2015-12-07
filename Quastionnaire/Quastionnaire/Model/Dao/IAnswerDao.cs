using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quastionnaire.Model.Dao
{
    interface IAnswerDao
    {
        void saveAnswer(Answer question);
        void deleteAnswer(Answer question);
        void deleteAnswer(int question_id);
        void updateAnswer(Answer question);
        void updateAnswer(int question_id);

    }
}
