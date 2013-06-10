using KWizCom.ResourceTranslator.Utilities;

namespace KWizCom.ResourceTranslator.Utilities
{
    public static class LogUtility
    {
        //TODO: put this in a constants file
        private static string m_ProductName = "KWizCom Resource Translator";

        private static SimpleEventLog m_EventLogger = null;
        private static SimpleEventLog EventLogger
        {
            get
            {
                if (m_EventLogger == null)
                    m_EventLogger = CreateEventLogger();
                return m_EventLogger;
            }
            set
            {
                m_EventLogger = value;
            }
        }

        private static SimpleEventLog CreateEventLogger()
        {            
            SimpleEventLog logger = new SimpleEventLog(m_ProductName);
            return logger;
        }

        public static void Start()
        {
            EventLogger = CreateEventLogger();
        }

        public static bool WriteInformation(string information)
        {
            return EventLogger.WriteInformation(information);
        }

        public static bool WriteWarning(string warning)
        {
            return EventLogger.WriteWarning(warning);
        }

        public static bool WriteError(string error)
        {
            return EventLogger.WriteError(error);
        }
    }
}
