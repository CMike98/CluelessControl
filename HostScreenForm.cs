using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CluelessControl
{
    public partial class HostScreenForm : Form
    {
        public HostScreenForm()
        {
            InitializeComponent();
        }

        private void HostScreenForm_Load(object sender, EventArgs e)
        {

        }

        #region Form Closing
        private void HostScreenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.WindowsShutDown:
                case CloseReason.TaskManagerClosing:
                case CloseReason.ApplicationExitCall:
                    break;
                default:
                    MessageBox.Show("Zamknij reżyserkę, by zamknąć program.", Constants.PROGRAM_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                    break;
            }
        }
        #endregion
    }
}
