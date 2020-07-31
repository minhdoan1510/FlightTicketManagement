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

using ServerFTM.Models;
using FlightTicketManagement.Helper;
using Library.Models;

namespace FlightTicketManagement.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
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

            string date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            Response<DashStatistic> result = await FlightBusControl.Instance.GetDashBoardStatistic(date);

            TicketWaiting.Visibility = Visibility.Hidden;
            MoneyWaiting.Visibility = Visibility.Hidden;
            ticketImage.Visibility = Visibility.Visible;
            moneyImage.Visibility = Visibility.Visible;

            dailyTicket.Text = result.Result.dailyTicket.ToString();
            dailyMoney.Text = result.Result.displayDailyMoney;
        }

        private async void loadFLightMap() {
            flightMapWating.Visibility = Visibility.Visible;

            Response<List<FlightRoute>> result = await FlightBusControl.Instance.GetFlightRoutes();

            await Task.Factory.StartNew(() => {
                foreach (var item in result.Result) {

                    this.Dispatcher.Invoke(() => {
                        Pushpin originPin = createOriginPin(item);
                        Pushpin destinationPin = createDestinationPin(item);

                        List<Pushpin> transitPin = createTransitPin(item.transitList);
                        MapPolyline path = createPolygonLine(item);

                        flightMap.Children.Add(path);
                        flightMap.Children.Add(originPin);
                        flightMap.Children.Add(destinationPin);

                        foreach (var transitPoint in transitPin) {
                            flightMap.Children.Add(transitPoint);
                        }
                    });
                }
            });

            flightMapWating.Visibility = Visibility.Hidden;
        }

        private MapPolyline createPolygonLine(FlightRoute item) {
            MapPolyline result = new MapPolyline();
            result.Stroke = Brushes.Black;
            result.StrokeThickness = 1;
            result.Opacity = 0.8f;

            LocationCollection line = new LocationCollection();

            line.Add(new Location() {
                Latitude = item.latOrigin,
                Longitude = item.lonOrigin
            });

            foreach (TransitLocation trans in item.transitList) {
                line.Add(new Location() {
                    Latitude = trans.transitLat,
                    Longitude = trans.transitLon
                });
            }

            line.Add(new Location() {
                Latitude = item.latDestination,
                Longitude = item.lonDestination
            });
            result.Locations = line;

            return result;
        }

        private Pushpin createOriginPin(FlightRoute item) {
            Pushpin originPin = new Pushpin();
            originPin.Background = Brushes.Green;
            originPin.Location = new Location();

            originPin.Location.Latitude = item.latOrigin;
            originPin.Location.Longitude = item.lonOrigin;
            originPin.ToolTip = item.originName;

            return originPin;
        }

        private Pushpin createDestinationPin(FlightRoute item) {
            Pushpin destinationPin = new Pushpin();
            destinationPin.Background = Brushes.Red;
            destinationPin.Location = new Location() {
                Latitude = item.latDestination,
                Longitude = item.lonDestination
            };
            destinationPin.ToolTip = item.destinationName;

            return destinationPin;
        }

        private List<Pushpin> createTransitPin(List<TransitLocation> transitLocation) {
            List<Pushpin> result = new List<Pushpin>();

            foreach (TransitLocation item in transitLocation) {
                Pushpin transitPin = new Pushpin();
                transitPin.Background = Brushes.Yellow;
                transitPin.Location = new Location() {
                    Latitude = item.transitLat,
                    Longitude = item.transitLon
                };
                transitPin.ToolTip = item.transitName;

                result.Add(transitPin);
            }
            return result;
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
