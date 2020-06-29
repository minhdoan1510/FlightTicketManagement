using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

using DTO;
using FlightTicketManagement.SupportForm;

namespace FlightTicketManagement
{
    /// <summary>
    /// Interaction logic for planeSchedule.xaml
    /// </summary>
    ///

    public partial class PlaneSchedule : UserControl
    {
        Response<List<AirportMenu>> airportList = new Response<List<AirportMenu>>();
        Response<List<Flight>> flightList = new Response<List<Flight>>();
        Response<List<Transit>> transitList = new Response<List<Transit>>();

        Flight flightToCreate = new Flight();

        List<Flight> flightListForDataGrid { get; set; }

        public static PlaneSchedule Instance;

        public PlaneSchedule() {
            Instance = this;
            InitializeComponent();

            airportList.Result = new List<AirportMenu>();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e) {
            this.refreshFlights();
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
            getTransit transitForm = new getTransit(_flightID);
            transitForm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            transitForm.ShowDialog();
        }

        private void callModifyFlightForm(ref object value) {
            getFlight flightForm = new getFlight(ref value);
            flightForm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            flightForm.ShowDialog();
        }

        private void callModifyTransit(ref object value) {
            getTransit transitForm = new getTransit(ref value);
            transitForm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            transitForm.ShowDialog();
        }

        private async void menuGetData(object sender) {
            string searchKey = "A";

            if ((sender as ComboBox).Text != "") {
                searchKey = (sender as ComboBox).Text;
            }
            else {
                airportList.Result.Clear();
                (sender as ComboBox).IsDropDownOpen = false;
                return;
            }

            await Task.Factory.StartNew(async () => {
                airportList = await BUS.BusControl.Instance.GetAirportMenu(searchKey);

                foreach (var item in airportList.Result) {
                    Console.WriteLine(item.AirportName);
                }
            });

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

                Console.WriteLine("origin: " + res.AirportID);
            }
            else if ((sender as ComboBox).Name == "destinationAP") {
                flightToCreate.DestinationApID = res.AirportID;

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

                Response<string> sign = await BUS.BusControl.Instance.CreateFlight(clone);
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

        private async void refreshFlights() {
            if (flightList.Result != null)
                flightList.Result.Clear();
            this.flightDataGridView.Items.Refresh();

            flightList = await BUS.BusControl.Instance.GetFlightAll();

            this.flightDataGridView.ItemsSource = flightList.Result;
            this.flightDataGridView.Items.Refresh();
        }

        private async void refreshTransits(string _flightID) {
            if (transitList.Result != null)
                transitList.Result.Clear();
            this.transitDataGridView.Items.Refresh();

            transitList = await BUS.BusControl.Instance.GetTransit(_flightID);

            this.transitDataGridView.ItemsSource = transitList.Result;
            this.transitDataGridView.Items.Refresh();
        }

        private void flightDataModify_Click(object sender, RoutedEventArgs e) {
            object value = flightDataGridView.SelectedItem;

            this.callModifyFlightForm(ref value);

            flightDataGridView.Items.Refresh();
        }

        private async void flightDataDelete_Click(object sender, RoutedEventArgs e) {
            Flight rowValue = flightDataGridView.SelectedItem as Flight;

            await BUS.BusControl.Instance.DisableFlight(rowValue);

            await Task.Factory.StartNew(() => {
                this.Dispatcher.Invoke(() => {
                    flightList.Result.Remove(rowValue);
                    flightDataGridView.Items.Refresh();
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

            await BUS.BusControl.Instance.DisableTransit(rowValue);

            await Task.Factory.StartNew(() => {
                this.Dispatcher.Invoke(() => {
                    transitList.Result.Remove(rowValue);
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
                    transitList.Result.Clear();
                    transitDataGridView.Items.Refresh();

                    await BUS.BusControl.Instance.DisableFlightTransit(rowValue);
                }
            }
        }

        private void flightRefresh_Click(object sender, RoutedEventArgs e) {
            this.refreshFlights();
        }

        private async void flightClearAll_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult res = MessageBox.Show("Bạn có chắc muốn xóa hết dữ liệu không ?", "Caution!",
               MessageBoxButton.YesNo);

            if (res == MessageBoxResult.Yes) {
                flightList.Result.Clear();
                flightDataGridView.Items.Refresh();

                await BUS.BusControl.Instance.DisableFlightAll();
            }
        }
    }
}
