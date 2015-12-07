using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quastionnaire.Model
{
    class Questionaire
    {
        private int _id;
        private string _questionaire_descr;
        private Account _account;
        private DateTime _date;

        public int id { get; set; }
        public string Questionaire_descr { get; set; }
        public Account Account { get; set; }
        public DateTime Date { get; set; }



    }
}
