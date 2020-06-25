using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Library.Models
{
    public class Signup
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("errorCode")]
        public long ErrorCode { get; set; }

        [JsonProperty("errorMessenge")]
        public string ErrorMessenge { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
