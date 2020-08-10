using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Caliburn.Micro;
using FlightTicketManagement.EventModels;
using FlightTicketManagement.Helper;
using Library.Models;
using ServerFTM.Models;

namespace FlightTicketManagement.Views
{
    /// <summary>
    /// Interaction logic for CreateTicket.xaml
    /// </summary>
    /// 
    public class TicketBusControl
    {
        private static TicketBusControl _instance;
        public static TicketBusControl Instance {
            get {
                if (_instance == null) {
                    _instance = new TicketBusControl();
                }
                return _instance;
            }
        }

        public async Task<Response<string>> AddTicket(Ticket value) {
            Response<string> result = await APIHelper.Instance.Post<Response<string>>(ApiRoutes.Ticket.AddTicket,
                value);
            return result;
        }

        public async Task<Response<string>> AddPassenger(Passenger value) {
            Response<string> result = await APIHelper.Instance.Post<Response<string>>(ApiRoutes.Ticket.AddPassenger,
                value);
            return result;
        }

        public async Task<Response<double>> GetPrice(string durationId, string classId) {
            string url = ApiRoutes.Ticket.GetPrice;
            url = url.Replace("{durationId}", durationId);
            url = url.Replace("{classId}", classId);

            Response<double> result = await APIHelper.Instance.Get<Response<double>>(url);
            return result;
        }

        public async Task<Response<FlightInfo>> GetFlightInfo(string flightId) {
            Response<FlightInfo> result = await APIHelper.Instance.Get<Response<FlightInfo>>
                (ApiRoutes.Ticket.GetFlightInfo.Replace("{id}", flightId));
            return result;
        }

        public async Task<Response<ChairState>> GetChairState(string durationId, string date) {
            Response<ChairState> result = await APIHelper.Instance.Post<Response<ChairState>>
                (ApiRoutes.Ticket.GetChairState, new ChairRequest() {
                    durationId = durationId,
                    date = date
                });
            return result;
        }
    }

    public partial class CreateTicketView : UserControl
    {
        public static CreateTicketView Instance;
        public FlightInfo _flightInfo = new FlightInfo();
        public List<Chair> listChair = new List<Chair>();

        public string flightId = "null";

        public CreateTicketView() {
            Instance = this;
            InitializeComponent();

            FutureDatePicker.DisplayDateStart = DateTime.Today.AddDays(1);
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e) {
            if (flightId != "null") {
                Response<FlightInfo> res = await TicketBusControl.Instance.GetFlightInfo(flightId);
                this._flightInfo = res.Result;
                this.showFlightInfo();
            }
        }

        public void showFlightInfo() {
            string text = $"ID = {this._flightInfo.durationID.Substring(0, 20)}";
            this.flightInfo.Text = text;
        }

        public async void showTotalMoney() {
            double total = 0.0f;

            foreach(var item in listChair) {
                Response<double> res = await TicketBusControl.Instance.GetPrice(this._flightInfo.durationID, item.classID);
                total += res.Result; 
            }
            this.sumMoney.Text = string.Format("{0:N0}", total);
        }

        public bool checkPassenger() {
            if (passengerName.Text.Length <= 10) {
                MessageBox.Show("Tên Khách Hàng Không Hợp Lệ");
                return false;
            }

            if (passengerPhone.Text.Length < 8) {
                MessageBox.Show("SĐT Khách Hàng Không Hợp Lệ");
                return false;
            }
            else if (passengerPhone.Text[0] != '0') {
                MessageBox.Show("SĐT Khách Hàng Không Hợp Lệ");
                return false;
            }

            if (passengerIdCard.Text.Length < 8) {
                MessageBox.Show("CMND Khách Hàng Không Hợp Lệ");
                return false;
            }
            return true;
        }

        public bool checkTicket() {
            if (!checkDate()) return false;
            if (this.flightId == "null") {
                MessageBox.Show("Tuyến Bay Không Hợp Lệ");
                return false;
            }
            if (this.listChair.Count <= 0) {
                MessageBox.Show("Ghế Ngồi Không Hợp Lệ");
                return false;
            }
            return true;
        }

        public bool checkDate() {
            if (FutureDatePicker.SelectedDate == null) {
                MessageBox.Show("Ngày Bay Không Hợp Lệ");
                return false;
            }
            return true;
        }

        public void price_KeyDown(object sender, KeyEventArgs e) {
            // backspace press 
            if (e.Key == Key.Back)
                return;

            bool keyboardHandle = true;

            if (!e.Key.ToString().Contains("Oem")) {
                foreach (char item in e.Key.ToString()) {
                    if (char.IsDigit(item)) {
                        keyboardHandle = false;
                        break;
                    }
                }
            }
            e.Handled = keyboardHandle;
        }

        public async void addTicket() {
            Passenger newPas = new Passenger() {
                PassengerName = this.passengerName.Text,
                IDCard = this.passengerIdCard.Text,
                Tel = this.passengerPhone.Text
            };
            Response<string> passenger = await TicketBusControl.Instance.AddPassenger(newPas);

            foreach(var item in this.listChair) {
                Ticket newTicket = new Ticket() {
                    IDPassenger = passenger.Result,
                    IDDurationFlight = this._flightInfo.durationID,
                    IDClass = item.classID,
                    TimeBooking = DateTime.Now,
                    TimeFlight = FutureDatePicker.SelectedDate.Value,
                    IsPaid = 1,
                    XChair = item.posX,
                    YChair = item.posY
                };
                await TicketBusControl.Instance.AddTicket(newTicket);
            }
            this.listChair.Clear();
        }

        private async void chairPicker_Click(object sender, RoutedEventArgs e) {
            if (!checkDate()) return;
            if (this.flightId == "null") {
                MessageBox.Show("Tuyến Bay Không Hợp Lệ");
                return;
            }
            Nullable<DateTime> t = FutureDatePicker.SelectedDate;
            Console.WriteLine(t.Value.ToString("yyyy/MM/dd HH:mm:ss"));

            Response<ChairState> res = await TicketBusControl.Instance.GetChairState(this._flightInfo.durationID,
                string.Format("{0:yyyy/MM/dd HH:mm:ss}", FutureDatePicker.SelectedDate.ToString()));

            ChairGrid newGrid = new ChairGrid(res.Result);
            newGrid.initData();
            newGrid.ShowDialog();

            this.listChair = newGrid.listChair;
            this.chairInfo.Text = $"Số Ghế {this.listChair.Count.ToString()}";

            this.showTotalMoney();
        }

        private void CreateTicket_Click(object sender, RoutedEventArgs e) {
            if (!checkPassenger() || !checkTicket())
                return;

            this.addTicket();
        }
    }
}
