using Sunrise.MultipleChoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quastionnaire.Model.Dao.Interface
{
    interface IQuestionaireDao
    {
        List<Questionaire> findQuestionare(string username, DateTime date, QuestionForm.UsernameType usernameType, QuestionForm.DateType dateType);
        void saveQuestionaire(Questionaire questionaire);
        void deleteQuestionaire(Questionaire questionaire);
        void updateQuestionaire(Questionaire questionaire);
        void addQuestionToQuestionaire(Questionaire questionaire, Question question);
        void removeQuestionFromQuestionaire(Questionaire questionaire, Question question);


    }
}
