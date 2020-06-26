using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFTM.Models
{
    public class PostFlight
    {
        public string flightID { get; set; }
        public string durationFlightID { get; set; }
        public string originAP { get; set; }
        public string destinationAP { get; set; }
        public int totalSeat { get; set; }
        public string price { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string duration { get; set; }
    }
}
