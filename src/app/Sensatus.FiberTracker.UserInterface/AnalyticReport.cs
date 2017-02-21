using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sensatus.FiberTracker.BusinessLogic;
using Sensatus.FiberTracker.Messaging;


namespace Sensatus.FiberTracker.UI
{
    public partial class AnalyticReport : AccountPlusBase
    {
        //private MonthYearStatic staticData = new MonthYearStatic();        

        public AnalyticReport()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            message1.Clear();
            var monthlyReport = new MonthlyReport();
            var month = cmbMonth.Text.Trim();
            var year = cmbYear.Text.Trim();
            var dsReportData = new DataSet();
            var message = string.Empty;

            if (month.Equals("[SELECT]") && year.Equals("[SELECT]"))
            {
                message = MessageManager.GetMessage("44");
                message1.MessageText = message;
                errorProvider1.SetError(cmbYear, message);
                errorProvider1.SetError(cmbMonth, message);

                dgrAnalyticReport.Visible = false;
                return;
            }

            if (month.Equals("[SELECT]"))
            {
                message = MessageManager.GetMessage("44");
                message1.MessageText = message;                
                errorProvider1.SetError(cmbMonth, message);

                dgrAnalyticReport.Visible = false;
                return;
            }

            if (year.Equals("[SELECT]"))
            {
                message = MessageManager.GetMessage("44");
                message1.MessageText = message;
                errorProvider1.SetError(cmbYear, message);                

                dgrAnalyticReport.Visible = false;
                return;
            }

            dsReportData = monthlyReport.MonthlyReportData(month, year);
            var analytics = new Analytics();
            var table = new DataTable();

            if(rbnItemWise.Checked)
                table = analytics.AnalyticReport(month, year, Analytics.AnalyticReportType.ItemWise );
            else
                table = analytics.AnalyticReport(month, year, Analytics.AnalyticReportType.Individual);
            
            if (table.Rows.Count > 0)
            {
                dgrAnalyticReport.Visible = true;
                dgrAnalyticReport.DataSource = table;
                grpAnalyticReport.Visible = true;
                SetGridStyle();
            }
            else
            {
                grpAnalyticReport.Visible = false;
                dgrAnalyticReport.Visible = false;
                MessageManager.DisplayMessage("45" ,month ,year);                    
            }

        }

        private void SetGridStyle()
        {
            foreach (DataGridViewColumn col in dgrAnalyticReport.Columns)
            {
                col.ReadOnly = true;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void AnalyticReport_Load(object sender, EventArgs e)
        {
            SetBGColor(this);
            cmbMonth.SelectedIndex = 0;
            cmbYear.SelectedIndex = 0;
        }

          
    }
}