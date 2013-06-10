using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Windows.Forms;
using KWizCom.ResourceTranslator.Forms;

namespace KWizCom.ResourceTranslator.Utilities
{
    public class FormUtility
    {
        public static bool UseMTS
        {
            get
            {
                return Properties.UserSettings.Default.UseMTS;
            }
        }

        public static bool UseBing
        {
            get
            {
                return Properties.UserSettings.Default.UseBing;
            }
        }

        public static string BingAppId
        {
            get
            {
                return Properties.UserSettings.Default.BingAppId;
            }
        }

        public static string MTSClientId
        {
            get
            {
                return Properties.UserSettings.Default.MTSClientId;
            }
        }

        public static string MTSClientSecret
        {
            get
            {
                return Properties.UserSettings.Default.MTSClientSecret;
            }
        }

        public static void ValidateSettingsUsingModalDialog(Form ownerForm, DoWorkEventHandler workEventHandler, AccountSettings accountSettings)
        {
            ownerForm.Enabled = false;
            using (MarqueeForm validatingSettingsDialog = new MarqueeForm("Validating Account Settings", workEventHandler, accountSettings))
            {
                validatingSettingsDialog.Owner = ownerForm;
                validatingSettingsDialog.ShowInTaskbar = false;
                validatingSettingsDialog.ShowDialog(ownerForm);
            }
            ownerForm.Enabled = true;
        }

        private static TranslatorService.LanguageServiceClient m_mtsLanguageServiceClient;
        public static TranslatorService.LanguageServiceClient MTSLanguageServiceClient
        {
            get
            {
                if (m_mtsLanguageServiceClient == null)
                    m_mtsLanguageServiceClient = new TranslatorService.LanguageServiceClient();

                return m_mtsLanguageServiceClient;
            }
        }

        private static DateTime m_mtsAdmAccessTokenReceived;
        private static AdmAccessToken m_mtsAdmAccessToken;
        public static AdmAccessToken MTSAdmAccessToken
        {
            get
            {
                if (m_mtsAdmAccessToken == null || (DateTime.Now - m_mtsAdmAccessTokenReceived).TotalSeconds > int.Parse(m_mtsAdmAccessToken.expires_in))
                {
                    AdmAuthentication admAuth = new AdmAuthentication(Utilities.FormUtility.MTSClientId, Utilities.FormUtility.MTSClientSecret);
                    m_mtsAdmAccessToken = admAuth.GetAccessToken();
                    m_mtsAdmAccessTokenReceived = DateTime.Now;
                }
                return m_mtsAdmAccessToken;
            }
        }

        public static HttpRequestMessageProperty MTSAccessHeader
        {
            get
            {
                // Create a header with the access_token property of the returned token
                string headerValue = "Bearer " + MTSAdmAccessToken.access_token;

                HttpRequestMessageProperty httpRequestProperty = new HttpRequestMessageProperty();
                httpRequestProperty.Method = "POST";
                httpRequestProperty.Headers.Add("Authorization", headerValue);
                return httpRequestProperty;
            }
        }

        private static bool? m_hasValidMTSCredentials;
        public static bool HasValidMTSCredentials
        {
            get
            {
                if (m_hasValidMTSCredentials == null)
                    m_hasValidMTSCredentials = ValidateMTSCredentials(MTSClientId, MTSClientSecret, false);
                return (bool)m_hasValidMTSCredentials;
            }
            set
            {
                m_hasValidMTSCredentials = value;
            }
        }

        public static bool ValidateMTSCredentials(string clientId, string clientSecret, bool setGlobalAmAccessToken)
        {
            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret)) return false;
            try
            {
                AdmAuthentication admAuth = new AdmAuthentication(clientId, clientSecret);
                AdmAccessToken admAccessToken = admAuth.GetAccessToken();               
                if(admAccessToken != null && int.Parse(admAccessToken.expires_in) > 0)
                {
                    if (setGlobalAmAccessToken)
                    {
                        m_mtsAdmAccessToken = admAccessToken;
                        m_mtsAdmAccessTokenReceived = DateTime.Now;
                        m_hasValidMTSCredentials = true;
                    }
                    return true;
                }
            }
            catch (WebException wex)
            {
                LogUtility.WriteError(wex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.WriteError(ex.ToString());
                return false;
            }

            return false;
        }

        private static bool? m_hasValidAppId;
        public static bool HasValidAppId
        {
            get
            {
                if (m_hasValidAppId == null)
                    m_hasValidAppId = ValidateAppId(BingAppId);
                return (bool)m_hasValidAppId;
            }
            set
            {
                m_hasValidAppId = value;
            }
        }

        public static bool ValidateAppId(string bingAppId)
        {
            try
            {
                if (string.IsNullOrEmpty(bingAppId)) return false;
                string[] texts = new string[] { "welcome", "hello" };

                TranslatorService.TranslateOptions options = new TranslatorService.TranslateOptions();

                TranslatorService.TranslateArrayResponse[] translatedTexts =
                    MTSLanguageServiceClient.TranslateArray(bingAppId, texts, "en", "fr", options);

                return true;
            }
            catch
            {

            }

            return false;
        }

        private static Dictionary<string, string> m_cultureDataSource;
        public static Dictionary<string, string> CultureDataSource
        {
            get
            {
                if (m_cultureDataSource == null || m_cultureDataSource.Count <= 0)
                    m_cultureDataSource = GetCulturesDataSource();
                return m_cultureDataSource;
            }
        }

        private static Dictionary<string, string> GetCulturesDataSource()
        {
            Dictionary<string, string> cultureDataSource = new Dictionary<string, string>();
            string[] languageCodes = null;
            string[] languageNames = null;

            if (HasValidMTSCredentials && Utilities.FormUtility.UseMTS)
            {
                try
                {
                    TranslatorService.TranslateOptions options = new TranslatorService.TranslateOptions();

                    using (OperationContextScope scope = new OperationContextScope(MTSLanguageServiceClient.InnerChannel))
                    {
                        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = MTSAccessHeader;
                        //Keep appId parameter blank as we are sending access token in authorization header.
                        languageCodes = MTSLanguageServiceClient.GetLanguagesForTranslate("");
                        languageNames = MTSLanguageServiceClient.GetLanguageNames(BingAppId, CultureInfo.CurrentUICulture.Name, languageCodes);
                    }
                }
                catch (WebException wex)
                {
                    LogUtility.WriteError(wex.ToString());
                }
                catch (Exception ex)
                {
                    LogUtility.WriteError(ex.ToString());
                }
            }
            else if (HasValidAppId && Utilities.FormUtility.UseBing)
            {
                languageCodes = MTSLanguageServiceClient.GetLanguagesForTranslate(BingAppId);
                languageNames = MTSLanguageServiceClient.GetLanguageNames(BingAppId, CultureInfo.CurrentCulture.Name, languageCodes);
            }

            if (languageCodes != null && languageCodes.Length > 0 && languageNames != null && languageNames.Length > 0 && languageCodes.Length == languageNames.Length)
            {
                for (int i = 0; i < languageNames.Length; i++)
                {
                    cultureDataSource.Add(languageCodes[i], languageNames[i]);
                }
            }
            else
            {
                cultureDataSource = new Dictionary<string, string>();
            }

            return cultureDataSource;
        }
    }
}
