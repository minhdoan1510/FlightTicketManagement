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
            public const string GetAll = Base + "/flights";

            public const string Get = Base + "/flights" + Key;

            public const string Update = Base + "/flights" + Key;

            public const string Delete = Base + "/flights" + Key;

            public const string Create = Base + "/flights";
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
            public const string GetAll = Base + "/passengers";

            public const string Get = Base + "/passengers" + Key;

            public const string Update = Base + "/passengers" + Key;

            public const string Delete = Base + "/passengers" + Key;

            public const string Create = Base + "/passengers";
        }
        public static class Ticket

        {
            public const string GetAll = Base + "/tickets";

            public const string Get = Base + "/tickets" + Key;

            public const string Update = Base + "/tickets" + Key;

            public const string Delete = Base + "/tickets" + Key;

            public const string Create = Base + "/tickets";
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

            public const string Get = Base + "/cities" + Key;

            public const string Update = Base + "/cities" + Key;

            public const string Delete = Base + "/cities" + Key;

            public const string Create = Base + "/cities";
        }
    }
}
