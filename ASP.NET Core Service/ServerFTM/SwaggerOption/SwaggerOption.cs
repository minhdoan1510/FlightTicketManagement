using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFTM
{
    public class SwaggerOption
    {
        [JsonProperty("JsonRoute")]
        public string JsonRoute { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("UIEndpoint")]
        public string UIEndpoint { get; set; }
    }
}
