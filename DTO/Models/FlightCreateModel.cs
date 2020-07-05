using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class FlightCreateModel
    {
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

        public string displayPrice
        {
            get
            {
                return string.Format(System.Globalization.CultureInfo.CurrentCulture,
                    "{0:n0}", double.Parse(this.Price));
            }
        }
    }
}
