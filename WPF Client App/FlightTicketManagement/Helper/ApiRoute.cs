using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManagement.Helper
{
    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Base = Root;

        public const string Keybase = "{id}";
        public const string Key = "/{id}";
        public static class Account
        {
            public const string LogIn = Base + "/Account/login";
            public const string SignUp = Base + "/Account/signup";

        }

        public static class Test
        {
            public const string Token = Base + "/Account/testTime";
        }
        public static class Flight
        {
            public const string GetAirportMenu = Base + "/Flight/airportMenu";
            public const string CreateFlight = Base + "/Flight/createFlight";
            public const string GetFlightAll = Base + "/Flight/getFlightAll";
            public const string CreateTransit = Base + "/Flight/createTransit";
            public const string GetTransit = Base + "/Flight/getTransit";   
        }
    }
}
