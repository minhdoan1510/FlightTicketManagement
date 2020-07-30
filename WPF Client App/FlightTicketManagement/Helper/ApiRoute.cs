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

        public static class Restriction
        {
            public const string Get = Base + "/Restrictions";
            public const string Post = Base + "/Restrictions";
        }
        public static class FlightT
        {
            public const string GetAll = Base + "/Flight";
            public const string GetFlightForCity = Base + "/Flight" + Key;


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

        public static class Transit

        {

            public const string Get = Base + "/Transit" + Key;


        }
        public static class City
        {
            public const string GetAll = Base + "/City";
        }
    }
}
