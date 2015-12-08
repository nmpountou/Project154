using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise.MultipleChoice.Model.Dao
{
    interface ISubjectDao
    {

        List<Subject> findSubject();
        void saveSubject(Subject subject);
        void deleteSubject(Subject subject);
        void updateSubject(Subject subject);
    }
}
