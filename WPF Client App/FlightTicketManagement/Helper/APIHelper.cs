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
    public class AuthenticatedUser
    {
        private static AuthenticatedUser instance = null;
        private InfoLogin userInfo;

        public static AuthenticatedUser Instance {
            get {
                if (instance == null)
                    instance = new AuthenticatedUser();
                return instance;
            }
        }

        public InfoLogin data {
            get { return this.userInfo; }
            set { this.userInfo = value; }
        }
    }
    public class APIHelper
    {
        private HttpClient apiClient { get; set; }
        private APIHelper() {
            InitializeClient();
        }
        private static APIHelper instance = null;
        public static APIHelper Instance {
            get {
                if (instance == null) {
                    instance = new APIHelper();
                }
                return instance;
            }
        }

        private void InitializeClient() {
            string api = ConfigurationManager.AppSettings["api"];

            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri(api);
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> PostInit<T>(string route, object body) {
            this.InitializeClient();

            using (HttpResponseMessage response = await apiClient.PostAsJsonAsync(route, body)) {
                if (response.IsSuccessStatusCode) {
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

        public async Task<T> Get<T>(string route) {
            this.InitializeClient();

            // add token to header 
            CheckToken headerValue = new CheckToken();
            headerValue.id = AuthenticatedUser.Instance.data.Id;
            headerValue.data = AuthenticatedUser.Instance.data.Token;

            apiClient.DefaultRequestHeaders.Add("id", headerValue.id);
            apiClient.DefaultRequestHeaders.Add("token", headerValue.data);

            using (HttpResponseMessage response = await apiClient.GetAsync(route)) {

                if (response.IsSuccessStatusCode) {
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

        public async Task<T> PostWithToken<T>(string route, object body = null) {
            this.InitializeClient();
            // add token to header 

            CheckToken headerValue = new CheckToken();
            headerValue.id = AuthenticatedUser.Instance.data.Id;
            headerValue.data = AuthenticatedUser.Instance.data.Token;

            apiClient.DefaultRequestHeaders.Add("id", headerValue.id);
            apiClient.DefaultRequestHeaders.Add("token", headerValue.data);

            using (HttpResponseMessage response = await apiClient.PostAsJsonAsync(route, body)) {
                if (response.IsSuccessStatusCode) {
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
