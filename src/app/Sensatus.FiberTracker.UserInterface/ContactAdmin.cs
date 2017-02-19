using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Sensatus.FiberTracker.UI
{
    public partial class ContactAdmin : AccountPlusBase
    {
        public ContactAdmin()
        {
            InitializeComponent();
        }

        private void ContactAdminOptions_CheckedChanged(object sender, EventArgs e)
        {
            string optionSelected = ((RadioButton)(sender)).Name;
            string subject = string.Empty ;
            switch (optionSelected)
            {
                case "rbnReportBug":
                    if (((RadioButton)(sender)).Checked)
                        subject = "Account Plus : Bug Report";
                    break;
                case "rbnQuery":
                    if (((RadioButton)(sender)).Checked)
                        subject = "Account Plus : Query";
                    break;
                case "rbnFeatureRequest":
                    if (((RadioButton)(sender)).Checked)
                        subject = "Account Plus : Feature Request";
                    break;
                case "rbnSuggestion":
                    if (((RadioButton)(sender)).Checked)
                        subject = "Account Plus : Suggestion";
                    break;                    
            }

            txtSubject.Text = subject;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rbnReportBug.Checked = true;
            txtaMessage.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtaMessage.Text.Trim(); 
            string subject = txtSubject.Text.Trim();
            string toMail = Sensatus.FiberTracker.Configurations.ApplicationConfiguration.SupportMail;
            Process.Start("mailto:"+ toMail +"&subject=" + subject + "&Body=" + message);            
            this.Close();
            this.Dispose();
        }

        private void txtaMessage_TextChanged(object sender, EventArgs e)
        {
            int length =  txtaMessage.Text.Trim().Length;
            if (length > 0)
                btnSend.Enabled = true;
            else
                btnSend.Enabled = false;
        }

        private void ContactAdmin_Load(object sender, EventArgs e)
        {
            base.SetBGColor(this);
        }

        private void linkRedirectURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Sensatus.FiberTracker.Configurations.ApplicationConfiguration.SupportURL);
        }
      
    }
}
