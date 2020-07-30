using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerFTM.Models;
using ServerFTM.DAL.Query;
using ServerFTM.DAL.DataProvider;
using System.Data;
using ServerFTM.DAL.Helper;

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

        #region Account

        public ResponseDTB SignUp(Account account)
        {
            try
            {
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.Query.ProcSignUp,
                    new object[] {
                        account.IDAccount,
                        account.Username ,
                        account.Password,
                        account.Name,
                        account.Acctype }) ? ResponseDTBHelper.OkResultDB() : ResponseDTBHelper.FailResultDB(); //--@id   @user @pass @name @acctype
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ResponseDTBHelper.ErrorResultDB(e.Message);
            }
        }


        public ResponseDTB Login(Account account)
        {
            try
            {
                return ResponseDTBHelper.OkResultDB(DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcLogin,
                    new object[] {
                        account.Username,
                        account.Password })); //--@id   @user @pass @name @acctype
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ResponseDTBHelper.ErrorResultDB(e.Message);
            }
        }
        #endregion

        #region Ticket

        public ResponseDTB AddTicket(Ticket ticket)
        {
            try
            {
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.Query.ProcAddTicket,
                    new object[] { ticket.IDTicket,
                        ticket.IDPassenger,
                    ticket.IDDurationFlight,
                    ticket.IDClass,
                    ticket.TimeFlight,
                    ticket.TimeBooking,
                    ticket.IsPaid,
                    ticket.IDChairBooked,
                    ticket.XChair,
                    ticket.YChair}) ? ResponseDTBHelper.OkResultDB() : ResponseDTBHelper.FailResultDB(); //@idTicket , @idPassenger , @iDDurationFlight , @classId , @timeflight , @timebooking , @isPaid , @xchair , @ychair";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ResponseDTBHelper.ErrorResultDB(e.Message);
            }
        }


        public ResponseDTB GetPrice(string iddur, string idclass)
        {
            try
            {
                return ResponseDTBHelper.OkResultDB(DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcGetPriceFight,
                    new object[] { iddur,idclass }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ResponseDTBHelper.ErrorResultDB(e.Message);
            }
        }

        internal ResponseDTB GetListChair(string id, DateTime timeDur)
        {
            try
            {
                return ResponseDTBHelper.OkResultDB(DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcGetListChair,
                    new object[] { id, timeDur }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ResponseDTBHelper.ErrorResultDB(e.Message);
            }
        }


        internal ResponseDTB GetDefineChairFlight(string id)
        {
            try
            {
                return ResponseDTBHelper.OkResultDB(DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcGetDefineChairFlight,
                    new object[] { id }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ResponseDTBHelper.ErrorResultDB(e.Message);
            }

        }
        #endregion

        #region Report

        public ResponseDTB GetMonthProfit(int month, int year)
        {
            try
            {
                return ResponseDTBHelper.OkResultDB(DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcMonthProfit,
                    new object[] { month ,year }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ResponseDTBHelper.ErrorResultDB(e.Message);
            }
        }

        public ResponseDTB GetYearProfit(int year)
        {
            try
            {
                return ResponseDTBHelper.OkResultDB(DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcYearProfit,
                    new object[] { year }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ResponseDTBHelper.ErrorResultDB(e.Message);
            }
        }
        #endregion

        #region Airport

        internal ResponseDTB GetAPinCity(string idcity, object idlocal)
        {
            try
            {
                return ResponseDTBHelper.OkResultDB(DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcAPinCity,
                    new object[] {
                        idlocal, idcity}));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ResponseDTBHelper.ErrorResultDB(e.Message);
            }
        }
        #endregion

        #region City

        internal ResponseDTB GetCityAlready(string idlocal)
        {
            try
            {
                return ResponseDTBHelper.OkResultDB(DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcCityAlready,
                    new object[] {
                        idlocal }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ResponseDTBHelper.ErrorResultDB(e.Message);
            }
        }
        #endregion

        #region DurationTime

        public ResponseDTB GetDurationFlight(string idoriap, string iddestap)
        {
            try
            {
                return ResponseDTBHelper.OkResultDB(DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcGetDurationTime,
                    new object[] {
                        idoriap, iddestap }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ResponseDTBHelper.ErrorResultDB(e.Message);
            }
        }

        #endregion

        #region Passenger

        public ResponseDTB GetExistPassenger(string tel)
        {
            try
            {
                return ResponseDTBHelper.OkResultDB(DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcGetExistPassenger,
                    new object[] {
                        tel }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ResponseDTBHelper.ErrorResultDB(e.Message);
            }
        }

        public ResponseDTB GetAddPassenger(Passenger passenger)
        {
            try
            {
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.Query.ProcAddPassenger,
                    new object[] {
                        passenger.IDPassenger,
                        passenger.PassengerName,
                        passenger.IDCard,
                        passenger.Tel
                        }) ? ResponseDTBHelper.OkResultDB() : ResponseDTBHelper.FailResultDB(); //--@IDPassenger , @PassengerName , @PassengerIDCard , @PassenserTel
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ResponseDTBHelper.ErrorResultDB(e.Message);
            }
        }

        

        #endregion

    }
}
