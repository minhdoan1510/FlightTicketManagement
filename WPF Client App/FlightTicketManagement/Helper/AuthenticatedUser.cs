using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManagement.Helper
{
    public class AuthenticatedUser
    {
        private static AuthenticatedUser instance = null;
        private InfoLogin userInfo;

        public static AuthenticatedUser Instance
        {
            get
            {
                if (instance == null)
                    instance = new AuthenticatedUser();
                return instance;
            }
        }

        public InfoLogin data
        {
            get { return this.userInfo; }
            set { this.userInfo = value; }
        }
    }
}
