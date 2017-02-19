namespace Sensatus.FiberTracker.UI
{
    partial class Diagnostic
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTracingEnabled = new System.Windows.Forms.Label();
            this.lblLoggingEnabled = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblOptionSelected = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.lstFileDetails = new System.Windows.Forms.ListBox();
            this.lblFilesInfo = new System.Windows.Forms.Label();
            this.rbnTrace = new System.Windows.Forms.RadioButton();
            this.rbnLog = new System.Windows.Forms.RadioButton();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblImportMessage = new Sensatus.FiberTracker.UI.Message();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTracingEnabled);
            this.groupBox1.Controls.Add(this.lblLoggingEnabled);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 63);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings : ";
            // 
            // lblTracingEnabled
            // 
            this.lblTracingEnabled.AutoSize = true;
            this.lblTracingEnabled.Location = new System.Drawing.Point(81, 33);
            this.lblTracingEnabled.Name = "lblTracingEnabled";
            this.lblTracingEnabled.Size = new System.Drawing.Size(48, 14);
            this.lblTracingEnabled.TabIndex = 3;
            this.lblTracingEnabled.Text = "Disabled";
            // 
            // lblLoggingEnabled
            // 
            this.lblLoggingEnabled.AutoSize = true;
            this.lblLoggingEnabled.Location = new System.Drawing.Point(81, 16);
            this.lblLoggingEnabled.Name = "lblLoggingEnabled";
            this.lblLoggingEnabled.Size = new System.Drawing.Size(45, 14);
            this.lblLoggingEnabled.TabIndex = 2;
            this.lblLoggingEnabled.Text = "Enabled";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tracing : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Logging : ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblOptionSelected);
            this.groupBox2.Controls.Add(this.btnExport);
            this.groupBox2.Controls.Add(this.lstFileDetails);
            this.groupBox2.Controls.Add(this.lblFilesInfo);
            this.groupBox2.Controls.Add(this.rbnTrace);
            this.groupBox2.Controls.Add(this.rbnLog);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(12, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(426, 156);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Information : ";
            // 
            // lblOptionSelected
            // 
            this.lblOptionSelected.AutoSize = true;
            this.lblOptionSelected.Location = new System.Drawing.Point(23, 46);
            this.lblOptionSelected.Name = "lblOptionSelected";
            this.lblOptionSelected.Size = new System.Drawing.Size(65, 14);
            this.lblOptionSelected.TabIndex = 7;
            this.lblOptionSelected.Text = "Log file(s) : ";
            // 
            // btnExport
            // 
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExport.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(345, 127);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "&Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lstFileDetails
            // 
            this.lstFileDetails.FormattingEnabled = true;
            this.lstFileDetails.ItemHeight = 14;
            this.lstFileDetails.Location = new System.Drawing.Point(22, 62);
            this.lstFileDetails.Name = "lstFileDetails";
            this.lstFileDetails.ScrollAlwaysVisible = true;
            this.lstFileDetails.Size = new System.Drawing.Size(161, 88);
            this.lstFileDetails.TabIndex = 5;
            this.lstFileDetails.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstFileDetails_MouseDoubleClick);
            // 
            // lblFilesInfo
            // 
            this.lblFilesInfo.AutoSize = true;
            this.lblFilesInfo.Location = new System.Drawing.Point(154, 18);
            this.lblFilesInfo.Name = "lblFilesInfo";
            this.lblFilesInfo.Size = new System.Drawing.Size(0, 14);
            this.lblFilesInfo.TabIndex = 4;
            // 
            // rbnTrace
            // 
            this.rbnTrace.AutoSize = true;
            this.rbnTrace.Location = new System.Drawing.Point(84, 16);
            this.rbnTrace.Name = "rbnTrace";
            this.rbnTrace.Size = new System.Drawing.Size(53, 18);
            this.rbnTrace.TabIndex = 1;
            this.rbnTrace.Text = "&Trace";
            this.rbnTrace.UseVisualStyleBackColor = true;
            this.rbnTrace.CheckedChanged += new System.EventHandler(this.rbnTrace_CheckedChanged);
            // 
            // rbnLog
            // 
            this.rbnLog.AutoSize = true;
            this.rbnLog.Checked = true;
            this.rbnLog.Location = new System.Drawing.Point(22, 16);
            this.rbnLog.Name = "rbnLog";
            this.rbnLog.Size = new System.Drawing.Size(43, 18);
            this.rbnLog.TabIndex = 0;
            this.rbnLog.TabStop = true;
            this.rbnLog.Text = "&Log";
            this.rbnLog.UseVisualStyleBackColor = true;
            this.rbnLog.CheckedChanged += new System.EventHandler(this.rbnLog_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(357, 235);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblImportMessage
            // 
            this.lblImportMessage.AutoSize = true;
            this.lblImportMessage.Location = new System.Drawing.Point(12, 235);
            this.lblImportMessage.MessageText = "";
            this.lblImportMessage.Name = "lblImportMessage";
            this.lblImportMessage.Size = new System.Drawing.Size(150, 26);
            this.lblImportMessage.TabIndex = 8;
            // 
            // Diagnostic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(450, 260);
            this.Controls.Add(this.lblImportMessage);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Diagnostic";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Diagnostic";
            this.Load += new System.EventHandler(this.Diagnostic_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblTracingEnabled;
        private System.Windows.Forms.Label lblLoggingEnabled;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ListBox lstFileDetails;
        private System.Windows.Forms.Label lblFilesInfo;
        private System.Windows.Forms.RadioButton rbnTrace;
        private System.Windows.Forms.RadioButton rbnLog;
        private System.Windows.Forms.Button btnClose;
        private Message lblImportMessage;
        private System.Windows.Forms.Label lblOptionSelected;
    }
}