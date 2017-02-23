using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sensatus.FiberTracker.UI
{
    public partial class Calendar : FormBase
    {
        public Calendar()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void Calendar_Load(object sender, EventArgs e)
        {
            SetBGColor(this);
        }
    }
}