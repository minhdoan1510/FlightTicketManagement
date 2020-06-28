using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightTicketManagement.Helper;
using Models;

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

        //public async Task<InfoLogin> Login(string username, string password) {
        //    UserAccount user = new UserAccount();
        //    user.Username = username;
        //    user.Password = password;

        //    return await APIHelper.Instance.Post<InfoLogin>(ApiRoutes.Account.LogIn,
        //        user);
        //}

        //public async Task<Signup> Signup(object account) {
        //    return await APIHelper.Instance.Post<Signup>(ApiRoutes.Account.SignUp,
        //        account);
        //}
    }
}
