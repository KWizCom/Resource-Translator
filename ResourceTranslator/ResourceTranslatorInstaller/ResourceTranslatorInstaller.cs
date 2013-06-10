using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Security.Permissions;

namespace ResourceTranslatorInstaller
{
    [RunInstaller(true)]
    public class ResourceTranslatorInstaller : Installer
    {
        //TODO: put this in a constants file
        private static string m_ProductName = "KWizCom Resource Translator";

        [SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Install(IDictionary savedState)
        {
            base.Install(savedState);
        }

        [SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
        }

        [SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }

        [SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
            CreateEventSource(m_ProductName);
        }

        private void CreateEventSource(string eventLogName)
        {
#if DEBUG
            System.Diagnostics.Debugger.Launch();
#endif
            if (EventLog.SourceExists(eventLogName) == false)
                EventLog.CreateEventSource(eventLogName, "KWizCom");
        }
    }
}
