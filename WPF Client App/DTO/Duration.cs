using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Duration
    {
        public string IDFlight { get; set; }
        public TimeSpan TimeDuration { get; set; }
        public string IDDurationFlight { get; set; }
        public string AmmountBooked { get; set; }
    }
}
