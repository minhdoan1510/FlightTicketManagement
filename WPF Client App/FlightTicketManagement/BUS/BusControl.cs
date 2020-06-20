using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightTicketManagement.Helper;
using DTO;

namespace FlightTicketManagement.BUS
{
    public class BusControl
    {
        public static BusControl _instance = null; 

        public static BusControl Instance {
            get {
                if (_instance == null) {
                    _instance = new BusControl();
                }
                return _instance;
            }
        }

        public async Task<InfoLogin.Data> Login(string username, string password) {
            UserAccount user = new UserAccount();
            user.Username = username;
            user.Password = password;

            return await APIHelper<InfoLogin.Data>.Instance.Post(ApiRoutes.Account.LogIn,
                user);
        }

        public async Task<Signup> Signup(object account) {
            return await APIHelper<Signup>.Instance.Post(ApiRoutes.Account.SignUp,
                account);
        }
    }
}
