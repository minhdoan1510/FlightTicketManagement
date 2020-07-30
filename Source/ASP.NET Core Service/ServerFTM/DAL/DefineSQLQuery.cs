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

        public string ProcSignUp = "EXECUTE ProcSignup @id , @user , @pass , @name , @acctype";
        public string ProcLogin = "EXECUTE ProcLogin @user , @pass";
        public string ProcMonthProfit = "EXECUTE GetMonthProfit @month , @year";
        public string ProcYearProfit = "EXECUTE GetYearProfit @year";
        public string ProcAddTicket = "EXECUTE ProcAddTicket @idTicket , @idPassenger , @iDDurationFlight , @classId , @timeflight , @timebooking , @isPaid  , @idChairBooked , @xchair , @ychair";
        public string ProcAddPassenger = "EXECUTE ProcAddPassenger @IDPassenger , @PassengerName , @PassengerIDCard , @PassenserTel";
        public string ProcAPinCity = "EXECUTE ProcAPinCity @idlocal , @idcity";
        public string ProcCityAlready = "EXECUTE ProcCityAlready @idlocal";
        public string ProcGetDurationTime = "EXECUTE ProcGetDurationTime @idoriap , @iddestap";
        public string ProcGetExistPassenger = "EXECUTE GetInfoPassenger @tel";
        public string ProcGetPriceFight = "EXECUTE ProcGetPriceFight @IDDurationFlight , @ClassId";
        public string ProcGetListChair = "EXECUTE ProcGetListChair @id , @timedur ";
        public string ProcGetDefineChairFlight = "EXECUTE ProcGetDefineChairFlight @id";
    }
}
