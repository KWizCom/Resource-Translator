using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;

namespace KWizCom.ResourceTranslator.Web
{
    public class HttpWebRequestAsync
    {
        #region Members

        private HttpWebRequestAsyncState m_requestState = null;
        private bool m_requestCanceled = false;
        private bool m_requestTimedOut = false;

        #endregion

        #region Delegates

        public delegate void ErrorHandler(object sender, ErrorEventArgs e);
        public delegate void TimeoutHandler(object sender, ErrorEventArgs e);
        public delegate void ResponseReceivedHandler(object sender, ResponseReceivedEventArgs e);

        #endregion

        #region Events

        public event ErrorHandler ErrorReceived;
        public event TimeoutHandler TimeoutOccurred;
        public event ResponseReceivedHandler ResponseReceived;

        #endregion

        public virtual void GetTokenResponseCallback(HttpWebRequestCallbackState state)
        {
            if (state.Exception != null)
            {
                if (m_requestCanceled && ((WebException)state.Exception).Status == WebExceptionStatus.RequestCanceled)
                {
                    return;
                }

                if (m_requestTimedOut)
                {
                    OnTimeout(state.Exception);
                }
                OnError(state.Exception);
            }

            OnResponseReceived(state.ResponseStream);

            //else
            //{
            //    var token = DeSerializeToJson<AdmAccessToken>(state.ResponseStream);
            //    OnTokenArrived(token);
            //}
        }

        private void BeginGetRequestStreamCallback(IAsyncResult asyncResult)
        {
            Stream requestStream = null;
            HttpWebRequestAsyncState asyncState = null;
            try
            {
                asyncState = (HttpWebRequestAsyncState)asyncResult.AsyncState;
                requestStream = asyncState.HttpWebRequest.EndGetRequestStream(asyncResult);
                requestStream.Write(asyncState.RequestBytes, 0, asyncState.RequestBytes.Length);
                requestStream.Close();
                IAsyncResult result = asyncState.HttpWebRequest.BeginGetResponse(BeginGetResponseCallback,
                  new HttpWebRequestAsyncState
                  {
                      HttpWebRequest = asyncState.HttpWebRequest,
                      ResponseCallback = asyncState.ResponseCallback,
                      State = asyncState.State
                  });



                // Timeout comes here
                ThreadPool.RegisterWaitForSingleObject(result.AsyncWaitHandle, new WaitOrTimerCallback(TimeOutCallback), asyncState.HttpWebRequest, 30000, true);
            }
            catch (Exception ex)
            {
                if (asyncState != null)
                    asyncState.ResponseCallback(new HttpWebRequestCallbackState(ex));
                else
                    throw;
            }
            finally
            {
                if (requestStream != null)
                    requestStream.Close();
            }
        }

        private void BeginGetResponseCallback(IAsyncResult asyncResult)
        {
            WebResponse webResponse = null;
            Stream responseStream = null;
            HttpWebRequestAsyncState asyncState = null;
            try
            {
                asyncState = (HttpWebRequestAsyncState)asyncResult.AsyncState;
                webResponse = asyncState.HttpWebRequest.EndGetResponse(asyncResult);
                responseStream = webResponse.GetResponseStream();
                var webRequestCallbackState = new HttpWebRequestCallbackState(responseStream, asyncState.State);
                asyncState.ResponseCallback(webRequestCallbackState);
                responseStream.Close();
                responseStream = null;
                webResponse.Close();
                webResponse = null;
            }
            catch (Exception ex)
            {
                if (asyncState != null)
                    asyncState.ResponseCallback(new HttpWebRequestCallbackState(ex));
                else
                    throw;
            }
            finally
            {
                if (responseStream != null)
                    responseStream.Close();
                if (webResponse != null)
                    webResponse.Close();
            }
        }

        /// <summary>
        /// This method does an Http POST sending any post parameters to the url provided
        /// </summary>
        /// <param name="url">The url to make an Http POST to</param>
        /// <param name="postParameters">The form parameters if any that need to be POSTed</param>
        /// <param name="responseCallback">The callback delegate that should be called when the response returns from the remote server</param>
        /// <param name="state">Any state information you need to pass along to be available in the callback method when it is called</param>
        /// <param name="contentType">The Content-Type of the Http request</param>
        public void PostAsync(string url, NameValueCollection postParameters, Action<HttpWebRequestCallbackState> responseCallback)
        {
            object state = null;
            string contentType = "application/x-www-form-urlencoded";
            var httpWebRequest = CreateHttpWebRequest(url, "POST", contentType);
            var requestBytes = GetRequestBytes(postParameters);
            httpWebRequest.ContentLength = requestBytes.Length;

            HttpWebRequestAsyncState httpWebRequestAsyncState = new HttpWebRequestAsyncState()
              {
                  RequestBytes = requestBytes,
                  HttpWebRequest = httpWebRequest,
                  ResponseCallback = responseCallback,
                  State = state
              };

            IAsyncResult result = httpWebRequest.BeginGetRequestStream(BeginGetRequestStreamCallback, httpWebRequestAsyncState);

            m_requestState = httpWebRequestAsyncState;

            ThreadPool.RegisterWaitForSingleObject(result.AsyncWaitHandle, new WaitOrTimerCallback(TimeOutCallback), httpWebRequest, 30000, true);
        }

        /// <summary>
        /// This method does an Http GET to the provided url and calls the responseCallback delegate
        /// providing it with the response returned from the remote server.
        /// </summary>
        /// <param name="url">The url to make an Http GET to</param>
        /// <param name="responseCallback">The callback delegate that should be called when the response returns from the remote server</param>
        /// <param name="state">Any state information you need to pass along to be available in the callback method when it is called</param>
        /// <param name="contentType">The Content-Type of the Http request</param>
        public void GetAsync(string url, Action<HttpWebRequestCallbackState> responseCallback)
        {
            object state = null;
            string contentType = "application/x-www-form-urlencoded";
            var httpWebRequest = CreateHttpWebRequest(url, "GET", contentType);

            httpWebRequest.BeginGetResponse(BeginGetResponseCallback,
              new HttpWebRequestAsyncState()
              {
                  HttpWebRequest = httpWebRequest,
                  ResponseCallback = responseCallback,
                  State = state
              });
        }

        private void TimeOutCallback(object state, bool timedOut)
        {
            if (timedOut)
            {
                HttpWebRequest request = state as HttpWebRequest;
                if (request != null)
                {
                    m_requestTimedOut = true;
                    request.Abort();
                }
            }
        }

        public void CancelRequest()
        {
            if (m_requestState != null && m_requestState.HttpWebRequest != null)
            {
                m_requestCanceled = true;
                m_requestState.HttpWebRequest.Abort();
            }
        }

        protected virtual void OnError(Exception ex)
        {
            if (ErrorReceived != null)
                ErrorReceived(this, new ErrorEventArgs(ex));
        }

        protected virtual void OnTimeout(Exception ex)
        {
            if (TimeoutOccurred != null)
                TimeoutOccurred(this, new ErrorEventArgs(new TimeoutException("Timeout", ex)));
        }

        protected virtual void OnResponseReceived(Stream response)
        {
            if (ResponseReceived != null)
                ResponseReceived(this, new ResponseReceivedEventArgs(response));
        }

        #region Helpers

        private static HttpWebRequest CreateHttpWebRequest(string url, string httpMethod, string contentType)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = contentType;
            httpWebRequest.Method = httpMethod;
            return httpWebRequest;
        }

        private static byte[] GetRequestBytes(NameValueCollection postParameters)
        {
            if (postParameters == null || postParameters.Count == 0)
                return new byte[0];
            var sb = new StringBuilder();
            foreach (var key in postParameters.AllKeys)
                sb.Append(key + "=" + postParameters[key] + "&");
            sb.Length = sb.Length - 1;
            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        /// <summary>
        /// If the response from a remote server is in text form
        /// you can use this method to get the text from the ResponseStream
        /// This method Disposes the stream before it returns
        /// </summary>
        /// <param name="responseStream">The responseStream that was provided in the callback delegate's HttpWebRequestCallbackState parameter</param>
        /// <returns></returns>
        public static string GetResponseText(Stream responseStream)
        {
            using (var reader = new StreamReader(responseStream))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// This method uses the DataContractJsonSerializer to
        /// Deserialize the contents of a stream to an instance
        /// of an object of type T.
        /// This method disposes the stream before returning
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">A Stream. Typically the ResponseStream</param>
        /// <returns>An instance of an object of type T</returns>
        public static T DeSerializeToJson<T>(Stream stream)
        {
            using (stream)
            {
                var deserializer = new DataContractJsonSerializer(typeof(T));
                return (T)deserializer.ReadObject(stream);
            }
        }
        #endregion
    }
}
