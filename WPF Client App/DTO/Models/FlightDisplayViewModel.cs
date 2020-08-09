using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Library.Models
{
    public class FlightDisplayModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("originCityId")]
        public string OriginCityId { get; set; }
        [JsonProperty("originCity")]
        public string OriginCity { get; set; }

        [JsonProperty("originAPId")]
        public string OriginAPId { get; set; }

        [JsonProperty("originAP")]
        public string OriginAP { get; set; }
        [JsonProperty("destCityId")]
        public string DestCityId { get; set; }
        [JsonProperty("destCity")]
        public string DestCity { get; set; }
        [JsonProperty("destAPId")]
        public string DestAPId { get; set; }

        [JsonProperty("destAP")]
        public string DestAP { get; set; }
        [JsonProperty("totalSeat")]
        public int TotalSeat { get; set; }

        [JsonProperty("transitNum")]
        public int TransitNum { get; set; }
        [JsonProperty("Time")]
        [System.Text.Json.Serialization.JsonConverter(typeof(JsonTimeSpanConverter))]
        public TimeSpan Time { get; set; }

        private bool _IsEnabled;
        public bool IsEnabled {
            get {
                _IsEnabled = (TransitNum > 0);
                return _IsEnabled;
            }
            set {
                _IsEnabled = value;
            }
        }
    }
}

