using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManagement.DTO
{
    class Profile
    {

        private string iDAccount, name;
        int acctype;
        public string IDAccount { get => iDAccount; set => iDAccount = value; }
        public string Name { get => name; set => name = value; }
        public int Acctype { get => acctype; set => acctype = value; }
        public Profile(string _iDAccount, string _name, int _acctype)
        {
            IDAccount = _iDAccount;
            Name = _name;
            Acctype = _acctype;
        }
        public Profile()
        { }
    }
}
