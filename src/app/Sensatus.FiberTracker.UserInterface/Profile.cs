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

namespace Sensatus.FiberTracker.UI
{
    public partial class Profile : AccountPlusBase 
    {
        

        public Profile()
        {
            InitializeComponent();
        }

        private void chkChangePassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkChangePassword.Checked)
            {
                txtOldPassword.Enabled = true;
                txtPAssword.Enabled = true;
                txtConfirmPassword.Enabled = true;

                txtOldPassword.Clear();
                txtPAssword.Clear();
                txtConfirmPassword.Clear();
            }
            else
            {
                txtOldPassword.Enabled = false;
                txtPAssword.Enabled = false;
                txtConfirmPassword.Enabled = false;

                txtOldPassword.Clear();
                txtPAssword.Clear();
                txtConfirmPassword.Clear();
            }
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            base.SetBGColor(this);
            InitScreenData();
        }

        private void InitScreenData()
        {
            Users manageProfile = new Users();
            DataTable dtUserProfile = manageProfile.GetUserProfile(SessionParameters.UserID);
            txtUserID.Text = DataFormat.GetString(dtUserProfile.Rows[0]["User_Name"]);
            txtFName.Text = DataFormat.GetString(dtUserProfile.Rows[0]["First_Name"]);
            txtLName.Text = DataFormat.GetString(dtUserProfile.Rows[0]["Last_Name"]);
            txtEmail.Text = DataFormat.GetString(dtUserProfile.Rows[0]["email"]);
            txtMobile.Text = DataFormat.GetString(dtUserProfile.Rows[0]["mobile"]);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            lblMessage.Clear();
            errorProvider1.Clear();

            string firstName = txtFName.Text.Trim();
            string lastName = txtLName.Text.Trim();
            string oldPassword = txtOldPassword.Text.Trim();
            string newPassword = txtPAssword.Text.Trim();   
            string confirmNewPassword = txtConfirmPassword.Text.Trim();
            string email = txtEmail.Text.Trim();
            string mobile = txtMobile.Text.Trim();
            string message = string.Empty;
            if (firstName == string.Empty)
            {
                message = MessageManager.GetMessage("46");
                errorProvider1.SetError(txtFName, message);
                lblMessage.MessageText = message;
                return;
            }

            if (chkChangePassword.Checked)
            {
                if (oldPassword == string.Empty)
                {
                    message = MessageManager.GetMessage("47");
                    errorProvider1.SetError(txtOldPassword, message);
                    lblMessage.MessageText = message;
                    return;
                }

                if (newPassword == string.Empty)
                {
                    message = MessageManager.GetMessage("48");
                    errorProvider1.SetError(txtPAssword, message);
                    lblMessage.MessageText = message;
                    return;
                }

                if (confirmNewPassword == string.Empty)
                {
                    message = MessageManager.GetMessage("49");
                    errorProvider1.SetError(txtConfirmPassword, message);
                    lblMessage.MessageText = message;
                    return;
                }

                if (string.Compare(newPassword, confirmNewPassword, true) != 0)
                {
                    message = MessageManager.GetMessage("50");
                    errorProvider1.SetError(txtConfirmPassword, message);
                    lblMessage.MessageText = message;
                    return;
                }

                if(!_manageProfile.IsValidPassword(SessionParameters.UserID, oldPassword))
                {
                    message = MessageManager.GetMessage("51");
                    errorProvider1.SetError(txtOldPassword, message);
                    lblMessage.MessageText = message;
                    return;
                }
            }

            bool status = false;
            status = chkChangePassword.Checked ? _manageProfile.UpdateUserProfile(SessionParameters.UserID, firstName, lastName, newPassword, email, mobile) : _manageProfile.UpdateUserProfile(SessionParameters.UserID, firstName, lastName, email, mobile);

            if (status)                            
                lblMessage.MessageText = MessageManager.GetMessage("52");            
            else                            
                lblMessage.MessageText = MessageManager.GetMessage("53");            
        }

        Users _manageProfile = new Users();
        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DataFormat.IsInteger(e.KeyChar) != true && (Keys)e.KeyChar != Keys.Back)
                e.Handled = true;
        }
    }
}