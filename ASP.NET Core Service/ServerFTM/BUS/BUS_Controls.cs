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

using System.Diagnostics;

using ServerFTM.Authorization;

namespace ServerFTM.BUS
{
    class BUS_Controls
    {
        #region Contructor
        private static BUS_Controls controls;
        public static BUS_Controls Controls {
            get {
                if (controls == null)
                    controls = new BUS_Controls();
                return controls;
            }
            set => controls = value;
        }

        private BUS_Controls() { }

        #endregion

        #region Account_Handle
        public Profile Login(Account account) {
            DataRow dataAcc;

            try {
                dataAcc = DAL_Controls.Controls.Login(account).Rows[0];
            }
            catch (Exception e) {
                return null;
            }

            Profile accountCurrent = new Profile();
            accountCurrent.IDAccount = dataAcc["IDAccount"].ToString();
            accountCurrent.Name = dataAcc["Name"].ToString();
            return accountCurrent;
        }
        public bool Signup(Account account) {
            account.IDAccount = GenerateID();

            return DAL_Controls.Controls.SignUp(account);
        }

        public List<AirportMenu> getAirportMenu(string searchKey) {
            List<AirportMenu> result = new List<AirportMenu>();

            DataTable airportMenu = DAL_Controls.Controls.GetAirportMenu(searchKey);

            if (airportMenu != null) {
                foreach (DataRow airport in airportMenu.Rows) {
                    AirportMenu item = new AirportMenu();

                    item.AirportID = airport["AirportID"].ToString();
                    item.AirportName = airport["AirportName"].ToString();

                    result.Add(item);
                }
            }
            return result;
        }

        public string createFlight(Flight flight) {
            flight.FlightID = GenerateID();
            flight.DurationID = GenerateID();

            if (!DAL_Controls.Controls.CreateFlight(flight)) {
                return "";
            }

            return flight.FlightID;
        }

        public bool createTransit(Transit transit) {
            transit.transitID = GenerateID();

            return DAL_Controls.Controls.CreateTransit(transit);
        }

        public List<Transit> getTransit(string flightID) {
            List<Transit> res = new List<Transit>();

            DataTable transits = DAL_Controls.Controls.GetTransit(flightID);

            if (transits != null) {
                foreach (DataRow row in transits.Rows) {
                    Transit item = new Transit();

                    item.transitID = row["transitID"].ToString();
                    item.flightID = row["flightID"].ToString();
                    item.airportID = row["airportID"].ToString();
                    item.airportName = row["airportName"].ToString();
                    item.transitOrder = int.Parse(row["transitOrder"].ToString());
                    item.transitTime = row["transitTime"].ToString();
                    item.transitNote = row["transitNote"].ToString();

                    res.Add(item);
                }
            }
            return res;
        }

        public List<Flight> getFlightAll() {
            List<Flight> result = new List<Flight>();

            DataTable flights = DAL_Controls.Controls.GetFlightAll();

            if (flights != null) {
                foreach (DataRow row in flights.Rows) {
                    Flight item = new Flight();

                    item.FlightID = row["FlightId"].ToString();
                    item.OriginApID = row["OriginApID"].ToString();
                    item.DestinationApID = row["DestinationApID"].ToString();
                    item.OriginAP = row["OriginAP"].ToString();
                    item.DestinationAP = row["DestinationAP"].ToString();
                    item.Price = row["Price"].ToString();
                    item.TotalSeat = int.Parse(row["TotalSeat"].ToString());
                    item.Width = int.Parse(row["width"].ToString());
                    item.Height = int.Parse(row["height"].ToString());
                    item.DurationID = row["IDDurationFlight"].ToString();
                    item.Duration = row["Duration"].ToString();

                    result.Add(item);
                }
            }
            return result;
        }

        public bool DisableFlight(Flight value) {
            return DAL_Controls.Controls.DisableFlight(value);
        }

        public bool UpdateFlight(Flight value) {
            return DAL_Controls.Controls.UpdateFlight(value);
        }

        public bool UpdateTransit(Transit value) {
            return DAL_Controls.Controls.UpdateTransit(value);
        }

        public bool DisableTransit(Transit value) {
            return DAL_Controls.Controls.DisableTransit(value);
        }

        public bool DisableFlightTransit(Flight value) {
            return DAL_Controls.Controls.DisableFlightTransit(value);
        }

        public bool DisableFlightAll() {
            return DAL_Controls.Controls.DisableFlightAll();
        }

        public DashStatistic GetDashStatistic(string date) {
            DashStatistic result = new DashStatistic();

            DataTable dailyTicket = DAL_Controls.Controls.getTicketCountDaily(date);
            DataTable dailyMoney = DAL_Controls.Controls.getSumMoneyDaily(date);

            if (dailyTicket != null && dailyMoney != null) {
                result.dailyTicket = int.Parse(dailyTicket.Rows[0]["ticketDaily"].ToString());
                result.dailyMoney = double.Parse(dailyMoney.Rows[0]["moneyDaily"].ToString());
            }
            return result;
        }

        public List<FlightRoute> GetFlightRoute() {
            List<FlightRoute> result = new List<FlightRoute>();

            DataTable flights = DAL_Controls.Controls.getFlightRouteAll();

            if (flights != null) {
                foreach (DataRow row in flights.Rows) {
                    FlightRoute item = new FlightRoute();

                    item.latOrigin = float.Parse(row["latOrigin"].ToString());
                    item.lonOrigin = float.Parse(row["lonOrigin"].ToString());
                    item.latDestination = float.Parse(row["latDestination"].ToString());
                    item.lonDestination = float.Parse(row["lonDestination"].ToString()); 
                    item.flightID = row["flightID"].ToString();
                    item.originName = row["originName"].ToString();
                    item.destinationName = row["destinationName"].ToString();

                    DataTable transits = DAL_Controls.Controls.getTransitRouteFromFlight(item.flightID);

                    if (transits != null) {
                        item.transitList = new List<TransitLocation>();

                        foreach (DataRow transRow in transits.Rows) {
                            TransitLocation transItem = new TransitLocation();

                            transItem.transitLat = float.Parse(transRow["transitLat"].ToString());
                            transItem.transitLon = float.Parse(transRow["transitLon"].ToString());
                            transItem.transitName = transRow["transitName"].ToString();

                            item.transitList.Add(transItem);
                        }
                    }
                    result.Add(item);
                }
            }
            return result;
        }

        public bool checkToken(string token) {
            AccessToken account = TokenManager.Instance.GetInfoToken(token);
            if (account == null) return false;

            string idAccount = account.IdAccount;
            DataRow row;

            try {
                row = DAL_Controls.Controls.GetAccount(idAccount).Rows[0];
            }
            catch (Exception e) {
                return false;
            }

            return true;
        }

        public bool checkIDUser(string idAccount) {
            DataRow row;

            try {
                row = DAL_Controls.Controls.GetAccount(idAccount).Rows[0];
            }
            catch (Exception e) {
                return false;
            }

            return true;
        }

        #endregion

        #region Utilities
        string GenerateID() {
            StringBuilder builder = new StringBuilder();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .ToList().ForEach(e => builder.Append(e));
            return builder.ToString();
        }
        #endregion

    }
}
