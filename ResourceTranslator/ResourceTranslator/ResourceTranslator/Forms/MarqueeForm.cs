using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace KWizCom.ResourceTranslator.Forms
{
    public partial class MarqueeForm : Form
    {
        private object m_WorkerArgument = null;

        public MarqueeForm(string whyWeAreWaiting, DoWorkEventHandler workEventHandler, object workerArgument)
        {
            InitializeComponent();
            m_WorkerArgument = workerArgument;
            Text = whyWeAreWaiting; // Show in title bar
            backgroundWorker1.DoWork += workEventHandler; // Event handler to be called in context of new thread. 
        }        

        //private void btnCancel_Click(object sender, EventArgs e)
        //{
        //    label1.Text = "Cancel pending";
        //    backgroundWorker1.CancelAsync(); // Tell worker to abort.
        //    btnCancel.Enabled = false;
        //}

        private void ProgressWithCancelForm_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync(m_WorkerArgument);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label1.Text = e.UserState as string;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }        
    }
}
