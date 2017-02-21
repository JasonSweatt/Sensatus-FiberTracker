using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sensatus.FiberTracker.Messaging;
using Sensatus.FiberTracker.BusinessLogic;
using Sensatus.FiberTracker.Formatting;


namespace Sensatus.FiberTracker.UI
{
    public partial class Login : AccountPlusBase
    {
        private UserAuthentication _userAuthentication = new UserAuthentication();
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var userName = txtUserID.Text.Trim();
                var password = txtPassword.Text.Trim();
                errorProvider1.Clear();
                lblMessage.Text = string.Empty;

                if (userName == string.Empty && password == string.Empty)
                {
                    lblMessage.Text = MessageManager.GetMessage("1", false);
                    errorProvider1.SetError(txtUserID, MessageManager.GetMessage("1", false));
                    return;
                }

                if (userName == string.Empty)
                {
                    lblMessage.Text = MessageManager.GetMessage("1", false);
                    errorProvider1.SetError(txtUserID, MessageManager.GetMessage("1", false));

                    return;
                }

                if (password == string.Empty)
                {
                    lblMessage.Text = MessageManager.GetMessage("1", false);
                    errorProvider1.SetError(txtPassword, MessageManager.GetMessage("1", false));

                    return;
                }

                var userId = 0;
                var role = new Common.UserRole();

                var validUser = _userAuthentication.IsValidUser(userName, password, out userId, out role);
                Logger.WriteTrace("Login", "Username : " + userName + Environment.NewLine + "Success : " + validUser.ToString());

                if (!validUser)
                {
                    MessageManager.DisplayCustomMessage("Invalid user Id or password.");
                }
                else
                {
                    if (chkRememberMe.Checked)
                        SavePreference(true);
                    else
                        SavePreference(false);
                    
                    SessionParameters.UserID = userId;
                    SessionParameters.UserName = userName;
                    SessionParameters.UserRole = role;                    
                    new Users().UpdateLastLoginDate(SessionParameters.UserID);

                    Hide();
                    var objHome = new Home();
                    objHome.Show();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        private void SavePreference(bool rememberMe)
        {
            if (rememberMe)
            {
                Preferences.SavePreference(Preferences.Preference.Username, txtUserID.Text.Trim());
                Preferences.SavePreference(Preferences.Preference.Password, txtPassword.Text.Trim());
                Preferences.SavePreference(Preferences.Preference.RememberMe, "true");
            }
            else
            {
                Preferences.SavePreference(Preferences.Preference.Username, string.Empty);
                Preferences.SavePreference(Preferences.Preference.Password, string.Empty);
                Preferences.SavePreference(Preferences.Preference.RememberMe, "false");
            }

            Preferences.SavePreference(Preferences.Preference.LastLoginDate, DateTime.Now.ToString());
            Preferences.SavePreference(Preferences.Preference.LastUser, txtUserID.Text.Trim());
            Preferences.Save();
        }

    

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {                 
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            SetBGColor(this);
            LoadPreferences();
            lblProductName.Text = ApplicationDetails.AssemblyProduct;
            lblVersion.Text = ApplicationDetails.AssemblyVersion;
        }

        private void LoadPreferences()
        {
            if(DataFormat.GetBoolean(Preferences.GetPreference(Preferences.Preference.RememberMe)))
            {
                txtUserID.Text = Preferences.GetPreference(Preferences.Preference.Username).Trim();
                txtPassword.Text = Preferences.GetPreference(Preferences.Preference.Password).Trim();
            }

            lblComputerName.Text = "Computer name : " + Environment.MachineName;
            lblLastLogonBy.Text = "Last logon by : " + Preferences.GetPreference(Preferences.Preference.LastUser);
            lblLastLogOnDate.Text = "Last logon date : " + Preferences.GetPreference(Preferences.Preference.LastLoginDate);
        }

 
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
            new ApplicationContext().Dispose();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            var buttonText = ((Button)sender).Text.Trim();
            switch (buttonText)
            {
                case "&Info >>":
                    Width = 425;
                    Height = 328;
                    btnLogin.Location = new Point(198, 268);
                    btnCancel.Location = new Point(261, 268);
                    btnInfo.Location = new Point(348, 268);
                    btnInfo.Text = "&Info <<";
                    grpLogonInfo.Location = new Point(20, 191);
                    break;
                case "&Info <<":
                    Width = 425;
                    Height = 255;
                    btnLogin.Location = new Point(198, 192);
                    btnCancel.Location = new Point(261, 192);
                    btnInfo.Location = new Point(348, 192);
                    btnInfo.Text = "&Info >>";
                    grpLogonInfo.Location = new Point(20, 220);

                    break;
            }
        }
    }
}