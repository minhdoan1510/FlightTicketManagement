using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Shared.APIResponse;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using ServerFTM.BUS;
using ServerFTM.Models;

namespace ServerFTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        [HttpPost("AddTicket")]
        public async Task<IActionResult> AddTicket([FromBody] Ticket ticket) {
            if (BUS_Controls.Controls.AddTicket(ticket))
                return new JsonResult(new ApiResponse<object>("Add Ticket Ok"));
            else
                return new JsonResult(new ApiResponse<object>(200, "Add Ticket Failed"));
        }

        [HttpPost("AddPassenger")]
        public async Task<IActionResult> AddPassenger([FromBody] Passenger passenger) {
            string result = BUS_Controls.Controls.AddPassenger(passenger);

            if (result != "")
                return new JsonResult(new ApiResponse<object>(result));
            else
                return new JsonResult(new ApiResponse<object>(200, "Add Passenger Failed"));
        }

        [HttpGet("GetPrice/durationId={durationId}/classId={classId}")]
        public async Task<IActionResult> GetPrice(string durationId, string classId) {
            return new JsonResult(new ApiResponse<object>(BUS_Controls.Controls.GetPrice(durationId, classId)));
        }

        [HttpGet("GetFlightInfo/flightId={id}")]
        public async Task<IActionResult> GetFlightInfo(string id) {
            FlightInfo result = BUS_Controls.Controls.GetFlightInfo(id); 

            if (result != null) {
                return new JsonResult(new ApiResponse<object>(result)); 
            }
            return new JsonResult(new ApiResponse<object>(200, "Get Flight Info Failed")); 
        }

        [HttpPost("GetChairState")]
        public async Task<IActionResult> GetChairState([FromBody] ChairRequest value) {
            ChairState result = BUS_Controls.Controls.GetChairState(value); 

            if (result != null) {
                return new JsonResult(new ApiResponse<object>(result)); 
            }
            return new JsonResult(new ApiResponse<object>(200, "Get Chair State Failed")); 
        }
    }
}
