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
using FlightTicketManagement.Views;
using System.Numerics;

namespace FlightTicketManagement.Views
{
    /// <summary>
    /// Interaction logic for getTransit.xaml
    /// </summary> 
    /// 
    enum transitSign
    {
        createTransit,
        modifyTransit
    }

    public partial class GetTransitView : Window
    {
        public string flightID;

        int mode;

        Transit transitData = new Transit();

        public GetTransitView(string _flightID) {
            InitializeComponent();

            this.flightID = _flightID;
            mode = (int)transitSign.createTransit;

            airport.PreviewTextInput += PlaneScheduleView.Instance.Menu_TextInput;
            airport.PreviewKeyDown += PlaneScheduleView.Instance.Menu_PreviewKeyDown;
        }

        public GetTransitView(ref object value) {
            InitializeComponent();

            transitData = value as Transit;
            this.flightID = transitData.flightID;
            mode = (int)transitSign.modifyTransit;

            airport.PreviewTextInput += PlaneScheduleView.Instance.Menu_TextInput;
            airport.PreviewKeyDown += PlaneScheduleView.Instance.Menu_PreviewKeyDown;

            this.loadData();
        }

        void loadData() {
            List<AirportMenu> defaultAP = new List<AirportMenu>();

            defaultAP.Add(new AirportMenu() {
                AirportID = transitData.airportID,
                AirportName = transitData.airportName
            });
            airport.ItemsSource = defaultAP;
            airport.DisplayMemberPath = "AirportName";
            airport.SelectedIndex = 0;

            timeTransit.Text = transitData.transitTime;
            Note.Text = transitData.transitNote;
        }

        private bool checkAirport() {
            if (airport.SelectedIndex == -1) {
                PlaneScheduleView.Instance.setDeniedStatus(airport_status);
                return false;
            }
            PlaneScheduleView.Instance.setApproveStatus(airport_status);
            return true;
        }

        private bool checkTimeTransit() {
            if (timeTransit.Text == "" || timeTransit.Text == null) {
                PlaneScheduleView.Instance.setDeniedStatus(timeTransit_status);
                return false;
            }
            PlaneScheduleView.Instance.setApproveStatus(timeTransit_status);
            return true;
        }

        private void airport_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Console.WriteLine("airplane selected");

            AirportMenu res = (sender as ComboBox).SelectedItem as AirportMenu;

            if (res == null)
                return;

            if (res.AirportID == PlaneScheduleView.Instance.originId ||
                res.AirportID == PlaneScheduleView.Instance.destinationId) {
                MessageBox.Show("Transit không được trùng với điểm khởi hành/kết thúc !");

                (sender as ComboBox).SelectedValue = null;
                (sender as ComboBox).SelectedIndex = -1;
                return;
            }

            transitData.airportID = res.AirportID;
            transitData.airportName = res.AirportName;
        }

        private void resetInformation() {
            PlaneScheduleView.Instance.setNormalStatus(airport_status);
            PlaneScheduleView.Instance.setNormalStatus(timeTransit_status);

            airport.SelectedIndex = -1;
            timeTransit.Text = "";
        }

        private async void saveBtn_Click(object sender, RoutedEventArgs e) {
            bool a1 = checkAirport();
            bool a2 = checkTimeTransit();

            bool res = a1 && a2;

            if (res == true) {
                Console.WriteLine("ready to post data");

                transitData.flightID = flightID;
                transitData.transitTime = timeTransit.Text.ToString();

                if (Note.Text == null || Note.Text == "")
                    transitData.transitNote = "không có ghi chú";
                else transitData.transitNote = Note.Text;

                if (mode == (int)transitSign.createTransit) {
                    await FlightBusControl.Instance.CreateTransit(transitData);
                }
                else if (mode == (int)transitSign.modifyTransit) {
                    await FlightBusControl.Instance.UpdateTransit(transitData);

                    this.DialogResult = true;
                    return;
                }

                this.transitData = new Transit();
                this.resetInformation();
            }
        }
    }
}
