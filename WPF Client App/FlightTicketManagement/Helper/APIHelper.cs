using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Library.Models;

namespace FlightTicketManagement.Helper
{
    public class APIHelper
    {
        private InfoLogin userInfo;
        private HttpClient apiClient { get; set; }
        private APIHelper()
        {
            InitializeClient();
        }
        private static APIHelper instance = null;
        public static APIHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new APIHelper();
                }
                return instance;
            }
        }

        private InfoLogin authenticatedUser
        {
            get { return this.userInfo; }
            set { this.userInfo = value; }
        }

        private void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];

            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri(api);
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if(userInfo!=null)
            {
                apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer { userInfo.Token}");
            }
        }

        public async Task<T> Get<T>(string route)
        {
            this.InitializeClient();

            using (HttpResponseMessage response = await apiClient.GetAsync(route))
            {

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsAsync<T>();
                    if (data != null)
                        return data;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
                return default;
            }
        }

        public async Task<T> Post<T>(string route, object body = null)
        {
            this.InitializeClient();
   
            using (HttpResponseMessage response = await apiClient.PostAsJsonAsync(route, body))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsAsync<T>();
                    if (data != null)
                        return data;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
                return default;
            }
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            this.InitializeClient();
            UserAccount account = new UserAccount
            {
                Username = username,
                Password = password

            };
            using (HttpResponseMessage response = await apiClient.PostAsJsonAsync(ApiRoutes.Account.LogIn, account))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsAsync<Response<InfoLogin>>();
                    if (data != null)
                    {
                        userInfo = data.Result;
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
