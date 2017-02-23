using System.Drawing;
using System.Windows.Forms;

namespace Sensatus.FiberTracker.UI
{
    public class FormBase : Form
    {
        /// <summary>
        /// Sets the color of the bg.
        /// </summary>
        /// <param name="form">The form.</param>
        protected void SetBGColor(Form form)
        {
            form.BackColor = BGColor;
        }

        protected Color BGColor => Color.FromArgb(122, 150, 223);
    }
}