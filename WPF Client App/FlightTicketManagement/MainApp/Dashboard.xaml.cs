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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Maps.MapControl.WPF;

using DTO;
using FlightTicketManagement.BUS;
using FlightTicketManagement.Helper;

namespace FlightTicketManagement
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        public Dashboard() {
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e) {
            this.welcomeUser.Text = AuthenticatedUser.Instance.data.UserName; 

            if (this.welcomeUser.Text == "") {
                this.welcomeUser.Text = "Username"; 
            }
            this.loadWeather();
            this.loadStatistics();
            this.loadFLightMap();
        }

        private async void loadWeather() {
            WeatherWaiting.Visibility = Visibility.Visible;

            GPSLocation.weather_data.RootObject weatherData = await
                GPSLocation.WeatherForecast.requestWeather();

            string weatherIcon = GPSLocation.WeatherForecast.getIconURL
                (weatherData.weather[0].icon);

            Console.WriteLine(weatherIcon);

            BitmapImage bitmap = new BitmapImage();
            await Task.Factory.StartNew(() => {
                this.Dispatcher.Invoke(() => {
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(weatherIcon, UriKind.Absolute);
                    bitmap.EndInit();

                    weatherStatusIcon.Source = bitmap;
                    weatherStatusIcon.Stretch = Stretch.Fill;
                });
            }); 

            WeatherWaiting.Visibility = Visibility.Hidden;

            if (weatherData.name == "" || weatherData.name == " " || weatherData.name == null) 
                weatherData.name = "undefined location name";

            weatherCity.Text = weatherData.name + ", " + weatherData.sys.country;
            weatherTemerature.Text = ((int)(weatherData.main.temp - 272.15f)).ToString()
                + "°C, " + weatherData.weather[0].description;
        }

        private async void loadStatistics() {
            TicketWaiting.Visibility = Visibility.Visible;
            ticketImage.Visibility = Visibility.Hidden;

            MoneyWaiting.Visibility = Visibility.Visible;
            moneyImage.Visibility = Visibility.Hidden;

            string date = string.Format("{0:yyyy/dd/MM HH:mm:ss tt}", DateTime.Now);

            Response<DashStatistic> result = await BusControl.Instance.GetDashBoardStatistic(date);

            TicketWaiting.Visibility = Visibility.Hidden;
            MoneyWaiting.Visibility = Visibility.Hidden;
            ticketImage.Visibility = Visibility.Visible;
            moneyImage.Visibility = Visibility.Visible;

            dailyTicket.Text = result.Result.dailyTicket.ToString();
            dailyMoney.Text = result.Result.displayDailyMoney;
        }

        private async void loadFLightMap() {
            flightMapWating.Visibility = Visibility.Visible;

            Response<List<FlightRoute>> result = await BusControl.Instance.GetFlightRoutes();

            foreach(var item in result.Result) {
                Pushpin newPin = new Pushpin();
                newPin.Location = new Location();

                newPin.Location.Latitude = item.latOrigin;
                newPin.Location.Longitude = item.lonOrigin;

                flightMap.Children.Add(newPin);
            }

            flightMapWating.Visibility = Visibility.Hidden;
        }

        private void flightMap_MouseWheel(object sender, MouseWheelEventArgs e) {
            e.Handled = true;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            Console.WriteLine(e.NewValue); 
            flightMap.ZoomLevel = e.NewValue;
        }
    }
}
