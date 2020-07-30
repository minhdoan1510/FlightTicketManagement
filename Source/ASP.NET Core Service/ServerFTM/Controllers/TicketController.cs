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
    public class TicketController : ControllerBase
    {
        [HttpPost("add")]
        public async Task<IActionResult> AddTicket([FromBody] Ticket ticket)
        {
            StringValues tokenRequest;
            bool isToken = Request.Headers.TryGetValue("token", out tokenRequest);
            if (BUS_Controls.Controls.CheckDevice(tokenRequest))
            {
                if (BUS_Controls.Controls.AddTicket(ticket))
                    return new JsonResult(new ApiResponse<object>(true));
                else
                    return new JsonResult(new ApiResponse<object>(false));
            }
            else
                return new JsonResult(new ApiResponse<object>(401, "Unauthorized"));
        }

        [HttpGet("getprice")]
        public async Task<IActionResult> GetPrice(string iddur, string idclass)
        {
            StringValues tokenRequest;
            bool isToken = Request.Headers.TryGetValue("token", out tokenRequest);
            if (BUS_Controls.Controls.CheckDevice(tokenRequest))
            {
                return new JsonResult(new ApiResponse<int>(BUS_Controls.Controls.GetPrice(iddur, idclass)));
            }
            else
                return new JsonResult(new ApiResponse<object>(401, "Unauthorized"));
        }
        [HttpGet("LoadStateChair")]
        public async Task<IActionResult> LoadStateChair(string time, string id) 
        {
            DateTime timeDur = new DateTime(Convert.ToInt64(time));
            List<ChairBooking> chairBookings =  BUS_Controls.Controls.GetListChair(id, timeDur);
            return new JsonResult(new ApiResponse<List<ChairBooking>>(chairBookings));
        }
    }
}
