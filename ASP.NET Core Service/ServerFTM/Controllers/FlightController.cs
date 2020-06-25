
using API.Shared.APIResponse;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using ServerFTM.BUS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFTM.Controllers
{
 
        [Route("api/[controller]")]
        [ApiController]
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

  
        }
    
}
