using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace FlightTicketManagement.Helper
{
    class CityManager
    {
        List<City> cities;
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


        public async Task GetAllFlight()
        {
            cities = await APIHelper<City>.Instance.GetAll(ApiRoutes.Flight.GetAll);
        }
        public List<City> GetFlightList()
        {
            return cities;
        }

        public City GetFromList(string id)
        {
            return cities.SingleOrDefault(x => x.Id == id);
        }

        public async Task<City> Get(string id)
        {
            return await APIHelper<City>.Instance.Get(ApiRoutes.City.GetAll.Replace(ApiRoutes.Key, id));
        }
        public async Task<bool> Update(City ct)
        {
            return await APIHelper<City>.Instance.Update(ApiRoutes.City.Update.Replace(ApiRoutes.Key, ct.Id), ct);
        }
        public async Task<bool> Delete(City ct)
        {
            return await APIHelper<City>.Instance.Delete(ApiRoutes.City.Update.Replace(ApiRoutes.Key, ct.Id));
        }

    }
}
