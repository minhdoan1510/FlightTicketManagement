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

using FlightTicketManagement.BUS;
using DTO; 

namespace FlightTicketManagement.SupportForm
{
    /// <summary>
    /// Interaction logic for getTransit.xaml
    /// </summary>
    public partial class getTransit : Window
    {
        public string flightID;
        Response<List<AirportMenu>> airportList = new Response<List<AirportMenu>>();
        Transit transitToCreate = new Transit();

        public getTransit(string _flightID) {
            this.flightID = _flightID;
            InitializeComponent();

            airportList.Result = new List<AirportMenu>();
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

        private void airport_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            var textBox = e.OriginalSource as TextBox;

            textBox.SelectionLength = 0;
            textBox.SelectionStart = textBox.Text.Length;
        }

        private bool checkAirport() {
            if (airport.SelectedIndex == -1) {
                this.setDeniedStatus(airport_status);
                return false;
            }
            this.setApproveStatus(airport_status);
            return true;
        }

        private bool checkTimeTransit() {
            if (timeTransit.Text == "" || timeTransit.Text == null) {
                this.setDeniedStatus(timeTransit_status);
                return false;
            }
            this.setApproveStatus(timeTransit_status);
            return true;
        }

        private async void airport_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Space || e.Key == Key.Enter) {
                var textBox = e.OriginalSource as TextBox;

                textBox.SelectionLength = 0;
                textBox.SelectionStart = textBox.Text.Length;
            }
            
            menuGetData(sender);
        }

        private void airport_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Console.WriteLine("airplane selected");

            AirportMenu res = (sender as ComboBox).SelectedItem as AirportMenu;

            if (res == null)
                return;

            transitToCreate.airportID = res.AirportID;
        }

        private void resetInformation() {
            setNormalStatus(airport_status);
            setNormalStatus(timeTransit_status);

            airport.SelectedIndex = -1;
            timeTransit.Text = "";
        }

        private async void saveBtn_Click(object sender, RoutedEventArgs e) {
            bool a1 = checkAirport();
            bool a2 = checkTimeTransit(); 

            bool res = a1 && a2;
                
            if (res == true) {
                Console.WriteLine("ready to post data");

                transitToCreate.flightID = flightID;
                transitToCreate.transitTime = timeTransit.Text.ToString();

                if (Note.Text == null)
                    transitToCreate.transitNote = "không có ghi chú"; 
                else transitToCreate.transitNote = Note.Text;

                await BusControl.Instance.CreateTransit(transitToCreate);

                this.transitToCreate = new Transit(); 
                this.resetInformation();
            }
        }
    }
}
