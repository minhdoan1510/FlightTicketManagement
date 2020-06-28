using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace FlightTicketManagement.Helper
{
    class TicketManager
    {
        List<Ticket> tickets;
        private static TicketManager instance = null;
        public static TicketManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TicketManager();
                }
                return instance;
            }
        }


        public async Task GetAll()
        {
            tickets = await APIHelper<Ticket>.Instance.GetAll(ApiRoutes.Ticket.GetAll);
        }
        public List<Ticket> GetList()
        {
            return tickets;
        }

        public Ticket GetFromList(string id)
        {
            return tickets.SingleOrDefault(x => x.Id == id);
        }

        public async Task<Ticket> Get(string id)
        {
            return await APIHelper<Ticket>.Instance.Get(ApiRoutes.Ticket.Get.Replace(ApiRoutes.Key, id));
        }
        public async Task<bool> Update(Ticket passenger)
        {
            return await APIHelper<Ticket>.Instance.Update(ApiRoutes.Ticket.Update.Replace(ApiRoutes.Key, passenger.Id), passenger);
        }
        public async Task<bool> Delete(Ticket passenger)
        {
            return await APIHelper<Ticket>.Instance.Delete(ApiRoutes.Ticket.Delete.Replace(ApiRoutes.Key, passenger.Id));
        }
        public async Task<bool> Create(Ticket passenger)
        {
            return await APIHelper<Ticket>.Instance.Post(ApiRoutes.Passenger.Create, passenger);
        }

        public void Clear()
        {
            tickets.Clear();
        }
    }
}
