using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerFTM.DTO
{
    enum TypeAccount { Admin, Staff}
    class Account
    {
        private string iDAccount, username, password, name;
        private TypeAccount acctype;
        public string IDAccount { get => iDAccount; set => iDAccount = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Name { get => name; set => name = value; }
        public TypeAccount Acctype { get => acctype; set => acctype = value; }
        public Account() { }
        public Account(string _iDAccount, string  _username, string _password, string _name, TypeAccount _acctype)
        {
            IDAccount = _iDAccount;
            Username = _username;
            Password = _password;
            Name = _name;
            Acctype = _acctype;
        }
        public Account(string _username, string _password, string _name, TypeAccount _acctype)
        {
            Username = _username;
            Password = _password;
            Name = _name;
            Acctype = _acctype;
        }
        public Account(string _username, string _password)
        {
            Username = _username;
            Password = _password;
        }
    }
}
