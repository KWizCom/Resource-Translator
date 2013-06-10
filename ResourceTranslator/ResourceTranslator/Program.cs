using System;
using System.Windows.Forms;

namespace KWizCom.ResourceTranslator
{
    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);            
            Application.Run(new Forms.MainForm());
        }
    }
}
