using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ServerFTM.Authorization
{
    public class TokenManager
    {
        static TokenManager instance;
        public static TokenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TokenManager();
                }
                return instance;
            }
            set => instance = value;
        }

        private static List<KeyValuePair<string,string>> accessTokens;

        TokenManager()
        {
            accessTokens = new List<KeyValuePair<string, string>>();
        }

        AccessToken GetInfoToken(string token)
        {
            try
            {
                string key = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                string[] parts = key.Split(new char[] { ':' });
                if (parts.Length == 4)
                {
                    string hash = parts[0];
                    string idAcc = parts[1];
                    string username = parts[2];
                    long ticks = long.Parse(parts[3]);
                    DateTime timeStamp = new DateTime(ticks);
                    return new AccessToken(idAcc, username, timeStamp, token);
                }
                else
                {
                    return null;
                }    
            }
            catch
            {
                return null;
            }
        }

        public void AddAccessToken(string idAcc, string token)
        {
            accessTokens.Add(new KeyValuePair<string, string>(idAcc, token));
        }

        public string GetAccessToken(string idAcc)
        {
            return accessTokens.Where(x => x.Key == idAcc).Single().Value;
        }

        public void DelAccessToken(string token)
        {
            foreach (KeyValuePair<string,string> item in accessTokens)
            {
                if(item.Value.Equals(token))
                {
                    accessTokens.Remove(item);
                    return;
                }    
            }
        }

        public string GetIDAccountToken(string token)
        {
            foreach (KeyValuePair<string, string> item in accessTokens)
            {
                if (item.Value.Equals(token))
                {
                    return item.Key;
                }                    
            }
            return null;
        }
    }
}
