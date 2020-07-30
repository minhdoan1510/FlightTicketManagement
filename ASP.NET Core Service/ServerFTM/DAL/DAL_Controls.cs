using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerFTM.DAL.Query;
using ServerFTM.DAL.DataProvider;
using System.Data;
using System.Reflection;
using Library.Models;
using ServerFTM.Models;

namespace ServerFTM.DAL.Controls
{
    class DAL_Controls
    {
        private static DAL_Controls controls;
        public static DAL_Controls Controls
        {
            get
            {
                if (controls == null)
                    controls = new DAL_Controls();
                return controls;
            }
            set => controls = value;
        }

        public bool SignUp(Account account) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.ProcSignUp,
                    new object[] {
                        account.IDAccount,
                        account.Username ,
                        account.Password,
                        account.Name }) > 0; //--@id   @user @pass @name @acctype
            }
            catch {
                return false;
            }
        }

        public DataTable Login(Account account) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.ProcLogin,
                    new object[] {
                        account.Username,
                        account.Password }); //--@id   @user @pass @name @acctype
            }
            catch (Exception e) {
                return null;
            }
        }

        public DataTable GetAllFlight()
        {
            try
            {
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.ProcGetAllFlight);
                    
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<T> CreateListFromTable<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row => {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            pro.SetValue(objT, row[pro.Name]);
                        }
                        catch (Exception ex) { }
                    }
                }
                return objT;
            }).ToList();
        }

        internal DataTable GetFlightForCity(string cityId)
        {
            try
            {
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.ProcGetFlightForCity,
                    new object[] { cityId});

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DataTable GetAllCity()
        {
            try
            {
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.ProcGetCity);

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DataTable GetTransits(string transitId)
        {

            try
            {
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.ProcGetTransits,
                    new object[] { transitId });
                      
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DataTable GetRestrictions()
        {
            try
            {
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.ProcGetRestrictions);
                    

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool ChangeRestrictions(RestrictionsModel model)
        {
            try
            {
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.ProcChangeRestrictions,
                    new object[] {  model.MinFlightTime,
                                    model.MaxTransit,
                                    model.MinTransitTime,
                                    model.MaxTransitTime,
                                    model.LatestBookingTime,
                                    model.LatestCancelingTime
                                     } )>0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #region flightCreate + Dashboard

        public DataTable GetAirportMenu(string searchKey) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.ProcGetAirportMenu,
                    new object[] {
                        searchKey
                    });
            }
            catch (Exception e) {
                return null;
            }
        }

        public bool CreateFlight(Flight flight) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.ProcCreateFlight,
                    new object[] {
                        flight.FlightID,
                        flight.DurationID,
                        flight.OriginApID,
                        flight.DestinationApID,
                        flight.TotalSeat,
                        flight.Price,
                        flight.Width,
                        flight.Height,
                        flight.Duration}) > 0;
            }
            catch (Exception e) {
                return false;
            }
        }

        public bool CreateTransit(Transit transit) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.ProcCreateTransit,
                    new object[] {
                        transit.transitID,
                        transit.flightID,
                        transit.airportID,
                        transit.transitOrder,
                        transit.transitTime,
                        transit.transitNote
                    }) > 0;
            }
            catch (Exception e) {
                return false;
            }
        }

        public DataTable GetTransit(string flightID) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.ProcGetTransit,
                    new object[] {
                        flightID
                });
            }
            catch (Exception e) {
                return null;
            }
        }

        public DataTable GetFlightAll() {
            try {
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.ProcGetFlightAll);
            }
            catch (Exception e) {
                return null;
            }
        }

        public bool UpdateFlight(Flight value) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.ProcUpdateFlight,
                    new object[] {
                        value.FlightID,
                        value.DurationID,
                        value.OriginApID,
                        value.DestinationApID,
                        value.Price,
                        value.Width,
                        value.Height,
                        value.TotalSeat,
                        value.Duration
                    }) > 0;
            }
            catch (Exception e) {
                return false;
            }
        }

        public bool DisableFlight(Flight value) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.ProcDisableFlight,
                    new object[] {
                        value.FlightID
                    }) > 0;
            }
            catch (Exception e) {
                return false;
            }
        }

        public bool UpdateTransit(Transit value) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.ProcUpdateTransit,
                    new object[] {
                        value.transitID,
                        value.airportID,
                        value.transitOrder,
                        value.transitTime,
                        value.transitNote
                    }) > 0;
            }
            catch (Exception e) {
                return false;
            }
        }

        public bool DisableTransit(Transit value) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.ProcDisableTransit,
                    new object[] {
                        value.transitID
                    }) > 0;
            }
            catch (Exception e) {
                return false;
            }
        }

        public bool DisableFlightTransit(Flight value) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.ProcDisableFlightTransit,
                    new object[] {
                        value.FlightID
                    }) > 0;
            }
            catch (Exception e) {
                return false;
            }
        }

        public bool DisableFlightAll() {
            try {
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.ProcDisableFlightAll) > 0;
            }
            catch (Exception e) {
                return false;
            }
        }

        public DataTable getTicketCountDaily(string date) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.ProcCountTicketDaily,
                    new object[] {
                        date
                    });
            }
            catch (Exception e) {
                return null;
            }
        }

        public DataTable getSumMoneyDaily(string date) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.ProcSumMoneyDaily,
                    new object[] {
                        date
                    });
            }
            catch (Exception e) {
                return null;
            }
        }

        public DataTable getFlightRouteAll() {
            try {
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.ProcGetFlightRouteAll);
            }
            catch (Exception e) {
                return null;
            }
        }

        public DataTable getTransitRouteFromFlight(string flightID) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.ProcGetTransitRouteFromFlight,
                    new object[] {
                        flightID
                    });
            }
            catch (Exception e) {
                return null;
            }
        }
        #endregion
    }
}
