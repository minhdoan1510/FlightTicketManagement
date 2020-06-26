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
        public static DefineSQLQuery Query
        {
            get
            {
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
        public string ProcTestTime = "exec testTimeMap"; 
    }
}
