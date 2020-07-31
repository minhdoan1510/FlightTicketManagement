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

            if (AuthenticatedUser.Instance.data != null) {
                apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {AuthenticatedUser.Instance.data.Token}");
            }
        }

        public async Task<T> Get<T>(string route) {
            this.InitializeClient();

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

        public async Task<T> Get<T>(string route, KeyValuePair<string, string>[] parameter = null) {
            this.InitializeClient();
            if (parameter != null) {
                for (int i = 0; i < parameter.Length; i++) {
                    route += ((i == 0) ? '?' : '&');
                    route += string.Format("{0}={1}", parameter[i].Key, parameter[i].Value);
                }
            }
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

        public async Task<T> Post<T>(string route, object body = null) {
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

        public async Task<bool> Authenticate(string username, string password) {
            this.InitializeClient();
            UserAccount account = new UserAccount {
                Username = username,
                Password = password

            };
            using (HttpResponseMessage response = await apiClient.PostAsJsonAsync(ApiRoutes.Account.LogIn, account)) {
                if (response.IsSuccessStatusCode) {
                    var data = await response.Content.ReadAsAsync<Response<InfoLogin>>();
                    if (data != null) {
                        AuthenticatedUser.Instance.data = data.Result;
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
