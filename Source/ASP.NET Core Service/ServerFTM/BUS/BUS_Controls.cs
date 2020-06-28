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

        public bool CheckDevice(string token)
        {
            string id = TokenManager.Instance.GetIDAccountToken(token);
            //Check quyen truy cap

            return !string.IsNullOrEmpty(id);
        }
        #endregion

        #region Account_Handle
        public Profile Login(Account account)
        {
            DataRow dataAcc = (DAL_Controls.Controls.Login(account))?.Rows[0];

            if (dataAcc == null)
                return null;
            Profile accountCurrent = new Profile();
            accountCurrent.IDAccount = dataAcc["IDAccount"].ToString();
            accountCurrent.Name = dataAcc["Name"].ToString();
            //accountCurrent.Acctype = (Convert.ToInt32(dataAcc["TypeAccount"]));
            return accountCurrent;
        }
        public bool Signup(Account account)
        {
            account.IDAccount = GenerateID();
            return DAL_Controls.Controls.SignUp(account);
        }
        #endregion

        #region Report_Handle
        
        public  List<ChildMonthReport> GetMonthReports(int mouth, int year)
        {
            DataTable data = DAL_Controls.Controls.GetMonthProfit(mouth, year);
            List<ChildMonthReport> reports = new List<ChildMonthReport>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                reports.Add(new ChildMonthReport()
                {
                    Rank = Convert.ToInt32(data.Rows[i]["Rank"]),
                    IdFight = data.Rows[i]["IDFlight"].ToString(),
                    OriginName = data.Rows[i]["OriginName"].ToString(),
                    DestinationName = data.Rows[i]["DestName"].ToString(),
                    OriginID = data.Rows[i]["OriginID"].ToString(),
                    DestinationID = data.Rows[i]["DestID"].ToString(),
                    TicketNum = Convert.ToInt32(data.Rows[i]["TicketNum"]),
                    Ratio = float.Parse(data.Rows[i]["Ratio"].ToString()),
                    Profit = Convert.ToInt32(data.Rows[i]["Profit"])
                });
            }
            return reports;
        }

        public  List<ChildYearReport> GetYearReports(int year)
        {
            DataTable data = DAL_Controls.Controls.GetYearProfit(year);
            List<ChildYearReport> reports = new List<ChildYearReport>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                reports.Add(new ChildYearReport()
                {
                    Month  = Convert.ToInt32(data.Rows[i]["Month"]),
                    TicketNum = Convert.ToInt32(data.Rows[i]["TicketNum"]),
                    Ratio = float.Parse(data.Rows[i]["Ratio"].ToString()),
                    Profit = Convert.ToInt32(data.Rows[i]["Profit"])
                });
            }
            return reports;
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
