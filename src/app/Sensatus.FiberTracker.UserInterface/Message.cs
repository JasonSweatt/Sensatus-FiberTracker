using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Sensatus.FiberTracker.UI
{
    public partial class Message : UserControl
    {
        public Message()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            lblMessage.Text = string.Empty;
        }

        public string MessageText
        {
            get { return lblMessage.Text; }
            set { SetMessage(value); }
        }

        public void SetMessage(string message, Color foreColor)
        {
            lblMessage.ForeColor = foreColor;
            lblMessage.Text = message;            
        }


        public void SetMessage(string message)
        {
            SetMessage(message, Color.Purple);
        }


    }
}
