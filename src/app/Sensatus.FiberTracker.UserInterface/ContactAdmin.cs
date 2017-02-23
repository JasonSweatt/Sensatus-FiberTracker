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
    public partial class ContactAdmin : FormBase
    {
        public ContactAdmin()
        {
            InitializeComponent();
        }

        private void ContactAdminOptions_CheckedChanged(object sender, EventArgs e)
        {
            var optionSelected = ((RadioButton)sender).Name;
            var subject = string.Empty ;
            switch (optionSelected)
            {
                case "rbnReportBug":
                    if (((RadioButton)sender).Checked)
                        subject = "Account Plus : Bug Report";
                    break;
                case "rbnQuery":
                    if (((RadioButton)sender).Checked)
                        subject = "Account Plus : Query";
                    break;
                case "rbnFeatureRequest":
                    if (((RadioButton)sender).Checked)
                        subject = "Account Plus : Feature Request";
                    break;
                case "rbnSuggestion":
                    if (((RadioButton)sender).Checked)
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
            Close();
            Dispose();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var message = txtaMessage.Text.Trim(); 
            var subject = txtSubject.Text.Trim();
            var toMail = Configurations.ApplicationConfiguration.SupportMail;
            Process.Start("mailto:"+ toMail +"&subject=" + subject + "&Body=" + message);            
            Close();
            Dispose();
        }

        private void txtaMessage_TextChanged(object sender, EventArgs e)
        {
            var length =  txtaMessage.Text.Trim().Length;
            if (length > 0)
                btnSend.Enabled = true;
            else
                btnSend.Enabled = false;
        }

        private void ContactAdmin_Load(object sender, EventArgs e)
        {
            SetBGColor(this);
        }

        private void linkRedirectURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Configurations.ApplicationConfiguration.SupportURL);
        }
      
    }
}
