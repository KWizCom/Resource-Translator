using System.Windows.Forms;

namespace KWizCom.ResourceTranslator.Controls
{
    public partial class MTSSettingsControl : UserControl
    {
        public string ClientId
        {
            get { return txtMTSClientId.Text; }
            set { txtMTSClientId.Text = value; }
        }

        public string ClientSecret
        {
            get { return txtMTSClientSecret.Text; }
            set { txtMTSClientSecret.Text = value; }
        }

        public MTSSettingsControl()
        {
            InitializeComponent();
        }

        private void lnkGetStartedWithMicrosoftTranslator_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://blogs.msdn.com/b/translation/p/gettingstarted1.aspx");
        }
    }
}
