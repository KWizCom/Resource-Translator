using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Resources;

namespace KWizCom.ResourceTranslator
{
    public class ResourceFile
    {
        #region Members

        private string m_toLanguage;
        private string m_fromLanguage;
        private string m_filename;
        private string m_outputFilename;
        private bool m_includeAllEntries = true;
        private List<ResourceEntry> m_entries;
        private bool m_entriesInitialized = false;
        private int m_totalCharacterCount = 0;        

        #endregion

        #region Properties

        public string ToLanguage
        {
            get { return m_toLanguage; }
            set { m_toLanguage = value; }
        }


        public string FromLanguage
        {
            get { return m_fromLanguage; }
            set { m_fromLanguage = value; }
        }


        public string Filename
        {
            get
            {
                return m_filename;
            }
        }


        public string OutputFilename
        {
            get
            {
                return m_outputFilename;
            }
            set
            {
                m_outputFilename = value;
            }
        }


        public bool IncludeAllEntries
        {
            get
            {
                return m_includeAllEntries;
            }
            set
            {
                m_includeAllEntries = value;
            }
        }

        public List<ResourceEntry> Entries
        {
            get
            {
                if (m_entries == null)
                    m_entries = new List<ResourceEntry>();
                return m_entries;
            }
            set
            {
                m_entries = value;
            }

        }

        public bool EntriesInitialized
        {
            get
            {
                return m_entriesInitialized;
            }
        }

        public int TotalCharacterCount
        {
            get
            {
                return m_totalCharacterCount;
            }
        }

        public int EntriesToTranslateCharacterCount
        {
            get
            {
                return (int)EntriesToTranslate.Sum(w => w.OriginalValue.Length);
            }
        }

        public bool IsReadyToTranslate
        {
            get
            {
                bool hasToLanguage = !string.IsNullOrEmpty(ToLanguage);
                bool hasFromLanguage = !string.IsNullOrEmpty(FromLanguage);
                bool hasOutputFilename = !string.IsNullOrEmpty(OutputFilename);
                bool hasEntriesToIncludeInOutput = EntriesToTranslate.Count > 0 || (EntriesUntranslated.Count > 0 && IncludeAllEntries);

                return hasToLanguage && hasFromLanguage && hasOutputFilename && hasEntriesToIncludeInOutput;
            }
        }

        public List<ResourceEntry> EntriesToTranslate
        {
            get
            {
                var entriesToTranslate = (from ent in m_entries
                                          where ent.Translate == true
                                          select ent).ToList();

                return entriesToTranslate;
            }
        }

        public List<ResourceEntry> EntriesUntranslated
        {
            get
            {
                var entriesUntranslated = (from ent in m_entries
                                           where ent.Translate == false
                                           select ent).ToList();

                return entriesUntranslated;
            }
        }

        public int AllEntriesToIncludeCount
        {
            get
            {
                if (IncludeAllEntries)
                {
                    return EntriesToTranslate.Count + EntriesUntranslated.Count;
                }
                else
                {
                    return EntriesToTranslate.Count;
                }
            }
        }

        #endregion

        #region Contructor

        public ResourceFile(string filename)
        {
            m_filename = filename;
            Initialize();
        }

        #endregion

        #region Methods


        public DataTable ToDataTable()
        {
            DataTable dt = new DataTable(Path.GetFileName(Filename));
            dt.Columns.Add("Translate", typeof(bool));
            dt.Columns.Add("OriginalValue", typeof(string));
            dt.Columns.Add("Key", typeof(string));

            foreach (ResourceEntry item in Entries)            
                dt.Rows.Add(item.Translate, item.OriginalValue, item.Key);            

            return dt;
        }

        private void Initialize()
        {
            try
            {
                ResXResourceReader reader = new ResXResourceReader(m_filename);

                IEnumerable<DictionaryEntry> enumerator = reader.OfType<DictionaryEntry>();

                if (enumerator == null)
                {
                    m_entriesInitialized = false;
                }
                else
                {
                    m_entries = (from ent in enumerator
                                 select new ResourceEntry(ent)).ToList();
                    m_totalCharacterCount = (int)m_entries.Sum(w => w.OriginalValue.Length);
                    m_entriesInitialized = true;
                }

                reader.Close();
            }
            catch
            {
                m_entriesInitialized = false;
            }
        }

        #endregion
    }
}
