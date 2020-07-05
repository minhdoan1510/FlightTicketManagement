using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class AirportMenu
    {
        [JsonProperty("AirportID")]
        public string AirportID { get; set; }

        [JsonProperty("AirportName")]
        public string AirportName { get; set; }
    }
}
