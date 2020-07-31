using API.Shared.APIResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using ServerFTM.BUS;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Models;

namespace ServerFTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportController : ControllerBase
    {
        [HttpGet("month")]
        public async Task<ActionResult> GetMonthProfit(int month, int year) {
            var reports = BUS_Controls.Controls.GetMonthReports(month, year);
            return new JsonResult(new ApiResponse<List<ChildMonthReport>>(reports));
        }

        [HttpGet("year")]
        public async Task<ActionResult> GetYearProfit(int year) {
            var reports = BUS_Controls.Controls.GetYearReports(year);
            return new JsonResult(new ApiResponse<List<ChildYearReport>>(reports));

        }
    }
}
