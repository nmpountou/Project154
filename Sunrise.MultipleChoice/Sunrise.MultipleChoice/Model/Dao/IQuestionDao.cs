using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quastionnaire.Model.Dao
{
    interface IQuestionDao
    {

        void saveQuestion(Question question);
        void deleteQuestion(Question question);
        void deleteQuestion(int question_id);
        void updateQuestion(Question question);
        void updateQuestion(int question_id);


    }
}
