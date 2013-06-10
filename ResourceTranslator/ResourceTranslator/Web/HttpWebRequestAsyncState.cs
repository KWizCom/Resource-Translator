using System;
using System.Net;

namespace KWizCom.ResourceTranslator.Web
{
    public class HttpWebRequestAsyncState
    {
        public byte[] RequestBytes { get; set; }
        public HttpWebRequest HttpWebRequest { get; set; }
        public Action<HttpWebRequestCallbackState> ResponseCallback { get; set; }
        public Object State { get; set; }
    }
}
