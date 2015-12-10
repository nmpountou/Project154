using MySql.Data.MySqlClient;
using Quastionnaire.Model;
using Sunrise.MultipleChoice.Data;
using Sunrise.MultipleChoice.Model.Dao.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise.MultipleChoice.Model.Dao.Impl
{
    class DepartmentDaoImpl : IDepartmentDao
    {

        public List<Department> findDepartment(Subject subject)
        {

            //List<Subject> subjectList = new List<Subject>();

            //String query = "select id,subject_id,department_id,department from subject_department SD inner join department S on S.id=SD.department_id " +
            //"AND SD.subject_id = 1;";

            //MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
            //    CurrentUserInfo.PASSWORD,
            //    CurrentUserInfo.HOSTNAME,
            //    CurrentUserInfo.PORT,
            //    CurrentUserInfo.DATABASE);

            //mysql.initializeConnection();
            //mysql.openMysqlConnection();

            //MySqlCommand cmd = new MySqlCommand(query, mysql.MysqlConnection);
            //MySqlDataReader dr = cmd.ExecuteReader();

            //int id;
            //string subject;

            //while (dr.Read())
            //{
            //    id = Int32.Parse(dr.GetString(0));
            //    subject = dr.GetString(1);

            //    subjectList.Add(new Subject() { Id = id, Subject_descr = subject });

            //}

            //mysql.closeMysqlConnection();


            //return subjectList;

            return null;
        }

        public void deleteDepartment(Department department)
        {
            throw new NotImplementedException();
        }


        public void saveDepartment(Department department)
        {
            throw new NotImplementedException();
        }

        public void updateDepartment(Department department)
        {
            throw new NotImplementedException();
        }
    }
}
