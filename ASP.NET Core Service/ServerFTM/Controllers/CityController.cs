using API.Shared.APIResponse;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerFTM.BUS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFTM.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CityController : Controller
    {
        [HttpGet]
        
        public async Task<IActionResult> GetAll()
        {
            List<CityModel> cities = BUS_Controls.Controls.GetAllCity();

            if (cities != null)
            {
                return new JsonResult(new ApiResponse<object>(cities));
            }
            else return new JsonResult(new ApiResponse<object>(200, "found nothing"));
        }
    }
}
