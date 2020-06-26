using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerFTM.Models
{
    class Profile
    {
        public string IDAccount { get; set ; }
        public string Name { get; set; }
        public Profile(string _iDAccount, string _name)
        {
            IDAccount = _iDAccount;
            Name = _name;
        }
        public Profile()
        { }
    }
}
