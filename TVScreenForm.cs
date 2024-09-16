using CluelessControl.Cheques;
using CluelessControl.Envelopes;

namespace CluelessControl
{
    public partial class TVScreenForm : Form
    {
        public TVScreenForm()
        {
            InitializeComponent();
        }

        private void TVScreenForm_Load(object sender, EventArgs e)
        {
            TryToLoadBackgroundImage();
            AddEvents();
        }

        private void TryToLoadBackgroundImage()
        {
            try
            {
                Image backgroundImage = Image.FromFile("img/background.jpg");
                BackgroundImage = backgroundImage;
            }
            catch
            {
                // If there's no background image, just exit.
            }
        }

        #region Events
        private void AddEvents()
        {
            var gameState = GameState.Instance;

            gameState.EventClearQuestion += GameState_EventClearQuestion;
            gameState.EventShowQuestion += GameState_EventShowQuestion;
            gameState.EventShowAnswers += GameState_EventShowAnswers;
            gameState.EventAnswerSelected += GameState_EventAnswerSelected;
            gameState.EventCorrectAnswerShown += GameState_EventCorrectAnswerShown;
            gameState.EventRefreshEnvelopes += GameState_EventRefreshEnvelopes;
            gameState.EventStartTrading += GameState_EventStartTrading;
            gameState.EventRefreshOffer += GameState_EventRefreshOffer;
            gameState.EventGameOver += GameState_EventGameOver;
        }

        private void GameState_EventClearQuestion(object? sender, EventArgs e)
        {
            // TODO: Empty for now
        }

        private void GameState_EventShowQuestion(object? sender, EventArgs e)
        {
            // TODO: Empty for now
        }

        private void GameState_EventShowAnswers(object? sender, EventArgs e)
        {
            // TODO: Empty for now
        }

        private void GameState_EventAnswerSelected(object? sender, EventArgs e)
        {
            // TODO: Empty for now
        }

        private void GameState_EventCorrectAnswerShown(object? sender, EventArgs e)
        {
            // TODO: Empty for now
        }

        private void GameState_EventRefreshEnvelopes(object? sender, EventArgs e)
        {
            // TODO: Empty for now
        }

        private void GameState_EventStartTrading(object? sender, EventArgs e)
        {
            // TODO: Empty for now
        }

        private void GameState_EventRefreshOffer(object? sender, EventArgs e)
        {
            // TODO: Empty for now
        }

        private void GameState_EventGameOver(object? sender, EventArgs e)
        {
            // TODO: Empty for now
        }
        #endregion


        #region Form Closing
        private void TVScreenForm_FormClosing(object sender, FormClosingEventArgs e)
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

        #region Envelope Drawing
        private void EnvelopePicture_Paint(object sender, PaintEventArgs e)
        {
            if (sender is not PictureBox pictureBox)
                return;

            string tag = (pictureBox.Tag as string) ?? string.Empty;
            Envelope? envelope = GameState.Instance.GetEnvelopeFromTag(tag);
            if (envelope == null)
            {
                pictureBox.BackColor = Color.Black;
                return;
            }

            pictureBox.BackColor = envelope.GetBackgroundColor();

            Rectangle clientRectangle = pictureBox.ClientRectangle;
            Point size = (Point)clientRectangle.Size;

            Point leftPoint = clientRectangle.Location;
            Point centerPoint = new(leftPoint.X + size.X / 2, leftPoint.Y + size.Y / 2);
            Point rightPoint = new(leftPoint.X + size.X, leftPoint.Y);

            e.Graphics.DrawLine(Pens.Black, leftPoint, centerPoint);
            e.Graphics.DrawLine(Pens.Black, centerPoint, rightPoint);

            e.Graphics.DrawString(envelope.EnvelopeNumber.ToString(), Constants.DRAWING_FONT, Brushes.Black, leftPoint.X, leftPoint.Y);

            BaseCheque cheque = envelope.Cheque;
            string chequeString = cheque.ToValueString();
            using Brush brush = new SolidBrush(cheque.GetTextColor());

            SizeF valueSize = e.Graphics.MeasureString(chequeString, Constants.DRAWING_FONT);
            e.Graphics.DrawString(chequeString, Constants.DRAWING_FONT, brush, leftPoint.X + size.X - valueSize.Width, leftPoint.Y + size.Y - valueSize.Height);
        }
        #endregion
    }
}
