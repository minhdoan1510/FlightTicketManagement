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

using FlightTicketManagement.BUS;
using FlightTicketManagement.Helper;

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
            WeatherIconWaiting.Visibility = Visibility.Visible;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
         //   this.welcomeUser.Text = AuthenticatedUser.Instance.data.UserName;

            if (this.welcomeUser.Text == "")
            {
                this.welcomeUser.Text = "Username";
            }
            this.loadWeather();
        }

        private async void loadWeather()
        {
            GPSLocation.WeatherForecast.initClient();

            GPSLocation.weather_data.RootObject weatherData = await
                GPSLocation.WeatherForecast.requestWeather();

            string weatherIcon = GPSLocation.WeatherForecast.getIconURL
                (weatherData.weather[0].icon);

            Console.WriteLine(weatherIcon);

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(weatherIcon, UriKind.Absolute);
            bitmap.EndInit();

            weatherStatusIcon.Source = bitmap;
            weatherStatusIcon.Stretch = Stretch.Fill;
            WeatherIconWaiting.Visibility = Visibility.Hidden;

            weatherCity.Text = weatherData.name + ", " + weatherData.sys.country;
            weatherTemerature.Text = ((int)(weatherData.main.temp - 272.15f)).ToString()
                + "°C";
        }
    }
}
