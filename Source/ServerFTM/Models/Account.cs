using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerFTM.Models
{
    public class Account
    {
        //private string iDAccount, username, password, name;
        //private TypeAccount acctype;
        public string IDAccount { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Acctype { get; set; }
        //public Account() { }
        //public Account(string _iDAccount, string  _username, string _password, string _name, int _acctype)
        //{
        //    IDAccount = _iDAccount;
        //    Username = _username;
        //    Password = _password;
        //    Name = _name;
        //    Acctype = _acctype;
        //}
        //public Account(string _username, string _password, string _name, int _acctype)
        //{
        //    Username = _username;
        //    Password = _password;
        //    Name = _name;
        //    Acctype = _acctype;
        //}
        //public Account(string _username, string _password)
        //{
        //    Username = _username;
        //    Password = _password;
        //}
    }
}
