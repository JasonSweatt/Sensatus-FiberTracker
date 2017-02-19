namespace Sensatus.FiberTracker.UI
{
    partial class ContactAdmin
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbnSuggestion = new System.Windows.Forms.RadioButton();
            this.rbnQuery = new System.Windows.Forms.RadioButton();
            this.rbnFeatureRequest = new System.Windows.Forms.RadioButton();
            this.rbnReportBug = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.txtaMessage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.linkRedirectURL = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(168, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Contact Admin";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.linkRedirectURL);
            this.groupBox1.Controls.Add(this.rbnSuggestion);
            this.groupBox1.Controls.Add(this.rbnQuery);
            this.groupBox1.Controls.Add(this.rbnFeatureRequest);
            this.groupBox1.Controls.Add(this.rbnReportBug);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(451, 81);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rearding : ";
            // 
            // rbnSuggestion
            // 
            this.rbnSuggestion.AutoSize = true;
            this.rbnSuggestion.Location = new System.Drawing.Point(129, 42);
            this.rbnSuggestion.Name = "rbnSuggestion";
            this.rbnSuggestion.Size = new System.Drawing.Size(78, 17);
            this.rbnSuggestion.TabIndex = 3;
            this.rbnSuggestion.Text = "S&uggestion";
            this.rbnSuggestion.UseVisualStyleBackColor = true;
            this.rbnSuggestion.CheckedChanged += new System.EventHandler(this.ContactAdminOptions_CheckedChanged);
            // 
            // rbnQuery
            // 
            this.rbnQuery.AutoSize = true;
            this.rbnQuery.Location = new System.Drawing.Point(129, 19);
            this.rbnQuery.Name = "rbnQuery";
            this.rbnQuery.Size = new System.Drawing.Size(59, 17);
            this.rbnQuery.TabIndex = 2;
            this.rbnQuery.Text = "&Query..";
            this.rbnQuery.UseVisualStyleBackColor = true;
            this.rbnQuery.CheckedChanged += new System.EventHandler(this.ContactAdminOptions_CheckedChanged);
            // 
            // rbnFeatureRequest
            // 
            this.rbnFeatureRequest.AutoSize = true;
            this.rbnFeatureRequest.Location = new System.Drawing.Point(14, 42);
            this.rbnFeatureRequest.Name = "rbnFeatureRequest";
            this.rbnFeatureRequest.Size = new System.Drawing.Size(99, 17);
            this.rbnFeatureRequest.TabIndex = 1;
            this.rbnFeatureRequest.Text = "&Feature request";
            this.rbnFeatureRequest.UseVisualStyleBackColor = true;
            this.rbnFeatureRequest.CheckedChanged += new System.EventHandler(this.ContactAdminOptions_CheckedChanged);
            // 
            // rbnReportBug
            // 
            this.rbnReportBug.AutoSize = true;
            this.rbnReportBug.Checked = true;
            this.rbnReportBug.Location = new System.Drawing.Point(14, 19);
            this.rbnReportBug.Name = "rbnReportBug";
            this.rbnReportBug.Size = new System.Drawing.Size(78, 17);
            this.rbnReportBug.TabIndex = 0;
            this.rbnReportBug.TabStop = true;
            this.rbnReportBug.Text = "Report &bug";
            this.rbnReportBug.UseVisualStyleBackColor = true;
            this.rbnReportBug.CheckedChanged += new System.EventHandler(this.ContactAdminOptions_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Subject : ";
            // 
            // txtSubject
            // 
            this.txtSubject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubject.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubject.Location = new System.Drawing.Point(89, 117);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.ReadOnly = true;
            this.txtSubject.Size = new System.Drawing.Size(372, 20);
            this.txtSubject.TabIndex = 4;
            this.txtSubject.Text = "Account Plus : Bug Report";
            // 
            // txtaMessage
            // 
            this.txtaMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtaMessage.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtaMessage.Location = new System.Drawing.Point(21, 162);
            this.txtaMessage.Multiline = true;
            this.txtaMessage.Name = "txtaMessage";
            this.txtaMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtaMessage.Size = new System.Drawing.Size(437, 203);
            this.txtaMessage.TabIndex = 5;
            this.txtaMessage.TextChanged += new System.EventHandler(this.txtaMessage_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "Message : ";
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSend.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(337, 371);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(126, 23);
            this.btnSend.TabIndex = 6;
            this.btnSend.Text = "&Send using Outlook";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(21, 371);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(51, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClear
            // 
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(78, 371);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(47, 23);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "C&lear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // linkRedirectURL
            // 
            this.linkRedirectURL.AutoSize = true;
            this.linkRedirectURL.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkRedirectURL.Location = new System.Drawing.Point(11, 62);
            this.linkRedirectURL.Name = "linkRedirectURL";
            this.linkRedirectURL.Size = new System.Drawing.Size(179, 14);
            this.linkRedirectURL.TabIndex = 7;
            this.linkRedirectURL.TabStop = true;
            this.linkRedirectURL.Text = "Click here to go to support site";
            this.linkRedirectURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRedirectURL_LinkClicked);
            // 
            // ContactAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(475, 406);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtaMessage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ContactAdmin";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ContactAdmin";
            this.Load += new System.EventHandler(this.ContactAdmin_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.TextBox txtaMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbnSuggestion;
        private System.Windows.Forms.RadioButton rbnQuery;
        private System.Windows.Forms.RadioButton rbnFeatureRequest;
        private System.Windows.Forms.RadioButton rbnReportBug;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.LinkLabel linkRedirectURL;
    }
}