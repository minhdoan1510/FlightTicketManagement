using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FlightTicketManagement.DAL.Controls;
using FlightTicketManagement.DTO;

namespace FlightTicketManagement.BUS
{
    class BUS_Controls
    {
        #region Propertion
        private static BUS_Controls controls;
        public static BUS_Controls Controls
        {
            get
            {
                if (controls == null)
                    controls = new BUS_Controls();
                return controls;
            }
            set => controls = value;
        }

        private Profile accountCurrent;
        internal Profile AccountCurrent { get => accountCurrent; set => accountCurrent = value; }
        #endregion
        public bool Login(Account account)
        {
            DataRow dataAcc = DAL_Controls.Controls.Login(account)?.Rows[0];

            if (dataAcc == null)
                return false;
            accountCurrent = new Profile();
            accountCurrent.IDAccount = dataAcc["ID"].ToString();
            accountCurrent.Name = dataAcc["Name"].ToString();
            accountCurrent.Acctype = Convert.ToInt32(dataAcc["TypeAccount"]);

           //accountCurrent.Acctype = Convert.ToInt32(dataAcc.Rows[0].ItemArray[1]);
            //accountCurrent.Name = dataAcc.Rows[0].ItemArray[2].ToString();
            return true;
        }
        public bool Signup(Account account)
        {
            return DAL_Controls.Controls.SignUp(account);
        }
    }
}
