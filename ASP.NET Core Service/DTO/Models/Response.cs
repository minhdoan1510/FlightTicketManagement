using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class Response<T>
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("errorCode")]
        public long ErrorCode { get; set; }

        [JsonProperty("errorMessenge")]
        public string ErrorMessenge { get; set; }

        [JsonProperty("result")]
        public T Result { get; set; }

        public Response()
        {
            this.IsSuccess = true;
        }
    }
}
