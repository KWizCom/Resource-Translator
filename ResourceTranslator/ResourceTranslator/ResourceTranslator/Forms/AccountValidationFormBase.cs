using System.ComponentModel;
using System.Windows.Forms;

namespace KWizCom.ResourceTranslator.Forms
{
    public class AccountValidationFormBase: Form
    {
        public void ValidateSavedSettings_WorkEventHandler(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            var accountSettings = e.Argument as AccountSettings;

            worker.ReportProgress(0, "Validating account settings...");
            if (accountSettings.UseMTS)
            {
                worker.ReportProgress(0, "Checking Microsoft Translator Account...");
                accountSettings.AccountValidated = Utilities.FormUtility.ValidateMTSCredentials(accountSettings.MTSClientId, accountSettings.MTSClientSecret, true);
                if (accountSettings.AccountValidated)
                {
                    worker.ReportProgress(0, "Microsoft Translator Account verified...");
                    Properties.UserSettings.Default.MTSClientId = accountSettings.MTSClientId;
                    Properties.UserSettings.Default.MTSClientSecret = accountSettings.MTSClientSecret;
                    Properties.UserSettings.Default.UseBing = false;
                    Properties.UserSettings.Default.UseMTS = true;
                    Properties.UserSettings.Default.Save();
                    Properties.UserSettings.Default.Reload();
                    Utilities.FormUtility.HasValidMTSCredentials = true;
                }
            }
            else if (accountSettings.UseBing)
            {
                worker.ReportProgress(0, "Checking Bing Application ID...");
                accountSettings.AccountValidated = Utilities.FormUtility.ValidateAppId(accountSettings.BingAppId);
                if (accountSettings.AccountValidated)
                {
                    worker.ReportProgress(0, "Bing Application ID verified...");
                    Properties.UserSettings.Default.BingAppId = accountSettings.BingAppId;
                    Properties.UserSettings.Default.UseBing = true;
                    Properties.UserSettings.Default.UseMTS = false;
                    Properties.UserSettings.Default.Save();
                    Properties.UserSettings.Default.Reload();
                    Utilities.FormUtility.HasValidAppId = true;
                }
            }
        }
    }
}
