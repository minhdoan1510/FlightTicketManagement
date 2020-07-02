using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Device.Location;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GPSLocation
{
    public static class WeatherForecast
    {
        public static HttpClient Client { get; set; }
        static string API_key = "8d485977ec335bf8e79029601c107efe";
        public static string iconUrl = "http://openweathermap.org/img/w/";


        public static void initClient() {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();

            Client.DefaultRequestHeaders.Accept.Add
                (new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.116 Safari/537.36"); 
        }

        public static DateTime toLocal(int input) // input the unix code
        {
            DateTime UnixEpoach = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            DateTime localTime = UnixEpoach.AddSeconds(input).ToLocalTime();
            return localTime;
        }

        public static string getIconURL(string iconName) {
            return iconUrl + iconName + ".png";
        }

        public static async Task<IPAdress> requestIP() {
            initClient();

            string url = "http://api.myip.com/";

            using (HttpResponseMessage respond = await Client.GetAsync(url)) {
                if (respond.IsSuccessStatusCode) {
                    string temp = await respond.Content.ReadAsStringAsync();
                    IPAdress data = JsonConvert.DeserializeObject<IPAdress>(temp);

                    Console.WriteLine(data.ip);

                    return data;
                }
                else {
                    throw new Exception("Please check your internet\n");
                }
            }
        }

        public static async Task<location_data> requestLocation() {
            initClient();

            IPAdress ip_address = await requestIP();

            string url = $"http://geolocation-db.com/json/" +
                $"697de680-a737-11ea-9820-af05f4014d91/" +
                $"{ip_address.ip}";

            Console.WriteLine(url); 

            using (HttpResponseMessage respond = await Client.GetAsync(url)) {
                if (respond.IsSuccessStatusCode) {
                    string dataString = respond.Content.ReadAsStringAsync().Result;
                    location_data data = JsonConvert.DeserializeObject<location_data>(dataString);

                    return data;
                }
                else {
                    throw new Exception("Please check your internet\n");
                }
            }
        }

        public static async Task<weather_data.RootObject> requestWeather() {
            initClient();

            location_data coord = await requestLocation();

            string url = "http://api.openweathermap.org/data/2.5/weather";
            url += $"?lat={ coord.latitude }&lon={ coord.longitude }";
            url += $"&appid={API_key}";

            Console.WriteLine(url);

            using (HttpResponseMessage respond = await Client.GetAsync(url)) {
                if (respond.IsSuccessStatusCode) {
                    weather_data.RootObject data = await respond.Content.
                        ReadAsAsync<weather_data.RootObject>();
                    data.sys.country = coord.country_name;
                    data.name = coord.city;
                    return data;
                }
                else {
                    throw new Exception("Please check your internet\n");
                }
            }

        }
    }

    public class IPAdress
    {
        [JsonProperty("ip")]
        public string ip { get; set; }
    }

    public class location_data
    {
        public string country_name { get; set; }
        public string city { get; set; }
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
