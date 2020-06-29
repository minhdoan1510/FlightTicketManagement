using API.Shared.APIResponse;
using Microsoft.AspNetCore.Mvc;
using ServerFTM.Authorization;
using ServerFTM.BUS;
using ServerFTM.Models;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ServerFTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        [HttpPost("createFlight")]
        public async Task<IActionResult> PostCreateFlight([FromBody] Flight flight) {
            string flightID = BUS_Controls.Controls.createFlight(flight);
            
            if (flightID != "") {
                return new JsonResult(new ApiResponse<object>(flightID)); 
            }
            return new JsonResult(new ApiResponse<object>(200, "create flight failed"));
        }

        [HttpPost("createTransit")]
        public async Task<IActionResult> PostCreateTransit([FromBody] Transit transit) {

            if (BUS_Controls.Controls.createTransit(transit)) {
                return new JsonResult(new ApiResponse<object>("OK"));
            }
            return new JsonResult(new ApiResponse<object>(200, "create transit failed"));
        }

        [HttpGet("airportMenu/Search={search}")]
        public async Task<IActionResult> GetAirportMenu(string search) {
            List<AirportMenu> menu = BUS_Controls.Controls.getAirportMenu(search);

            return new JsonResult(new ApiResponse<object>(menu));
        }

        [HttpGet("getTransit/ID={id}")]
        public async Task<IActionResult> GetTransit(string id) {
            List<Transit> result = BUS_Controls.Controls.getTransit(id);

            return new JsonResult(new ApiResponse<object>(result)); 
        }

        [HttpPost("updateFlight")]
        public async Task<IActionResult> UpdateFlight([FromBody] Flight value) {
            if (BUS_Controls.Controls.UpdateFlight(value)) {
                return new JsonResult(new ApiResponse<object>("OK")); 
            }
            return new JsonResult(new ApiResponse<object>(200, "udate flight failed")); 
        }

        [HttpPost("disableFlight")]
        public async Task<IActionResult> DisableFlight([FromBody] Flight value) {
            if (BUS_Controls.Controls.DisableFlight(value)) {
                return new JsonResult(new ApiResponse<object>("OK")); 
            }
            return new JsonResult(new ApiResponse<object>(200, "disable failed")); 
        }

        [HttpPost("updateTransit")]
        public async Task<IActionResult> UpdateTransit([FromBody] Transit value) {
            if (BUS_Controls.Controls.UpdateTransit(value)) {
                return new JsonResult(new ApiResponse<object>("OK")); 
            }
            return new JsonResult(new ApiResponse<object>(200, "update transit failed")); 
        }

        [HttpPost("disableTransit")]
        public async Task<IActionResult> DisableTransit([FromBody] Transit value) {
            if (BUS_Controls.Controls.DisableTransit(value)) {
                return new JsonResult(new ApiResponse<object>("OK")); 
            }
            return new JsonResult(new ApiResponse<object>(200, "disable transit failed")); 
        }

        [HttpPost("disableFlightTransit")]
        public async Task<IActionResult> DisableFlightTransit([FromBody] Flight value) {
            if (BUS_Controls.Controls.DisableFlightTransit(value)) {
                return new JsonResult(new ApiResponse<object>("OK"));
            }
            return new JsonResult(new ApiResponse<object>(200, "disable FlightTransit failed"));
        }

        [HttpPost("disableFlightAll")]
        public async Task<IActionResult> DisableFlightAll() {
            if (BUS_Controls.Controls.DisableFlightAll()) {
                return new JsonResult(new ApiResponse<object>("OK"));
            }
            return new JsonResult(new ApiResponse<object>(200, "disable FlightTransit failed"));
        }

        [HttpGet("getFlightAll")]
        public async Task<IActionResult> GetFlightAll() {
            List<Flight> result = BUS_Controls.Controls.getFlightAll();

            return new JsonResult(new ApiResponse<object>(result)); 
        }
    }
}
