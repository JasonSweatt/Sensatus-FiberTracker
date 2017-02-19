using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sensatus.FiberTracker.UI
{
    public partial class Calendar : AccountPlusBase
    {
        public Calendar()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void Calendar_Load(object sender, EventArgs e)
        {
            base.SetBGColor(this);
        }
    }
}