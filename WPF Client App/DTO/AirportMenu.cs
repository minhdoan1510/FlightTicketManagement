using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DTO
{
    public class AirportMenu
    {
        [JsonProperty("AirportID")]
        public string AirportID { get; set; }

        [JsonProperty("AirportName")]
        public string AirportName { get; set; }
    }
}
