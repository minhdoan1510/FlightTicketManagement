using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace FlightTicketManagement.Helper
{
    class TransitManager
    {
        List<Transit> transits;
        private static TransitManager instance = null;
        public static TransitManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TransitManager();
                }
                return instance;
            }
        }


        public async Task GetAll()
        {
            transits = await APIHelper<Transit>.Instance.GetAll(ApiRoutes.Transit.GetAll);
        }
        public List<Transit> GetList()
        {
            return transits;
        }

        public async Task<Transit> Get(string id)
        {
            return await APIHelper<Transit>.Instance.Get(ApiRoutes.Transit.Get.Replace(ApiRoutes.Key, id));
        }
        public async Task<bool> Update(Transit passenger)
        {
            return await APIHelper<Transit>.Instance.Update(ApiRoutes.Transit.Update.Replace(ApiRoutes.Key, passenger.Id), passenger);
        }
        public async Task<bool> Delete(Transit passenger)
        {
            return await APIHelper<Transit>.Instance.Delete(ApiRoutes.Transit.Delete.Replace(ApiRoutes.Key, passenger.Id));
        }
        public async Task<bool> Create(Transit passenger)
        {
            return await APIHelper<Transit>.Instance.Post(ApiRoutes.Transit.Create, passenger);
        }

        public void Clear()
        {
            transits.Clear();
        }
    }
}
