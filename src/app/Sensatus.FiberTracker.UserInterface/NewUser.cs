using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sensatus.FiberTracker.BusinessLogic;
using Sensatus.FiberTracker.Messaging;
using Sensatus.FiberTracker.Formatting;
using System.Collections;



namespace Sensatus.FiberTracker.UI
{
    public partial class NewUser : FormBase
    {
        
        public NewUser()
        {          
            InitializeComponent();
            new Arch().FillDataInCombo(cmbRole, Arch.ComboBoxItem.Role);
            cmbRole.SelectedIndex = 0;
            GetData(-1, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            AddUpdateUser();
        }

        private void AddUpdateUser()
        {
            errorProvider1.Clear();
            lblMessage.Clear();
            var user = new Users();
            var userName = txtUserId.Text.Trim();
            var password = txtPassword.Text.Trim();
            var confirmPassword = txtConfirmPassword.Text.Trim();
            var firstName = txtFirstName.Text.Trim();
            var lastName = txtLastName.Text.Trim();
            var email = txtEmail.Text.Trim();
            var mobile = txtMobile.Text.Trim();
            var roleId = DataFormat.GetInteger(((DictionaryEntry)cmbRole.SelectedItem).Key);

            var message = string.Empty;

            if (SessionParameters.UserRole != Common.UserRole.Admin)
            {
                lblMessage.SetMessage(MessageManager.GetMessage("4"));
                return;
            }

            if (roleId == -1)
            {
                message = MessageManager.GetMessage("19");
                errorProvider1.SetError(cmbRole, message);
                lblMessage.SetMessage(message);
                return;
            }

            if (userName == string.Empty)
            {
                message = MessageManager.GetMessage("20");
                errorProvider1.SetError(txtUserId, message);
                lblMessage.SetMessage(message);
                return;
            }
            else
            {
                if (!txtUserId.ReadOnly) 
                    if (user.CheckUserExist(userName))
                    {
                        message = MessageManager.GetMessage("27", userName);
                        errorProvider1.SetError(txtUserId, message);                    
                        return;
                    }                
            }

            if (password == string.Empty)
            {
                message = MessageManager.GetMessage("21");
                errorProvider1.SetError(txtPassword, message);
                lblMessage.SetMessage(message);
                return;
            }

            if (confirmPassword == string.Empty)
            {
                message = MessageManager.GetMessage("22");
                errorProvider1.SetError(txtConfirmPassword, message);
                lblMessage.SetMessage(message);
                return;
            }

            if (firstName == string.Empty)
            {
                message = MessageManager.GetMessage("23");
                errorProvider1.SetError(txtFirstName, message);
                lblMessage.SetMessage(message);
                return;
            }

            if (string.Compare(password, confirmPassword, true) != 0)
            {
                message = MessageManager.GetMessage("24");
                errorProvider1.SetError(txtPassword, message);
                errorProvider1.SetError(txtConfirmPassword, message);
                lblMessage.SetMessage(message);
                return;
            }

            if (_userId != 0)
            {
                if (user.UpdateUserDetails(_userId, roleId, password, firstName, lastName, email, mobile, chkActive.Checked ))
                {                                        
                    GetData(-1, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    Clear();
                    lblMessage.SetMessage(MessageManager.GetMessage("25"));
                }
                else
                {
                    lblMessage.SetMessage(MessageManager.GetMessage("26"));
                }
            }
            else
            {
                if (user.CreateNewUser(roleId, userName, password, firstName, lastName, email, mobile, chkActive.Checked))
                {                    
                    GetData(-1, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    Clear();
                    lblMessage.SetMessage(MessageManager.GetMessage("28", userName));
                }
                else
                {
                    lblMessage.SetMessage(MessageManager.GetMessage("29", userName));
                }
            }
            
        }

        private void GetData(int roleId, string userName, string firstName, string lastName, string email, string mobile)
        {
            errorProvider1.Clear();
            lblMessage.Clear(); 
            var sqlCommand = new StringBuilder("SELECT U.User_Id, U.Pwd , U.User_Name,U.RoleId, R.Role, U.First_Name, U.Last_Name, U.EMail, U.Mobile, U.IsActive "); 
            sqlCommand.Append("FROM User_Info AS U, RoleDetails AS R ");
            sqlCommand.Append("WHERE U.RoleId = R.RoleId ");

            if (userName != string.Empty)
                sqlCommand.Append("AND U.User_Name LIKE '" + userName + "'");

            if (firstName != string.Empty)
                sqlCommand.Append("AND U.First_Name LIKE '" + firstName + "' ");

            if (lastName != string.Empty)
                sqlCommand.Append("AND U.Last_Name LIKE '"+ lastName +"' ");

            if (roleId != -1)
                sqlCommand.Append("AND U.RoleId = " + roleId.ToString());

            if (email != string.Empty)
                sqlCommand.Append("AND U.Email LIKE '" + email + "'");

            if (mobile != string.Empty)
                sqlCommand.Append("AND U.Mobile = '" + mobile + "'");

            GRID_VIEW_USER_DETAILS.AutoGenerateColumns = false;
            GRID_VIEW_USER_DETAILS.DataSource = new DataAccess.DBHelper().ExecuteDataTable(sqlCommand.ToString());
                                        
        }

        private void CreaNewUser(int roleId, string userName, string password, string confirmPassword, string firstName, string lastName, string email, string mobile, bool isActive)
        {
            var objUsers = new Users();                
                                
            if(objUsers.CreateNewUser(roleId, userName, password, firstName, lastName, email, mobile, isActive))
                lblMessage.SetMessage(MessageManager.GetMessage("28", userName));
            else 
                lblMessage.SetMessage(MessageManager.GetMessage("29", userName));
       }
        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            errorProvider1.Clear();
            lblMessage.Clear();
            txtConfirmPassword.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPassword.Clear();
            txtUserId.Clear();
            txtEmail.Clear();
            txtMobile.Clear();
            txtUserId.ReadOnly = false;
            
            btnUpdate.Enabled = false;
            btnSubmit.Enabled = true;
            btnSearch.Enabled = true;
            cmbRole.SelectedIndex = 0;
            _userId = 0;
        }

        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DataFormat.IsInteger(e.KeyChar) != true  && (Keys)e.KeyChar != Keys.Back)
                e.Handled = true ;
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetData(DataFormat.GetInteger(((DictionaryEntry)cmbRole.SelectedItem).Key), txtUserId.Text.Trim(), txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtEmail.Text.Trim(), txtMobile.Text.Trim());
        }

        private int _userId = 0;
        private void GRID_VIEW_USER_DETAILS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            errorProvider1.Clear();
            lblMessage.Clear();

            if (e.RowIndex == -1)
                return;
    
            var userId = DataFormat.GetInteger(GRID_VIEW_USER_DETAILS.Rows[e.RowIndex].Cells["UserId"].Value);
            var password = new DataSecurity().Decrypt(DataFormat.GetString(GRID_VIEW_USER_DETAILS.Rows[e.RowIndex].Cells["Password"].Value));
            _userId = userId;
            txtUserId.Text  = DataFormat.GetString(GRID_VIEW_USER_DETAILS.Rows[e.RowIndex].Cells["UserName"].Value);
            txtFirstName.Text = DataFormat.GetString(GRID_VIEW_USER_DETAILS.Rows[e.RowIndex].Cells["FirstName"].Value);
            txtLastName.Text = DataFormat.GetString(GRID_VIEW_USER_DETAILS.Rows[e.RowIndex].Cells["LastName"].Value);
            txtEmail.Text = DataFormat.GetString(GRID_VIEW_USER_DETAILS.Rows[e.RowIndex].Cells["Email"].Value);
            txtMobile.Text = DataFormat.GetString(GRID_VIEW_USER_DETAILS.Rows[e.RowIndex].Cells["Mobile"].Value);
            txtPassword.Text = password ;
            txtConfirmPassword.Text = password;
            chkActive.Checked = DataFormat.GetString(GRID_VIEW_USER_DETAILS.Rows[e.RowIndex].Cells["IsActive"].Value) =="0" ? false : true;
            cmbRole.SelectedItem = new DictionaryEntry(DataFormat.GetString(GRID_VIEW_USER_DETAILS.Rows[e.RowIndex].Cells["RoleId"].Value), DataFormat.GetString(GRID_VIEW_USER_DETAILS.Rows[e.RowIndex].Cells["Role"].Value));
           
            btnUpdate.Enabled = true;
            txtUserId.ReadOnly = true;
            btnSubmit.Enabled = false;
            btnSearch.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageManager.DisplayMessage("31", txtUserId.Text.Trim() , MessageBoxButtons.YesNo) == DialogResult.Yes)
                if (new Users().DeleteUser(_userId))
                {
                    Clear();
                    lblMessage.SetMessage(MessageManager.GetMessage("32"));
                }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            AddUpdateUser();            
        }

        private void NewUser_Load(object sender, EventArgs e)
        {
            SetBGColor(this);
        }

       

     
    }
}