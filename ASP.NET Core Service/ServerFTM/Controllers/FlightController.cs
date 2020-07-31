using API.Shared.APIResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using ServerFTM.BUS;
using ServerFTM.Models;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

using Library.Models;

namespace ServerFTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FlightController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            List<FlightDisplayModel> flights = BUS_Controls.Controls.GetAllFlight();

            if (flights != null) {
                return new JsonResult(new ApiResponse<object>(flights));
            }
            else return new JsonResult(new ApiResponse<object>(200, "found nothing"));
        }

        [HttpGet("{cityId}")]
        public async Task<IActionResult> GetForCity([FromRoute] string cityId) {
            List<FlightDisplayModel> flights = BUS_Controls.Controls.GetFlightForCity(cityId);

            if (flights != null) {
                return new JsonResult(new ApiResponse<object>(flights));
            }
            else return new JsonResult(new ApiResponse<object>(200, "found nothing"));
        }

        [HttpGet("getFlightRoute/{flightId}")]
        public async Task<IActionResult> GetFlightRoute([FromRoute] string flightId) {
            List<FlightRoute> result = BUS_Controls.Controls.GetFlightRoute(flightId);

            return new JsonResult(new ApiResponse<object>(result));
        }

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

        [HttpPost("getDashStatistic")]
        public async Task<IActionResult> GetDashStatistic([FromBody] DashDate value) {
            DashStatistic result = BUS_Controls.Controls.GetDashStatistic(value.date);

            return new JsonResult(new ApiResponse<object>(result)); 
        }

        [HttpGet("getFlightRoute")]
        public async Task<IActionResult> GetFlightRoute() {
            List<FlightRoute> result = BUS_Controls.Controls.GetFlightRoute();

            return new JsonResult(new ApiResponse<object>(result)); 
        }
    }
}
