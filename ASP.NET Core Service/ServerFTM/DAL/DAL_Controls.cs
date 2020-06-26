using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerFTM.Models;
using ServerFTM.DAL.Query;
using ServerFTM.DAL.DataProvider;
using System.Data;

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

        public bool CreateFlight(PostFlight flight) {
            try {
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.Query.ProcCreateFlight,
                    new object[] {
                        flight.flightID,
                        flight.durationFlightID,
                        flight.originAP,
                        flight.destinationAP,
                        flight.totalSeat,
                        flight.price,
                        flight.width,
                        flight.height,
                        flight.duration}) > 0;
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

        public DataTable testTime() {
            try {
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcTestTime);
            }
            catch (Exception e) {
                return null;
            }
        }
    }
}
