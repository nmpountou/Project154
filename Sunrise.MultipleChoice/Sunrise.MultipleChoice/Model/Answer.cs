using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quastionnaire.Model
{
    public class Answer
    {
        private int _id;
        private string _answer_descr;
        private Account _account;
        private DateTime _date;
        private bool _correct;

        public int Id { get; set; }
        public string Answer_descr { get; set; }
        public Account Account { get; set; }
        public DateTime Date { get; set; }
        public bool Correct { get; set; }

    }
}
