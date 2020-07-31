using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GPSLocation
{
    public static class WeatherForecast
    {
        public static HttpClient Client { get; set; }
        static string API_key = "8d485977ec335bf8e79029601c107efe";
        public static string iconUrl = "http://openweathermap.org/img/w/";


        public static void initClient()
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add
                (new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static DateTime toLocal(int input) // input the unix code
        {
            DateTime UnixEpoach = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            DateTime localTime = UnixEpoach.AddSeconds(input).ToLocalTime();
            return localTime;
        }

        public static string getIconURL(string iconName)
        {
            return iconUrl + iconName + ".png";
        }

        public static async Task<string> requestIP()
        {
            initClient();
            string url = "http://checkip.dyndns.org/json";

            using (HttpResponseMessage respond = await Client.GetAsync(url))
            {
                if (respond.IsSuccessStatusCode)
                {
                    string data = await respond.Content.ReadAsStringAsync();

                    int index = data.IndexOf("Current IP Address:");
                    data = data.Substring(index + 20);

                    index = data.IndexOf('<');
                    data = data.Remove(index);

                    return data;
                }
                else
                {
                    throw new Exception("Please check your internet\n");
                }
            }
        }

        public static async Task<location_data> requestLocation()
        {
            initClient();
            string ip_address = await requestIP();
            string url = $"https://api.ipdata.co/{ip_address}" +
                $"?api-key=5eda9e6bd49b361f4bf8f23a85d3b57692ba0c963f8132c9460c4bf9";

            using (HttpResponseMessage respond = await Client.GetAsync(url))
            {
                if (respond.IsSuccessStatusCode)
                {
                    location_data data = await respond.Content.
                        ReadAsAsync<location_data>();
                    return data;
                }
                else
                {
                    throw new Exception("Please check your internet\n");
                }
            }
        }

        public static async Task<weather_data.RootObject> requestWeather()
        {
            initClient();
            location_data current_location = await requestLocation();

            string url = "http://api.openweathermap.org/data/2.5/weather";
            url += $"?lat={ current_location.latitude }&lon={ current_location.longitude }";
            url += $"&appid={API_key}";

            using (HttpResponseMessage respond = await Client.GetAsync(url))
            {
                if (respond.IsSuccessStatusCode)
                {
                    weather_data.RootObject data = await respond.Content.
                        ReadAsAsync<weather_data.RootObject>();
                    return data;
                }
                else
                {
                    throw new Exception("Please check your internet\n");
                }
            }
        }
    }

    public class ip
    {
        public string CurrentIPAddress { get; set; }
    }

    public class location_data
    {
        public double longitude { get; set; }
        public double latitude { get; set; }
    }

    public class weather_data
    {
        public class Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class Main
        {
            public double temp { get; set; }
            public double pressure { get; set; }
            public int humidity { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
        }

        public class Wind
        {
            public double speed { get; set; }
        }

        public class Clouds
        {
            public int all { get; set; }
        }

        public class Sys
        {
            public string country { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }

        public class RootObject
        {
            public Coord coord { get; set; }
            public List<Weather> weather { get; set; }
            public Main main { get; set; }
            public double visibility { get; set; }
            public Wind wind { get; set; }
            public Clouds clouds { get; set; }
            public int dt { get; set; }
            public Sys sys { get; set; }
            public string name { get; set; }
        }
    }
}
