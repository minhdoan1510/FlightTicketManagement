using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFTM.Models
{
    public class Flight {
        public string FlightID { get; set; }
        public string OriginApID { get; set; }
        public string DestinationApID { get; set; }
        public string OriginAP { get; set; }
        public string DestinationAP { get; set; }
        public string Price { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int TotalSeat { get; set; }
        public string DurationID { get; set; }
        public string Duration { get; set; }
    }
}
