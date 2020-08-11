using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

using Library.Models;
using ServerFTM.Models;

namespace FlightTicketManagement.Views
{
    /// <summary>
    /// Interaction logic for getFlight.xaml
    /// </summary>
    public partial class GetFlightView : Window
    {
        Flight updateFlight = new Flight();

        public GetFlightView(ref object dataItem) {
            InitializeComponent();

            updateFlight = dataItem as Flight;

            origionalAP.PreviewTextInput += PlaneScheduleView.Instance.Menu_TextInput;
            origionalAP.PreviewKeyDown += PlaneScheduleView.Instance.Menu_PreviewKeyDown;

            destinationAP.PreviewTextInput += PlaneScheduleView.Instance.Menu_TextInput;
            destinationAP.PreviewKeyDown += PlaneScheduleView.Instance.Menu_PreviewKeyDown;

            price.PreviewKeyDown += PlaneScheduleView.Instance.price_KeyDown;
            price.TextChanged += PlaneScheduleView.Instance.price_TextChanged;

            verticalSeat.PreviewKeyDown += PlaneScheduleView.Instance.price_KeyDown;
            horizontalSeat.PreviewKeyDown += PlaneScheduleView.Instance.price_KeyDown;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            List<AirportMenu> defaultOrigion = new List<AirportMenu>();
            List<AirportMenu> defaultDestination = new List<AirportMenu>();

            defaultOrigion.Add(new AirportMenu() {
                AirportID = updateFlight.OriginApID,
                AirportName = updateFlight.OriginAP
            });

            defaultDestination.Add(new AirportMenu() {
                AirportID = updateFlight.DestinationApID,
                AirportName = updateFlight.DestinationAP
            });

            origionalAP.ItemsSource = defaultOrigion;
            destinationAP.ItemsSource = defaultDestination;
            origionalAP.DisplayMemberPath = destinationAP.DisplayMemberPath = "AirportName";
            origionalAP.SelectedIndex = destinationAP.SelectedIndex = 0;

            price.Text = updateFlight.Price;
            timeFlight.Text = updateFlight.Duration;
            verticalSeat.Text = updateFlight.Height.ToString();
            horizontalSeat.Text = updateFlight.Width.ToString();
        }

        private void subMenu_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            AirportMenu res = (sender as ComboBox).SelectedItem as AirportMenu;

            if (res != null) {
                if ((sender as ComboBox).Name == "origionalAP") {
                    PlaneScheduleView.Instance.originId = res.AirportID;
                    updateFlight.OriginApID = res.AirportID;
                    updateFlight.OriginAP = res.AirportName;
                }
                else if ((sender as ComboBox).Name == "destinationAP") {
                    PlaneScheduleView.Instance.destinationId = res.AirportID;
                    updateFlight.DestinationApID = res.AirportID;
                    updateFlight.DestinationAP = res.AirportName;
                }
            }
        }

        public bool checkOrigionalAP() {
            if (origionalAP.SelectedIndex == -1) {
                PlaneScheduleView.Instance.setDeniedStatus(origionalAP_status);
                return false;
            }
            PlaneScheduleView.Instance.setApproveStatus(origionalAP_status);
            return true;
        }

        public bool checkDestinationAP() {
            if (destinationAP.SelectedIndex == -1) {
                PlaneScheduleView.Instance.setDeniedStatus(destinationAP_status);
                return false;
            }
            PlaneScheduleView.Instance.setApproveStatus(destinationAP_status);
            return true;
        }

        public bool checkPrice() {
            if (price.Text == "") {
                PlaneScheduleView.Instance.setDeniedStatus(price_status);
                return false;
            }
            PlaneScheduleView.Instance.setApproveStatus(price_status);
            return true;
        }

        public bool checkTimeFlight() {
            if (timeFlight.Text == "" || timeFlight.Text == null) {
                PlaneScheduleView.Instance.setDeniedStatus(timeFlight_status);
                return false;
            }
            PlaneScheduleView.Instance.setApproveStatus(timeFlight_status);
            return true;
        }

        public bool checkVerticalSeat() {
            int value = 0;

            if (!int.TryParse(verticalSeat.Text, out value) || value < 2) {
                PlaneScheduleView.Instance.setDeniedStatus(verticalSeat_status);
                return false;
            }
            PlaneScheduleView.Instance.setApproveStatus(verticalSeat_status);
            return true;
        }

        public bool checkHorizontalSeat() {
            int value = 0;

            if (!int.TryParse(horizontalSeat.Text, out value) || value < 2) {
                PlaneScheduleView.Instance.setDeniedStatus(horizontalSeat_status);
                return false;
            }
            PlaneScheduleView.Instance.setApproveStatus(horizontalSeat_status);
            return true;
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
                updateFlight.Price = price.Text;

                updateFlight.Duration = string.Format("{0:HH:mm:ss}", DateTime.Parse(timeFlight.Text));
                updateFlight.Width = int.Parse(horizontalSeat.Text);
                updateFlight.Height = int.Parse(verticalSeat.Text);
                updateFlight.TotalSeat = updateFlight.Width * updateFlight.Height;

                Flight clone = new Flight();
                clone = updateFlight;

                await FlightBusControl.Instance.UpdateFlight(clone);

                this.DialogResult = true;
            }
        }
    }
}
