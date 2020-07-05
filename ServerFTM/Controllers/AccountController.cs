﻿using API.Shared.APIResponse;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServerFTM.BUS;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServerFTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        

        private IConfiguration _config;

        public AccountController(IConfiguration config)
        {
            _config = config;
        }

        private string GenerateJSONWebToken(Profile profile) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,profile.Name),
                new Claim(JwtRegisteredClaimNames.Jti,profile.IDAccount)
            };

            var token = new JwtSecurityToken(null, null,
                                        claims,
                                        expires: DateTime.Now.AddMinutes(120),
                                        signingCredentials: credentials);
            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodeToken;
        }

        [HttpPost("login")]
        public async Task<IActionResult> PostLogin([FromBody] UserAccount account) {
            Profile profile = BUS_Controls.Controls.Login(account);

            if (profile != null) {
                var tokenStr = GenerateJSONWebToken(profile);
                InfoLogin infoLogin = new InfoLogin { 
                     Id = profile.IDAccount, Name =  profile.Name, Token = tokenStr };
                return new JsonResult(new ApiResponse<object>(infoLogin));
            }
            else return new JsonResult(new ApiResponse<object>(200, "login failed"));
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> PostSignUp([FromBody] UserAccount account) {
            if (BUS_Controls.Controls.Signup(account)) {
                Debug.WriteLine("Signup success User: " + account.Username);

                return new JsonResult(new ApiResponse<object>("ok"));
            }
            else return new JsonResult(new ApiResponse<object>(200, "signup failed"));
        }
    }
}
