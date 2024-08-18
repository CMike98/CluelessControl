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
    public partial class ContestantScreenForm : Form
    {
        public ContestantScreenForm()
        {
            InitializeComponent();
        }

        private void ContestantScreenForm_Load(object sender, EventArgs e)
        {

        }

        #region Form Closing
        private void ContestantScreenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.WindowsShutDown:
                case CloseReason.TaskManagerClosing:
                case CloseReason.ApplicationExitCall:
                    break;
                default:
                    MessageBox.Show(Constants.CLOSE_ON_DIRECTOR_FORM_MESSAGE, Constants.PROGRAM_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                    break;
            }
        }
        #endregion

        private void EnvelopePicture_Paint(object sender, PaintEventArgs e)
        {
            if (sender is not PictureBox pictureBox)
                return;

            if (!pictureBox.Visible)
                return;

            string tag = (pictureBox.Tag as string) ?? string.Empty;
            Envelope? envelope = GameState.Instance.GetEnvelopeFromTag(tag);
            if (envelope == null)
            {
                pictureBox.Visible = false;
                return;
            }

            pictureBox.Visible = true;

            Point size = (Point)pictureBox.ClientRectangle.Size;

            Point leftPoint = pictureBox.ClientRectangle.Location;
            Point centerPoint = new(leftPoint.X + size.X / 2, leftPoint.Y + size.Y / 2);
            Point rightPoint = new(leftPoint.X + size.X, leftPoint.Y);

            e.Graphics.DrawLine(Pens.Black, leftPoint, centerPoint);
            e.Graphics.DrawLine(Pens.Black, centerPoint, rightPoint);
            
            e.Graphics.DrawString(envelope.EnvelopeNumber.ToString(), Constants.DRAWING_FONT, Brushes.Black, leftPoint.X, leftPoint.Y);

            if (envelope.IsOpen)
            {
                BaseCheque cheque = envelope.Cheque;
                string chequeString = cheque.ToValueString();
                using Brush brush = new SolidBrush(cheque.GetTextColor());

                SizeF valueSize = e.Graphics.MeasureString(chequeString, Constants.DRAWING_FONT);
                e.Graphics.DrawString(chequeString, Constants.DRAWING_FONT, brush, leftPoint.X + size.X - valueSize.Width, leftPoint.Y + size.Y - valueSize.Height);
            }
        }
    }
}
