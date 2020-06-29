using ServerFTM.DAL.Controls;
using ServerFTM.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerFTM.Authorization.TokenManager;
using Library.Models;
namespace ServerFTM.BUS
{
    class BUS_Controls
    {
        #region Contructor
        private static BUS_Controls controls;
        public static BUS_Controls Controls
        {
            get
            {
                if (controls == null)
                    controls = new BUS_Controls();
                return controls;
            }
            set => controls = value;
        }

      
        private BUS_Controls() { }
        
        #endregion

        #region DeviceManager
        public void AddDevice(string idUser,string token)
        {
            TokenManager.Instance.AddAccessToken(idUser, token);
        }

      

        public void DelDevice(string token)
        {
            TokenManager.Instance.DelAccessToken(token);
        }
        #endregion

        #region Account_Handle
        public Profile Login(Account account)
        {
            DataRow dataAcc = DAL_Controls.Controls.Login(account)?.Rows[0];

            if (dataAcc == null)
                return null;
            Profile accountCurrent = new Profile();
            accountCurrent.IDAccount = dataAcc["ID"].ToString();
            accountCurrent.Name = dataAcc["Name"].ToString();
            accountCurrent.Acctype = (Convert.ToInt32(dataAcc["TypeAccount"]));
            return accountCurrent;
        }
        public bool Signup(Account account)
        {
            account.IDAccount = GenerateID();
            return DAL_Controls.Controls.SignUp(account);
        }
        #endregion

        #region Flights
        public List<FlightModel> GetAllFlight()
        {
            List<FlightModel> models = new List<FlightModel>();
            DataTable dataTable = DAL_Controls.Controls.GetAllFlight();
            if (dataTable != null)
            {
                //Pass datatable from dataset to our DAL Method.
                models = DAL_Controls.Controls.CreateListFromTable<FlightModel>(dataTable);
            }
            return models;
        }
        internal List<FlightModel> GetFlightForCity(string cityId)
        {
            List<FlightModel> models = new List<FlightModel>();
            DataTable dataTable = DAL_Controls.Controls.GetFlightForCity(cityId);
            if (dataTable != null)
            {
                //Pass datatable from dataset to our DAL Method.
                models = DAL_Controls.Controls.CreateListFromTable<FlightModel>(dataTable);
            }
            return models;
        }

        #endregion

        internal List<CityModel> GetAllCity()
        {
            List<CityModel> models = new List<CityModel>();
            DataTable dataTable = DAL_Controls.Controls.GetAllCity();
            if (dataTable != null)
            {
                //Pass datatable from dataset to our DAL Method.
                models = DAL_Controls.Controls.CreateListFromTable<CityModel>(dataTable);
            }
            return models;
        }

        #region Transit
        public List<TransitModel> GetTransits(string transitId)
        {
            List<TransitModel> models = new List<TransitModel>();
            DataTable dataTable = DAL_Controls.Controls.GetTransits(transitId);
            if (dataTable != null)
            {
   
                models = DAL_Controls.Controls.CreateListFromTable<TransitModel>(dataTable);
            }
            return models;
        }
        #endregion
        #region Restriction
        public RestrictionsModel GetRestriction()
        {
            List<RestrictionsModel> models = new List<RestrictionsModel>();
            DataTable dataTable = DAL_Controls.Controls.GetRestrictions();
            if (dataTable != null)
            {

                models = DAL_Controls.Controls.CreateListFromTable<RestrictionsModel>(dataTable);
            }
            if (models.Count != 1) return null;
            return models[0];
        }

        public bool ChangeRestriction(RestrictionsModel model)
        {
            return DAL_Controls.Controls.ChangeRestrictions(model);
        }
        #endregion

        #region Utilities
        string GenerateID()
        {
            StringBuilder builder = new StringBuilder();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(10)
                .ToList().ForEach(e => builder.Append(e));
            return builder.ToString();
        }
        #endregion

    }
}
