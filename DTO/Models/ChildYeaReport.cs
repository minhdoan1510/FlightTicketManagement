using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class ChildYearReport
    {
        private int month;
        private int ticketNum;
        private int profit;
        private float ratio;

        public int Month { get => month; set => month = value; }
        public int TicketNum { get => ticketNum; set => ticketNum = value; }
        public int Profit { get => profit; set => profit = value; }
        public float Ratio { get => ratio; set => ratio = value; }
    }
}
