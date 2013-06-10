using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using KWizCom.ResourceTranslator.Web;

namespace KWizCom.ResourceTranslator
{
    // TODO: use async calls so that the UI doesn't lock when getting the access token and the user can cancel the request
    public class AdmAuthentication : HttpWebRequestAsync
    {
        private static readonly string m_datamarketAccessUri = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
        private string m_clientId;
        private string m_clientSecret;
        private string m_request;
        public delegate void TokenReceivedHandler(object sender, TokenArrivedEventArgs e);
        public event TokenReceivedHandler TokenArrived;

        public AdmAuthentication(string clientId, string clientSecret)
        {
            m_clientId = clientId;
            m_clientSecret = clientSecret;
            //If clientid or client secret has special characters, encode before sending request
            m_request = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com", HttpUtility.UrlEncode(clientId), HttpUtility.UrlEncode(clientSecret));
        }

        public AdmAccessToken GetAccessToken()
        {
            return HttpPost(m_datamarketAccessUri, m_request);
        }

        private AdmAccessToken HttpPost(string DatamarketAccessUri, string requestDetails)
        {
            //Prepare OAuth request 
            WebRequest webRequest = WebRequest.Create(DatamarketAccessUri);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(requestDetails);
            webRequest.ContentLength = bytes.Length;            
            using (Stream outputStream = webRequest.GetRequestStream())
            {
                outputStream.Write(bytes, 0, bytes.Length);
            }
            using (WebResponse webResponse = webRequest.GetResponse())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AdmAccessToken));
                //Get deserialized object from JSON stream
                AdmAccessToken token = (AdmAccessToken)serializer.ReadObject(webResponse.GetResponseStream());
                return token;
            }
        }

        #region Async

        public void GetAccessTokenAsync()
        {
            PostAsync(m_datamarketAccessUri, GetParameters(this.m_clientId, this.m_clientSecret), GetTokenResponseCallback);
        }

        private static NameValueCollection GetParameters(string clientId, string clientSecret)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("grant_type", "client_credentials");
            parameters.Add("client_id", HttpUtility.UrlEncode(clientId));
            parameters.Add("client_secret", HttpUtility.UrlEncode(clientSecret));
            parameters.Add("scope", "http://api.microsofttranslator.com");
            return parameters;
        }

        public override void GetTokenResponseCallback(HttpWebRequestCallbackState state)
        {
            base.GetTokenResponseCallback(state);
            object token = null;
            try
            {
                token = DeSerializeToJson<AdmAccessToken>(state.ResponseStream);
            }
            catch
            {
            }
            OnTokenArrived(token);
        }

        protected virtual void OnTokenArrived(object token)
        {
            if (TokenArrived != null)
                TokenArrived(this, new TokenArrivedEventArgs(token));
        }

        #endregion        
    }
}
