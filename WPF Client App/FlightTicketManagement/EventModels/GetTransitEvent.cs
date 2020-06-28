using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManagement.EventModels
{
    public class GetTransitEvent
    {
        public string FlightId { get; private set; }
        public GetTransitEvent(string flightId)
        {
            FlightId = flightId;
        }

    }
}
