using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class TransitCreateModel
    {
        public string transitID { get; set; }
        public string flightID { get; set; }
        public string airportID { get; set; }
        public string airportName { get; set; }
        public int transitOrder { get; set; }
        public string transitTime { get; set; }
        public string transitNote { get; set; }
    }
}
