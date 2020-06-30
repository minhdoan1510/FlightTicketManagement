
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
            List<FlightModel> flights = BUS_Controls.Controls.GetAllFlight();

            if (flights != null)
            {
                return new JsonResult(new ApiResponse<object>(flights));
            }
            else return new JsonResult(new ApiResponse<object>(200, "found nothing"));
        }
        [HttpGet("{cityId}")]
        public async Task<IActionResult> GetForCity([FromRoute]string cityId)
        {
            List<FlightModel> flights = BUS_Controls.Controls.GetFlightForCity(cityId);

            if (flights != null)
            {
                return new JsonResult(new ApiResponse<object>(flights));
            }
            else return new JsonResult(new ApiResponse<object>(200, "found nothing"));
        }


    }
    
}
