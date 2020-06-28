using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFTM.Models
{
    public class ChildMonthReport
    {
        private int rank;
        private string idFight;
        private string originName;
        private string destinationName;
        private string originID;
        private string destinationID;
        private int ticketNum;
        private float ratio;
        private int profit;

        public int Rank { get => rank; set => rank = value; }
        public string IdFight { get => idFight; set => idFight = value; }
        public string OriginID { get => originID; set => originID = value; }
        public string OriginName { get => originName; set => originName = value; }
        public string DestinationID { get => destinationID; set => destinationID = value; }
        public string DestinationName { get => destinationName; set => destinationName = value; }
        public int TicketNum { get => ticketNum; set => ticketNum = value; }
        public float Ratio { get => ratio; set => ratio = value; }
        public int Profit { get => profit; set => profit = value; }
    }
}
