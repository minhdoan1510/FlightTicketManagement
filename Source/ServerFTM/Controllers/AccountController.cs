using API.Shared.APIResponse;
using Microsoft.AspNetCore.Mvc;
using ServerFTM.Authorization;
using ServerFTM.BUS;
using ServerFTM.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ServerFTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Account
        /// </summary>
        /// <param name="type"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost("{type}")]
        public async Task<IActionResult> Post(string type, [FromBody] Account account)
        {
            if (type.Equals("login"))
            {
                Profile profile = BUS_Controls.Controls.Login(account);
                if (profile!=null)
                {
                    string ip = Request.Headers["X-Forwarded-For"];
                    if (string.IsNullOrEmpty(ip))
                        ip = Request.Host.Value;
                    AccessToken accessToken = new AccessToken(profile.IDAccount, account.Username, account.Password, ip);
                    InfoLogin infoLogin = new InfoLogin(accessToken.IdAccount, profile.Name, accessToken.Token);
                    BUS_Controls.Controls.AddDevice(accessToken.IdAccount, accessToken.Token);
                    Debug.WriteLine("IDuser " + accessToken.Token + " login");
                    Debug.WriteLine("Create succsess token:" + accessToken.Token);
                    return new JsonResult(new ApiResponse<object>(infoLogin));
                }
                else
                    return new JsonResult(new ApiResponse<object>(401, "Incorrect information"));
            }
            else if (type.Equals("signup"))
            {
                if (BUS_Controls.Controls.Signup(account))
                {
                    Debug.WriteLine("Signup success User: " + account.Username);
                    return new JsonResult(new ApiResponse<object>(201, "Account successfully created"));
                }
                else
                    return new JsonResult(new ApiResponse<object>(401, "Account creation failed"));
            }
            else
                return new JsonResult(new ApiResponse<object>(401, "Error syntax"));
        }


    }
}
