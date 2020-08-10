using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerFTM.DAL.Query
{
    class DefineSQLQuery 
    {
        public const string ProcSignUp = "EXECUTE ProcSignup @id , @user , @pass , @name";
        public const string ProcLogin = "EXECUTE ProcLogin @user , @pass";

        public const string ProcGetAllFlight = "EXECUTE GetAllFlight";
        public const string ProcGetTransits = "EXECUTE GetTransit @flightId";
        public const string ProcGetRestrictions = "EXECUTE GetRestrictions";
        public const string ProcChangeRestrictions = "EXECUTE ChangeRestrictions @MinFlightTime , @MaxTransit , @MinTransitTime , @MaxTransitTime , @LatestBookingTime , @LatestCancelingTime";
        public const string ProcMonthProfit = "EXECUTE GetMonthProfit @month , @year";
        public const string ProcYearProfit = "EXECUTE GetYearProfit @year";


        public const string ProcGetCity = "EXECUTE GetCity";
        public const string ProcGetFlightForCity = "EXECUTE GetFlightForCity @cityId";

        public const string ProcGetAccount = "EXECUTE ProcGetAccount @idaccount";

        public const string ProcGetAirportMenu = "EXECUTE ProcGetAirportMenu @key";
        public const string ProcCreateFlight = "EXECUTE ProcCreateFlight @flightID , @durationFlightID , @originAP , @destinationAP , @totalSeat , @price , @width , @height , @duration";
        public const string ProcGetFlightAll = "EXECUTE ProcGetFlightAll";
        public const string ProcCreateTransit = "EXECUTE ProcCreateTransit @transitID , @flightID , @airportID , @transitOrder , @transitTime , @transitNote";
        public const string ProcGetTransit = "EXECUTE ProcGetTransit @flightID";
        public const string ProcUpdateFlight = "EXECUTE ProcUpdateFlight @flightID , @durationID , @originApID , @destinationAPID , @price , @width , @height , @totalSeat , @duration";
        public const string ProcDisableFlight = "EXECUTE ProcDisableFlight @flightID";
        public const string ProcUpdateTransit = "EXECUTE ProcUpdateTransit @transitID , @airportID , @transitOrder , @transitTime , @transitNote";
        public const string ProcDisableTransit = "EXECUTE ProcDisableTransit @transitID";
        public const string ProcDisableFlightTransit = "EXECUTE ProcDisableFlightTransit @flightID";
        public const string ProcDisableFlightAll = "EXECUTE ProcDisableFlightAll";
        public const string ProcCountTicketDaily = "EXECUTE ProcCountTicketDaily @day";
        public const string ProcSumMoneyDaily = "EXECUTE ProcSumMoneyDaily @day";
        public const string ProcGetFlightRouteAll = "EXECUTE ProcGetFlightRouteAll";
        public const string ProcGetFlightRoute = "EXECUTE ProcGetFlightRoute @flightId";
        public const string ProcGetTransitRouteFromFlight = "EXECUTE ProcGetTransitRouteFromFlight @flightID";
        public const string ProcTestTime = "exec testTimeMap";

        public const string ProcAddTicket = "EXECUTE ProcAddTicket @idTicket , @idPassenger , @iDDurationFlight , @classId , @timeflight , @timebooking , @isPaid  , @idChairBooked , @xchair , @ychair";
        public const string ProcAddPassenger = "EXECUTE ProcAddPassenger @IDPassenger , @PassengerName , @PassengerIDCard , @PassenserTel";
        public const string ProcGetPriceFlight = "EXECUTE ProcGetPriceFlight @IDDurationFlight , @ClassId";
        public const string ProcGetDefineSizeFlight = "EXECUTE ProcGetDefineSizeFlight @IdDurationFlight";
        public const string ProcGetBookedChair = "EXECUTE ProcGetBookedChair @IdDurationFlight , @date";
        public const string ProcGetFlightInfo = "EXECUTE ProcGetFlightInfo @flightId"; 

        //public const string ProcAPinCity = "EXECUTE ProcAPinCity @idlocal , @idcity";
        //public const string ProcCityAlready = "EXECUTE ProcCityAlready @idlocal";
        //public const string ProcGetDurationTime = "EXECUTE ProcGetDurationTime @idoriap , @iddestap";
        //public const string ProcGetListChair = "EXECUTE ProcGetListChair @id , @timedur ";
        //public const string ProcGetDefineChairFlight = "EXECUTE ProcGetDefineChairFlight @id";
    }
}
