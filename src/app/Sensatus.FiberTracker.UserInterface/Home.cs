using Sensatus.FiberTracker.BusinessLogic;
using Sensatus.FiberTracker.DataAccess;
using Sensatus.FiberTracker.Formatting;
using Sensatus.FiberTracker.Messaging;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace Sensatus.FiberTracker.UI
{
    public partial class Home : FormBase
    {
        private int _expenseId = 0;
        private int _expenseByUserId = 0;
        private Expense _expense = new Expense();
        private Arch _arch = new Arch();
        private DBHelper _helper = new DBHelper();
        private HomePageReport _homePageReport = new HomePageReport();

        public Home()
        {
            InitializeComponent();
        }

        private void DisableAddAndClearMessage()
        {
            btnAdd.Visible = false;
        }

        public void Home_Load(object sender, EventArgs e)
        {
            SetBGColor(this);
            InitScreenData();
            DisplayReport();
            Text = ApplicationDetails.AssemblyProduct + " " + ApplicationDetails.AssemblyVersion;
            if (SessionParameters.UserRole == Common.UserRole.Admin)
                btnAdd.Enabled = false;
        }

        private void InitScreenData()
        {
            DisplayDataInGrid();
            var arch = new Arch();
            arch.FillDataInCombo(cmbItem, Arch.ComboBoxItem.Item);
            GetStatusBarDetails();

            if (SessionParameters.UserRole != Common.UserRole.Admin)
                MENU_FILE_ADD_USER.Enabled = false;

            lblExpenseCCY.Text = Configurations.ApplicationConfiguration.ExpenseCCY;
        }

        private void GetStatusBarDetails()
        {
            var user = new Users();
            STATUS_BAR_USER_NAME.Text = "You are logged in as : " + SessionParameters.UserName;
            STATUS_BAR_LAST_LOGIN_DATE.Text = "Last login : " + user.GetLastLastLoginDate(SessionParameters.UserId);
            STATUS_BAR_CURRENT_DATE.Text = DataFormat.DateToDisp(DateTime.Now.ToShortDateString());
        }

        private void ShowUpdateDelBtn()
        {
            lblMessage.Text = string.Empty;
            btnDel.Visible = true;

            label6.Visible = true;
            txtEntryDate.Visible = true;
        }

        private void DisplayDataInGrid()
        {
            GRID_VIEW_EXPENSE_DETAILS.DataSource = _homePageReport.HomePageDetails();
            GRID_VIEW_EXPENSE_DETAILS.Columns[0].Visible = false;
            lblItemStatus.Text = MessageManager.GetMessage("3", _homePageReport.HomePageDetails().Rows.Count.ToString());
            lblItemStatus.Visible = true;
        }

        private void GetDataForUpdateAndDelete(int expenseID)
        {
            var dt = new DataTable();
            dt = _expense.GetDisplayData(expenseID);
            cmbItem.Text = DataFormat.GetString(dt.Rows[0][1]);
            //cmbItem.Enabled = false;
            txtaDesc.Text = DataFormat.GetString(dt.Rows[0][2]);
            txtPrice.Text = DataFormat.GetString(dt.Rows[0][3]);
            txtEntryDate.Value = Convert.ToDateTime(dt.Rows[0][4].ToString().Trim());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearMessage();
            if (SessionParameters.UserRole == Common.UserRole.Admin)
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("4");
                return;
            }

            var itemId = DataFormat.GetInteger(((DictionaryEntry)cmbItem.SelectedItem).Key);
            var amount = txtPrice.Text.Trim();
            var message = string.Empty;

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
            AddExpense(itemId, txtaDesc.Text.Trim(), txtPrice.Text.Trim(), SessionParameters.UserId, txtEntryDate.Value.ToString());
            DisplayReport();
        }

        private void AddExpense(int itemId, string expDesc, string expAmount, int expBy, string expDate)
        {
            var objExp = new Expense();
            var addStatus = objExp.AddNewExpense(itemId, expDesc, expAmount, expBy, expDate);

            if (addStatus)
            {
                DisplayDataInGrid();
                ClearFunction();
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("9");
            }
            else
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("10");
            }
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
        }

        private void UpadteExpense(int itemId, string expDesc, string expAmount, string expDate)
        {
            ClearMessage();
            if (_expense.ModifyExpenses(itemId, _expenseId, expDesc, expAmount, expDate))
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("11");
                DisplayDataInGrid();
                ClearFunction();
            }
            else
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("12");
            }
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

            if (SessionParameters.UserId != _expenseByUserId)
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("13");
                return;
            }

            if (MessageManager.DisplayMessage("15", new string[] { Configurations.ApplicationConfiguration.ExpenseCCY, GRID_VIEW_EXPENSE_DETAILS.SelectedRows[0].Cells["Amount"].Value.ToString(), GRID_VIEW_EXPENSE_DETAILS.SelectedRows[0].Cells["Item"].Value.ToString() }, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Logger.WriteTrace("Home [Delete Expense]", "User : " + SessionParameters.UserName + Environment.NewLine + "Expense Id : " + _expenseId);
                _expense.DeleteExpenses(_expenseId);
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
            _expenseId = 0;

            btnDel.Enabled = false;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            cmbItem.SelectedIndex = 0;
            STATUS_BAR_STATUS_MESSAGE.Text = string.Empty;

            if (GRID_VIEW_EXPENSE_DETAILS.Rows.Count > 0)
                foreach (DataGridViewRow row in GRID_VIEW_EXPENSE_DETAILS.Rows)
                    if (row.Selected)
                    {
                        row.Selected = false;
                        return;
                    }
        }

        private void MENU_FILE_LOG_OFF_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MENU_FILE_ADD_USER_Click(object sender, EventArgs e)
        {
            OpenNewUser();
        }

        private void OpenNewUser()
        {
            var objNewUser = new NewUser();
            objNewUser.ShowDialog();
        }

        private void MENU_FILE_ADD_ITEM_Click(object sender, EventArgs e)
        {
            OpennewItem();
        }

        private void OpennewItem()
        {
            var objItem = new NewItem();
            objItem.ShowDialog();
            _arch.FillDataInCombo(cmbItem, Arch.ComboBoxItem.Item);
        }

        private void DisplayReport()
        {
            if (_homePageReport.HomePageReportData().Rows.Count > 0)
            {
                GRID_VIEW_INDIVIDUAL_EXP_REPORT.DataSource = _homePageReport.HomePageReportData();
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
            var objExpenseReport = new IndividualExpReport();
            objExpenseReport.ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                DisplayDataInGrid();
                DisplayReport();
            }
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isApplicationExit)
                if (!DoLogOff())
                    e.Cancel = true;
        }

        private bool _isApplicationExit = false;

        private bool DoLogOff()
        {
            var dr = MessageManager.DisplayMessage("16", MessageBoxButtons.YesNo);
            var retValue = false;
            if (dr == DialogResult.Yes)
            {
                _isApplicationExit = true;
                //Application.Exit();
                new ApplicationContext().Dispose();
                retValue = true;
            }
            else
            {
                retValue = false;
            }

            return retValue;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void MENU_HELP_ABOUT_Click(object sender, EventArgs e)
        {
            var aboutAccountPlus = new AboutForm();
            aboutAccountPlus.ShowDialog();
        }

        private void MENU_REPORT_MONTHLY_REPORT_Click(object sender, EventArgs e)
        {
            OpenCustomReport();
        }

        private void OpenCustomReport()
        {
            var customReport = new CustomReport();
            customReport.ShowDialog();
        }

        private void MENU_EDIT_PROFILE_Click(object sender, EventArgs e)
        {
            EditProfileForm();
        }

        private void EditProfileForm()
        {
            var profile = new Profile();
            profile.ShowDialog();

            DisplayDataInGrid();
            DisplayReport();
        }

        private void backupDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var config = new Configuration();
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
            Close();
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
            var analyticReport = new AnalyticReport();
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
            var dr = MessageManager.DisplayMessage("17", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                Preferences.ClearCredentials();
                Hide();
                var objLogin = new Login();
                objLogin.Show();
            }
        }

        private bool _isSelectedForUpdate = false;

        private void GRID_VIEW_EXPENSE_DETAILS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            errorProvider1.Clear();
            STATUS_BAR_STATUS_MESSAGE.Text = string.Empty;
            _expenseId = DataFormat.GetInteger(GRID_VIEW_EXPENSE_DETAILS.SelectedRows[0].Cells[0].Value.ToString());
            _expenseByUserId = DataFormat.GetInteger(GRID_VIEW_EXPENSE_DETAILS.SelectedRows[0].Cells["UserId"].Value.ToString());
            GetDataForUpdateAndDelete(_expenseId);
            _isSelectedForUpdate = true;

            if (_expenseByUserId != SessionParameters.UserId)
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
            var message = string.Empty;

            if (SessionParameters.UserRole == Common.UserRole.Admin)
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("12");
                return;
            }

            if (SessionParameters.UserId != _expenseByUserId)
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("4"); ;
                return;
            }

            if (!_isSelectedForUpdate)
            {
                STATUS_BAR_STATUS_MESSAGE.Text = MessageManager.GetMessage("18");
                return;
            }

            var itemId = DataFormat.GetInteger(((DictionaryEntry)cmbItem.SelectedItem).Key);
            var amount = txtPrice.Text.Trim();

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

            Logger.WriteTrace("Home [Update Expense]", "User : " + SessionParameters.UserName + Environment.NewLine + "Expense Id : " + _expenseId + Environment.NewLine + "Description : " + txtaDesc.Text.Trim() + Environment.NewLine + "Price : " + txtPrice.Text.Trim());
            UpadteExpense(itemId, txtaDesc.Text.Trim(), txtPrice.Text.Trim(), txtEntryDate.Value.ToShortDateString());
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
            var expReport = new IndividualExpReport();
            expReport.ShowDialog();
        }

        private void MENU_REPORT_ANALYTIC_REPORT_Click(object sender, EventArgs e)
        {
            var analyticRep = new AnalyticReport();
            analyticRep.ShowDialog();
        }

        private void MENU_TOOLS_CONFIGURATION_Click(object sender, EventArgs e)
        {
            var config = new Configuration();
            config.ShowDialog();
        }

        private void MENU_TOOLS_DIAGNOSTICS_Click(object sender, EventArgs e)
        {
            new Diagnostic().ShowDialog();
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
            new Calendar().Show();
        }

        private void TOOL_HELP_Click(object sender, EventArgs e)
        {
            ShowHelpFile();
        }

        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            var itemId = DataFormat.GetInteger(((DictionaryEntry)cmbItem.SelectedItem).Key);
            if (itemId == -1)
            {
                txtaDesc.Clear();
                return;
            }
            txtaDesc.Text = new Items().GetItemDescription(itemId.ToString());
        }

        private void MENU_HELP_CONTACT_ADMIN_Click(object sender, EventArgs e)
        {
            new ContactAdmin().ShowDialog();
        }

        private void MENU_HELP_HELP_Click(object sender, EventArgs e)
        {
            ShowHelpFile();
        }

        private void ShowHelpFile()
        {
            Process.Start(Application.StartupPath + @"\Help\Sensatus.FiberTracker.pdf");
        }

        private void testCommToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TestComm().ShowDialog();
        }
    }
}