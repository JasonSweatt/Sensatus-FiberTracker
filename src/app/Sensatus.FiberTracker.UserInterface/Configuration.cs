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
using System.IO;

namespace Sensatus.FiberTracker.UI
{
    public partial class Configuration : AccountPlusBase
    {        

        public Configuration()
        {
            InitializeComponent();
        }
                      
        private void Configuration_Load(object sender, EventArgs e)
        {
            SetBGColor(this);
            InitScreenData();
        }
                     
        private void InitScreenData()
        {
            lblDBName.Text = Configurations.ApplicationConfiguration.DBProvider;
            if (string.Compare(lblDBName.Text.Trim(), "MSACCESS", true) == 0 && SessionParameters.UserRole == Common.UserRole.Admin )
                btnBackup.Enabled = true;                      
        }
             
        private void btnBackup_Click(object sender, EventArgs e)
        {
            var folderBrowse = new FolderBrowserDialog();
            folderBrowse.ShowDialog();
            var selectedPath = folderBrowse.SelectedPath;

            if (selectedPath == string.Empty)
            {
                message1.SetMessage(MessageManager.GetMessage("54"));
                return;
            }

            var sourceFileName = GetDBLocation();

            try
            {
                File.Copy(sourceFileName, selectedPath + @"\" + Path.GetFileName(sourceFileName), true);
                message1.SetMessage(MessageManager.GetMessage("55"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetDBLocation()
        {
            var msAccessDBPath = string.Empty;
            var dbNameAttribute = Configurations.ApplicationConfiguration.DBProvider == "MSACCESS" ? "Data Source" : "dbq";
            foreach (var str in Configurations.ApplicationConfiguration.ConnectionString.Split(Convert.ToChar(";")))
                if (str.Contains("="))
                    if (string.Compare(str.Split(Convert.ToChar("="))[0], dbNameAttribute, true) == 0)
                    {
                        msAccessDBPath = str.Split(Convert.ToChar("="))[1];
                        break;
                    }

            return msAccessDBPath;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

       
    }
}