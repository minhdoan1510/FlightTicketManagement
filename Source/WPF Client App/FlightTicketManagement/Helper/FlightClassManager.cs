using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace FlightTicketManagement.Helper
{
    class FlightClassManager
    {
        List<Class> classes;
        private static FlightManager instance = null;
        public static FlightManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FlightManager();
                }
                return instance;
            }
        }


        public async Task GetAllClass()
        {
            classes = await APIHelper<Class>.Instance.GetAll(ApiRoutes.Flight.GetAll);
        }
        public List<Class> GetClassList()
        {
            return classes;
        }

        public Class GetFromList(string id)
        {
            return classes.SingleOrDefault(x => x.Id == id);
        }

        public async Task<Class> Get(string id)
        {
            return await APIHelper<Class>.Instance.Get(ApiRoutes.Flight.GetAll.Replace(ApiRoutes.Key, id));
        }
        public async Task<bool> Update(Class fclass)
        {
            return await APIHelper<Class>.Instance.Update(ApiRoutes.Flight.Update.Replace(ApiRoutes.Key, fclass.Id), fclass);
        }
        public async Task<bool> Delete(Class fclass)
        {
            return await APIHelper<Class>.Instance.Delete(ApiRoutes.Flight.Update.Replace(ApiRoutes.Key, fclass.Id));
        }
    }
}
