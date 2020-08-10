using System;
using System.CodeDom;
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

        public static class Passenger
        {
            public const string GetExist = Base + "/Passenger/GetExist";
            public const string Add = Base + "/Passenger/AddPassenger";
            public const string GetAll = Base + "/passengers";
            public const string Get = Base + "/passengers" + Key;
            public const string Update = Base + "/passengers" + Key;
            public const string Delete = Base + "/passengers" + Key;
            public const string Create = Base + "/passengers";
        }
        public static class Ticket
        {
            public const string AddTicket = Base + "/Ticket/AddTicket";
            public const string AddPassenger = Base + "/Ticket/AddPassenger";
            public const string GetPrice = Base + "/Ticket/GetPrice/durationId={durationId}/classId={classId}";
            public const string GetFlightInfo = Base + "/Ticket/GetFlightInfo/flightId={id}";
            public const string GetChairState = Base + "/Ticket/GetChairState";
        }

        public static class Report
        {
            public const string GetMonthReport = Base + "/Report/month";
            public const string GetYearReport = Base + "/Report/year";
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
            public const string GetDashStatistic = Base + "/Flight/getDashStatistic";
            public const string GetFlightRouteAll = Base + "/Flight/getFlightRoute";

            public const string GetFlightRoute = Base + "/Flight/getFlightRoute" + Key;


            public const string GetDurationTime = Base + "/flight/durationtime";
            public const string GetDefineChairFlight = Base + "/flight/DefineChairFlight";
        }

        public static class Transit
        {
            public const string Get = Base + "/Transit" + Key;
        }

        public static class Airport
        {
            public const string GetAll = Base + "/airports";
            public const string GetAPAlready = Base + "/Airport";
            public const string Get = Base + "/airports" + Key;
            public const string Update = Base + "/airports" + Key;
            public const string Delete = Base + "/airports" + Key;
            public const string Create = Base + "/airports";
        }

        public static class City
        {
            public const string GetCityAlready = Base + "/City";
            public const string GetAll = Base + "/City";
            public const string Get = Base + "/cities" + Key;
            public const string Update = Base + "/cities" + Key;
            public const string Delete = Base + "/cities" + Key;
            public const string Create = Base + "/cities";
        }
    }
}
