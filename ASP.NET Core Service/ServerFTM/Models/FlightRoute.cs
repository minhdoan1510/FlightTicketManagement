using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFTM.Models
{
    public class FlightRoute
    {
        public string flightID { get; set; }
        public float latOrigin { get; set; }
        public float lonOrigin { get; set; }
        public float latDestination { get; set; }
        public float lonDestination { get; set; }
        public string originName { get; set; }
        public string destinationName { get; set; }

        public List<TransitLocation> transitList { get; set; }
    }

    public class TransitLocation
    {
        public float transitLat { get; set; }
        public float transitLon { get; set; }
        public string transitName { get; set; }
    }
}
