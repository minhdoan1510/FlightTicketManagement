using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFTM.Models
{
    public class InfoLogin
    {
        public string Name { get; set; }
        public string Token { get; set; }
        public string ID { get; set; }

        public InfoLogin() { }

        public InfoLogin(string iDAccount, string name, string token)
        {
            this.ID = iDAccount;
            this.Name = name;
            this.Token = token;
        }
    }
}
