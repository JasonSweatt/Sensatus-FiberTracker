using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sensatus.FiberTracker.BusinessLogic;
using Sensatus.FiberTracker.Messaging;
using Sensatus.FiberTracker.Configurations;
using Sensatus.FiberTracker.DataAccess;
using Sensatus.FiberTracker.Formatting;
using System.Collections;
using System.Diagnostics;

namespace Sensatus.FiberTracker.UI
{
    public partial class Home : AccountPlusBase
    {
        private int _expenseID = 0;
        private int _expenseByUserID = 0;
        private Expense exp = new Expense();
        private Arch arch = new Arch();
        private DBHelper helper = new DBHelper();
        private HomePageReport homePageReport = new HomePageReport();
        
        
        public Home()
        {
            InitializeComponent();
        }

        private void DisableAddAndClearMessage()
        {            
            btnAdd.Visible  = false;
        }
                   
        public void Home_Load(object sender, EventArgs e)
        {
            base.SetBGColor(this);
            InitScreenData();
            DisplayReport();
            this.Text = ApplicationDetails.AssemblyProduct + " " + ApplicationDetails.AssemblyVersion;
            if (SessionParameters.UserRole == Common.UserRole.Admin)
                btnAdd.Enabled = false;
        }

        private void InitScreenData()
        {
            DisplayDataInGrid();
            Arch arch = new Arch();            
            arch.FillDataInCombo(cmbItem, Arch.ComboBoxItem.Item );
            GetStatusBarDetails();

            if (SessionParameters.UserRole != Common.UserRole.Admin)
                MENU_FILE_ADD_USER.Enabled = false;

            lblExpenseCCY.Text = Configurations.ApplicationConfiguration.ExpenseCCY;
        }

        private void GetStatusBarDetails()
        {
            
            Users user = new Users();
            STATUS_BAR_USER_NAME.Text = "You are logged in as : " + SessionParameters.UserName;
            STATUS_BAR_LAST_LOGIN_DATE.Text = "Last login : " + user.GetLastLastLoginDate(SessionParameters.UserID);
            STATUS_BAR_CURRENT_DATE.Text = DataFormat.DateToDisp(System.DateTime.Now.ToShortDateString());
        }

     

        private void ShowUpdateDelBtn()
        {
            lblMessage.Text = string.Empty;           
            btnDel.Visible = true ;
            
            label6.Visible = true;
            txtEntryDate.Visible = true;
        }

        private void DisplayDataInGrid()
        {                               
            GRID_VIEW_EXPENSE_DETAILS.DataSource = homePageReport.HomePageDetails();
            GRID_VIEW_EXPENSE_DETAILS.Columns[0].Visible = false;            
            lblItemStatus.Text = MessageManager.GetMessage("3", homePageReport.HomePageDetails().Rows.Count.ToString());
            lblItemStatus.Visible = true;
        }   

        private void GetDataForUpdateAndDelete(int expenseID)
        {         
            DataTable dt = new DataTable();
            dt = exp.GetDisplayData(expenseID);                       
            cmbItem.Text = DataFormat.GetString(dt.Rows[0][1]);
            //cmbItem.Enabled = false;
            txtaDesc.Text  = DataFormat.GetString(dt.Rows[0][2]);
            txtPrice.Text = DataFormat.GetString(dt.Rows[0][3]);
            txtEntryDate.Value  = Convert.ToDateTime( dt.Rows[0][4].ToString().Trim());
        }
          
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearMessage();
            if (SessionParameters.UserRole == Common.UserRole.Admin)
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("4");                
                return;
            }         

            int itemId = DataFormat.GetInteger(((DictionaryEntry)(cmbItem.SelectedItem)).Key);
            string amount = txtPrice.Text.Trim();
            string message = string.Empty;

            if (itemId == -1)
            {
                message = MessageManager.GetMessage("5");
                errorProvider1.SetError(cmbItem, message);
                STATUS_BAR_STATUS_MESSAGE.Text = message;
                return;
            }

            if (amount == string.Empty)
            {
                message = MessageManager.GetMessage("6");
                errorProvider1.SetError(txtPrice, message);
                STATUS_BAR_STATUS_MESSAGE.Text = message;
                return;
            }

            if (!DataFormat.IsNumeric(amount))
            {
                message = MessageManager.GetMessage("7");
                errorProvider1.SetError(txtPrice, message);
                STATUS_BAR_STATUS_MESSAGE.Text = message;
                return;
            }

            if (DataFormat.GetDouble(amount) <= 0)
            {
                message = MessageManager.GetMessage("61");
                errorProvider1.SetError(txtPrice, message);
                STATUS_BAR_STATUS_MESSAGE.Text = message;
                return;
            }

            if (txtEntryDate.Value > DateTime.Now)
            {
                message = MessageManager.GetMessage("8");
                errorProvider1.SetError(txtEntryDate, message);
                STATUS_BAR_STATUS_MESSAGE.Text = message;
                return;
            }

            Logger.WriteTrace("Home [Add new Expense]", "User : " + SessionParameters.UserName + Environment.NewLine + "Item Id : " + itemId.ToString() + Environment.NewLine + "Description : " + txtaDesc.Text.Trim() + Environment.NewLine + "Price : " + txtPrice.Text.Trim());
            AddExpense(itemId, txtaDesc.Text.Trim(), txtPrice.Text.Trim(), SessionParameters.UserID, txtEntryDate.Value.ToString());
            DisplayReport();
        }

      
        
        private void  AddExpense(int itemId, string expDesc, string expAmount, int expBy, string expDate)
        {            
            Expense objExp = new Expense();
            bool addStatus = objExp.AddNewExpense(itemId, expDesc, expAmount, expBy, expDate);

            if (addStatus)
            {
                DisplayDataInGrid();
                ClearFunction();                    
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("9");
            }
            else
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("10");
                           
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {

        }
        
            
        private void UpadteExpense(int itemId, string expDesc, string expAmount, string expDate)
        {
            ClearMessage();
            if (exp.ModifyExpenses(itemId, _expenseID, expDesc, expAmount, expDate))
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("11");
                DisplayDataInGrid();
                ClearFunction();
            }
            else
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("12");
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            ClearMessage();
            if (SessionParameters.UserRole == Common.UserRole.Admin)
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("13");
                return;
            }

            if (!_isSelectedForUpdate)
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("14");
                return;
            }
            
            if (SessionParameters.UserID != _expenseByUserID)
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("13");
                return;
            }


            if (MessageManager.DisplayMessage("15", new string[] { Sensatus.FiberTracker.Configurations.ApplicationConfiguration.ExpenseCCY, GRID_VIEW_EXPENSE_DETAILS.SelectedRows[0].Cells["Amount"].Value.ToString(), GRID_VIEW_EXPENSE_DETAILS.SelectedRows[0].Cells["Item"].Value.ToString() }, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Logger.WriteTrace("Home [Delete Expense]", "User : " + SessionParameters.UserName + Environment.NewLine + "Expense Id : " + _expenseID);
                exp.DeleteExpenses(_expenseID);
                DisplayDataInGrid();
                ClearFunction();
                DisplayReport();
            }                                                              
         }

   
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFunction();
            _isSelectedForUpdate = false;
        }

        private void ClearMessage()
        {
            errorProvider1.Clear();
            STATUS_BAR_STATUS_MESSAGE.Text = string.Empty;
        }

        private void ClearFunction()
        {
            errorProvider1.Clear();
            cmbItem.Text = string.Empty;
            txtaDesc.Text = string.Empty;            
            txtPrice.Text = string.Empty;
            txtEntryDate.Text = string.Empty;            
            cmbItem.Enabled = true;
            _isSelectedForUpdate = false;
            _expenseID = 0;

            btnDel.Enabled = false;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            cmbItem.SelectedIndex = 0;
            STATUS_BAR_STATUS_MESSAGE.Text = string.Empty;

            if (GRID_VIEW_EXPENSE_DETAILS.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in GRID_VIEW_EXPENSE_DETAILS.Rows)
                {
                    if (row.Selected)
                    {
                        row.Selected = false;
                        return;
                    }
                }
            }
        }

        private void MENU_FILE_LOG_OFF_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void MENU_FILE_ADD_USER_Click(object sender, EventArgs e)
        {
            OpenNewUser();
        }

        private void OpenNewUser()
        {          
            NewUser objNewUser = new NewUser();
            objNewUser.ShowDialog();
        }

        private void MENU_FILE_ADD_ITEM_Click(object sender, EventArgs e)
        {
            OpennewItem();      
        }

        private void OpennewItem()
        {            
            NewItem objItem = new NewItem();
            objItem.ShowDialog();
            arch.FillDataInCombo(cmbItem, Arch.ComboBoxItem.Item);
        }

       
        private void DisplayReport()
        {
            if (homePageReport.HomePageReportData().Rows.Count > 0)
            {
                GRID_VIEW_INDIVIDUAL_EXP_REPORT.DataSource = homePageReport.HomePageReportData();
                GRID_VIEW_INDIVIDUAL_EXP_REPORT.Columns[2].Width = 150;
                grpReport.Visible = true;
            }              
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GRID_VIEW_INDIVIDUAL_EXP_REPORT.Text = string.Empty;
            grpReport.Visible = false;
        }



        private void btnCalculate_Click(object sender, EventArgs e)
        {
            OpenExpenseReport();
        }

        private void OpenExpenseReport()
        {
            IndividualExpReport objExpenseReport = new IndividualExpReport();
            objExpenseReport.ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                DisplayDataInGrid();
                DisplayReport();
            }
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (! _isApplicationExit)
                if (!DoLogOff())            
                    e.Cancel = true;
            
        }

        bool _isApplicationExit = false;
        private bool DoLogOff()
        {
            DialogResult dr = MessageManager.DisplayMessage("16", MessageBoxButtons.YesNo);
            bool retValue = false;
            if (dr == DialogResult.Yes)
            {
                _isApplicationExit = true;
                //Application.Exit();
                (new ApplicationContext()).Dispose();
                retValue = true;
            }
            else
                retValue = false;

            return retValue;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void MENU_HELP_ABOUT_Click(object sender, EventArgs e)
        {
            AboutAccountPlus aboutAccountPlus = new AboutAccountPlus();
            aboutAccountPlus.ShowDialog();
        }

        private void MENU_REPORT_MONTHLY_REPORT_Click(object sender, EventArgs e)
        {
            OpenCustomReport();
        }

        private void OpenCustomReport()
        {
            CustomReport customReport = new CustomReport();
            customReport.ShowDialog();
        }

        private void MENU_EDIT_PROFILE_Click(object sender, EventArgs e)
        {
            EditProfileForm();
        }

        private void EditProfileForm()
        {
            Profile profile = new Profile();
            profile.ShowDialog();

            DisplayDataInGrid();
            DisplayReport();

        }

        private void backupDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuration config = new Configuration();
            config.Show();           
        }

        private void changeDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            
        }

        private void analyticReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAnalyticReport();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void TOOL_PROFILE_Click(object sender, EventArgs e)
        {
            EditProfileForm();
        }



        private void TOOL_LOG_OFF_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TOOL_EXPENSE_REPORT_Click(object sender, EventArgs e)
        {
            OpenExpenseReport();
        }

        private void TOOL_MONTHLY_REPORT_Click(object sender, EventArgs e)
        {
            OpenCustomReport();
        }

        private void TOOL_ANALYTIC_REPORT_Click(object sender, EventArgs e)
        {
            ShowAnalyticReport();
        }

        private void ShowAnalyticReport()
        {
            ClearMessage();
            AnalyticReport analyticReport = new AnalyticReport();
            analyticReport.ShowDialog();
        }

   
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            ClearMessage();
            OpennewItem();
        }

      

        private void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            ClearMessage();
            DisplayDataInGrid();
        }

        private void manageMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {          
        }

        private void MENU_FILE_SWITCH_USER_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageManager.DisplayMessage("17", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                Preferences.ClearCredentials();
                this.Hide();
                Login objLogin = new Login();
                objLogin.Show();
            }
        }

        private bool _isSelectedForUpdate = false;
        private void GRID_VIEW_EXPENSE_DETAILS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            errorProvider1.Clear();
            STATUS_BAR_STATUS_MESSAGE.Text = String.Empty;
            _expenseID = DataFormat.GetInteger( GRID_VIEW_EXPENSE_DETAILS.SelectedRows[0].Cells[0].Value.ToString());
            _expenseByUserID = DataFormat.GetInteger(GRID_VIEW_EXPENSE_DETAILS.SelectedRows[0].Cells["UserId"].Value.ToString());
            GetDataForUpdateAndDelete(_expenseID);
            _isSelectedForUpdate = true;

            if (_expenseByUserID != SessionParameters.UserID)
            {
                btnUpdate.Enabled = false;
                btnDel.Enabled = false;
                btnAdd.Enabled = false;
                return;
            }
            else
            {                
                btnDel.Enabled = true;
                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;                
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            STATUS_BAR_STATUS_MESSAGE.Text = string.Empty;
            errorProvider1.Clear();
            string message = string.Empty;

            if (SessionParameters.UserRole == Common.UserRole.Admin)
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("12");
                return;
            }

            if (SessionParameters.UserID != _expenseByUserID)
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("4"); ;
                return;
            }

            if (!_isSelectedForUpdate)
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("18");
                return;
            }
                                    
            int itemId = DataFormat.GetInteger(((DictionaryEntry)(cmbItem.SelectedItem)).Key);
            string amount = txtPrice.Text.Trim();

            if (itemId == -1)
            {
                message = MessageManager.GetMessage("5");
                errorProvider1.SetError(cmbItem, message);
                STATUS_BAR_STATUS_MESSAGE.Text = message;
                return;
            }

            if (amount == string.Empty)
            {
                message = MessageManager.GetMessage("6");
                errorProvider1.SetError(txtPrice, message);
                STATUS_BAR_STATUS_MESSAGE.Text = message;
                return;
            }

            if (DataFormat.GetDouble(amount) <= 0)
            {
                message = MessageManager.GetMessage("61");
                errorProvider1.SetError(txtPrice, message);
                STATUS_BAR_STATUS_MESSAGE.Text = message;
                return;
            }

            if (!DataFormat.IsNumeric(amount))
            {
                message = MessageManager.GetMessage("7");
                errorProvider1.SetError(txtPrice, message);
                STATUS_BAR_STATUS_MESSAGE.Text = message;
                return;
            }

            if (txtEntryDate.Value > DateTime.Now)
            {
                message = MessageManager.GetMessage("8");
                errorProvider1.SetError(txtEntryDate, message);
                STATUS_BAR_STATUS_MESSAGE.Text = message;
                return;
            }
            
            Logger.WriteTrace("Home [Update Expense]", "User : " + SessionParameters.UserName + Environment.NewLine + "Expense Id : " + _expenseID + Environment.NewLine + "Description : " + txtaDesc.Text.Trim() + Environment.NewLine + "Price : " + txtPrice.Text.Trim());
            UpadteExpense(itemId, txtaDesc.Text.Trim(), txtPrice.Text.Trim(),  txtEntryDate.Value.ToShortDateString());
            ClearFunction();
            DisplayReport();
           
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DataFormat.IsInteger(e.KeyChar) != true && e.KeyChar != '.' && (Keys)e.KeyChar != Keys.Back)
                e.Handled = true;
        }

        private void MENU_REPORT_EXPENCE_REPORT_Click(object sender, EventArgs e)
        {
            IndividualExpReport expReport = new IndividualExpReport();
            expReport.ShowDialog();
        }

        private void MENU_REPORT_ANALYTIC_REPORT_Click(object sender, EventArgs e)
        {
            AnalyticReport analyticRep = new AnalyticReport();
            analyticRep.ShowDialog();
        }

        private void MENU_TOOLS_CONFIGURATION_Click(object sender, EventArgs e)
        {
            Configuration config = new Configuration();
            config.ShowDialog();
        }

        private void MENU_TOOLS_DIAGNOSTICS_Click(object sender, EventArgs e)
        {
            (new Diagnostic()).ShowDialog();
        }

        private void MENU_TOOLS_CALCULATOR_Click(object sender, EventArgs e)
        {
            RunProcess("calc");
        }

        private void RunProcess(string processName)
        {
            Process.Start(processName);
        }

        private void MENU_TOOLS_NOTEPAD_Click(object sender, EventArgs e)
        {
            RunProcess("notepad");
        }

        private void MENU_TOOLS_CALENDAR_Click(object sender, EventArgs e)
        {
            (new Calendar()).Show();
        }

        private void TOOL_HELP_Click(object sender, EventArgs e)
        {
            ShowHelpFile();
        }

        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            int itemId = DataFormat.GetInteger(((DictionaryEntry)(cmbItem.SelectedItem)).Key);
            if ( itemId == -1) 
            {
                txtaDesc.Clear();
                return;
            }
            txtaDesc.Text = (new Items()).GetItemDescription(itemId.ToString());            
        }

        private void MENU_HELP_CONTACT_ADMIN_Click(object sender, EventArgs e)
        {
            (new ContactAdmin()).ShowDialog();
        }

        private void MENU_HELP_HELP_Click(object sender, EventArgs e)
        {
            ShowHelpFile();
        }

        private void ShowHelpFile()
        {
            Process.Start(Application.StartupPath + @"\Help\Sensatus.FiberTracker.pdf");
        }

       
                     
    }
}