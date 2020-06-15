using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerFTM.DTO
{
    class Profile
    {

        private string iDAccount, name;
        TypeAccount acctype;
        public string IDAccount { get => iDAccount; set => iDAccount = value; }
        public string Name { get => name; set => name = value; }
        public TypeAccount Acctype { get => acctype; set => acctype = value; }
        public Profile(string _iDAccount, string _name, TypeAccount _acctype)
        {
            IDAccount = _iDAccount;
            Name = _name;
            Acctype = _acctype;
        }
        public Profile()
        { }
    }
}
