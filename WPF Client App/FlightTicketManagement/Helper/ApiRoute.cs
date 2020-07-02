using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManagement.Helper
{
    public class ApiRoutes
    {
        public const string Base = "api";

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
            public const string GetTransit = Base + "/Flight/getTransit/ID={id}";
            public const string GetAirportMenu = Base + "/Flight/airportMenu/Search={search}";
            public const string CreateFlight = Base + "/Flight/createFlight";
            public const string GetFlightAll = Base + "/Flight/getFlightAll";
            public const string CreateTransit = Base + "/Flight/createTransit";
            public const string UpdateFlight = Base + "/Flight/updateFlight";
            public const string DisableFlight = Base + "/Flight/disableFlight";
            public const string UpdateTransit = Base + "/Flight/updateTransit";
            public const string DisableTransit = Base + "/Flight/disableTransit";
            public const string DisableFlightTransit = Base + "/Flight/disableFlightTransit";
            public const string DisableFlightAll = Base + "/Flight/disableFlightAll";
            public const string GetDashStatistic = Base + "/Flight/getDashStatistic/Date={date}";
            public const string GetFlightRoute = Base + "/Flight/getFlightRoute"; 
        }
    }
}
