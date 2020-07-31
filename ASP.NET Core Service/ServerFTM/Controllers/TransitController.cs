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
    public class TransitController : Controller
    {
        [HttpGet("{flightId}")]
        public async Task<IActionResult> Get([FromRoute] string flightId) {
            List<TransitDisplayModel> transits = BUS_Controls.Controls.GetTransits(flightId);

            if (transits != null) {
                return new JsonResult(new ApiResponse<object>(transits));
            }
            else return new JsonResult(new ApiResponse<object>(200, "found nothing"));
        }
    }
}
