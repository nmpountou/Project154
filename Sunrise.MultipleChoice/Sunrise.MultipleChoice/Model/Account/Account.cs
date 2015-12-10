using Sunrise.MultipleChoice.Model.Account;

namespace Quastionnaire.Model
{
    internal class Account
    {

        private int _id;
        private string _username;
        private string _password;
        private UserInformation _userInformation;

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserInformation UserInformation { get; set; }


    }
}