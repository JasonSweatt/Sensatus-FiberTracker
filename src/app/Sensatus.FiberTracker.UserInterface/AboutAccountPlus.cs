using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;

namespace Sensatus.FiberTracker.UI
{
    partial class AboutAccountPlus : AccountPlusBase
    {
        public AboutAccountPlus()
        {
            InitializeComponent();
            this.Text = string.Format("About {0}",ApplicationDetails.AssemblyTitle);
            this.lblProductName.Text = string.Format("Product Name : {0}", ApplicationDetails.AssemblyProduct);
            this.lblVersion.Text = string.Format("Version : {0}", ApplicationDetails.AssemblyVersion);
            this.lblCopyRight.Text = string.Format("Copy Right : {0}", ApplicationDetails.AssemblyCopyright);
            this.lblCompanyName.Text = string.Format("Company Name : {0}", ApplicationDetails.AssemblyCompany);
            this.txtaDescription.Text = ApplicationDetails.AssemblyDescription;
        }        

        private void AboutAccountPlus_Load(object sender, EventArgs e)
        {
            base.SetBGColor(this);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void linkRedirectURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Sensatus.FiberTracker.Configurations.ApplicationConfiguration.SupportURL);
        }
    }
}
