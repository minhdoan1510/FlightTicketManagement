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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RestrictionsModel model)
        {
            if(BUS_Controls.Controls.ChangeRestriction(model))
            {
                return new JsonResult(new ApiResponse<object>("Ok"));
            }
            else return new JsonResult(new ApiResponse<object>(200, "cant change"));

        }
    }
}
