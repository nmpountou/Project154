using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quastionnaire.Model.Dao
{
    interface IQuestionaireDao
    {

        void saveQuestionaire(Questionaire question);
        void deleteQuestionaire(Questionaire question);
        void deleteQuestionaire(int question_id);
        void updateQuestionaire(Questionaire question);
        void updateQuestionaire(int question_id);


    }
}
