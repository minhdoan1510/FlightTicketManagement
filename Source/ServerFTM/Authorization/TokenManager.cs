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
        public static TokenManager GetInstance
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

        private Hashtable accessTokens;

        TokenManager()
        {
            accessTokens = new Hashtable();
        }

        AccessToken GetInfoToken(string token)
        {
            try
            {
                // Base64 decode the string, obtaining the token:username:timeStamp.
                string key = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                // Split the parts.
                string[] parts = key.Split(new char[] { ':' });
                if (parts.Length == 4)
                {
                    // Get the hash message, username, and timestamp.
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

        void AddAccessToken(string idAcc, AccessToken token)
        {
            accessTokens.Add(idAcc, token);
        }

        AccessToken GetAccessToken(string idAcc)
        {
            return (AccessToken)accessTokens[idAcc];
        }

        string GetIDAccountToken(AccessToken token)
        {
            foreach (DictionaryEntry item in accessTokens)
            {
                if (((AccessToken)item.Value).Token.Equals(token))
                {
                    return (string)item.Key;
                }                    
            }
            return null;
        }
    }
}
