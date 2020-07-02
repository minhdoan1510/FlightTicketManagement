using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerFTM.Models;
using ServerFTM.DAL.Query;
using ServerFTM.DAL.DataProvider;
using System.Data;
using Microsoft.OpenApi.Models;

namespace ServerFTM.DAL.Controls
{
    class DAL_Controls
    {
        private static DAL_Controls controls;
        public static DAL_Controls Controls {
            get {
                if (controls == null)
                    controls = new DAL_Controls();
                return controls;
            }
            set => controls = value;
        }
        public bool SignUp(Account account) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.Query.ProcSignUp,
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
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcLogin,
                    new object[] {
                        account.Username,
                        account.Password }); //--@id   @user @pass @name @acctype
            }
            catch (Exception e) {
                return null;
            }
        }

        public DataTable GetAccount(string idAccount) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcGetAccount,
                    new object[] {
                        idAccount
                    });
            }
            catch (Exception e) {
                return null;
            }
        }

        public DataTable GetAirportMenu(string searchKey) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcGetAirportMenu,
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
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.Query.ProcCreateFlight,
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
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.Query.ProcCreateTransit,
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
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcGetTransit,
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
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcGetFlightAll);
            }
            catch (Exception e) {
                return null;
            }
        }

        public bool UpdateFlight(Flight value) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.Query.ProcUpdateFlight,
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
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.Query.ProcDisableFlight,
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
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.Query.ProcUpdateTransit,
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
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.Query.ProcDisableTransit,
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
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.Query.ProcDisableFlightTransit,
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
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.Query.ProcDisableFlightAll) > 0;
            }
            catch (Exception e) {
                return false;
            }
        }

        public DataTable getTicketCountDaily(string date) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcCountTicketDaily,
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
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcSumMoneyDaily,
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
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcGetFlightRouteAll);
            }
            catch (Exception e) {
                return null;
            }
        }

        public DataTable getTransitRouteFromFlight(string flightID) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcGetTransitRouteFromFlight,
                    new object[] {
                        flightID
                    });
            }
            catch (Exception e) {
                return null;
            }
        }
    }
}
