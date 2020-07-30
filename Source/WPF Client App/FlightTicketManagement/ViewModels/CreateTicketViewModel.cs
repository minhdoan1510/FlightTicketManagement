using Caliburn.Micro;
using FlightTicketManagement.Helper;
using FlightTicketManagement.Views;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;

namespace FlightTicketManagement.ViewModels
{
    public class CreateTicketViewModel : Conductor<object>
    {
        private IEventAggregator _events;
        SimpleContainer _container;
        //ChairGridViewModel UCSelectTicket;

        const string idlocalAP = "I2MTDO85YaCt4en9zRuUkF7BNcQSp0wVfdAyLJg3ibjhEKlZmWPo1XxvsqGrH6";

        private BindingList<TimeSpan> departureTime;
        public BindingList<TimeSpan> DepartureTime { get => departureTime; set { departureTime = value; NotifyOfPropertyChange(() => DepartureTime); } }

        private BindingList<City> destCity;
        public BindingList<City> DestCity { get => destCity; set { destCity = value; NotifyOfPropertyChange(() => DestCity); } }

        private BindingList<DurationTime> durationTime;
        public BindingList<DurationTime> DurationTime { get => durationTime; set { durationTime = value; NotifyOfPropertyChange(() => DurationTime); } }

        private City citySelected;
        public City CitySelected { get => citySelected; set {
                citySelected = value;
                NotifyOfPropertyChange(() => CitySelected);
                if (null != CitySelected)
                    LoadDestAP(CitySelected);
            } }

        private Airport airportSelected;
        public Airport AirportSelected {
            get => airportSelected;
            set {
                airportSelected = value;
                NotifyOfPropertyChange(() => AirportSelected);
                if (AirportSelected != null)
                    LoadDurationTime();
            }
        }

        private DurationTime durationSelected;
        public DurationTime DurationSelected
        {
            get => durationSelected;
            set
            {
                durationSelected = value;
                LoadUCSelectTicket();
                NotifyOfPropertyChange(() => CompleteTicket);
                NotifyOfPropertyChange(() => DurationSelected);
            }
        }

        private DateTime timeBooking;
        public DateTime TimeBooking {
            get => timeBooking;
            set {
                timeBooking = value;
                NotifyOfPropertyChange(() => TimeBooking);
            }
        }

        private bool isPaid;
        public bool IsPaid { get => isPaid; set { 
                isPaid = value; 
                NotifyOfPropertyChange(() => IsPaid);
                NotifyOfPropertyChange(() => CompleteTicket);
            } }

        private BindingList<Airport> destAirport;
        public BindingList<Airport> DestAirport { get => destAirport; set { destAirport = value; DurationTime?.Clear(); NotifyOfPropertyChange(() => DestAirport); } }

        private string passengerName;
        private string iDCard;
        private string tel;
        private bool existPassenger;

        public string PassengerName { get => passengerName; set { passengerName = value; NotifyOfPropertyChange(() => PassengerName); } }
        public string IDCard { get => iDCard; set { iDCard = value; NotifyOfPropertyChange(() => IDCard); } }
        public string Tel { get => tel; set {
                tel = value;
                CheckInfoPassenger();
                NotifyOfPropertyChange(() => Tel);
            }
        }

        private Int64 totals;
        public Int64 Totals {
            get => totals;
            set
            {
                if (value == null)
                    totals = 0;
                else
                    totals = value;
                NotifyOfPropertyChange(() => Totals);
            }
        }


        private Int64 receive;
        public Int64 Receive
        {
            get => receive;
            set
            {
                if (value==null)
                    receive = 0;
                else
                    receive = value;
                NotifyOfPropertyChange(() => Receive);
                NotifyOfPropertyChange(() => CompleteTicket);
            }
        }

        public bool CompleteTicket {
            get {
                if (DurationSelected != null)
                    if (ClassSelected != null)
                    {
                        CalTotalMoney();
                        if (IsPaid)
                        {
                            if (Totals != 0 && Convert.ToInt64(Totals) - Convert.ToInt64(Receive) <= 0)
                            {
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return true;

                    }
                return false;
            }
        }


        private Passenger passenger;
        public Passenger Passenger { 
            get => passenger; 
            set { 
                passenger = value;
                NotifyOfPropertyChange(() => Passenger);
            }
        }


        private BindingList<string> classList;
        public BindingList<string> ClassList {
            get => classList; 
            set
            {
                classList = value;
                NotifyOfPropertyChange(() => ClassList);
            }
        }
        private string classSelected;

        public string ClassSelected {
            get { return classSelected; }

            set
            {
                classSelected= value;
                NotifyOfPropertyChange(() => ClassSelected);
                NotifyOfPropertyChange(() => CompleteTicket);
            }
        }

        public string PosW { get; set; }
        public string PosH { get; set; }


        ChairGridView UCSelectTicket;
        public List<ChairBooking> DataChairs;

        KeyValuePair<int, int> DefineChair;

        public CreateTicketViewModel()
        {
            ActivateItem(UCSelectTicket);
            this.TimeBooking = DateTime.Now;
            LoadDestCity();
            ClassList = new BindingList<string>();
            ClassList.Add("Classic");
            classList.Add("VIP");
            IsPaid = true;


            //UCSelectTicket.PropertyChanged += (e,s)=>NotifyOfPropertyChange(() => CompleteTicket);
        }

        private async void LoadUCSelectTicket()
        {
            //UCSelectTicket = new ChairGridView();
            //DefineChair = (await APIHelper.Instance.Get<Response<KeyValuePair<int,int>>>(ApiRoutes.Flight.GetDefineChairFlight,
            //    new KeyValuePair<string, string>[] {
            //        new KeyValuePair<string, string>("id", DurationSelected.IDDurationTime)})).Result;

            //UCSelectTicket.SetDefineDataChair(DefineChair.Key,DefineChair.Value);

            //DataChairs = (await APIHelper.Instance.Get<Response<List<ChairBooking>>>(ApiRoutes.Ticket.LoadStateChair,
            //    new KeyValuePair<string, string>[] {
            //        new KeyValuePair<string, string>("time", DateTime.Today.Ticks.ToString()),
            //        new KeyValuePair<string, string>("id", DurationSelected.IDDurationTime)})).Result;
            //UCSelectTicket.SetDataChair (DataChairs);
        }

        private async void CheckInfoPassenger()
        {
            if (!Regex.IsMatch(Tel, @"(0[9|3|7|8|4]|01[2|6|8|9])+([0-9]{8})\b"))
                return;
            Response<Passenger> response = await APIHelper.Instance.Get<Response<Passenger>>(ApiRoutes.Passenger.GetExist,
                new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("tel" , tel)
                }
            );
            if(response.IsSuccess == true)
            {
                Passenger = response.Result;
                IDCard = Passenger.IDCard;
                PassengerName = Passenger.PassengerName;
                existPassenger = true;
            }
            else
            {
                IDCard = string.Empty;
                PassengerName = string.Empty;
                Passenger = new Passenger();
                existPassenger = false;
            }
        }

        private async void LoadDestCity()
        {
            Response<List<City>> response = await APIHelper.Instance.Get<Response<List<City>>>(ApiRoutes.City.GetCityAlready,
                new KeyValuePair<string, string>[] {
                    new KeyValuePair<string, string>("idlocal",idlocalAP)});

            if(response.IsSuccess&&response.Result.Count>0)
            {
                DestCity = new BindingList<City>(response.Result);
            }
            else
            {
                MessageBox.Show("Sân bay hiện tại không có tuyến đường nào!!!");
            }
        }

        private async void LoadDestAP(City _CitySelected)
        {
            Response<List<Airport>> response = await APIHelper.Instance.Get<Response<List<Airport>>>(ApiRoutes.Airport.GetAPAlready,
                new KeyValuePair<string, string>[] {
                    new KeyValuePair<string, string>("idcity",_CitySelected.IDCity),
                    new KeyValuePair<string, string>("idairport", idlocalAP)});

            if (response.IsSuccess && response.Result.Count > 0)
            {
                DestAirport = new BindingList<Airport>(response.Result);
            }
            else
            {
                MessageBox.Show("Sân bay hiện tại không có tuyến đường nào!!!");
            }
        }

        private async void LoadDurationTime()
        {
            Response<List<DurationTime>> response = await APIHelper.Instance.Get<Response<List<DurationTime>>>(ApiRoutes.Flight.GetDurationTime,
                new KeyValuePair<string, string>[] {
                    new KeyValuePair<string, string>("iddestap", AirportSelected.IDAirport),
                    new KeyValuePair<string, string>("idoriap", idlocalAP)});

            if (response.IsSuccess && response.Result.Count > 0)
            {
                DurationTime = new BindingList<DurationTime>(response.Result);
            }
            else
            {
                MessageBox.Show("Sân bay hiện tại không có tuyến đường nào!!!");
            }
        }

        public async void CreateTicket()
        {
            if (!existPassenger)
            {
                passenger = new Passenger()
                {
                    IDPassenger = Guid.NewGuid().ToString(),
                    PassengerName = this.PassengerName,
                    IDCard = this.IDCard,
                    Tel = this.Tel
                };
                if(!(await APIHelper.Instance.Post<Response<bool>>(ApiRoutes.Passenger.Add, passenger)).IsSuccess)
                {
                    return;
                }
            }

            Ticket ticket = new Ticket() {
                IDTicket = Guid.NewGuid().ToString(),
                IDDurationFlight = DurationSelected.IDDurationTime,
                IDChairBooked = Guid.NewGuid().ToString(),
                IDClass = ClassSelected.Equals("VIP")?"1":"2",
                TimeBooking = DateTime.Now,
                TimeFlight = this.TimeBooking,
                XChair = Convert.ToInt32(PosW),
                YChair = Convert.ToInt32(PosH),
                IDPassenger = Passenger.IDPassenger,
                IsPaid = ((this.IsPaid)?1:0)
            };

            if (!(await APIHelper.Instance.Post<Response<bool>>(ApiRoutes.Ticket.Add, ticket)).IsSuccess)
            {
                MessageBox.Show(string.Format("Tạo vé không thành công!!!"));
                return;
            }
            else
            {
                MessageBox.Show(string.Format("Tạo vé thành công!!!"));
                ReloadScene();
            }
        }

        private async void CalTotalMoney()
        {
            Response<int> response = await APIHelper.Instance.Get<Response<int>>(ApiRoutes.Ticket.GetPrice,
                new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>( "iddur" , DurationSelected.IDDurationTime ),
                    new KeyValuePair<string, string>( "idclass" , (ClassSelected.Equals("VIP")?"1":"2") ),
                });
            if (response.IsSuccess)
            {
                Totals = response.Result;
            }
        }

        public void ReloadScene()
        {
            IDCard = string.Empty;
            PassengerName = string.Empty;
            Passenger = new Passenger();
            existPassenger = false;
            CitySelected = null;
            DestAirport = null;
            DurationTime = null;
            TimeBooking = DateTime.Now;
            Tel = string.Empty;
        }

    }

    public class CalMultiBinding : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                Int64 temp = Int64.Parse(values[0].ToString().Split(' ')[0].Replace(",","")) - Int64.Parse(values[1].ToString().Split(' ')[0].Replace(",", ""));
                if (temp >= 0)
                    return 0;
               return -temp;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
