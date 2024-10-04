using CluelessControl.Cheques;
using CluelessControl.Constants;
using CluelessControl.EnvelopeColorStates;
using CluelessControl.Envelopes;
using CluelessControl.Questions;

namespace CluelessControl
{
    public partial class HostScreenForm : Form
    {
        private readonly Dictionary<int, Label> _answerLabels;

        public HostScreenForm()
        {
            InitializeComponent();

            _answerLabels = new Dictionary<int, Label>()
            {
                { 1, AnswerALabel },
                { 2, AnswerBLabel },
                { 3, AnswerCLabel },
                { 4, AnswerDLabel },
            };

            TryToLoadBackgroundImage();
        }

        private void HostScreenForm_Load(object sender, EventArgs e)
        {
            ClearQuestionLabels();
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

        private void AddEvents()
        {
            var gameState = GameState.Instance;

            gameState.EventClearEverything += GameState_EventClearEverything;
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

        #region Events

        private void GameState_EventClearEverything(object? sender, EventArgs e)
        {
            ClearQuestionLabels();
            ClearAnswerLockIn();
            RedrawEnvelopes();
        }

        private void GameState_EventClearQuestion(object? sender, EventArgs e)
        {
            ClearQuestionLabels();
            RedrawEnvelopes();
        }

        private void GameState_EventShowQuestion(object? sender, EventArgs e)
        {
            ShowQuestion();
        }

        private void GameState_EventShowAnswers(object? sender, EventArgs e)
        {
            ShowPossibleAnswers();
        }

        private void GameState_EventAnswerSelected(object? sender, EventArgs e)
        {
            LockInAnswer();
        }

        private void GameState_EventCorrectAnswerShown(object? sender, EventArgs e)
        {
            ShowCorrectAnswer();
        }

        private void GameState_EventRefreshEnvelopes(object? sender, EventArgs e)
        {
            RedrawEnvelopes();
        }

        private void GameState_EventStartTrading(object? sender, EventArgs e)
        {
            PrepareTrading();
        }

        private void GameState_EventRefreshOffer(object? sender, EventArgs e)
        {
            RefreshOffer();
        }

        private void GameState_EventGameOver(object? sender, EventArgs e)
        {
            GameOver();
        }

        #endregion

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
                    MessageBox.Show(GameConstants.CLOSE_ON_DIRECTOR_FORM_MESSAGE, GameConstants.PROGRAM_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                    break;
            }
        }
        #endregion

        #region Refreshing methods

        public void ClearAnswerAndExplanation()
        {
            CorrectAnswerLabel.Text = string.Empty;
            ExplanationLabel.Text = string.Empty;
        }

        public void ShowAnswerAndExplanation(Question question)
        {
            if (question is null)
                return;

            CorrectAnswerLabel.Text = Utils.AnswerToLetter(question.CorrectAnswerNumber);
            ExplanationLabel.Text = question.Comment;
        }

        public void ClearAnswerLockIn()
        {
            AnswerALabel.BackColor = Color.Black;
            AnswerALabel.ForeColor = Color.White;

            AnswerBLabel.BackColor = Color.Black;
            AnswerBLabel.ForeColor = Color.White;

            AnswerCLabel.BackColor = Color.Black;
            AnswerCLabel.ForeColor = Color.White;

            AnswerDLabel.BackColor = Color.Black;
            AnswerDLabel.ForeColor = Color.White;
        }

        public void ClearQuestionLabels()
        {
            ClearAnswerLockIn();

            QuestionLabel.Text = string.Empty;

            AnswerALabel.Text = string.Empty;
            AnswerBLabel.Text = string.Empty;
            AnswerCLabel.Text = string.Empty;
            AnswerDLabel.Text = string.Empty;

            CorrectAnswerLabel.Text = string.Empty;
            ExplanationLabel.Text = string.Empty;
        }

        public void ShowQuestion()
        {
            var currentQuestion = GameState.Instance.GetCurrentQuestion();

            QuestionLabel.Text = currentQuestion.Text;
        }

        public void ShowPossibleAnswers()
        {
            var currentQuestion = GameState.Instance.GetCurrentQuestion();

            AnswerALabel.Text = $"A: {currentQuestion.Answer1}";
            AnswerBLabel.Text = $"B: {currentQuestion.Answer2}";
            AnswerCLabel.Text = $"C: {currentQuestion.Answer3}";
            AnswerDLabel.Text = $"D: {currentQuestion.Answer4}";
        }

        public void LockInAnswer()
        {
            var gameStateInstance = GameState.Instance;
            var currentQuestion = gameStateInstance.GetCurrentQuestion();
            int? answer = gameStateInstance.ContestantAnswer;

            if (answer is null)
            {
                ClearAnswerLockIn();
                ClearAnswerAndExplanation();
                
                return;
            }

            ShowAnswerAndExplanation(currentQuestion);

            int answerValue = answer.Value;
            _answerLabels[answerValue].BackColor = DrawingConstants.LOCK_IN_ANS_COLOR;
            _answerLabels[answerValue].ForeColor = DrawingConstants.LOCK_IN_ANS_FONT_COLOR;
        }

        public void ShowCorrectAnswer()
        {
            var currentQuestion = GameState.Instance.GetCurrentQuestion();
            int correctAnswer = currentQuestion.CorrectAnswerNumber;

            _answerLabels[correctAnswer].BackColor = DrawingConstants.CORRECT_ANS_COLOR;
            _answerLabels[correctAnswer].ForeColor = DrawingConstants.CORRECT_ANS_FONT_COLOR;
        }

        public void RedrawEnvelopes()
        {
            Refresh();
        }

        public void PrepareTrading()
        {
            ClearQuestionLabels();

            ContestantTextLabel.Visible = true;
            HostTextLabel.Visible = true;
            CashTextLabel.Visible = true;
            CashLabel.Visible = true;
            OfferTextLabel.Visible = true;
            OfferLabel.Visible = true;
        }

        public void RefreshOffer()
        {
            var gameStateInstance = GameState.Instance;
            CashLabel.Text = Utils.AmountToString(gameStateInstance.ContestantCash);
            OfferLabel.Text = Utils.AmountToString(gameStateInstance.CashOffer);

            RedrawEnvelopes();
        }

        public void GameOver()
        {
            var gameStateInstance = GameState.Instance;

            ClearQuestionLabels();

            QuestionLabel.Text = string.Format("{0}{1}{2}", GameConstants.PRIZE_STRING, Environment.NewLine, gameStateInstance.FinalPrize);
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
                pictureBox.BackColor = Color.Transparent;
                return;
            }

            EnvelopeColorCollection colorCollection = envelope.GetColorsForScreen();

            Rectangle clientRectangle = pictureBox.ClientRectangle;
            Size size = clientRectangle.Size;

            Point leftPoint = clientRectangle.Location;
            Point centerPoint = new(leftPoint.X + size.Width / 2, leftPoint.Y + size.Height / 2);
            Point rightPoint = new(leftPoint.X + size.Width, leftPoint.Y);

            using (Brush brush = new SolidBrush(colorCollection.BackgroundColor))
            {
                e.Graphics.FillRectangle(brush, clientRectangle);
            }
            
            using (Pen linePen = new Pen(colorCollection.LineColor))
            {
                e.Graphics.DrawLine(Pens.Black, leftPoint, centerPoint);
                e.Graphics.DrawLine(Pens.Black, centerPoint, rightPoint);
            }

            string envelopeNumber = string.Format("{0,2}", envelope.EnvelopeNumber);
            Size envelopeNumberSize = TextRenderer.MeasureText(e.Graphics, envelopeNumber, DrawingConstants.ENVELOPE_DRAWING_FONT);

            using (Brush backgroundBrush = new SolidBrush(colorCollection.BackgroundColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, leftPoint.X, leftPoint.Y, envelopeNumberSize.Width, envelopeNumberSize.Height);
            }

            TextRenderer.DrawText(e.Graphics, envelopeNumber, DrawingConstants.ENVELOPE_DRAWING_FONT, leftPoint, colorCollection.NumberFontColor);

            BaseCheque cheque = envelope.Cheque;
            string chequeString = cheque.ToValueString();
            Size bottomHalfSize = new(width: size.Width, height: size.Height / 2);

            using (Font maxFont = FontHelper.GetMaxFont(chequeString, e.Graphics, DrawingConstants.ENVELOPE_DRAWING_FONT, bottomHalfSize, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS))
            {
                Size chequeValueSize = TextRenderer.MeasureText(chequeString, maxFont);

                Rectangle bottomRight = new(
                    x: leftPoint.X + size.Width - chequeValueSize.Width,
                    y: leftPoint.Y + size.Height - chequeValueSize.Height,
                    width: chequeValueSize.Width,
                    height: chequeValueSize.Height);

                TextRenderer.DrawText(e.Graphics, chequeString, maxFont, bottomRight, colorCollection.ChequeFontColor, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS);
            }
        }

        #endregion
    }
}
