using ServerFTM.DAL.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Library.Models;
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
        public Profile Login(UserAccount account)
        {
            DataRow dataAcc = DAL_Controls.Controls.Login(account)?.Rows[0];

            if (dataAcc == null)
                return null;
            Profile accountCurrent = new Profile();
            accountCurrent.IDAccount = dataAcc["IDAccount"].ToString();
            accountCurrent.Name = dataAcc["Name"].ToString();
            return accountCurrent;
        }
        public bool Signup(UserAccount account)
        {
            account.IdAccount = GenerateID();
            return DAL_Controls.Controls.SignUp(account);
        }
        #endregion

        #region Flights
        public List<FlightDisplayModel> GetAllFlight()
        {
            List<FlightDisplayModel> models = new List<FlightDisplayModel>();
            DataTable dataTable = DAL_Controls.Controls.GetAllFlight();
            if (dataTable != null)
            {
                //Pass datatable from dataset to our DAL Method.
                models = DAL_Controls.Controls.CreateListFromTable<FlightDisplayModel>(dataTable);
            }
            return models;
        }
        public List<FlightDisplayModel> GetFlightForCity(string cityId)
        {
            List<FlightDisplayModel> models = new List<FlightDisplayModel>();
            DataTable dataTable = DAL_Controls.Controls.GetFlightForCity(cityId);
            if (dataTable != null)
            {
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

        public List<AirportMenu> getAirportMenu(string searchKey)
        {
            List<AirportMenu> result = new List<AirportMenu>();

            DataTable airportMenu = DAL_Controls.Controls.GetAirportMenu(searchKey);

            if (airportMenu != null)
            {
                foreach (DataRow airport in airportMenu.Rows)
                {
                    AirportMenu item = new AirportMenu();

                    item.AirportID = airport["AirportID"].ToString();
                    item.AirportName = airport["AirportName"].ToString();

                    result.Add(item);
                }
            }
            return result;
        }

        public string createFlight(FlightCreateModel flight)
        {
            flight.FlightID = GenerateID();
            flight.DurationID = GenerateID();

            if (!DAL_Controls.Controls.CreateFlight(flight))
            {
                return "";
            }

            return flight.FlightID;
        }

        public bool createTransit(TransitCreateModel transit)
        {
            transit.transitID = GenerateID();
            transit.transitOrder = 1;

            return DAL_Controls.Controls.CreateTransit(transit);
        }

        public List<TransitCreateModel> getTransit(string flightID)
        {
            List<TransitCreateModel> res = new List<TransitCreateModel>();

            DataTable transits = DAL_Controls.Controls.GetTransit(flightID);

            if (transits != null)
            {
                foreach (DataRow row in transits.Rows)
                {
                    TransitCreateModel item = new TransitCreateModel();

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

        public List<FlightCreateModel> getFlightAll()
        {
            List<FlightCreateModel> result = new List<FlightCreateModel>();

            DataTable flights = DAL_Controls.Controls.GetFlightAll();

            if (flights != null)
            {
                foreach (DataRow row in flights.Rows)
                {
                    FlightCreateModel item = new FlightCreateModel();

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

        public bool DisableFlight(FlightCreateModel value)
        {
            return DAL_Controls.Controls.DisableFlight(value);
        }

        public bool UpdateFlight(FlightCreateModel value)
        {
            return DAL_Controls.Controls.UpdateFlight(value);
        }

        public bool UpdateTransit(TransitCreateModel value)
        {
            return DAL_Controls.Controls.UpdateTransit(value);
        }

        public bool DisableTransit(TransitCreateModel value)
        {
            return DAL_Controls.Controls.DisableTransit(value);
        }

        public bool DisableFlightTransit(FlightCreateModel value)
        {
            return DAL_Controls.Controls.DisableFlightTransit(value);
        }

        public bool DisableFlightAll()
        {
            return DAL_Controls.Controls.DisableFlightAll();
        }


        #region Report_Handle

        public List<ChildMonthReport> GetMonthReports(int mouth, int year)
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

        public List<ChildYearReport> GetYearReports(int year)
        {
            DataTable data = DAL_Controls.Controls.GetYearProfit(year);
            List<ChildYearReport> reports = new List<ChildYearReport>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                reports.Add(new ChildYearReport()
                {
                    Month = Convert.ToInt32(data.Rows[i]["Month"]),
                    TicketNum = Convert.ToInt32(data.Rows[i]["TicketNum"]),
                    Ratio = float.Parse(data.Rows[i]["Ratio"].ToString()),
                    Profit = Convert.ToInt32(data.Rows[i]["Profit"])
                });
            }
            return reports;
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
