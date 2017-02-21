using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sensatus.FiberTracker.BusinessLogic;

namespace Sensatus.FiberTracker.UI
{
    public partial class MiscellaneousReport : Form
    {        
        public MiscellaneousReport()
        {
            InitializeComponent();
        }      

        private void MiscellaneousReport_Load(object sender, EventArgs e)
        {
            cmbMonth.SelectedIndex = 0;
            cmbYear.SelectedIndex = 0;
        }

       

        private void btnGenerate_Click(object sender, EventArgs e)
        {            
            var month = cmbMonth.Text.Trim();
            var year = cmbYear.Text.Trim();
            var dsReportData = new DataSet();

            if (month.Equals("") || year.Equals(""))
            {
                
            }
            else
            {
                
            }
        }

      }
}