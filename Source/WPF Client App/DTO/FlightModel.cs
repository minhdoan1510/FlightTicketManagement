using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Models
{
    public class FlightModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("originAPId")]
        public string OriginAPId { get; set; }

        [JsonProperty("originAP")]
        public string OriginAP { get; set; }

        [JsonProperty("destAPId")]
        public string DestAPId { get; set; }

        [JsonProperty("destAP")]
        public string DestAP { get; set; }

        [JsonProperty("totalSeat")]
        public int TotalSeat { get; set; }

        [JsonProperty("Time")]
        public DateTime Time { get; set; }
        // public int NumOfTransit { get; set; } public class InfoLogin

    }
}

