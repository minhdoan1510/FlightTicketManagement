using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace ServerFTM.Authorization
{
    public class AccessToken
    {
        private string idAccount;
        private DateTime timeCreate;
        private string token;
        private string ip;
        private string username;

        private const string _alg = "HmacSHA256";
        private const string _salt = "TNWgHEf80cx4bUUPAdQg"; 


        public string Token { get => token; set => token = value; }
        public string IdAccount { get => idAccount; set => idAccount = value; }
        public string IP { get => ip; set => ip = value; }
        public DateTime TimeCreate { get => timeCreate; set => timeCreate = value; }
        public string Username { get => username; set => username = value; }

        public AccessToken(string idAccount, string username, string password, string _ip)
        {
            IdAccount = idAccount;
            IP = _ip;
            timeCreate = DateTime.Now;
            Token = GenerateToken(username, password, ip);
        }

        public AccessToken(string idAcc, string username, DateTime timeStamp, string token)
        {
            this.IdAccount = idAcc;
            this.Username = username;
            this.timeCreate = timeStamp;
            this.Token = token;
        }

        #region GenerateToken

        public string GenerateToken(string username, string password, string ip)
        {   
            timeCreate = DateTime.Now;
            string hash = string.Join(":", new string[] { username, ip, TimeCreate.Ticks.ToString() });
            string hashLeft = "";
            string hashRight = "";
            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                hmac.Key = Encoding.UTF8.GetBytes(GetHashedPassword(password));
                hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));
                hashLeft = Convert.ToBase64String(hmac.Hash);
                hashRight = string.Join(":", new string[] { IdAccount, username, timeCreate.Ticks.ToString(),ip});
            }
            string temp = string.Join(":", hashLeft, hashRight);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(temp));
        }

        public string GetHashedPassword(string password)
        {
            string key = string.Join(":", new string[] { password, _salt });

            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                hmac.Key = Encoding.UTF8.GetBytes(_salt);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(key));
                return Convert.ToBase64String(hmac.Hash);
            }
        }
        #endregion
    }

}
