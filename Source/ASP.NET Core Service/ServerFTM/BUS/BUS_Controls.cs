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
using ServerFTM.DAL.Helper;
using Microsoft.AspNetCore.Mvc.Filters;

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
            ResponseDTB response = DAL_Controls.Controls.Login(account);
            if (!response.IsSuccess)
                return null;
            DataRow dataAcc = response.Result?.Rows[0];

            if (dataAcc == null)
                return null;
            Profile accountCurrent = new Profile();
            accountCurrent.IDAccount = dataAcc["IDAccount"].ToString();
            accountCurrent.Name = dataAcc["Name"].ToString();
            //accountCurrent.Acctype = (Convert.ToInt32(dataAcc["TypeAccount"]));
            return accountCurrent;
        }

        internal KeyValuePair<int, int> GetDefineChairFlight(string id)
        {
            ResponseDTB response = DAL_Controls.Controls.GetDefineChairFlight(id);
            if (!response.IsSuccess)
                return default;
            DataRow data = response.Result?.Rows[0];

            if (data == null)
                return default;
            KeyValuePair<int, int> result = new KeyValuePair<int, int>(Convert.ToInt32(data["Width"]), Convert.ToInt32(data["Height"]));
            return result;
        }

        public bool Signup(Account account)
        {
            account.IDAccount = GenerateID();
            return DAL_Controls.Controls.SignUp(account).IsSuccess;
        }
        #endregion

        #region Report_Handle
        
        public List<ChildMonthReport> GetMonthReports(int mouth, int year)
        {
            ResponseDTB response = DAL_Controls.Controls.GetMonthProfit(mouth, year);
            if (!response.IsSuccess)
                return default;
            DataTable data = response.Result;
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
            ResponseDTB response = DAL_Controls.Controls.GetYearProfit(year);
            if (!response.IsSuccess)
                return default;
            DataTable data = response.Result;
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

        #region City_Handle

        public List<City> GetCityAlready(string idlocal)
        {
            ResponseDTB response = DAL_Controls.Controls.GetCityAlready(idlocal);
            if (!response.IsSuccess)
                return default;
            DataTable data = response.Result;
            List<City> reports = new List<City>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                reports.Add(new City()
                {
                    IDCity = data.Rows[i]["IDCity"].ToString(),
                    CityName = data.Rows[i]["CityName"].ToString()
                });
            }
            return reports;
        }

        #endregion

        #region Flight_Handle

        public List<DurationTime> GetDurationFlight(string idoriap, string iddestap)
        {
            ResponseDTB response = DAL_Controls.Controls.GetDurationFlight(idoriap, iddestap);
            if (!response.IsSuccess)
                return default;
            DataTable data = response.Result;
            List<DurationTime> result = new List<DurationTime>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                result.Add(new DurationTime()
                {
                    IDDurationTime = data.Rows[i]["IDDuration"].ToString(),
                    DurTime = data.Rows[i]["Duration"].ToString()
                }) ;
            }
            return result;
        }
        #endregion

        #region Airport_Handle


        public List<Airport> GetAPinCity(string idcity, string idairport)
        {
            ResponseDTB response = DAL_Controls.Controls.GetAPinCity(idairport, idcity);
            if (!response.IsSuccess)
                return default;
            DataTable data = response.Result;
            List<Airport> reports = new List<Airport>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                reports.Add(new Airport()
                {
                    IDAirport = data.Rows[i]["IDAirport"].ToString(),
                    AirportName = data.Rows[i]["AirportName"].ToString()
                });
            }
            return reports;
        }

        #endregion

        #region Ticket_Handle
        public bool AddTicket(Ticket ticket)
        {
            return DAL.Controls.DAL_Controls.Controls.AddTicket(ticket).IsSuccess;
        }

        public int GetPrice(string iddur, string idclass)
        {
            return Convert.ToInt32(DAL_Controls.Controls.GetPrice(iddur, idclass).Result.Rows[0]["Price"]);
        }

        public List<ChairBooking> GetListChair(string id, DateTime timeDur)
        {
            ResponseDTB response = DAL_Controls.Controls.GetListChair(id, timeDur);
            if (!response.IsSuccess)
                return default;
            DataTable data = response.Result;
            List<ChairBooking> result = new List<ChairBooking>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                result.Add(new ChairBooking()
                {
                    IDchair = Convert.ToChar(Convert.ToInt32(data.Rows[i]["XPos"]) + 65).ToString() + Convert.ToInt32(data.Rows[i]["YPos"]).ToString(),
                    Status = ChairStatus.Booked
                });
            }
            return result;
        }

        #endregion

        #region Passenger_Handle
        public Passenger GetExistPassenger(string tel)
        {
            ResponseDTB responseDTB =  DAL.Controls.DAL_Controls.Controls.GetExistPassenger(tel);
            if (responseDTB.IsSuccess)
            {
                Passenger passenger = new Passenger()
                {
                    IDPassenger = responseDTB.Result.Rows[0]["IDPassenger"].ToString(),
                    IDCard = responseDTB.Result.Rows[0]["IDCard"].ToString(),
                    Tel = responseDTB.Result.Rows[0]["TEL"].ToString(),
                    PassengerName = responseDTB.Result.Rows[0]["NAME"].ToString(),
                };
                return passenger;
            }
            else
            {
                return null;
            }    
        }


        public bool GetAddPassenger(Passenger passenger)
        {
            ResponseDTB responseDTB = DAL.Controls.DAL_Controls.Controls.GetAddPassenger(passenger);
            if (responseDTB.IsSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
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
