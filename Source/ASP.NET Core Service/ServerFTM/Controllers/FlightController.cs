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
    public class FlightController : ControllerBase
    {
        [HttpGet("durationtime")]
        public async Task<ActionResult> GetDurationFlight(string iddestap, string idoriap)
        {
            StringValues tokenRequest;
            bool isToken = Request.Headers.TryGetValue("token", out tokenRequest);
            if (BUS.BUS_Controls.Controls.CheckDevice(tokenRequest.ToString()))
            {
                List<DurationTime> durtimes = BUS_Controls.Controls.GetDurationFlight(idoriap, iddestap);
                return new JsonResult(new ApiResponse<List<DurationTime>>(durtimes));
            }
            return new JsonResult(new ApiResponse<object>(401, "Unauthorized"));
        }

        [HttpGet("DefineChairFlight")]
        public async Task<ActionResult> GetDefineChairFlight(string id)
        {
            StringValues tokenRequest;
            bool isToken = Request.Headers.TryGetValue("token", out tokenRequest);
            if (true)//BUS.BUS_Controls.Controls.CheckDevice(tokenRequest.ToString()))
            {
                KeyValuePair<int,int> DefineChairFlight = BUS_Controls.Controls.GetDefineChairFlight(id);
                return new JsonResult(new ApiResponse<KeyValuePair<int, int>>(DefineChairFlight));
            }
            return new JsonResult(new ApiResponse<object>(401, "Unauthorized"));
        }
    }
}
