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

        PostFlight flightToCreate = new PostFlight();

        List<Flight> flightListForDataGrid { get; set; }

        public PlaneSchedule Instance {
            get {
                return this; 
            }
        }

        public PlaneSchedule() {
            InitializeComponent();

            airportList.Result = new List<AirportMenu>();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e) {
            flightList = await BUS.BusControl.Instance.GetFlightAll();

            this.flightDataGridView.ItemsSource = flightList.Result;
            this.flightDataGridView.Items.Refresh();
        }

        private void setApproveStatus(UIElement icon) {
            var status = icon as MaterialDesignThemes.Wpf.PackIcon;

            status.Kind = MaterialDesignThemes.Wpf.PackIconKind.Check;
            status.Foreground = Brushes.Green;
        }

        private void setDeniedStatus(UIElement icon) {
            var status = icon as MaterialDesignThemes.Wpf.PackIcon;

            status.Kind = MaterialDesignThemes.Wpf.PackIconKind.Cancel;
            status.Foreground = Brushes.Red;
        }

        private void setNormalStatus(UIElement icon) {
            var status = icon as MaterialDesignThemes.Wpf.PackIcon;

            status.Kind = MaterialDesignThemes.Wpf.PackIconKind.Check;
            status.Foreground = Brushes.Transparent;
        }

        private void callTransitInputForm(string _flightID) {
            getTransit transitForm = new getTransit(_flightID);
            transitForm.WindowStartupLocation = WindowStartupLocation.CenterOwner; 
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
                airportList = await BUS.BusControl.Instance.GetAirportList(searchKey);
            });

            (sender as ComboBox).ItemsSource = airportList.Result;
            (sender as ComboBox).DisplayMemberPath = "AirportName";
            (sender as ComboBox).IsDropDownOpen = true;
        }

        private void Menu_TextInput(object sender, TextCompositionEventArgs e) {
            var textBox = e.OriginalSource as TextBox;

            textBox.SelectionLength = 0;
            textBox.SelectionStart = textBox.Text.Length;
        }

        private async void Menu_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Space || e.Key == Key.Enter) {
                var textBox = e.OriginalSource as TextBox;

                textBox.SelectionLength = 0;
                textBox.SelectionStart = textBox.Text.Length;
            }
            menuGetData(sender);
        }

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Console.WriteLine("airplane selected");

            AirportMenu res = (sender as ComboBox).SelectedItem as AirportMenu;

            if (res == null)
                return;

            if ((sender as ComboBox).Name == "origionalAP") {
                flightToCreate.originAP = res.AirportID;

                Console.WriteLine("origion: " + res.AirportID);
            }
            else if ((sender as ComboBox).Name == "destinationAP") {
                flightToCreate.destinationAP = res.AirportID;

                Console.WriteLine("destination: " + res.AirportID);
            }
        }

        private void price_KeyDown(object sender, KeyEventArgs e) {
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

        private void price_TextChanged(object sender, TextChangedEventArgs e) {
            string text = (sender as TextBox).Text;
            double value = 0.0f;

            if (text.Length > 2 && double.TryParse(text, out value)) {
                (sender as TextBox).Text = string.Format(System.Globalization.CultureInfo.CurrentCulture,
                    "{0:n0}", value);
                (sender as TextBox).CaretIndex = (sender as TextBox).Text.Length;
            }
        }

        private bool checkOrigionalAP() {
            if (origionalAP.SelectedIndex == -1) {
                this.setDeniedStatus(origionalAP_status);
                return false;
            }
            this.setApproveStatus(origionalAP_status);
            return true;
        }

        private bool checkDestinationAP() {
            if (destinationAP.SelectedIndex == -1) {
                this.setDeniedStatus(destinationAP_status);
                return false;
            }
            this.setApproveStatus(destinationAP_status);
            return true;
        }

        private bool checkPrice() {
            if (price.Text == "") {
                this.setDeniedStatus(price_status);
                return false;
            }
            this.setApproveStatus(price_status);
            return true;
        }

        private bool checkTimeFlight() {
            if (timeFlight.Text == "" || timeFlight.Text == null) {
                this.setDeniedStatus(timeFlight_status);
                return false;
            }
            this.setApproveStatus(timeFlight_status);
            return true;
        }

        private bool checkVerticalSeat() {
            int value = 0;

            if (!int.TryParse(verticalSeat.Text, out value) || value < 2) {
                this.setDeniedStatus(verticalSeat_status);
                return false;
            }
            this.setApproveStatus(verticalSeat_status);
            return true;
        }

        private bool checkHorizontalSeat() {
            int value = 0;

            if (!int.TryParse(horizontalSeat.Text, out value) || value < 2) {
                this.setDeniedStatus(horizontalSeat_status);
                return false;
            }
            this.setApproveStatus(horizontalSeat_status);
            return true;
        }

        private void resetInformation() {
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
                flightToCreate.price = price.Text;
                flightToCreate.duration = timeFlight.Text.ToString();
                flightToCreate.width = int.Parse(horizontalSeat.Text);
                flightToCreate.height = int.Parse(verticalSeat.Text);
                flightToCreate.totalSeat = flightToCreate.width * flightToCreate.height;

                PostFlight clone = new PostFlight();
                clone = flightToCreate;

                flightToCreate = new PostFlight();
                this.resetInformation();

                Response<string> sign = await BUS.BusControl.Instance.CreateFlight(clone);
                string flightID = sign.Result;

                this.callTransitInputForm(flightID); 
            }
        }

        private async void flightDataGridView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Flight rowValue = flightDataGridView.SelectedItem as Flight;

            Console.WriteLine(rowValue.FlightId);

            transitList = await BUS.BusControl.Instance.GetTransit(rowValue.FlightId);
            transitDataGridView.ItemsSource = transitList.Result;
            transitDataGridView.Items.Refresh();
        }
    }
}
