using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DTO
{
    public class Flight
    {
        public string FlightId { get; set; }
        public string OriginApID { get; set; }
        public string DestinationApID { get; set; }
        public string OriginAP { get; set; }
        public string DestinationAP { get; set; }
        public double Price { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string Duration { get; set; }

        public string displayPrice {
            get {
                return string.Format("{0:n0}", this.Price);
            }
        }
        public int displayTotalSeat {
            get {
                return width * height;
            }
        }
    }
}
