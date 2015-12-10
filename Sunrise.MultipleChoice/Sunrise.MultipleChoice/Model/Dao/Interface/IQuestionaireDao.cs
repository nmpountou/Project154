using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quastionnaire.Model.Dao.Interface
{
    interface IQuestionaireDao
    {
        List<Questionaire> findQuestionare(Account account);
        void saveQuestionaire(Questionaire questionaire);
        void deleteQuestionaire(Questionaire questionaire);
        void updateQuestionaire(Questionaire questionaire);

    }
}
