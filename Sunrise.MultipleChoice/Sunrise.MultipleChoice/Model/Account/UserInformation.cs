using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise.MultipleChoice.Model.Account
{
    public class UserInformation
    {
        private int _id;
        private string _name;
        private string _lastname;
        private string _email;
        private City _city;
        private Country _country;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public City City { get; set; }
        public Country Country { get; set; }


    }
}
