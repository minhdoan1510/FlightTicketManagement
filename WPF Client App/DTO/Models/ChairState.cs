using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFTM.Models
{
    public class ChairState
    {
        public int width { get; set; }
        public int height { get; set; }

        public List<ChairPos> chairBooked { get; set; }
    }

    public class ChairPos
    {
        public int posX { get; set; }
        public int posY { get; set; }
    }
}
