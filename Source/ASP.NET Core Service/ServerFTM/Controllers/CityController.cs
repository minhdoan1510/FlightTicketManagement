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
    public class CityController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAPinCity(string idlocal)
        {
            StringValues tokenRequest;
            bool isToken = Request.Headers.TryGetValue("token", out tokenRequest);
            if (BUS.BUS_Controls.Controls.CheckDevice(tokenRequest.ToString()))
            {
                List<City> airports = BUS_Controls.Controls.GetCityAlready(idlocal);
                return new JsonResult(new ApiResponse<List<City>>(airports));
            }
            return new JsonResult(new ApiResponse<object>(401, "Unauthorized"));
        }
    }
}
