using CluelessControl.Cheques;
using CluelessControl.Constants;
using CluelessControl.EnvelopeColorStates;
using CluelessControl.Envelopes;
using System.Text;

namespace CluelessControl
{
    public partial class TVScreenForm : Form
    {
        private PictureBox[] _envelopeSelectionPictureBoxes = new PictureBox[GameConstants.MAX_ENVELOPES_COUNT];

        private PictureBox _questionBarPictureBox = new PictureBox();
        private PictureBox[] _envelopesSelected = new PictureBox[GameConstants.MAX_ENVELOPE_POSSIBLE_COUNT];

        public TVScreenForm()
        {
            InitializeComponent();
        }

        private void TVScreenForm_Load(object sender, EventArgs e)
        {
            TryToLoadBackgroundImage();
            PreparePictureBoxes();
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
                // If there's no background image, just ignore it.
            }
        }

        private void PrepareEnvelopePictureBoxes()
        {
            for (int i = 0; i < GameConstants.MAX_ENVELOPES_COUNT; ++i)
            {
                int rowNumber = i / DrawingConstants.ENVELOPES_IN_ONE_ROW;
                int columnNumber = i % DrawingConstants.ENVELOPES_IN_ONE_ROW;

                Point calculatedLocation = new Point(
                    x: DrawingConstants.ENVELOPE_SELECT_FIRST_LOCATION.X + DrawingConstants.ENVELOPE_SIZE_WITH_PADDING.Width * columnNumber,
                    y: DrawingConstants.ENVELOPE_SELECT_FIRST_LOCATION.Y + DrawingConstants.ENVELOPE_SIZE_WITH_PADDING.Height * rowNumber);

                var newPictureBox = new PictureBox()
                {
                    Name = string.Format("StartEnvelope{0}PictureBox", i),
                    Visible = false,
                    Tag = i + 1,
                    Size = DrawingConstants.ENVELOPE_SIZE,
                    Location = calculatedLocation
                };
                newPictureBox.Paint += AllEnvelopePicture_Paint;

                _envelopeSelectionPictureBoxes[i] = newPictureBox;

                Controls.Add(newPictureBox);
            }
        }

        private void PrepareQuestionStatisticsPictureBox()
        {
            _questionBarPictureBox = new PictureBox()
            {
                Name = "QuestionAndStatisticsPictureBox",
                Visible = false,
                Size = DrawingConstants.QUESTION_BAR_SIZE_PADDING,
                Location = DrawingConstants.QUESTION_BAR_LOCATION
            };
            _questionBarPictureBox.Paint += QuestionAndStatistics_Paint;
            Controls.Add(_questionBarPictureBox);
        }

        private void PreparePictureBoxes()
        {
            PrepareEnvelopePictureBoxes();
            PrepareQuestionStatisticsPictureBox();
        }

        #region Events
        private void AddEvents()
        {
            var gameState = GameState.Instance;

            gameState.EventShowEnvelopesStart += GameState_EventShowEnvelopesStart;
            gameState.EventHideEnvelopesStart += GameState_EventHideEnvelopesStart;
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

        private void GameState_EventShowEnvelopesStart(object? sender, EventArgs e)
        {
            SetVisibleEnvelopeSelectionPictureBoxes(visible: true);
        }

        private void GameState_EventHideEnvelopesStart(object? sender, EventArgs e)
        {
            SetVisibleEnvelopeSelectionPictureBoxes(visible: false);
        }

        private void GameState_EventClearQuestion(object? sender, EventArgs e)
        {
            SetVisibleQuestionBar(visible: false);
        }

        private void GameState_EventShowQuestion(object? sender, EventArgs e)
        {
            SetVisibleQuestionBar(visible: true);
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
            RedrawEnvelopes();
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

        #region Methods

        private void SetVisibleEnvelopeSelectionPictureBoxes(bool visible)
        {
            foreach (var pictureBox in _envelopeSelectionPictureBoxes)
            {
                pictureBox.Visible = visible;
            }
        }

        private void SetVisibleQuestionBar(bool visible)
        {
            _questionBarPictureBox.Visible = visible;
        }

        private void RedrawEnvelopes()
        {
            Refresh();
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
                    MessageBox.Show(GameConstants.CLOSE_ON_DIRECTOR_FORM_MESSAGE, GameConstants.PROGRAM_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                    break;
            }
        }
        #endregion

        #region Envelope Drawing

        private void DrawEnvelopeInPictureBox(Envelope envelope, PictureBox pictureBox, Graphics graphics)
        {
            if (envelope == null)
                throw new ArgumentNullException(nameof(envelope));
            if (pictureBox == null)
                throw new ArgumentNullException(nameof(pictureBox));
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics));

            EnvelopeColorCollection colorPairing = envelope.GetColorsForTv();
            pictureBox.BackColor = colorPairing.BackgroundColor;

            Rectangle clientRectangle = pictureBox.ClientRectangle;
            Size size = clientRectangle.Size;

            Point leftPoint = clientRectangle.Location;
            Point centerPoint = new(leftPoint.X + size.Width / 2, leftPoint.Y + size.Height / 2);
            Point rightPoint = new(leftPoint.X + size.Width, leftPoint.Y);

            using (Pen linePen = new(colorPairing.LineColor))
            {
                graphics.DrawLine(linePen, leftPoint, centerPoint);
                graphics.DrawLine(linePen, centerPoint, rightPoint);
            }
            
            string envelopeNumberString = string.Format("{0,2}", envelope.EnvelopeNumber);
            SizeF envelopeNumberSize = graphics.MeasureString(envelopeNumberString, DrawingConstants.ENVELOPE_DRAWING_FONT);

            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.GammaCorrected;
            graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

            using (Brush backgroundBrush = new SolidBrush(pictureBox.BackColor))
            {
                graphics.FillRectangle(backgroundBrush, leftPoint.X, leftPoint.Y, envelopeNumberSize.Width, envelopeNumberSize.Height);
            }

            using (Brush numberBrush = new SolidBrush(colorPairing.NumberFontColor))
            {
                graphics.DrawString(envelopeNumberString, DrawingConstants.ENVELOPE_DRAWING_FONT, numberBrush, leftPoint.X, leftPoint.Y);
            }

            if (GameState.Instance.GameSettings.ShowAmountsOnTv || envelope.IsOpen)
            {
                BaseCheque cheque = envelope.Cheque;
                string chequeString = cheque.ToValueString();
                SizeF chequeValueSize = graphics.MeasureString(chequeString, DrawingConstants.ENVELOPE_DRAWING_FONT);

                using Brush chequeBrush = new SolidBrush(colorPairing.ChequeFontColor);
                graphics.DrawString(chequeString, DrawingConstants.ENVELOPE_DRAWING_FONT, chequeBrush, leftPoint.X + size.Width - chequeValueSize.Width, leftPoint.Y + size.Height - chequeValueSize.Height);
            }
        }

        private void AllEnvelopePicture_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not PictureBox pictureBox)
                return;

            int tag = (int)(pictureBox.Tag ?? throw new InvalidOperationException($"Tag should be an envelope number!"));

            Envelope envelope = GameState.Instance.EnvelopeTable.GetEnvelope(tag);

            DrawEnvelopeInPictureBox(envelope, pictureBox, e.Graphics);
        }

        private void EnvelopePicture_Paint(object? sender, PaintEventArgs e)
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

            DrawEnvelopeInPictureBox(envelope, pictureBox, e.Graphics);
        }

        #endregion

        #region Question Drawing

        private void QuestionAndStatistics_Paint(object? sender, PaintEventArgs e)
        {
            Rectangle clientRectangle = _questionBarPictureBox.ClientRectangle;
            Size size = clientRectangle.Size;
            Point location = clientRectangle.Location;

            using (Brush brush = new SolidBrush(DrawingConstants.QUESTION_BAR_BACKGROUND_OUT))
            {
                e.Graphics.FillRectangle(brush, location.X, location.Y, size.Width, size.Height);
            }

            RectangleF questionRectangle = new(
                x: location.X + DrawingConstants.QUESTION_BAR_PADDING.Width,
                y: location.Y + DrawingConstants.QUESTION_BAR_PADDING.Height,
                width: size.Width - DrawingConstants.QUESTION_BAR_PADDING.Width * 2,
                height: size.Height - DrawingConstants.QUESTION_BAR_PADDING.Height * 2);

            using (Brush brush = new SolidBrush(DrawingConstants.QUESTION_BAR_BACKGROUND_OUT))
            {
                e.Graphics.FillRectangle(brush, questionRectangle);
            }

            StringFormat questionDrawingFormat = new()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center,
            };

            Questions.Question currentQuestion = GameState.Instance.GetCurrentQuestion();

            e.Graphics.DrawString(currentQuestion.Text, DrawingConstants.QUESTION_DRAWING_FONT, Brushes.White, questionRectangle, questionDrawingFormat);
        }

        #endregion
    }
}
