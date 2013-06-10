using System.Windows.Forms;

namespace KWizCom.ResourceTranslator.Controls
{
    public partial class BingSettingsControl : UserControl
    {
        public string BingAppId
        {
            get { return txtBingAppId.Text; }
            set { txtBingAppId.Text = value; }
        }

        public BingSettingsControl()
        {
            InitializeComponent();            
        }

        private void lnkGetBingAppId_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {        
            System.Diagnostics.Process.Start("https://ssl.bing.com/webmaster/developers");        
        }
    }
}
