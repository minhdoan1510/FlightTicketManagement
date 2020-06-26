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
        [HttpPost("airportMenu")]
        public async Task<IActionResult> GetAirportMenu([FromBody] AirportSearchKey value) {
            List<AirportMenu> menu = BUS_Controls.Controls.getAirportMenu(value.searchKey);

            return new JsonResult(new ApiResponse<object>(menu));
        }

        [HttpPost("createFlight")]
        public async Task<IActionResult> PostCreateFlight([FromBody] PostFlight flight) {
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

        [HttpPost("getTransit")]
        public async Task<IActionResult> GetTransit([FromBody] FlightSearchKey searchKey) {
            List<Transit> result = BUS_Controls.Controls.getTransit(searchKey.flightID);

            return new JsonResult(new ApiResponse<object>(result)); 
        }

        [HttpPost("getFlightAll")]
        public async Task<IActionResult> GetFlightAll() {
            List<Flight> result = BUS_Controls.Controls.getFlightAll();

            return new JsonResult(new ApiResponse<object>(result)); 
        }

        [HttpGet("test")]
        public async Task<IActionResult> test(string a, string b) {
            Debug.WriteLine(a + " " + b);
            return new JsonResult(new ApiResponse<object>(200, "create transit failed"));
        }
    }
}
