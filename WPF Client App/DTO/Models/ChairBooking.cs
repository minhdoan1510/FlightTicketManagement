using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFTM.Models
{
    public enum ChairStatus { Booked, NonBooking, }
    public enum ChairType { VIP = 1, Classic = 2 }

    public class ChairBooking
    {
        public string IDchair { get; set; }
        public ChairStatus Status { get; set; }
        public ChairType TypeChair { get; set; }
    }
}
