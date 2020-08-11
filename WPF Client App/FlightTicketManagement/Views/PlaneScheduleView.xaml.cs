using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
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
using System.Windows.Threading;

using ServerFTM.Models;
using FlightTicketManagement.Helper;
using Library.Models;
using FlightTicketManagement.Views;
using System.Xaml;

public class FlightBusControl
{
    public static FlightBusControl _instance = null;

    public static FlightBusControl Instance {
        get {
            if (_instance == null) {
                _instance = new FlightBusControl();
            }
            return _instance;
        }
    }

    public async Task<Response<string>> CreateFlight(Flight flight) {
        return await APIHelper.Instance.Post<Response<string>>
            (ApiRoutes.Flight.CreateFlight, flight);
    }

    public async Task<Response<string>> CreateTransit(Transit transit) {
        return await APIHelper.Instance.Post<Response<string>>
            (ApiRoutes.Flight.CreateTransit, transit);
    }

    public async Task<Response<List<Transit>>> GetTransit(string _flightID) {

        string apiURL = ApiRoutes.Flight.GetTransit;
        apiURL = apiURL.Replace("{id}", _flightID);

        return await APIHelper.Instance.Get<Response<List<Transit>>>(apiURL);
    }

    public async Task<Response<List<AirportMenu>>> GetAirportMenu(string searchKey) {
        string apiURL = ApiRoutes.Flight.GetAirportMenu;
        apiURL = apiURL.Replace("{search}", searchKey);

        Console.WriteLine(apiURL);

        return await APIHelper.Instance.Get<Response<List<AirportMenu>>>(apiURL);
    }

    public async Task<Response<List<Flight>>> GetFlightAll() {
        return await APIHelper.Instance.Get<Response<List<Flight>>>
            (ApiRoutes.Flight.GetFlightAll);
    }

    public async Task<Response<string>> UpdateFlight(Flight value) {
        return await APIHelper.Instance.Post<Response<string>>
            (ApiRoutes.Flight.UpdateFlight, value);
    }

    public async Task<Response<string>> DisableFlight(Flight value) {
        return await APIHelper.Instance.Post<Response<string>>
            (ApiRoutes.Flight.DisableFlight, value);
    }

    public async Task<Response<string>> UpdateTransit(Transit value) {
        return await APIHelper.Instance.Post<Response<string>>
            (ApiRoutes.Flight.UpdateTransit, value);
    }

    public async Task<Response<string>> DisableTransit(Transit value) {
        return await APIHelper.Instance.Post<Response<string>>
            (ApiRoutes.Flight.DisableTransit, value);
    }

    public async Task<Response<string>> DisableFlightTransit(Flight value) {
        return await APIHelper.Instance.Post<Response<string>>
            (ApiRoutes.Flight.DisableFlightTransit, value);
    }

    public async Task<Response<string>> DisableFlightAll() {
        return await APIHelper.Instance.Post<Response<string>>
            (ApiRoutes.Flight.DisableFlightAll);
    }

    public async Task<Response<DashStatistic>> GetDashBoardStatistic(string value) {
        return await APIHelper.Instance.Post<Response<DashStatistic>>
            (ApiRoutes.Flight.GetDashStatistic, new { date = value });
    }

    public async Task<Response<List<FlightRoute>>> GetFlightRoutes() {
        return await APIHelper.Instance.Get<Response<List<FlightRoute>>>
            (ApiRoutes.Flight.GetFlightRouteAll);
    }
}

namespace FlightTicketManagement.Views
{
    public partial class PlaneScheduleView : UserControl
    {
        Response<List<Flight>> flightList = new Response<List<Flight>>();

        Flight flightToCreate = new Flight();

        List<Flight> flightListForDataGrid { get; set; }

        public string originId, destinationId;

        public static PlaneScheduleView Instance;

        public PlaneScheduleView() {
            Instance = this;
            InitializeComponent();

            flightLoadingStatus.Visibility = Visibility.Hidden;
            transitLoadingStatus.Visibility = Visibility.Hidden;

            initSearchType();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e) {
            this.refreshFlights();
        }

        private void initSearchType() {
            List<KeyValuePair<string, string>> tempList = new List<KeyValuePair<string, string>>();
            tempList.Add(new KeyValuePair<string, string>("Khởi Hành", "OriginAP"));
            tempList.Add(new KeyValuePair<string, string>("Kết Thúc", "DestinationAP"));
            tempList.Add(new KeyValuePair<string, string>("Giá", "Price"));
            tempList.Add(new KeyValuePair<string, string>("Giờ Khởi Hành", "Duration"));
            tempList.Add(new KeyValuePair<string, string>("Số Ghế", "TotalSeat"));

            searchType.ItemsSource = tempList;
            searchType.DisplayMemberPath = "Key";
            searchType.SelectedIndex = 0;
        }

        private void searchType_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            int typeIndex = searchType.SelectedIndex;
            string typeText = (searchType.ItemsSource as List<KeyValuePair<string, string>>)[typeIndex].Value;

            if (typeText == "TotalSeat") {
                int testValue = 0;

                if (searchTextbox.Text.Length > 0 && !int.TryParse(searchTextbox.Text, out testValue)) {
                    searchTextbox.Clear();
                }
            }
        }

        private void searchTextbox_KeyDown(object sender, KeyEventArgs e) {
            int typeIndex = searchType.SelectedIndex;
            string typeText = (searchType.ItemsSource as List<KeyValuePair<string, string>>)[typeIndex].Value;

            if (typeText == "TotalSeat" || typeText == "Price") {
                this.price_KeyDown(sender, e);
            }
        }

        private void searchTextbox_KeyUp(object sender, KeyEventArgs e) {
            filterFlight();
        }

        private void searchTextbox_TextChanged(object sender, TextChangedEventArgs e) {
            int typeIndex = searchType.SelectedIndex;
            string typeText = (searchType.ItemsSource as List<KeyValuePair<string, string>>)[typeIndex].Value;

            if (typeText == "Price") {
                this.price_TextChanged(sender, e);
            }
        }

        private void filterFlight() {
            List<Flight> tempList = flightList.Result;

            int typeIndex = searchType.SelectedIndex;
            string typeText = (searchType.ItemsSource as List<KeyValuePair<string, string>>)[typeIndex].Value;

            if (searchTextbox.Text == "" || searchTextbox.Text == null) {
                flightDataGridView.ItemsSource = tempList;
                flightDataGridView.Items.Refresh();
                return;
            }
            if (tempList == null)
                return;

            List<Flight> newItems = null;
            switch (typeText) {
                case "OriginAP":
                    newItems = (from x in tempList
                                where x.OriginAP.StartsWith(searchTextbox.Text)
                                select x).ToList();
                    break;
                case "DestinationAP":
                    newItems = (from x in tempList
                                where x.DestinationAP.StartsWith(searchTextbox.Text)
                                select x).ToList();
                    break;
                case "Price":
                    newItems = (from x in tempList
                                where x.Price.StartsWith(searchTextbox.Text)
                                select x).ToList();
                    break;
                case "Duration":
                    newItems = (from x in tempList
                                where x.Duration.StartsWith(searchTextbox.Text)
                                select x).ToList();
                    break;
                case "TotalSeat":
                    int checkValue;
                    if (int.TryParse(searchTextbox.Text, out checkValue)) {
                        newItems = (from x in tempList
                                    where x.TotalSeat.Equals(checkValue)
                                    select x).ToList();
                    }
                    break;
                default:
                    return;
            }
            flightDataGridView.ItemsSource = newItems;
            flightDataGridView.Items.Refresh();
        }

        public void setApproveStatus(UIElement icon) {
            var status = icon as MaterialDesignThemes.Wpf.PackIcon;

            status.Kind = MaterialDesignThemes.Wpf.PackIconKind.Check;
            status.Foreground = Brushes.Green;
        }

        public void setDeniedStatus(UIElement icon) {
            var status = icon as MaterialDesignThemes.Wpf.PackIcon;

            status.Kind = MaterialDesignThemes.Wpf.PackIconKind.Cancel;
            status.Foreground = Brushes.Red;
        }

        public void setNormalStatus(UIElement icon) {
            var status = icon as MaterialDesignThemes.Wpf.PackIcon;

            status.Kind = MaterialDesignThemes.Wpf.PackIconKind.Check;
            status.Foreground = Brushes.Transparent;
        }

        private void callTransitInputForm(string _flightID) {
            GetTransitView transitForm = new GetTransitView(_flightID);
            transitForm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            transitForm.ShowDialog();
        }

        private void callModifyFlightForm(ref object value) {
            GetFlightView flightForm = new GetFlightView(ref value);
            flightForm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            flightForm.ShowDialog();
        }

        private void callModifyTransit(ref object value) {
            GetTransitView transitForm = new GetTransitView(ref value);
            transitForm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            transitForm.ShowDialog();
        }

        private async void menuGetData(object sender) {
            string searchKey = "A";

            if ((sender as ComboBox).Text != "") {
                searchKey = (sender as ComboBox).Text;
            }
            else {
                List<AirportMenu> tempList = (sender as ComboBox).ItemsSource as List<AirportMenu>;
                if (tempList != null)
                    tempList.Clear();

                (sender as ComboBox).IsDropDownOpen = false;
                return;
            }

            Response<List<AirportMenu>> airportList = await
                FlightBusControl.Instance.GetAirportMenu(searchKey);

            (sender as ComboBox).ItemsSource = airportList.Result;
            (sender as ComboBox).DisplayMemberPath = "AirportName";
            (sender as ComboBox).IsDropDownOpen = true;
        }

        public void Menu_TextInput(object sender, TextCompositionEventArgs e) {
            var textBox = e.OriginalSource as TextBox;

            textBox.SelectionLength = 0;
            textBox.SelectionStart = textBox.Text.Length;
        }

        public async void Menu_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Space || e.Key == Key.Enter) {
                var textBox = e.OriginalSource as TextBox;

                textBox.SelectionLength = 0;
                textBox.SelectionStart = textBox.Text.Length;
            }
            menuGetData(sender);
        }

        public void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Console.WriteLine("airplane selected");

            AirportMenu res = (sender as ComboBox).SelectedItem as AirportMenu;

            if (res == null)
                return;

            if ((sender as ComboBox).Name == "origionalAP") {
                flightToCreate.OriginApID = res.AirportID;
                this.originId = res.AirportID;

                Console.WriteLine("origin: " + res.AirportID);
            }
            else if ((sender as ComboBox).Name == "destinationAP") {
                flightToCreate.DestinationApID = res.AirportID;
                this.destinationId = res.AirportID;

                Console.WriteLine("destination: " + res.AirportID);
            }
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

        public void price_TextChanged(object sender, TextChangedEventArgs e) {
            string text = (sender as TextBox).Text;
            double value = 0.0f;

            if (text.Length > 2 && double.TryParse(text, out value)) {
                (sender as TextBox).Text = string.Format(System.Globalization.CultureInfo.CurrentCulture,
                    "{0:n0}", value);
                (sender as TextBox).CaretIndex = (sender as TextBox).Text.Length;
            }
        }

        public bool checkOrigionalAP() {
            if (origionalAP.SelectedIndex == -1) {
                this.setDeniedStatus(origionalAP_status);
                return false;
            }
            this.setApproveStatus(origionalAP_status);
            return true;
        }

        public bool checkDestinationAP() {
            if (destinationAP.SelectedIndex == -1) {
                this.setDeniedStatus(destinationAP_status);
                return false;
            }
            this.setApproveStatus(destinationAP_status);
            return true;
        }

        public bool checkPrice() {
            if (price.Text == "") {
                this.setDeniedStatus(price_status);
                return false;
            }
            this.setApproveStatus(price_status);
            return true;
        }

        public bool checkTimeFlight() {
            if (timeFlight.Text == "" || timeFlight.Text == null) {
                this.setDeniedStatus(timeFlight_status);
                return false;
            }
            this.setApproveStatus(timeFlight_status);
            return true;
        }

        public bool checkVerticalSeat() {
            int value = 0;

            if (!int.TryParse(verticalSeat.Text, out value) || value < 2) {
                this.setDeniedStatus(verticalSeat_status);
                return false;
            }
            this.setApproveStatus(verticalSeat_status);
            return true;
        }

        public bool checkHorizontalSeat() {
            int value = 0;

            if (!int.TryParse(horizontalSeat.Text, out value) || value < 2) {
                this.setDeniedStatus(horizontalSeat_status);
                return false;
            }
            this.setApproveStatus(horizontalSeat_status);
            return true;
        }

        public void resetInformation() {
            setNormalStatus(origionalAP_status);
            setNormalStatus(destinationAP_status);
            setNormalStatus(price_status);
            setNormalStatus(timeFlight_status);
            setNormalStatus(verticalSeat_status);
            setNormalStatus(horizontalSeat_status);

            origionalAP.SelectedIndex = -1;
            destinationAP.SelectedIndex = -1;
            price.Clear();
            timeFlight.Text = "";
            verticalSeat.Clear();
            horizontalSeat.Clear();
        }

        private async void saveFlightData_Click(object sender, RoutedEventArgs e) {

            bool a1 = checkOrigionalAP();
            bool a2 = checkDestinationAP();
            bool a3 = checkPrice();
            bool a4 = checkTimeFlight();
            bool a5 = checkVerticalSeat();
            bool a6 = checkHorizontalSeat();

            bool res = a1 && a2 && a3 && a4 && a5 && a6;

            if (res == true) {
                Console.WriteLine("ready to post data");

                // orginAP, destinationAP
                flightToCreate.Price = price.Text;
                flightToCreate.Duration = timeFlight.Text.ToString();
                flightToCreate.Width = int.Parse(horizontalSeat.Text);
                flightToCreate.Height = int.Parse(verticalSeat.Text);
                flightToCreate.TotalSeat = flightToCreate.Width * flightToCreate.Height;

                Flight clone = new Flight();
                clone = flightToCreate;

                flightToCreate = new Flight();
                this.resetInformation();

                Response<string> sign = await FlightBusControl.Instance.CreateFlight(clone);
                string flightID = sign.Result;

                this.callTransitInputForm(flightID);
                this.refreshFlights();
            }
        }

        private async void flightDataGridView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Flight rowValue = flightDataGridView.SelectedItem as Flight;

            if (rowValue != null) {
                this.refreshTransits(rowValue.FlightID);
            }
        }

        private async Task refreshFlights() {
            List<Flight> tempList = this.flightDataGridView.ItemsSource as List<Flight>;
            if (tempList != null)
                tempList.Clear();

            this.flightDataGridView.Items.Refresh();

            flightLoadingStatus.Visibility = Visibility.Visible;

            flightList = await FlightBusControl.Instance.GetFlightAll();

            this.flightDataGridView.ItemsSource = flightList.Result;
            this.flightDataGridView.Items.Refresh();

            flightLoadingStatus.Visibility = Visibility.Hidden;
        }

        private async void refreshTransits(string _flightID) {
            List<Transit> tempList = this.flightDataGridView.ItemsSource as List<Transit>;
            if (tempList != null)
                tempList.Clear();

            this.transitDataGridView.Items.Refresh();

            transitLoadingStatus.Visibility = Visibility.Visible;

            Response<List<Transit>> transitList = await FlightBusControl.Instance.GetTransit(_flightID);

            this.transitDataGridView.ItemsSource = transitList.Result;
            this.transitDataGridView.Items.Refresh();

            transitLoadingStatus.Visibility = Visibility.Hidden;
        }

        private void flightDataModify_Click(object sender, RoutedEventArgs e) {
            object value = flightDataGridView.SelectedItem;

            this.callModifyFlightForm(ref value);

            flightDataGridView.Items.Refresh();
        }

        private async void flightDataDelete_Click(object sender, RoutedEventArgs e) {
            Flight rowValue = flightDataGridView.SelectedItem as Flight;

            await FlightBusControl.Instance.DisableFlight(rowValue);

            await Task.Factory.StartNew(() => {
                this.Dispatcher.Invoke(async () => {
                    List<Flight> tempList = flightDataGridView.ItemsSource as List<Flight>;
                    tempList.Remove(rowValue);

                    await refreshFlights();
                    searchTextbox.Clear();
                });
            });
        }

        private void transitDataModify_Click(object sender, RoutedEventArgs e) {
            object value = transitDataGridView.SelectedItem;

            this.callModifyTransit(ref value);

            transitDataGridView.Items.Refresh();
        }

        private async void transitDataDelete_Click(object sender, RoutedEventArgs e) {
            Transit rowValue = transitDataGridView.SelectedItem as Transit;

            await FlightBusControl.Instance.DisableTransit(rowValue);

            await Task.Factory.StartNew(() => {
                this.Dispatcher.Invoke(() => {
                    List<Transit> tempList = transitDataGridView.ItemsSource as List<Transit>;
                    tempList.Remove(rowValue);
                    transitDataGridView.Items.Refresh();
                });
            });
        }

        private void transitAdd_Click(object sender, RoutedEventArgs e) {
            Flight rowValue = flightDataGridView.SelectedItem as Flight;

            if (rowValue == null) {
                MessageBox.Show("hãy chọn một chuyến bay");
            }
            else {
                this.callTransitInputForm(rowValue.FlightID);
                this.refreshTransits(rowValue.FlightID);
            }
        }

        private async void transitClearAll_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult res = MessageBox.Show("Bạn có chắc muốn xóa hết dữ liệu không ?", "Caution!",
                MessageBoxButton.YesNo);

            if (res == MessageBoxResult.Yes) {
                Flight rowValue = flightDataGridView.SelectedItem as Flight;

                if (rowValue == null) {
                    MessageBox.Show("hãy chọn một chuyến bay");
                }
                else {
                    List<Transit> tempList = flightDataGridView.ItemsSource as List<Transit>;
                    if (tempList != null)
                        tempList.Clear();

                    transitDataGridView.Items.Refresh();

                    await FlightBusControl.Instance.DisableFlightTransit(rowValue);
                }
            }
        }

        private async void flightRefresh_Click(object sender, RoutedEventArgs e) {
            await this.refreshFlights();
        }

        private async void flightClearAll_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult res = MessageBox.Show("Bạn có chắc muốn xóa hết dữ liệu không ?", "Caution!",
               MessageBoxButton.YesNo);

            if (res == MessageBoxResult.Yes) {
                List<Flight> tempList = flightDataGridView.ItemsSource as List<Flight>;
                if (tempList != null)
                    tempList.Clear();
                if (flightList.Result != null)
                    flightList.Result.Clear();

                flightDataGridView.Items.Refresh();

                await FlightBusControl.Instance.DisableFlightAll();
            }
        }
    }
}
