using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManagement.EventModels
{
    public class CreateTicketEvent
    {
        public CreateTicketEvent(string id) {
            this.flightId = id;
        }
        public string flightId { get; set; }
    }
}
