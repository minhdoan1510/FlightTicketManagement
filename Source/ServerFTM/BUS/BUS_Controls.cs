using ServerFTM.DAL.Controls;
using ServerFTM.DTO;
using System;
using System.Data;
using System.Linq;
using System.Text;

namespace ServerFTM.BUS
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
            accountCurrent.Acctype = (TypeAccount)(Convert.ToInt32(dataAcc["TypeAccount"]));

            //accountCurrent.Acctype = Convert.ToInt32(dataAcc.Rows[0].ItemArray[1]);
            //accountCurrent.Name = dataAcc.Rows[0].ItemArray[2].ToString();
            return true;
        }
        public bool Signup(Account account)
        {
            account.IDAccount = GenerateID();
            account.Acctype = TypeAccount.Staff;
            return DAL_Controls.Controls.SignUp(account);
        }

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
    }
}
