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
        public static class Flight
        {
            public const string GetAll = Base + "/Flight";

            public const string Get = Base + "/flights" + Key;

            public const string Update = Base + "/flights" + Key;

            public const string Delete = Base + "/flights" + Key;

            public const string Create = Base + "/flights";

            public const string GetDurationTime = Base + "/flight/durationtime";
            public const string GetDefineChairFlight = Base + "/flight/DefineChairFlight";
        }
        public static class Class

        {
            public const string GetAll = Base + "/classes";

            public const string Get = Base + "/classes" + Key;

            public const string Update = Base + "/classes" + Key;

            public const string Delete = Base + "/classes" + Key;

            public const string Create = Base + "/classes";
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
            
            public const string GetPrice = Base + "/Ticket/getprice";

            public const string Add = Base + "/Ticket/add";

            public const string GetAll = Base + "/tickets";

            public const string Get = Base + "/tickets" + Key;

            public const string Update = Base + "/tickets" + Key;

            public const string Delete = Base + "/tickets" + Key;

            public const string Create = Base + "/tickets";

            public const string LoadStateChair = Base + "/Ticket/LoadStateChair";
        }
        public static class Transit

        {
            public const string GetAll = Base + "/transits";

            public const string Get = Base + "/transits" + Key;

            public const string Update = Base + "/transits" + Key;

            public const string Delete = Base + "/transits" + Key;

            public const string Create = Base + "/transits";
        }
        public static class City

        {
            public const string GetAll = Base + "/cities";

            public const string GetCityAlready = Base + "/City";

            public const string Get = Base + "/cities" + Key;

            public const string Update = Base + "/cities" + Key;

            public const string Delete = Base + "/cities" + Key;

            public const string Create = Base + "/cities";
        }

        public static class Report
        {
            public const string GetMonthReport = Base + "/Report/month";
            public const string GetYearReport = Base + "/Report/year";
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

    }
}
