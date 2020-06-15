using Microsoft.AspNetCore.Mvc;
using ServerFTM.Authorization;
using ServerFTM.BUS;
using ServerFTM.DTO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;

namespace ServerFTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        public HttpResponseMessage Post(string type, string user, string pass, string name)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            if (type.Equals("login"))
            {
                if (BUS_Controls.Controls.Login(new Account(user, pass)))
                {
                    string ip = Request.Headers["X-Forwarded-For"];
                    if (string.IsNullOrEmpty(ip))
                    {
                        ip = Request.Host.Value;
                    }
                    AccessToken accessToken = new AccessToken(BUS_Controls.Controls.AccountCurrent.IDAccount, user, pass, ip);
                    responseMessage.Headers.Add("ID", accessToken.IdAccount);
                    responseMessage.Headers.Add("Name", BUS_Controls.Controls.AccountCurrent.Name);
                    responseMessage.Headers.Add("Token", accessToken.Token);
                    responseMessage.StatusCode = HttpStatusCode.OK;
                    Debug.WriteLine("IDuser " + accessToken.Token+ " login" );
                    Debug.WriteLine("Create succsess token:" + accessToken.Token);

                    return responseMessage;
                }
                else
                {
                    responseMessage.StatusCode = HttpStatusCode.Unauthorized;
                    return responseMessage;
                }
            }
            else if (type.Equals("signup"))
            {
                if (BUS_Controls.Controls.Signup(new Account()
                {
                    Name = name,
                    Username = user,
                    Password = pass
                }))
                {
                    Debug.WriteLine("Signup success User: " + user);
                    responseMessage.StatusCode = HttpStatusCode.Created;
                    return responseMessage;
                }
                else
                {
                    responseMessage.StatusCode = HttpStatusCode.NoContent;
                    return responseMessage;
                }
            }
            else
            {
                responseMessage.StatusCode = HttpStatusCode.BadRequest;
                return responseMessage;
            }
        }

        // PUT api/<AccountController>/5
        [HttpPut]
        public void Put(int idAcc, [FromBody] string value)
        {

        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
