using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Shared.APIResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using ServerFTM.BUS;
using ServerFTM.Models;

namespace ServerFTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        [HttpGet("GetExist")]
        public async Task<IActionResult> GetExistPassenger(string tel)
        {
            StringValues tokenRequest;
            bool isToken = Request.Headers.TryGetValue("token", out tokenRequest);
            if (BUS.BUS_Controls.Controls.CheckDevice(tokenRequest.ToString()))
            {
                Passenger passenger = BUS_Controls.Controls.GetExistPassenger(tel);
                if (passenger == null)
                {
                    return new JsonResult(new ApiResponse<object>(false));
                }
                else
                    return new JsonResult(new ApiResponse<Passenger>(passenger));
            }
            return new JsonResult(new ApiResponse<object>(401, "Unauthorized"));
        }

        [HttpPost("AddPassenger")]
        public async Task<IActionResult> GetAddPassenger([FromBody]Passenger passenger)
        {
            StringValues tokenRequest;
            bool isToken = Request.Headers.TryGetValue("token", out tokenRequest);
            if (BUS.BUS_Controls.Controls.CheckDevice(tokenRequest.ToString()))
            {
                if( BUS_Controls.Controls.GetAddPassenger(passenger))
                    return new JsonResult(new ApiResponse<object>(true));
                else
                    return new JsonResult(new ApiResponse<object>(false));
            }
            return new JsonResult(new ApiResponse<object>(401, "Unauthorized"));
        }
    }
}
