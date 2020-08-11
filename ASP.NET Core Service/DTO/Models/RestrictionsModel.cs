using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Library.Models
{
     public class RestrictionsModel
    {
		[JsonProperty("MinFlightTime")]
		[System.Text.Json.Serialization.JsonConverter(typeof(JsonTimeSpanConverter))]
		public TimeSpan MinFlightTime { get; set; }
		[JsonProperty("MaxTransit")]
		public int MaxTransit { get; set; }
		[JsonProperty("MinTransitTime")]
		[System.Text.Json.Serialization.JsonConverter(typeof(JsonTimeSpanConverter))]
		public TimeSpan MinTransitTime { get; set; }
		[JsonProperty("MaxTransitTime")]
		[System.Text.Json.Serialization.JsonConverter(typeof(JsonTimeSpanConverter))]
		public TimeSpan MaxTransitTime { get; set; }
		[JsonProperty("LatestBookingTime")]
		public int LatestBookingTime { get; set; }
		[JsonProperty("LatestCancelingTime")]
		public int LatestCancelingTime { get; set; }
	}
}
