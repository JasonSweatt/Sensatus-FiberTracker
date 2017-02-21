using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sensatus.FiberTracker.BusinessLogic;
using Sensatus.FiberTracker.Messaging;
using Sensatus.FiberTracker.Format;
using Sensatus.FiberTracker.Formatting;


namespace Sensatus.FiberTracker.UI
{
    public partial class CustomReport : AccountPlusBase
    {
        
        private MonthlyReport monthlyReport = new MonthlyReport();
        

        public CustomReport()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            message1.Clear();
            var month = cmbMonth.Text.Trim();
            var year = cmbYear.Text.Trim();
            var dsReportData = new DataSet();
            var columnIndex = 0;
            var reportStatistics = string.Empty;
            var message = string.Empty;

            if (month.Equals("[SELECT]"))
            {
                message = MessageManager.GetMessage("44");
                errorProvider1.SetError(cmbMonth, message);
                message1.MessageText = message;
                grpExpenseStatistics.Visible = false;
                grpReport.Visible = false ;
                return;
            }

            if (year.Equals("[SELECT]"))
            {
                message = MessageManager.GetMessage("44");
                errorProvider1.SetError(cmbYear, message);
                message1.MessageText = message ;
                grpExpenseStatistics.Visible = false;
                grpReport.Visible = false;
                return;
            }
            
            if (rbnIndividual.Checked)                
                dsReportData = monthlyReport.MonthlyReportData(month, year, MonthlyReport.ReportType.Individual);                
            else                
                dsReportData = monthlyReport.MonthlyReportData(month, year, MonthlyReport.ReportType.ItemWise);

            if (dsReportData.Tables[0].Rows.Count > 0)
            {
                lblMessage.Text = monthlyReport.GeneralDetails(month, year);
                lblFinalizeDetails.Text = GetReportFinalizationDetails(month, year);
                grpExpenseStatistics.Visible = true;
                grpReport.Visible = true;
                dgrReport.DataSource = dsReportData.Tables[0];
                
                if(rbnIndividual.Checked)
                    dgrReport.Columns[0].Visible = false;

                columnIndex = rbnIndividual.Checked == true ? 2 : 1;
                dgrReport.Columns[columnIndex].Width = 150;
                SetGridStyle();
            }
            else
            {
                grpExpenseStatistics.Visible = false;
                grpReport.Visible = false;
                MessageManager.DisplayMessage("45", month, year);
            }                                                     
            
        }

        private void SetGridStyle()
        {
            foreach (DataGridViewColumn col in dgrReport.Columns)
            {
                col.ReadOnly = true;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private string GetReportFinalizationDetails(string month, string year)
        {
            var finalizationDetails = monthlyReport.ReportFinalizeDetails(month, year);
            var details = string.Empty;
            var serialNo =0;
            
            if(finalizationDetails != null && finalizationDetails.Length > 0)
            {
                details = details + "Finalization Dates" + Environment.NewLine;
                for(var i=0;i<finalizationDetails.Length ;i++)
                {                    
                    serialNo = i+1;
                    if(!finalizationDetails[i].Equals("N\\A"))
                        details = details + serialNo.ToString() + ". " + DataFormat.GetDateFromDBDate(finalizationDetails[i]) + Environment.NewLine ;
                    else
                        details = details + finalizationDetails[i] + Environment.NewLine;
                }                
            }

            return details;
        }
        

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void CustomReport_Load(object sender, EventArgs e)
        {
            SetBGColor(this);
            cmbMonth.SelectedIndex = 0;
            cmbYear.SelectedIndex = 0;
        }       
    }
}