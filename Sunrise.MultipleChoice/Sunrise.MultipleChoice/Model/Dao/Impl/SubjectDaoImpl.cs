using MySql.Data.MySqlClient;
using Quastionnaire.Model;
using Sunrise.MultipleChoice.Data;
using Sunrise.MultipleChoice.Model.Dao.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise.MultipleChoice.Model.Dao.Impl
{
    class SubjectDaoImpl : ISubjectDao
    {

        public List<Subject> findSubject()
        {

            List<Subject> subjectList = new List<Subject>();
            List<Department> depList = new List<Department>();

            String querySubjects = "select id,subject from subject";
            String queryDepartments;

            MysqlConnector mysql = new MysqlConnector(CurrentUserInfo.USERNAME,
                CurrentUserInfo.PASSWORD,
                CurrentUserInfo.HOSTNAME,
                CurrentUserInfo.PORT,
                CurrentUserInfo.DATABASE);

            mysql.initializeConnection();
            mysql.openMysqlConnection();

            int subject_id, department_id;
            string subject, department;

            using (MySqlCommand cmd = new MySqlCommand(querySubjects, mysql.MysqlConnection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            subject_id = Int32.Parse(reader.GetString(0));
                            subject = reader.GetString(1);

                            subjectList.Add(new Subject() { Id = subject_id, Subject_descr = subject });

                        }
                    }
                } // reader closed and disposed up here

            } // command disposed here



            foreach (Subject sub in subjectList)
            {
                queryDepartments = "select subject_id,department_id,department from subject_department SD inner join department S on S.id=SD.department_id " +
                                   "AND SD.subject_id = " + sub.Id + ";";

                using (MySqlCommand cmd = new MySqlCommand(queryDepartments, mysql.MysqlConnection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                Debug.WriteLine(reader.GetString(1));
                                department_id = reader.GetInt32(1);
                                department = reader.GetString(2);
                                depList.Add(new Department() { Department_descr = department, Id = department_id });
                            }
                            sub.DepList = depList;
                            depList = new List<Department>();
                        }
                    } // reader closed and disposed up here

                } // command disposed here

            }
               

            return subjectList;

        }

        public void deleteSubject(Subject subject)
        {
            throw new NotImplementedException();
        }



        public void saveSubject(Subject subject)
        {
            throw new NotImplementedException();
        }

        public void updateSubject(Subject subject)
        {
            throw new NotImplementedException();
        }
    }
}
