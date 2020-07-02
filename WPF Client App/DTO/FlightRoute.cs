using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class FlightRoute
    {
        public string flightID { get; set; }
        public float latOrigin { get; set; }
        public float lonOrigin { get; set; }
        public float latDestination { get; set; }
        public float lonDestination { get; set; }

        List<TransitLocation> transitList { get; set; }
    }

    public class TransitLocation
    {
        public float transitLat { get; set; }
        public float transitLon { get; set; }
    }
}
