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

        internal KeyValuePair<int, int> GetDefineChairFlight(string id) {
            ResponseDTB response = DAL_Controls.Controls.GetDefineChairFlight(id);
            if (!response.IsSuccess)
                return default;
            DataRow data = response.Result?.Rows[0];

            if (data == null)
                return default;
            KeyValuePair<int, int> result = new KeyValuePair<int, int>(Convert.ToInt32(data["Width"]), Convert.ToInt32(data["Height"]));
            return result;
        }

        public bool AddTicket(Ticket ticket) {
            return DAL.Controls.DAL_Controls.Controls.AddTicket(ticket).IsSuccess;
        }

        public int GetPrice(string iddur, string idclass) { 
            return Convert.ToInt32(DAL_Controls.Controls.GetPrice(iddur, idclass).Result.Rows[0]["Price"]);
        }

        public List<ChairBooking> GetListChair(string id, DateTime timeDur) {
            ResponseDTB response = DAL_Controls.Controls.GetListChair(id, timeDur);
            if (!response.IsSuccess)
                return default;
            DataTable data = response.Result;
            List<ChairBooking> result = new List<ChairBooking>();
            for (int i = 0; i < data.Rows.Count; i++) {
                result.Add(new ChairBooking() {
                    IDchair = Convert.ToChar(Convert.ToInt32(data.Rows[i]["XPos"]) + 65).ToString() + Convert.ToInt32(data.Rows[i]["YPos"]).ToString(),
                    Status = ChairStatus.Booked
                });
            }
            return result;
        }

        #endregion

        #region City_Handle

        public List<City> GetCityAlready(string idlocal) {
            ResponseDTB response = DAL_Controls.Controls.GetCityAlready(idlocal);
            if (!response.IsSuccess)
                return default;
            DataTable data = response.Result;
            List<City> reports = new List<City>();
            for (int i = 0; i < data.Rows.Count; i++) {
                reports.Add(new City() {
                    IDCity = data.Rows[i]["IDCity"].ToString(),
                    CityName = data.Rows[i]["CityName"].ToString()
                });
            }
            return reports;
        }

        #endregion

        #region Flight_Handle

        public List<DurationTime> GetDurationFlight(string idoriap, string iddestap) {
            ResponseDTB response = DAL_Controls.Controls.GetDurationFlight(idoriap, iddestap);
            if (!response.IsSuccess)
                return default;
            DataTable data = response.Result;
            List<DurationTime> result = new List<DurationTime>();
            for (int i = 0; i < data.Rows.Count; i++) {
                result.Add(new DurationTime() {
                    IDDurationTime = data.Rows[i]["IDDuration"].ToString(),
                    DurTime = data.Rows[i]["Duration"].ToString()
                });
            }
            return result;
        }
        #endregion

        #region Airport_Handle


        public List<Airport> GetAPinCity(string idcity, string idairport) {
            ResponseDTB response = DAL_Controls.Controls.GetAPinCity(idairport, idcity);
            if (!response.IsSuccess)
                return default;
            DataTable data = response.Result;
            List<Airport> reports = new List<Airport>();
            for (int i = 0; i < data.Rows.Count; i++) {
                reports.Add(new Airport() {
                    IDAirport = data.Rows[i]["IDAirport"].ToString(),
                    AirportName = data.Rows[i]["AirportName"].ToString()
                });
            }
            return reports;
        }

        #endregion

        #region Passenger_Handle
        public Passenger GetExistPassenger(string tel) {
            ResponseDTB responseDTB = DAL.Controls.DAL_Controls.Controls.GetExistPassenger(tel);
            if (responseDTB.IsSuccess) {
                Passenger passenger = new Passenger() {
                    IDPassenger = responseDTB.Result.Rows[0]["IDPassenger"].ToString(),
                    IDCard = responseDTB.Result.Rows[0]["IDCard"].ToString(),
                    Tel = responseDTB.Result.Rows[0]["TEL"].ToString(),
                    PassengerName = responseDTB.Result.Rows[0]["NAME"].ToString(),
                };
                return passenger;
            }
            else {
                return null;
            }
        }


        public bool GetAddPassenger(Passenger passenger) {
            ResponseDTB responseDTB = DAL.Controls.DAL_Controls.Controls.GetAddPassenger(passenger);
            if (responseDTB.IsSuccess) {
                return true;
            }
            else {
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
                .ToList().ForEach(e => builder.Append(e));
            return builder.ToString();
        }
        #endregion

    }
}
