using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sensatus.FiberTracker.Configurations;
using System.Diagnostics;

namespace Sensatus.FiberTracker.UI
{
    public partial class Error : AccountPlusBase
    {
        public Error()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtaExceptionDetails.Text.Trim());
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            (new ApplicationContext()).Dispose();
        }

        public string ExceptionMessage
        {
            get;
            set;
        }

        private void Error_Load(object sender, EventArgs e)
        {
            base.SetBGColor(this);
            txtaExceptionDetails.Text = ExceptionMessage;
        }

        private void btnReportBug_Click(object sender, EventArgs e)
        {
            string supportMail = Sensatus.FiberTracker.Configurations.ApplicationConfiguration.SupportMail;
            string subject = "Account Plus : Bug Report";
            string mailBody = txtaExceptionDetails.Text.Trim();
            Process.Start("mailto:" + supportMail + "&subject=" + subject + "&Body=" + mailBody);
        }
    }
}
