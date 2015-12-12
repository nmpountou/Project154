using Sunrise.MultipleChoice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quastionnaire.Model.Dao.Interface
{
    interface IQuestionDao
    {
        List<Question> findQuestion(Account account,Subject subject,Department department);
        List<Question> findQuestion(Subject subject, Department department);
        void saveQuestion(Question question);
        void deleteQuestion(Question question);
        void updateQuestion(Question question);


    }
}
