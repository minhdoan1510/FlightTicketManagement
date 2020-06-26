using API.Shared.APIResponse;
using Microsoft.AspNetCore.Mvc;
using ServerFTM.Authorization;
using ServerFTM.BUS;
using ServerFTM.Models;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ServerFTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> PostLogin([FromBody] Account account) {
            Profile profile = BUS_Controls.Controls.Login(account);

            if (profile != null) {
                string ip = Request.Headers["X-Forwarded-For"];
                if (string.IsNullOrEmpty(ip))
                    ip = Request.Host.Value;

                AccessToken accessToken = new AccessToken(profile.IDAccount, account.Username, account.Password, ip);
                InfoLogin infoLogin = new InfoLogin(accessToken.IdAccount, profile.Name, accessToken.Token);

                Debug.WriteLine("IDuser " + accessToken.Token + " login");
                Debug.WriteLine("Create succsess token:" + accessToken.Token);

                return new JsonResult(new ApiResponse<object>(infoLogin));
            }
            else return new JsonResult(new ApiResponse<object>(200, "login failed"));
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> PostSignUp([FromBody] Account account) {
            if (BUS_Controls.Controls.Signup(account)) {
                Debug.WriteLine("Signup success User: " + account.Username);

                return new JsonResult(new ApiResponse<object>("ok"));
            }
            else return new JsonResult(new ApiResponse<object>(200, "signup failed"));
        }

        [HttpPost("testTime")]
        public async Task<IActionResult> PostTestTime() {

            return new JsonResult(new ApiResponse<object>(BUS_Controls.Controls.testTime())); 
        }
    }
}
