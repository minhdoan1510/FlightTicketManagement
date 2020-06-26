using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFTM.Models
{
    public class Flight {
        public string FlightId { get; set; }
        public string OriginApID { get; set; }
        public string DestinationApID { get; set; }
        public string OriginAP { get; set; }
        public string DestinationAP { get; set; }
        public double Price { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string Duration { get; set; }
    }
}
