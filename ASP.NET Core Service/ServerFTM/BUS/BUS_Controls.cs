using ServerFTM.DAL.Controls;
using ServerFTM.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerFTM.Authorization.TokenManager;

namespace ServerFTM.BUS
{
    class BUS_Controls
    {
        #region Contructor
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

        private BUS_Controls() { }
        
        #endregion

        #region DeviceManager
        public void AddDevice(string idUser,string token)
        {
            TokenManager.Instance.AddAccessToken(idUser, token);
        }
        public void DelDevice(string token)
        {
            TokenManager.Instance.DelAccessToken(token);
        }
        #endregion

        #region Account_Handle
        public Profile Login(Account account)
        {
            DataRow dataAcc = DAL_Controls.Controls.Login(account)?.Rows[0];

            if (dataAcc == null)
                return null;
            Profile accountCurrent = new Profile();
            accountCurrent.IDAccount = dataAcc["ID"].ToString();
            accountCurrent.Name = dataAcc["Name"].ToString();
            accountCurrent.Acctype = (Convert.ToInt32(dataAcc["TypeAccount"]));
            return accountCurrent;
        }
        public bool Signup(Account account)
        {
            account.IDAccount = GenerateID();
            return DAL_Controls.Controls.SignUp(account);
        }
        #endregion

        #region Utilities
        string GenerateID()
        {
            StringBuilder builder = new StringBuilder();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(10)
                .ToList().ForEach(e => builder.Append(e));
            return builder.ToString();
        }
        #endregion

    }
}
