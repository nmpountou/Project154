using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise.MultipleChoice.Model.Dao.Interface
{
    interface IDepartmentDao
    {

        List<Department> findDepartment(Subject subject);
        void saveDepartment(Department department);
        void deleteDepartment(Department department);
        void updateDepartment(Department department);


    }
}
