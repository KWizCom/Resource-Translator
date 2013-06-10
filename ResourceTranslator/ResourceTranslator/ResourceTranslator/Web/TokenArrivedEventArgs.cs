using System;

namespace KWizCom.ResourceTranslator.Web
{
    public class TokenArrivedEventArgs : EventArgs
    {
        public object Token;
        public TokenArrivedEventArgs(object token)
        {
            this.Token = token;
        }
    }
}
