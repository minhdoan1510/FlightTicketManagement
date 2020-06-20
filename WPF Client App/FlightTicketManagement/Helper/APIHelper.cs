using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using DTO;

namespace FlightTicketManagement.Helper
{
    public class APIHelper<T>
    {
        private InfoLogin.Result userInfo;
        private HttpClient apiClient { get ; set ; }
        private APIHelper()
        {
            InitializeClient();
        }
        private static APIHelper<T> instance = null;
        public static APIHelper<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new APIHelper<T>();
                }
                return instance;
            }
        }

        private InfoLogin.Result authenticatedUser {
            get { return this.userInfo; }
            set { this.userInfo = value; }
        }

        private void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["api"];

            apiClient = new HttpClient();
            apiClient.BaseAddress =new Uri(api);
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
      
        public async Task<T> Get (string route)
        {
            this.InitializeClient();

            // add token to header 
            apiClient.DefaultRequestHeaders.Add("token", authenticatedUser.Token);

            using (HttpResponseMessage response = await apiClient.GetAsync(route))
            {

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsAsync<T>();
                    if(data != null)
                        return data;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
                return default;
            }
        }

        public async Task<T> Post(string route, object body)
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
                else {
                    throw new Exception(response.ReasonPhrase);
                }
                return default;
            }
        }
    }
}
