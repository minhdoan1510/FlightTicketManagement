using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Library.Models
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
        private string fullName;

        public int Rank { get => rank; set => rank = value; }
        public string IdFight { get => idFight; set => idFight = value; }
        public string OriginID { get => originID; set => originID = value; }
        public string OriginName { get => originName; set { originName = value; FullName = string.Format("{0} - {1}", originName, destinationName); } }
        public string DestinationID { get => destinationID; set => destinationID = value; }
        public string DestinationName { get => destinationName; set { destinationName = value; FullName = string.Format("{0} - {1}", originName, destinationName); } }
        public int TicketNum { get => ticketNum; set => ticketNum = value; }
        public float Ratio { get => ratio; set => ratio = value; }
        public int Profit { get => profit; set => profit = value; }
        public string FullName { get => fullName; set => fullName = value; }
    }
}
