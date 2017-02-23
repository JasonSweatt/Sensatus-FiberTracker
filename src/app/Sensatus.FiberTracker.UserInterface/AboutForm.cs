using System;
using System.Diagnostics;
using System.Windows.Forms;
using Sensatus.FiberTracker.Resources;

namespace Sensatus.FiberTracker.UI
{
    internal partial class AboutForm : FormBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutForm"/> class.
        /// </summary>
        public AboutForm()
        {
            InitializeComponent();
            Text = $"{Commons.About} {ApplicationDetails.AssemblyTitle}";
            lblProductName.Text = $"{Commons.ProductName} : {ApplicationDetails.AssemblyProduct}";
            lblVersion.Text = $"{Commons.Version} : {ApplicationDetails.AssemblyVersion}";
            lblCopyRight.Text = $"{Commons.CopyRight} : {ApplicationDetails.AssemblyCopyright}";
            lblCompanyName.Text = $"{Commons.CompanyName} : {ApplicationDetails.AssemblyCompany}";
            txtaDescription.Text = ApplicationDetails.AssemblyDescription;
        }

        /// <summary>
        /// Handles the Load event of the AboutAccountPlus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AboutAccountPlus_Load(object sender, EventArgs e)
        {
            SetBGColor(this);
        }

        /// <summary>
        /// Handles the Click event of the btnOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        /// <summary>
        /// Handles the LinkClicked event of the linkRedirectURL control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void linkRedirectURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Configurations.ApplicationConfiguration.SupportURL);
        }
    }
}