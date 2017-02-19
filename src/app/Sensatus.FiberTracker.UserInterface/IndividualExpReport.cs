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
    public partial class IndividualExpReport : AccountPlusBase
    {
     

        public IndividualExpReport()
        {
            InitializeComponent();
        }

        private void ExpenseReport_Load(object sender, EventArgs e)
        {
            base.SetBGColor(this);
            btnFinalize.Enabled = SessionParameters.UserRole == Common.UserRole.Admin ? true : false;
            InitScreenData();
        }

        private void InitScreenData()
        {            
            ExpenseReport objReport = new ExpenseReport();
            string otherDetails = string.Empty;
            string totalExpense = objReport.GetTotalExpenses();

            if (objReport.GetDataTableForDisplay().Rows.Count > 0)
            {
                dataGridView1.DataSource = objReport.GetDataTableForDisplay();
                string message = "Total Expense " + Sensatus.FiberTracker.Configurations.ApplicationConfiguration.ExpenseCCY + " : " + totalExpense + "\n" + "No of participants : " + objReport.GetAllUsers().Length.ToString() + "\n" + "Individual Contribution (" + Sensatus.FiberTracker.Configurations.ApplicationConfiguration.ExpenseCCY + ") : " + objReport.GetIndividualExpense();
                lblMessage.Text = message;

                otherDetails = "Date : " + System.DateTime.Now.ToLongDateString();
                otherDetails = otherDetails + "\n" + "Days : " + objReport.ReportForDays();
                otherDetails = otherDetails + "\n" + "Per Day Expense (" + Sensatus.FiberTracker.Configurations.ApplicationConfiguration.ExpenseCCY + ") : " + objReport.PerDayExpense();

                btnFinalize.Enabled = totalExpense != "0" ? true : false;
                lblDateTime.Text = otherDetails;
            }
            else
            {
                btnFinalize.Enabled = false;
                message1.SetMessage(MessageManager.GetMessage("39"));
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
            this.Dispose();
        }

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            if (SessionParameters.UserRole != Common.UserRole.Admin)
                MessageManager.DisplayMessage("4");
            else
                FinalizeExpense();            
        }

        private void FinalizeExpense()
        {
            string message = string.Empty;
            DialogResult dr = MessageManager.DisplayMessage("40", MessageBoxButtons.YesNo);           

            if (!dr.Equals(DialogResult.Yes))            
                return;                        
            dr = MessageManager.DisplayMessage("41",Environment.NewLine, Environment.NewLine,  MessageBoxButtons.OKCancel);

            if (dr == DialogResult.OK)
            {
                FinalizeReport finalizeReport = new FinalizeReport();
                bool finalizeStatus = false;                               
                finalizeStatus = finalizeReport.Finalize();

                if (finalizeStatus)
                {
                    MessageManager.DisplayMessage("42");
                    InitScreenData();
                }
                else
                    MessageManager.DisplayMessage("43");                   
            }
            else            
                return;                        
        }
    }
}