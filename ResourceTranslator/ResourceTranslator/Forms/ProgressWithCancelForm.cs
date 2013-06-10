using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace KWizCom.ResourceTranslator.Forms
{
    public partial class ProgressWithCancelForm : Form
    {
        public ProgressWithCancelForm(string whyWeAreWaiting, DoWorkEventHandler work)//,AccountSettings accountSettings)
        {
            InitializeComponent();
            Text = whyWeAreWaiting; // Show in title bar
            backgroundWorker1.DoWork += work; // Event handler to be called in context of new thread.            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            label1.Text = "Cancel pending";
            backgroundWorker1.CancelAsync(); // Tell worker to abort.
            btnCancel.Enabled = false;
        }

        private void ProgressWithCancelForm_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = e.UserState as string;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }
    }
}
