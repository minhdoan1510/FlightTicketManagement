using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFTM.DTO
{
    public class InfoLogin
    {
        private string id;
        private string token;
        private object iDAccount;
        private string name;

        public string Token { get => token; set => token = value; }
        public string Id { get => id; set => id = value; }

        public InfoLogin() { }

        public InfoLogin(object iDAccount, string name, string token)
        {
            this.iDAccount = iDAccount;
            this.name = name;
            this.token = token;
        }
    }
}
