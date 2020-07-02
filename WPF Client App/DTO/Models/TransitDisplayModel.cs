using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Library.Models
{
    public class TransitDisplayModel
    {
        [JsonProperty("Number")]
        public int Number { get; set; }
        [JsonProperty("APName")]
        public string APName { get; set; }
        [JsonProperty("CityName")]
        public string CityName { get; set; }
        [JsonProperty("CountryName")]
        public string CountryName { get; set; }
        [JsonProperty("Time")]
        [System.Text.Json.Serialization.JsonConverter(typeof(JsonTimeSpanConverter))]
        public TimeSpan Time { get; set; }
       
        [JsonProperty("Note")]
        public string Note { get; set; }

    }
}
