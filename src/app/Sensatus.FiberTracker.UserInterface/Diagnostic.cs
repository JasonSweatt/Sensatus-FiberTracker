using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Sensatus.FiberTracker.Messaging;
using System.IO;

namespace Sensatus.FiberTracker.UI
{
    public partial class Diagnostic : AccountPlusBase
    {
        public Diagnostic()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            if (Sensatus.FiberTracker.Configurations.ApplicationConfiguration.TracingEnabled)
                lblTracingEnabled.Text = "Enable";
            else
            {
                lblTracingEnabled.Text = "Disable";
                rbnTrace.Enabled = false;
            }

            if (Sensatus.FiberTracker.Configurations.ApplicationConfiguration.LoggingEnabled)
                lblLoggingEnabled.Text = "Enable";
            else
                lblLoggingEnabled.Text = "Disable";
        }
  
        private void DisplayDignosisInfo()
        {
            lblImportMessage.Text = string.Empty;

            string[] files = null;
            string logTrace = string.Empty;
            if (rbnLog.Checked)
            {
                lblOptionSelected.Text = "Log file(s) : ";
                files = GetFiles(Environment.CurrentDirectory + @"\Diagnostics\Log\");
                logTrace = "log";
            }
            else
            {
                lblOptionSelected.Text = "Trace file(s) : ";
                files = GetFiles(Environment.CurrentDirectory + @"\Diagnostics\Trace\");
                logTrace = "trace";
            }

            if ((files != null))
            {
                lstFileDetails.Items.Clear();

                if (files.Length > 0)
                {
                    foreach (string file in files)
                        lstFileDetails.Items.Add(file);

                    lblFilesInfo.Text =  MessageManager.GetMessage("56",logTrace , files.Length.ToString());
                    btnExport.Enabled = true;
                }
                else
                {
                    lblFilesInfo.Text = MessageManager.GetMessage("56", logTrace, "0");
                    btnExport.Enabled = false;
                }
            }
            else
            {
                lblFilesInfo.Text = MessageManager.GetMessage("56", logTrace, "0");
                btnExport.Enabled = false;
            }
            
        }

        private string[] GetFiles(string path)
        {
            List<string> fileName = new List<string>();
            string[] files = null;
            if (Directory.Exists(path))
            {
                files = Directory.GetFiles(path);
                foreach (string file in files)                
                    fileName.Add(Path.GetFileName(file));
                
            }

            return fileName.ToArray();
        }
              
        private void OpenFile(string fileName)
        {
            if (System.IO.File.Exists(fileName))            
                Process.Start(fileName);
        }

       

        private void ImportFiles(string importLocation)
        {
            try
            {
                string fromPath = string.Empty;
                if (rbnLog.Checked)
                    fromPath = Environment.CurrentDirectory + @"\Diagnostics\Log\";                
                else
                    fromPath = Environment.CurrentDirectory + @"\Diagnostics\Trace\";                

                if (Directory.Exists(fromPath))
                {
                    string[] files = Directory.GetFiles(fromPath);
                    string fileName = string.Empty;
                    if ((files != null))
                    {
                        foreach (string filePath in files)
                        {
                            fileName = Path.GetFileName(filePath);
                            File.Copy(filePath, importLocation + "\\" + fileName, true);
                        }
                        lblImportMessage.Text = MessageManager.GetMessage("57");
                    }
                }
                else
                    lblImportMessage.Text = MessageManager.GetMessage("58");
            }
            catch (Exception ex)
            {
                lblImportMessage.Text = MessageManager.GetMessage("59");
                throw ex;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (lstFileDetails.Items.Count == 0)
            {
                lblImportMessage.Text = MessageManager.GetMessage("58");
                return;
            }

            lblImportMessage.Text = string.Empty;
            FolderBrowserDialog folderBrowse = new FolderBrowserDialog();

            if (folderBrowse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = folderBrowse.SelectedPath;

                if (string.IsNullOrEmpty(path))
                    lblImportMessage.Text = MessageManager.GetMessage("60");
                else                
                    ImportFiles(path);                
            }            
        }

        private void lstFileDetails_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lblImportMessage.Text = string.Empty;
            if (lstFileDetails.Items.Count == 0) return;

            string selectedFile = string.Empty;
            if ((lstFileDetails.SelectedItem != null))
            {
                selectedFile = lstFileDetails.SelectedItem.ToString();
                if (rbnLog.Checked)
                    OpenFile(Environment.CurrentDirectory + @"\Diagnostics\Log\" + selectedFile);                
                else
                    OpenFile(Environment.CurrentDirectory + @"\Diagnostics\Trace\" + selectedFile);                
                
            }
        }

        private void Diagnostic_Load(object sender, EventArgs e)
        {
            base.SetBGColor(this);
            LoadData();
            DisplayDignosisInfo();
        }

        private void rbnLog_CheckedChanged(object sender, EventArgs e)
        {
            DisplayDignosisInfo();
        }

        private void rbnTrace_CheckedChanged(object sender, EventArgs e)
        {
            DisplayDignosisInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

     

    }
}
