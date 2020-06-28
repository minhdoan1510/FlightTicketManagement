using API.Shared.APIResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using ServerFTM.BUS;
using ServerFTM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerFTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpGet("month")]
        public async Task<ActionResult> GetMonthProfit(int month, int year)
        {
            StringValues tokenRequest;
            bool isToken = Request.Headers.TryGetValue("token", out tokenRequest);

            if (BUS_Controls.Controls.CheckDevice(tokenRequest.ToString()))
            {
                var reports = BUS_Controls.Controls.GetMonthReports(month, year);
                return new JsonResult(new ApiResponse<List<ChildMonthReport>>(reports));
            }
            else
            {
                return new JsonResult(new ApiResponse<object>(401, "unauthorized access"));
            }
        }

        [HttpGet("year")]
        public async Task<ActionResult> GetYearProfit(int year)
        {
            StringValues tokenRequest;
            bool isToken = Request.Headers.TryGetValue("token", out tokenRequest);

            if (BUS_Controls.Controls.CheckDevice(tokenRequest.ToString()))
            {
                var reports = BUS_Controls.Controls.GetYearReports(year);
                return new JsonResult(new ApiResponse<List<ChildYearReport>>(reports));
            }
            else
            {
                return new JsonResult(new ApiResponse<object>(401, "unauthorized access"));
            }
        }
    }
}
