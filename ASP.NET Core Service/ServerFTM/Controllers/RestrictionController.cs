using API.Shared.APIResponse;
using Library.Models;
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
    public class RestrictionsController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            RestrictionsModel restriction = BUS_Controls.Controls.GetRestriction();

            if (restriction != null)
            {
                return new JsonResult(new ApiResponse<object>(restriction));
            }
            else return new JsonResult(new ApiResponse<object>(200, "found nothing"));
        }


    }
}
