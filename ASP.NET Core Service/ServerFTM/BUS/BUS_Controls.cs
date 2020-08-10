using ServerFTM.DAL.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Library.Models;
using ServerFTM.Models;
using ServerFTM.DAL.Helper;
using Models;
using System.Diagnostics;

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
        #endregion

        #region mingkhoi

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

        public List<FlightRoute> GetFlightRoute(string flightId) {
            List<FlightRoute> result = new List<FlightRoute>();

            DataTable flightDT = DAL_Controls.Controls.GetFlightRoute(flightId);
            if (flightDT != null) {
                foreach (DataRow row in flightDT.Rows) {
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

        #endregion

        #region minhtien
        public List<FlightDisplayModel> GetAllFlight() {
            List<FlightDisplayModel> models = new List<FlightDisplayModel>();
            DataTable dataTable = DAL_Controls.Controls.GetAllFlight();
            if (dataTable != null) {
                //Pass datatable from dataset to our DAL Method.
                models = DAL_Controls.Controls.CreateListFromTable<FlightDisplayModel>(dataTable);
            }
            return models;
        }
        public List<FlightDisplayModel> GetFlightForCity(string cityId) {
            List<FlightDisplayModel> models = new List<FlightDisplayModel>();
            DataTable dataTable = DAL_Controls.Controls.GetFlightForCity(cityId);
            if (dataTable != null) {
                //Pass datatable from dataset to our DAL Method.
                models = DAL_Controls.Controls.CreateListFromTable<FlightDisplayModel>(dataTable);
            }
            return models;
        }

        #endregion

        #region City
        public List<CityModel> GetAllCity()
        {
            List<CityModel> models = new List<CityModel>();
            DataTable dataTable = DAL_Controls.Controls.GetAllCity();
            if (dataTable != null)
            {
                //Pass datatable from dataset to our DAL Method.
                models = DAL_Controls.Controls.CreateListFromTable<CityModel>(dataTable);
            }
            return models;
        }
        #endregion

        #region Transit
        public List<TransitDisplayModel> GetTransits(string transitId)
        {
            List<TransitDisplayModel> models = new List<TransitDisplayModel>();
            DataTable dataTable = DAL_Controls.Controls.GetTransits(transitId);
            if (dataTable != null)
            {
                models = DAL_Controls.Controls.CreateListFromTable<TransitDisplayModel>(dataTable);
            }
            return models;
        }
        #endregion

        #region Restriction
        public RestrictionsModel GetRestriction()
        {
            List<RestrictionsModel> models = new List<RestrictionsModel>();
            DataTable dataTable = DAL_Controls.Controls.GetRestrictions();
            if (dataTable != null)
            {

                models = DAL_Controls.Controls.CreateListFromTable<RestrictionsModel>(dataTable);
            }
            if (models.Count != 1) return null;
            return models[0];
        }

        public bool ChangeRestriction(RestrictionsModel model)
        {
            return DAL_Controls.Controls.ChangeRestrictions(model);
        }
        #endregion

        #region Report_Handle

        public List<ChildMonthReport> GetMonthReports(int mouth, int year) {
            DataTable data = DAL_Controls.Controls.GetMonthProfit(mouth, year);
            List<ChildMonthReport> reports = new List<ChildMonthReport>();
            for (int i = 0; i < data.Rows.Count; i++) {
                reports.Add(new ChildMonthReport() {
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

        public List<ChildYearReport> GetYearReports(int year) {
            DataTable data = DAL_Controls.Controls.GetYearProfit(year);
            List<ChildYearReport> reports = new List<ChildYearReport>();
            for (int i = 0; i < data.Rows.Count; i++) {
                reports.Add(new ChildYearReport() {
                    Month = Convert.ToInt32(data.Rows[i]["Month"]),
                    TicketNum = Convert.ToInt32(data.Rows[i]["TicketNum"]),
                    Ratio = float.Parse(data.Rows[i]["Ratio"].ToString()),
                    Profit = Convert.ToInt32(data.Rows[i]["Profit"])
                });
            }
            return reports;
        }

        #endregion

        #region Ticket_Handle

        public bool AddTicket(Ticket ticket) {
            ticket.IDTicket = GenerateID();
            ticket.IDChairBooked = GenerateID();
            return DAL.Controls.DAL_Controls.Controls.AddTicket(ticket);
        }

        public double GetPrice(string iddur, string idclass) { 
            return double.Parse(DAL_Controls.Controls.GetPrice(iddur, idclass).Rows[0]["Price"].ToString());
        }

        public FlightInfo GetFlightInfo(string flightId) {
            FlightInfo result = new FlightInfo();

            DataTable flightInfo = DAL_Controls.Controls.GetFlightInfo(flightId); 

            if (flightInfo != null) {
                result.durationID = flightInfo.Rows[0]["IDDurationFlight"].ToString();
                result.flightTime = flightInfo.Rows[0]["Duration"].ToString();
                result.originID = flightInfo.Rows[0]["originAp"].ToString();
                result.originName = flightInfo.Rows[0]["originName"].ToString();
                result.destinationID = flightInfo.Rows[0]["destinationAp"].ToString();
                result.destinationName = flightInfo.Rows[0]["destinationName"].ToString();
            }
            return result;
        }

        public ChairState GetChairState(ChairRequest value) {
            ChairState result = new ChairState();

            DataTable sizeFlight = DAL_Controls.Controls.GetDefineSizeFlight(value.durationId);
            DataTable bookedChair = DAL_Controls.Controls.GetBookedChair(value.durationId, value.date);

            if (sizeFlight != null && bookedChair != null) {
                result.width = int.Parse(sizeFlight.Rows[0]["width"].ToString());
                result.height = int.Parse(sizeFlight.Rows[0]["height"].ToString());

                result.chairBooked = new List<ChairPos>();

                foreach(DataRow row in bookedChair.Rows) {
                    ChairPos booked = new ChairPos();
                    booked.posX = int.Parse(row["XPos"].ToString());
                    booked.posY = int.Parse(row["YPos"].ToString());

                    result.chairBooked.Add(booked); 
                }
            }
            return result;
        } 

        #endregion

        #region Passenger_Handle

        public string AddPassenger(Passenger passenger) {
            string result = ""; 

            passenger.IDPassenger = GenerateID();

            DataTable _passenger = DAL_Controls.Controls.AddPassenger(passenger);
            
            if (_passenger != null) {
                result = _passenger.Rows[0]["PassengerId"].ToString();
            }
            return result;
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
                .ToList().ForEach(e => builder.Append(e));
            return builder.ToString();
        }
        #endregion

    }
}
