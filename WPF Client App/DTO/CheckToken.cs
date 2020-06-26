using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DTO
{
    public class CheckToken
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("data")]
        public string data { get; set; }
    }
}
