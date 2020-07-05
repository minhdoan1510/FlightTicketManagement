
using API.Shared.APIResponse;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using ServerFTM.BUS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ServerFTM.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FlightController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<FlightDisplayModel> flights = BUS_Controls.Controls.GetAllFlight();

            if (flights != null)
            {
                return new JsonResult(new ApiResponse<object>(flights));
            }
            else return new JsonResult(new ApiResponse<object>(200, "found nothing"));
        }
        [HttpGet("{cityId}")]
        public async Task<IActionResult> GetForCity([FromRoute]string cityId)
        {
            List<FlightDisplayModel> flights = BUS_Controls.Controls.GetFlightForCity(cityId);

            if (flights != null)
            {
                return new JsonResult(new ApiResponse<object>(flights));
            }
            else return new JsonResult(new ApiResponse<object>(200, "found nothing"));
        }

        [HttpPost("createFlight")]
        public async Task<IActionResult> PostCreateFlight([FromBody] FlightCreateModel flight)
        {
            string flightID = BUS_Controls.Controls.createFlight(flight);

            if (flightID != "")
            {
                return new JsonResult(new ApiResponse<object>(flightID));
            }
            return new JsonResult(new ApiResponse<object>(200, "create flight failed"));
        }

        [HttpPost("createTransit")]
        public async Task<IActionResult> PostCreateTransit([FromBody] TransitCreateModel transit)
        {

            if (BUS_Controls.Controls.createTransit(transit))
            {
                return new JsonResult(new ApiResponse<object>("OK"));
            }
            return new JsonResult(new ApiResponse<object>(200, "create transit failed"));
        }

        [HttpGet("airportMenu/Search={search}")]
        public async Task<IActionResult> GetAirportMenu(string search)
        {
            List<AirportMenu> menu = BUS_Controls.Controls.getAirportMenu(search);

            return new JsonResult(new ApiResponse<object>(menu));
        }

        [HttpGet("getTransit/ID={id}")]
        public async Task<IActionResult> GetTransit(string id)
        {
            List<TransitCreateModel> result = BUS_Controls.Controls.getTransit(id);

            return new JsonResult(new ApiResponse<object>(result));
        }

        [HttpPost("updateFlight")]
        public async Task<IActionResult> UpdateFlight([FromBody] FlightCreateModel value)
        {
            if (BUS_Controls.Controls.UpdateFlight(value))
            {
                return new JsonResult(new ApiResponse<object>("OK"));
            }
            return new JsonResult(new ApiResponse<object>(200, "udate flight failed"));
        }

        [HttpPost("disableFlight")]
        public async Task<IActionResult> DisableFlight([FromBody] FlightCreateModel value)
        {
            if (BUS_Controls.Controls.DisableFlight(value))
            {
                return new JsonResult(new ApiResponse<object>("OK"));
            }
            return new JsonResult(new ApiResponse<object>(200, "disable failed"));
        }

        [HttpPost("updateTransit")]
        public async Task<IActionResult> UpdateTransit([FromBody] TransitCreateModel value)
        {
            if (BUS_Controls.Controls.UpdateTransit(value))
            {
                return new JsonResult(new ApiResponse<object>("OK"));
            }
            return new JsonResult(new ApiResponse<object>(200, "update transit failed"));
        }

        [HttpPost("disableTransit")]
        public async Task<IActionResult> DisableTransit([FromBody] TransitCreateModel value)
        {
            if (BUS_Controls.Controls.DisableTransit(value))
            {
                return new JsonResult(new ApiResponse<object>("OK"));
            }
            return new JsonResult(new ApiResponse<object>(200, "disable transit failed"));
        }

        [HttpPost("disableFlightTransit")]
        public async Task<IActionResult> DisableFlightTransit([FromBody] FlightCreateModel value)
        {
            if (BUS_Controls.Controls.DisableFlightTransit(value))
            {
                return new JsonResult(new ApiResponse<object>("OK"));
            }
            return new JsonResult(new ApiResponse<object>(200, "disable FlightTransit failed"));
        }

        [HttpPost("disableFlightAll")]
        public async Task<IActionResult> DisableFlightAll()
        {
            if (BUS_Controls.Controls.DisableFlightAll())
            {
                return new JsonResult(new ApiResponse<object>("OK"));
            }
            return new JsonResult(new ApiResponse<object>(200, "disable FlightTransit failed"));
        }

        [HttpGet("getFlightAll")]
        public async Task<IActionResult> GetFlightAll()
        {
            List<FlightCreateModel> result = BUS_Controls.Controls.getFlightAll();

            return new JsonResult(new ApiResponse<object>(result));
        }

        [HttpGet("getDashStatistic/Date={date}")]
        public async Task<IActionResult> GetDashStatistic(string date)
        {
            DashStatistic result = BUS_Controls.Controls.GetDashStatistic(date);

            return new JsonResult(new ApiResponse<object>(result));
        }

        [HttpGet("getFlightRoute")]
        public async Task<IActionResult> GetFlightRoute()
        {
            List<FlightRoute> result = BUS_Controls.Controls.GetFlightRoute();

            return new JsonResult(new ApiResponse<object>(result));
        }
    }
    
}
