using Sunrise.MultipleChoice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quastionnaire.Model
{
    public class Question
    {

        public int _id;
        private string _question_descr;
        private int _level;
        private DateTime _date;
        private Account _account;
        private Department _department;
        private Subject _subject;
        private List<Answer> _answerList = new List<Answer>();
        private bool _hasAnswers = false;

        public int Id { get; set; }
        public string Question_descr { get; set; }
        public int Level { get; set; }
        public DateTime Date { get; set; }
        public Account Account { get; set; }
        public Department Department { get; set; }
        public Subject Subject { get; set; }
        public List<Answer> AnswerList { get; set; }
        public bool HasAnswers { get; set; }


    }
}
