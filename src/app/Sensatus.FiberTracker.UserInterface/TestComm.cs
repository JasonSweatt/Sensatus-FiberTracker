using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sensatus.FiberTracker.UI
{
    public partial class TestComm : Form
    {
        public TestComm()
        {
            InitializeComponent();
        }

        private void TestCom_Load(object sender, EventArgs e)
        {
            serialComm.CommPort = 2; // Use Any Com Port
            serialComm.Settings = "9600,n,8,1"; // Setup the Com Port
            serialComm.PortOpen = true; // Open the Port
            serialComm.Output = "Hello World";   // Send some data
            while (serialComm.InBufferCount > 0) // Is there any incoming data
            {
                textBoxTest.AppendText(serialComm.Input.ToString()); // Receive Data
            }
            serialComm.PortOpen = false; // Close the port.
        }

        private void serialComm_OnComm(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
