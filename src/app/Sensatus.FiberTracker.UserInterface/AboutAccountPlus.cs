using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;

namespace Sensatus.FiberTracker.UI
{
    internal partial class AboutAccountPlus : AccountPlusBase
    {
        public AboutAccountPlus()
        {
            InitializeComponent();
            Text = string.Format("About {0}",ApplicationDetails.AssemblyTitle);
            lblProductName.Text = string.Format("Product Name : {0}", ApplicationDetails.AssemblyProduct);
            lblVersion.Text = string.Format("Version : {0}", ApplicationDetails.AssemblyVersion);
            lblCopyRight.Text = string.Format("Copy Right : {0}", ApplicationDetails.AssemblyCopyright);
            lblCompanyName.Text = string.Format("Company Name : {0}", ApplicationDetails.AssemblyCompany);
            txtaDescription.Text = ApplicationDetails.AssemblyDescription;
        }        

        private void AboutAccountPlus_Load(object sender, EventArgs e)
        {
            SetBGColor(this);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void linkRedirectURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Configurations.ApplicationConfiguration.SupportURL);
        }
    }
}
