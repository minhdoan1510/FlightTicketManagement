using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightTicketManagement.Helper;
using Library.Models;

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
        public async Task<Response<InfoLogin>> Login(string username, string password)
        {
            UserAccount user = new UserAccount();
            user.Username = username;
            user.Password = password;

            Response<InfoLogin> result = await APIHelper.Instance.Post<Response<InfoLogin>>
                (ApiRoutes.Account.LogIn, user);
            AuthenticatedUser.Instance.data = result.Result;
            return result;
        }

        public async Task<Response<string>> Signup(object account)
        {
            return await APIHelper.Instance.Post<Response<string>>(ApiRoutes.Account.SignUp,
                account);
        }

        public async Task<Response<string>> CreateFlight(FlightCreateModel flight)
        {
            return await APIHelper.Instance.Post<Response<string>>
                (ApiRoutes.Flight.CreateFlight, flight);
        }

        public async Task<Response<string>> CreateTransit(TransitCreateModel transit)
        {
            return await APIHelper.Instance.Post<Response<string>>
                (ApiRoutes.Flight.CreateTransit, transit);
        }

        public async Task<Response<List<TransitCreateModel>>> GetTransit(string _flightID)
        {

            string apiURL = ApiRoutes.Flight.GetTransit;
            apiURL = apiURL.Replace("{id}", _flightID);

            return await APIHelper.Instance.Get<Response<List<TransitCreateModel>>>(apiURL);
        }

        public async Task<Response<List<AirportMenu>>> GetAirportMenu(string searchKey)
        {
            string apiURL = ApiRoutes.Flight.GetAirportMenu;
            apiURL = apiURL.Replace("{search}", searchKey);

            Console.WriteLine(apiURL);

            return await APIHelper.Instance.Get<Response<List<AirportMenu>>>(apiURL);
        }

        public async Task<Response<List<FlightCreateModel>>> GetFlightAll()
        {
            return await APIHelper.Instance.Get<Response<List<FlightCreateModel>>>
                (ApiRoutes.Flight.GetFlightAll);
        }

        public async Task<Response<string>> UpdateFlight(FlightCreateModel value)
        {
            return await APIHelper.Instance.Post<Response<string>>
                (ApiRoutes.Flight.UpdateFlight, value);
        }

        public async Task<Response<string>> DisableFlight(FlightCreateModel value)
        {
            return await APIHelper.Instance.Post<Response<string>>
                (ApiRoutes.Flight.DisableFlight, value);
        }

        public async Task<Response<string>> UpdateTransit(TransitCreateModel value)
        {
            return await APIHelper.Instance.Post<Response<string>>
                (ApiRoutes.Flight.UpdateTransit, value);
        }

        public async Task<Response<string>> DisableTransit(TransitCreateModel value)
        {
            return await APIHelper.Instance.Post<Response<string>>
                (ApiRoutes.Flight.DisableTransit, value);
        }

        public async Task<Response<string>> DisableFlightTransit(FlightCreateModel value)
        {
            return await APIHelper.Instance.Post<Response<string>>
                (ApiRoutes.Flight.DisableFlightTransit, value);
        }

        public async Task<Response<string>> DisableFlightAll()
        {
            return await APIHelper.Instance.Post<Response<string>>
                (ApiRoutes.Flight.DisableFlightAll);
        }
    }
    //public async Task<InfoLogin> Login(string username, string password) {
    //    UserAccount user = new UserAccount();
    //    user.Username = username;
    //    user.Password = password;

    //    return await APIHelper<InfoLogin>.Instance.Post(ApiRoutes.Account.LogIn,
    //        user);
    //}

    //public async Task<Signup> Signup(object account) {
    //    return await APIHelper<Signup>.Instance.Post(ApiRoutes.Account.SignUp,
    //        account);
    //}
}

