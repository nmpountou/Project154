using Sunrise.MultipleChoice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quastionnaire.Model
{
    class Question
    {

        private int id;
        private string question;
        private int level;
        private DateTime date;
        private Account account;
        private Answer answer;
        private Department department;
        private Subject subject;


        public int Id { get; set; }
        public string Question_des { get; set; }
        public int Level { get; set; }
        public DateTime Date { get; set; }
        public Account Account { get; set; }
        public Answer Answer { get; set; }
        public Department Department { get; set; }
        public Subject Subject { get; set; }


    }
}
