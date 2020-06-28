using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Models;
using System.Diagnostics;

namespace FlightTicketManagement.Helper
{
    public class APIHelper
    {
        private InfoLogin userInfo;
        private HttpClient apiClient { get; set; }
        private string accesstoken;
        public string Accesstoken { get => accesstoken; set => accesstoken = value; }
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

        private APIHelper()
        {
            InitializeClient();            
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
        }

        public async Task<T> Get<T>(string route, KeyValuePair<string, string>[] parameter = null)
        {
            this.InitializeClient();

            // add token to header 
            apiClient.DefaultRequestHeaders.Add("token", accesstoken);//authenticatedUser.Result.Token);

            if (parameter != null)
            {
                for (int i = 0; i < parameter.Length; i++)
                {
                    route += ((i == 0) ? '?' : '&');
                    route += string.Format("{0}={1}", parameter[i].Key, parameter[i].Value);
                } 
            }

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

        public async Task<Response<InfoLogin>> PostLoginAsync(string route, object body = null)
        {
            this.InitializeClient();
            using (HttpResponseMessage response = await apiClient.PostAsJsonAsync(route, body))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsAsync<Response<InfoLogin>>();
                    if (data.Result != null)
                    {
                        accesstoken = data.Result.Token;
                        return data;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }    
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
                return default;
            }
        }
    }
}
