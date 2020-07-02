using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Library.Models
{
    public class UserAccount
    {
        [JsonProperty("idAccount")]
        public string IdAccount { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
