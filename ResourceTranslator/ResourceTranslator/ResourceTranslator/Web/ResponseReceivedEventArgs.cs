using System.IO;

namespace KWizCom.ResourceTranslator.Web
{
    public class ResponseReceivedEventArgs
    {
        public Stream Response;
        public ResponseReceivedEventArgs(Stream response)
        {
            this.Response = response;
        }
    }
}
