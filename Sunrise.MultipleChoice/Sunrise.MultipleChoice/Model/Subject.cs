using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise.MultipleChoice.Model
{
    public class Subject
    {
        private int _id;
        private string _subject_descr;
        private List<Department> _depList;

        public int Id { get; set; }
        public string Subject_descr { get; set; }
        public List<Department> DepList{ get; set; }
    }
}
