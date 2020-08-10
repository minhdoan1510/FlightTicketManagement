using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class FlightInfo
    {
        public string originID { get; set; }
        public string originName { get; set; }
        public string destinationID { get; set; }
        public string destinationName { get; set; }
        public string durationID { get; set; }
        public string flightTime { get; set; }
    }
}
