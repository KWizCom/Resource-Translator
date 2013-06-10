using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KWizCom.ResourceTranslator
{
    public class ResourceEntry
    {
        #region Members

        private Regex m_tokenRegex = new Regex(@"\[(?<myMatch>([^\]])*)\]");
        private bool m_translate;
        private string m_key;
        private string m_originalValue;
        private string m_originalValueWithPlaceHolders;
        private string m_translatedValue;
        private Dictionary<string, string> m_tokens;

        #endregion

        #region Properties

        public bool Translate
        {
            get { return m_translate; }
            set { m_translate = value; }
        }

        public string Key
        {
            get { return m_key; }
            set { m_key = value; }
        }

        public string OriginalValue
        {
            get { return m_originalValue; }
            set
            {
                m_originalValue = value;
                m_originalValueWithPlaceHolders = GetValueWithPlaceHolders();
            }
        }

        public string OriginalValueWithPlaceHolders
        {
            get { return m_originalValueWithPlaceHolders; }
            set { m_originalValueWithPlaceHolders = value; }
        }

        public string TranslatedValue
        {
            get { return m_translatedValue; }
            set { m_translatedValue = value; }
        }

        public Dictionary<string, string> Tokens
        {
            get
            {
                return m_tokens;
            }
            set
            {
                m_tokens = value;
            }
        }

        #endregion

        #region Constructor

        public ResourceEntry(string key, string value)
        {
            m_key = key;
            m_originalValue = value;

            if (m_tokens == null) m_tokens = new Dictionary<string, string>();

            m_originalValueWithPlaceHolders = GetValueWithPlaceHolders();
        }


        public ResourceEntry(DictionaryEntry entry)
        {
            m_key = (string)entry.Key;
            m_originalValue = (string)entry.Value;

            if (m_tokens == null) m_tokens = new Dictionary<string, string>();

            m_originalValueWithPlaceHolders = GetValueWithPlaceHolders();
        }

        #endregion

        #region Methods

        private string GetValueWithPlaceHolders()
        {
            MatchCollection matches = m_tokenRegex.Matches(m_originalValue);

            int i = 0;
            string temp = m_originalValue;
            foreach (Match m in matches)
            {
                string placeholder = string.Format("[{0}]", i);
                temp = m_originalValue.Replace(m.Value, placeholder);
                m_tokens.Add(placeholder, m.Value);
                i++;
            }

            return temp;
        }

        public string ReplacePlaceHoldersWithTokens(string translatedValue)
        {
            foreach (var key in m_tokens.Keys)
            {
                translatedValue = translatedValue.Replace(key, m_tokens[key]);
            }

            m_translatedValue = translatedValue;
            return m_translatedValue;
        }

        #endregion
    }
}
