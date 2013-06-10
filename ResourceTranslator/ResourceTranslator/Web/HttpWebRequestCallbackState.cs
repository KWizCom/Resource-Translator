using System;
using System.IO;

namespace KWizCom.ResourceTranslator.Web
{
    public class HttpWebRequestCallbackState
    {
        public Stream ResponseStream { get; private set; }
        public Exception Exception { get; private set; }
        public Object State { get; set; }

        public HttpWebRequestCallbackState(Stream responseStream, object state)
        {
            ResponseStream = responseStream;
            State = state;
        }

        public HttpWebRequestCallbackState(Exception exception)
        {
            Exception = exception;
        }
    }
}
