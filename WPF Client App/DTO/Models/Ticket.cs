using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFTM.Models
{
    public class Ticket
    {
        public string IDTicket { get; set; }
        public string IDPassenger { get; set; }
        public string IDDurationFlight { get; set; }
        public string IDClass { get; set; }
        public DateTime TimeBooking { get; set; }
        public DateTime TimeFlight { get; set; }
        public int IsPaid { get; set; }
        public int XChair { get; set; }
        public int YChair { get; set; }
        public string IDChairBooked { get; set; }
    }
}
