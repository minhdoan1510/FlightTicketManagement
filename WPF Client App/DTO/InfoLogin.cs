using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DTO
{
    public class InfoLogin
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("errorCode")]
        public long ErrorCode { get; set; }

        [JsonProperty("errorMessenge")]
        public string ErrorMessenge { get; set; }

        [JsonProperty("result")]
        public Result Result { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
