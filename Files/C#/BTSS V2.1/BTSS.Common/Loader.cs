using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BTSS.Common
{
    public partial class Loader<T> : Form where T:class 
    {
        Action<T> action;
        public Exception Error { get; set; }
        bool isWorkCompleted = false;
        
        public Loader(Action<T> action)
        {
            InitializeComponent();

            if (action == null) throw new ArgumentNullException("action");
            this.action = action; 
            Closing += new System.ComponentModel.CancelEventHandler(ProgressDlg_Closing);
        }

        public void RunWorker()
        {

            System.Windows.Forms.Application.DoEvents();
            using (var worker = new ProgressWorker<T> { Action = action })
            {
                worker.RunWorkerAsync();
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                ShowDialog();
            }        
        }

        private void ProgressDlg_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {             
            if (!isWorkCompleted)
            {
                e.Cancel = true;
            }
        }
         
        private void worker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Error = e.Error;
                DialogResult = DialogResult.Abort;
                return;
            }
            isWorkCompleted = true;
            DialogResult = DialogResult.OK;
        }
         
    }

    public class ProgressWorker<TArgument> : BackgroundWorker where TArgument : class
    {
        public Action<TArgument> Action { get; set; }

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            if (Action != null)
            {
                Action(e.Argument as TArgument);
            }
        } 
    } 


}
