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

        private int _id;
        private string _question_descr;
        private int _level;
        private DateTime _date;
        private Account _account;
        private Answer _answer;
        private Department _department;
        private Subject _subject;


        public int Id { get; set; }
        public string Question_descr { get; set; }
        public int Level { get; set; }
        public DateTime Date { get; set; }
        public Account Account { get; set; }
        public Answer Answer { get; set; }
        public Department Department { get; set; }
        public Subject Subject { get; set; }


    }
}
