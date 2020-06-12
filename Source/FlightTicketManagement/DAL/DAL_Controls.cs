using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightTicketManagement.DTO;
using FlightTicketManagement.DAL.Query;
using FlightTicketManagement.DAL.DataProvider;
using System.Data;

namespace FlightTicketManagement.DAL.Controls
{
    class DAL_Controls
    {
        private static DAL_Controls controls;
        private DataProvider.DataProvider mDataProvider;
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
        public bool SignUp(Account account)
        {
            try
            {
                return DataProvider.DataProvider.Instance.ExecuteNonQuery(DefineSQLQuery.Query.ProcSignUp, 
                    new object[] { 
                        account.IDAccount, 
                        account.Username , 
                        account.Password, 
                        account.Name, 
                        account.Acctype })>0; //--@id   @user @pass @name @acctype
            }
            catch
            {
                return false;
            }
        }

        public DataTable Login(Account account)
        {
            try
            {
                return DataProvider.DataProvider.Instance.ExecuteQuery(DefineSQLQuery.Query.ProcLogin, 
                    new object[] {
                        account.Username,
                        account.Password }); //--@id   @user @pass @name @acctype
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
