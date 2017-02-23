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
    public partial class Error : FormBase
    {
        public Error()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtaExceptionDetails.Text.Trim());
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            new ApplicationContext().Dispose();
        }

        public string ExceptionMessage
        {
            get;
            set;
        }

        private void Error_Load(object sender, EventArgs e)
        {
            SetBGColor(this);
            txtaExceptionDetails.Text = ExceptionMessage;
        }

        private void btnReportBug_Click(object sender, EventArgs e)
        {
            var supportMail = Configurations.ApplicationConfiguration.SupportMail;
            var subject = "Account Plus : Bug Report";
            var mailBody = txtaExceptionDetails.Text.Trim();
            Process.Start("mailto:" + supportMail + "&subject=" + subject + "&Body=" + mailBody);
        }
    }
}
