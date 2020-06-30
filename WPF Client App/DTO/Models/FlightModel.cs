﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
    }

    public class FlightModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("originCityId")]
        public string OriginCityId { get; set; }
        [JsonProperty("originCity")]
        public string OriginCity { get; set; }

        [JsonProperty("originAPId")]
        public string OriginAPId { get; set; }

        [JsonProperty("originAP")]
        public string OriginAP { get; set; }
        [JsonProperty("destCityId")]
        public string DestCityId { get; set; }
        [JsonProperty("destCity")]
        public string DestCity { get; set; }
        [JsonProperty("destAPId")]
        public string DestAPId { get; set; }

        [JsonProperty("destAP")]
        public string DestAP { get; set; }
        [JsonProperty("totalSeat")]
        public int TotalSeat { get; set; }

        [JsonProperty("transitNum")]
        public int TransitNum { get; set; }
        [JsonProperty("Time")]
        [System.Text.Json.Serialization.JsonConverter(typeof(JsonTimeSpanConverter))]
        public TimeSpan Time { get; set; }

      
 
        public bool  IsEnabled
        {
            get {
                return TransitNum > 0;
            }
           
        }

    }
}
