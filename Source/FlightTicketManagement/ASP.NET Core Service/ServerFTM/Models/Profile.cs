using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerFTM.Models
{
    public class Profile
    {
        public string IDAccount { get; set; }
        public string Name { get; set; }
        public int Acctype { get; set; }
        public IFormFile Avatar { get; set; }
        public Profile() { }
    }
}
