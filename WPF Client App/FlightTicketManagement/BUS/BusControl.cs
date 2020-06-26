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

        public async Task<Response<InfoLogin>> Login(string username, string password) {
            UserAccount user = new UserAccount();
            user.Username = username;
            user.Password = password;

            Response<InfoLogin> result = await APIHelper.Instance.PostInit<Response<InfoLogin>>
                (ApiRoutes.Account.LogIn, user);
            AuthenticatedUser.Instance.data = result.Result;
            return result;
        }

        public async Task<Response<string>> Signup(object account) {
            return await APIHelper.Instance.PostInit<Response<string>>(ApiRoutes.Account.SignUp,
                account);
        }

        public async Task<Response<List<AirportMenu>>> GetAirportList(string search) {
            return await APIHelper.Instance.PostWithToken<Response<List<AirportMenu>>>
                (ApiRoutes.Flight.GetAirportMenu, new { searchKey = search });
        }

        public async Task<Response<string>> CreateFlight(PostFlight flight) {
            return await APIHelper.Instance.PostWithToken<Response<string>>
                (ApiRoutes.Flight.CreateFlight, flight); 
        }

        public async Task<Response<string>> CreateTransit(Transit transit) {
            return await APIHelper.Instance.PostWithToken<Response<string>>
                (ApiRoutes.Flight.CreateTransit, transit);
        }

        public async Task<Response<List<Transit>>> GetTransit(string _flightID) {
            return await APIHelper.Instance.PostWithToken<Response<List<Transit>>>
                (ApiRoutes.Flight.GetTransit, new { flightID = _flightID });
        }

        public async Task<Response<List<Flight>>> GetFlightAll() {
            return await APIHelper.Instance.PostWithToken<Response<List<Flight>>>
                (ApiRoutes.Flight.GetFlightAll);
        }

        public async Task<Response<List<Duration>>> testTime() {
            return await APIHelper.Instance.PostWithToken<Response<List<Duration>>>
                (ApiRoutes.Test.Token); 
        }
    }
}
