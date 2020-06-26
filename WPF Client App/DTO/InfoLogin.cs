using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DTO
{
    public partial class InfoLogin
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
