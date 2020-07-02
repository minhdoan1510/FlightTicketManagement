using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerFTM.DAL.Query
{
    class DefineSQLQuery
    {
        private static DefineSQLQuery query;
        public static DefineSQLQuery Query {
            get {
                if (query == null)
                    query = new DefineSQLQuery();
                return query;
            }
            set => query = value;
        }

        public string ProcSignUp = "EXECUTE ProcSignup @id , @user , @pass , @name";
        public string ProcLogin = "EXECUTE ProcLogin @user , @pass";
        public string ProcGetAccount = "EXECUTE ProcGetAccount @idaccount";
        public string ProcGetAirportMenu = "EXECUTE ProcGetAirportMenu @key";
        public string ProcCreateFlight = "EXECUTE ProcCreateFlight @flightID , @durationFlightID , @originAP , @destinationAP , @totalSeat , @price , @width , @height , @duration";
        public string ProcGetFlightAll = "EXECUTE ProcGetFlightAll";
        public string ProcCreateTransit = "EXECUTE ProcCreateTransit @transitID , @flightID , @airportID , @transitOrder , @transitTime , @transitNote";
        public string ProcGetTransit = "EXECUTE ProcGetTransit @flightID";
        public string ProcUpdateFlight = "EXECUTE ProcUpdateFlight @flightID , @durationID , @originApID , @destinationAPID , @price , @width , @height , @totalSeat , @duration";
        public string ProcDisableFlight = "EXECUTE ProcDisableFlight @flightID";
        public string ProcUpdateTransit = "EXECUTE ProcUpdateTransit @transitID , @airportID , @transitOrder , @transitTime , @transitNote";
        public string ProcDisableTransit = "EXECUTE ProcDisableTransit @transitID";
        public string ProcDisableFlightTransit = "EXECUTE ProcDisableFlightTransit @flightID";
        public string ProcDisableFlightAll = "EXECUTE ProcDisableFlightAll";
        public string ProcCountTicketDaily = "EXECUTE ProcCountTicketDaily @day";
        public string ProcSumMoneyDaily = "EXECUTE ProcSumMoneyDaily @day";
        public string ProcGetFlightRouteAll = "EXECUTE ProcGetFlightRouteAll";
        public string ProcGetTransitRouteFromFlight = "EXECUTE ProcGetTransitRouteFromFlight @flightID"; 
        public string ProcTestTime = "exec testTimeMap";
    }
}
