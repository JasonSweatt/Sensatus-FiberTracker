namespace Sensatus.FiberTracker.UI
{
    partial class TestComm
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
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.serialComm = new SCommLib.SComm();
            this.textBoxTest = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // serialComm
            // 
            this.serialComm.CommPort = ((short)(1));
            this.serialComm.DTREnable = true;
            this.serialComm.EOFChar = ((byte)(26));
            this.serialComm.EOFEnable = false;
            this.serialComm.Handshaking = SCommLib.HandshakeConstants.comNone;
            this.serialComm.InBufferSize = ((short)(4096));
            this.serialComm.InputLen = ((short)(0));
            this.serialComm.InputMode = SCommLib.InputModeConstants.comInputModeText;
            this.serialComm.NullDiscard = false;
            this.serialComm.OutBufferSize = ((short)(4096));
            this.serialComm.ParentForm = this;
            this.serialComm.ParityReplace = "?";
            this.serialComm.RThreshold = ((short)(0));
            this.serialComm.RTSEnable = true;
            this.serialComm.Settings = "9600,n,8,1";
            this.serialComm.SThreshold = ((short)(0));
            this.serialComm.OnComm += new SCommLib.SComm._OnComm(this.serialComm_OnComm);
            // 
            // textBoxTest
            // 
            this.textBoxTest.Location = new System.Drawing.Point(102, 30);
            this.textBoxTest.Multiline = true;
            this.textBoxTest.Name = "textBoxTest";
            this.textBoxTest.Size = new System.Drawing.Size(354, 172);
            this.textBoxTest.TabIndex = 0;
            this.textBoxTest.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // TestComm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 327);
            this.Controls.Add(this.textBoxTest);
            this.Name = "TestComm";
            this.Text = "TestCom";
            this.Load += new System.EventHandler(this.TestCom_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.IO.Ports.SerialPort serialPort;
        private SCommLib.SComm serialComm;
        private System.Windows.Forms.TextBox textBoxTest;
    }
}