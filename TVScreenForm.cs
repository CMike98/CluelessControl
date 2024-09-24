﻿using CluelessControl.Cheques;
using CluelessControl.Constants;
using CluelessControl.Envelopes;
using CluelessControl.Questions;

namespace CluelessControl
{
    public partial class TVScreenForm : Form
    {
        private enum QuestionBarState
        {
            CLEAR,
            QUESTION_ONLY,
            QUESTION_AND_ANSWERS,
            QUESTION_AND_ANSWERS_LOCK,
            QUESTION_AND_ANSWERS_CORRECT,
            SHOW_ENVELOPES_AND_QUESTION,
            SHOW_ENVELOPES_ONLY
        }

        private static readonly StringFormat _textCenterDrawingFormat = new()
        {
            LineAlignment = StringAlignment.Center,
            Alignment = StringAlignment.Center,
        };

        private PictureBox[] _envelopeSelectionPictureBoxes = new PictureBox[GameConstants.MAX_ENVELOPES_COUNT];

        private QuestionBarState _questionBarState = QuestionBarState.CLEAR;
        private PictureBox _questionBarPictureBox = new PictureBox();
        private PictureBox _questionCountPictureBox = new PictureBox();
        private PictureBox _questionPlayingForPictureBox = new PictureBox();

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

        private void PrepareEnvelopeSelectionPictureBoxes()
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

                Controls.Add(_envelopeSelectionPictureBoxes[i]);
            }
        }

        private void PrepareQuestionStatisticsPictureBox()
        {
            _questionBarPictureBox = new PictureBox()
            {
                Name = "QuestionAndStatisticsPictureBox",
                Visible = false,
                Size = DrawingConstants.QUESTION_BAR_SIZE_FULL,
                Location = DrawingConstants.QUESTION_BAR_LOCATION
            };
            _questionBarPictureBox.Paint += QuestionAndStatistics_Paint;
            Controls.Add(_questionBarPictureBox);
        }

        private void PrepareQuestionCountPictureBox()
        {
            _questionCountPictureBox = new PictureBox()
            {
                Name = "QuestionCountPictureBox",
                Visible = false,
                Size = DrawingConstants.QUESTION_COUNTER_SIZE_FULL,
                Location = DrawingConstants.QUESTION_COUNTER_LOCATION
            };
            _questionCountPictureBox.Paint += QuestionCounter_Paint;
            Controls.Add(_questionCountPictureBox);
        }

        private void PrepareQuestionPlayingForPictureBox()
        {
            Point location = new Point(
                x: DrawingConstants.QUESTION_BAR_LOCATION.X + (DrawingConstants.QUESTION_BAR_SIZE_FULL.Width - DrawingConstants.ENVELOPE_SIZE.Width) / 2,
                y: DrawingConstants.QUESTION_COUNTER_LOCATION.Y + DrawingConstants.QUESTION_COUNTER_SIZE_FULL.Height - DrawingConstants.ENVELOPE_SIZE.Height);

            _questionPlayingForPictureBox = new PictureBox()
            {
                Name = "QuestionPlayingForPictureBox",
                Visible = false,
                Size = DrawingConstants.ENVELOPE_SIZE,
                Location = location
            };
            _questionPlayingForPictureBox.Paint += QuestionPlayingForPictureBox_Paint;
            Controls.Add(_questionPlayingForPictureBox);
        }

        private void PreparePictureBoxes()
        {
            PrepareEnvelopeSelectionPictureBoxes();
            PrepareQuestionStatisticsPictureBox();
            PrepareQuestionCountPictureBox();
            PrepareQuestionPlayingForPictureBox();
        }

        #region Events
        private void AddEvents()
        {
            var gameState = GameState.Instance;

            gameState.EventShowEnvelopesStart += GameState_EventShowEnvelopesStart;
            gameState.EventHideEnvelopesStart += GameState_EventHideEnvelopesStart;
            gameState.EventClearQuestion += GameState_EventClearQuestion;
            gameState.EventShowQuestion += GameState_EventShowQuestion;
            gameState.EventShowEnvelopesBeforeQuestion += GameState_EventShowEnvelopesBeforeQuestion;
            gameState.EventShowAnswers += GameState_EventShowAnswers;
            gameState.EventAnswerSelected += GameState_EventAnswerSelected;
            gameState.EventCorrectAnswerShown += GameState_EventCorrectAnswerShown;
            gameState.EventShowEnvelopesAfterQuestion += GameState_EventShowEnvelopesAfterQuestion;
            gameState.EventRefreshEnvelopes += GameState_EventRefreshEnvelopes;
            gameState.EventStartTrading += GameState_EventStartTrading;
            gameState.EventRefreshOffer += GameState_EventRefreshOffer;
            gameState.EventGameOver += GameState_EventGameOver;
        }

        private void GameState_EventShowEnvelopesStart(object? sender, EventArgs e)
        {
            _questionBarState = QuestionBarState.CLEAR;

            SetVisibleEnvelopeSelectionPictureBoxes(visible: true);
        }

        private void GameState_EventHideEnvelopesStart(object? sender, EventArgs e)
        {
            SetVisibleEnvelopeSelectionPictureBoxes(visible: false);
        }

        private void GameState_EventClearQuestion(object? sender, EventArgs e)
        {
            _questionBarState = QuestionBarState.CLEAR;

            SetVisibleEnvelopePlayingFor(visible: false);
            SetVisibleQuestionBar(visible: false);
            SetVisibleCounter(visible: false);
        }

        private void GameState_EventShowQuestion(object? sender, EventArgs e)
        {
            _questionBarState = QuestionBarState.QUESTION_ONLY;

            SetVisibleQuestionBar(visible: true);
            SetVisibleCounter(visible: true);
        }

        private void GameState_EventShowEnvelopesBeforeQuestion(object? sender, EventArgs e)
        {
            _questionBarState = QuestionBarState.SHOW_ENVELOPES_AND_QUESTION;

            _questionBarPictureBox.Refresh();
        }

        private void GameState_EventShowAnswers(object? sender, EventArgs e)
        {
            _questionBarState = QuestionBarState.QUESTION_AND_ANSWERS;

            SetVisibleEnvelopePlayingFor(visible: true);
            SetVisibleQuestionBar(visible: true);
        }

        private void GameState_EventAnswerSelected(object? sender, EventArgs e)
        {
            _questionBarState = QuestionBarState.QUESTION_AND_ANSWERS_LOCK;

            _questionBarPictureBox.Refresh();
        }

        private void GameState_EventCorrectAnswerShown(object? sender, EventArgs e)
        {
            _questionBarState = QuestionBarState.QUESTION_AND_ANSWERS_CORRECT;

            _questionBarPictureBox.Refresh();
        }

        private void GameState_EventShowEnvelopesAfterQuestion(object? sender, EventArgs e)
        {
            _questionBarState = QuestionBarState.SHOW_ENVELOPES_ONLY;

            SetVisibleEnvelopePlayingFor(visible: false);
            _questionBarPictureBox.Refresh();
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
            _questionBarPictureBox.Refresh();
        }

        private void SetVisibleCounter(bool visible)
        {
            _questionCountPictureBox.Visible = visible;
            _questionCountPictureBox.Refresh();
        }

        private void SetVisibleEnvelopePlayingFor(bool visible)
        {
            _questionPlayingForPictureBox.Visible = visible;
            _questionPlayingForPictureBox.Refresh();
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

        private void PaintEnvelopeInArea(Envelope envelope, Graphics graphics, RectangleF areaRectangle, Color? requestedBackgroundColor = null)
        {
            if (envelope is null)
                throw new ArgumentNullException(nameof(envelope));
            if (graphics is null)
                throw new ArgumentNullException(nameof(graphics));
            if (areaRectangle.IsEmpty)
                throw new ArgumentException($"Area rectangle is empty!", nameof(areaRectangle));

            SizeF size = areaRectangle.Size;

            PointF leftPoint = areaRectangle.Location;
            PointF centerPoint = new(leftPoint.X + size.Width / 2, leftPoint.Y + size.Height / 2);
            PointF rightPoint = new(leftPoint.X + size.Width, leftPoint.Y);

            var colorCollection = envelope.GetColorsForTv();

            Color backgroundColor = requestedBackgroundColor ?? colorCollection.BackgroundColor;

            using (Brush backgroundBrush = new SolidBrush(backgroundColor))
            {
                graphics.FillRectangle(backgroundBrush, areaRectangle);
            }

            using (Pen linePen = new Pen(colorCollection.LineColor))
            {
                graphics.DrawLine(linePen, leftPoint, centerPoint);
                graphics.DrawLine(linePen, centerPoint, rightPoint);
            }

            string envelopeNumberString = string.Format("{0,2}", envelope.EnvelopeNumber);
            SizeF envelopeNumberSize = graphics.MeasureString(envelopeNumberString, DrawingConstants.ENVELOPE_DRAWING_FONT);

            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.GammaCorrected;
            graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

            using (Brush backgroundBrush = new SolidBrush(backgroundColor))
            {
                graphics.FillRectangle(backgroundBrush, leftPoint.X, leftPoint.Y, envelopeNumberSize.Width, envelopeNumberSize.Height);
            }

            using (Brush numberBrush = new SolidBrush(colorCollection.NumberFontColor))
            {
                graphics.DrawString(envelopeNumberString, DrawingConstants.ENVELOPE_DRAWING_FONT, numberBrush, leftPoint.X, leftPoint.Y);
            }

            if (GameState.Instance.GameSettings.ShowAmountsOnTv || envelope.IsOpen)
            {
                BaseCheque cheque = envelope.Cheque;
                string chequeString = cheque.ToValueString();
                SizeF chequeValueSize = graphics.MeasureString(chequeString, DrawingConstants.ENVELOPE_DRAWING_FONT);

                using Brush chequeBrush = new SolidBrush(colorCollection.ChequeFontColor);
                graphics.DrawString(chequeString, DrawingConstants.ENVELOPE_DRAWING_FONT, chequeBrush, leftPoint.X + size.Width - chequeValueSize.Width, leftPoint.Y + size.Height - chequeValueSize.Height);
            }
        }

        private void DrawEnvelopeInPictureBox(Envelope envelope, PictureBox pictureBox, Graphics graphics)
        {
            if (envelope == null)
                throw new ArgumentNullException(nameof(envelope));
            if (pictureBox == null)
                throw new ArgumentNullException(nameof(pictureBox));
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics));

            PaintEnvelopeInArea(envelope, graphics, pictureBox.ClientRectangle);
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
                pictureBox.BackColor = Color.Transparent;
                pictureBox.BringToFront();
                return;
            }

            DrawEnvelopeInPictureBox(envelope, pictureBox, e.Graphics);
            pictureBox.BringToFront();
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

            RectangleF questionBarRectangle = new(
                x: location.X + DrawingConstants.QUESTION_BAR_BORDER.Width,
                y: location.Y + DrawingConstants.QUESTION_BAR_BORDER.Height,
                width: size.Width - DrawingConstants.QUESTION_BAR_BORDER.Width * 2,
                height: size.Height - DrawingConstants.QUESTION_BAR_BORDER.Height * 2);

            using (Brush brush = new SolidBrush(DrawingConstants.QUESTION_BAR_BACKGROUND_IN))
            {
                e.Graphics.FillRectangle(brush, questionBarRectangle);
            }

            var gameStateInstance = GameState.Instance;
            var currentQuestion = gameStateInstance.GetCurrentQuestion();

            switch (_questionBarState)
            {
                case QuestionBarState.QUESTION_ONLY:
                    PaintQuestion(currentQuestion, e.Graphics, questionBarRectangle);
                    break;
                case QuestionBarState.QUESTION_AND_ANSWERS:
                    PaintQuestionAndAnswers(currentQuestion, e.Graphics, questionBarRectangle, lockedInAnswer: null, correctAnswer: null);
                    break;
                case QuestionBarState.QUESTION_AND_ANSWERS_LOCK:
                    PaintQuestionAndAnswers(currentQuestion, e.Graphics, questionBarRectangle, lockedInAnswer: gameStateInstance.ContestantAnswer, correctAnswer: null);
                    break;
                case QuestionBarState.QUESTION_AND_ANSWERS_CORRECT:
                    PaintQuestionAndAnswers(currentQuestion, e.Graphics, questionBarRectangle, lockedInAnswer: gameStateInstance.ContestantAnswer, correctAnswer: currentQuestion.CorrectAnswerNumber);
                    break;
                case QuestionBarState.SHOW_ENVELOPES_AND_QUESTION:
                    PaintQuestionBarEnvelopesAndQuestion(gameStateInstance.ContestantEnvelopeSet.Envelopes, currentQuestion, e.Graphics, questionBarRectangle);
                    break;
                case QuestionBarState.SHOW_ENVELOPES_ONLY:
                    PaintQuestionBarEnvelopesAndQuestion(gameStateInstance.ContestantEnvelopeSet.Envelopes, question: null, e.Graphics, questionBarRectangle);
                    break;
                case QuestionBarState.CLEAR:
                    break;
                default:
                    throw new NotImplementedException($"Not recognized question bar state: {_questionBarState}!");
            }
        }

        private void PaintQuestion(Question question, Graphics graphics, RectangleF areaRectangle)
        {
            if (question is null)
                throw new ArgumentNullException(nameof(question));
            if (graphics is null)
                throw new ArgumentNullException(nameof(graphics));
            if (areaRectangle.IsEmpty)
                throw new ArgumentException($"The question rectangle is empty.", nameof(areaRectangle));

            string questionToDraw = question.Text;

            graphics.DrawString(questionToDraw, DrawingConstants.QUESTION_DRAWING_FONT, Brushes.White, areaRectangle, _textCenterDrawingFormat);
        }

        private void PaintQuestionAndAnswers(Question question, Graphics graphics, RectangleF areaRectangle, int? lockedInAnswer = null, int? correctAnswer = null)
        {
            if (question is null)
                throw new ArgumentNullException(nameof(question));
            if (graphics is null)
                throw new ArgumentNullException(nameof(graphics));
            if (areaRectangle.IsEmpty)
                throw new ArgumentException($"The question rectangle is empty.", nameof(areaRectangle));

            // Draw question

            RectangleF questionRectangle = new RectangleF(areaRectangle.X, areaRectangle.Y, areaRectangle.Width, areaRectangle.Height / 2);
            using (Font font = FontHelper.GetMaxFont(question.Text, graphics, DrawingConstants.QUESTION_ANSWER_DRAWING_FONT, questionRectangle.Size))
            {
                graphics.DrawString(question.Text, font, Brushes.White, questionRectangle, _textCenterDrawingFormat);
            }
            
            // Draw answers
            for (int i = GameConstants.MIN_ANSWER_NUMBER; i <= GameConstants.MAX_ANSWER_NUMBER; ++i)
            {
                Color fontColor = Color.White;
                if (i == correctAnswer)
                    fontColor = DrawingConstants.CORRECT_ANS_COLOR;
                else if (i == lockedInAnswer)
                    fontColor = DrawingConstants.LOCK_IN_ANS_COLOR;

                PaintAnswer(question, graphics, areaRectangle, i, fontColor);
            }
        }

        private void PaintAnswer(Question question, Graphics graphics, RectangleF areaRectangle, int answerNumber, Color color)
        {
            if (question is null)
                throw new ArgumentNullException(nameof(question));
            if (graphics is null)
                throw new ArgumentNullException(nameof(graphics));
            if (areaRectangle.IsEmpty)
                throw new ArgumentException("This area rectangle is empty.", nameof(areaRectangle));
            if (answerNumber < GameConstants.MIN_ANSWER_NUMBER || answerNumber > GameConstants.MAX_ANSWER_NUMBER)
                throw new ArgumentOutOfRangeException(nameof(answerNumber), $"The answer index must be between [{GameConstants.MIN_ANSWER_NUMBER}...{GameConstants.MAX_ANSWER_NUMBER}]!");
            if (color.IsEmpty)
                throw new ArgumentException("This color is empty.", nameof(color));

            const int midwayNumber = GameConstants.MIN_ANSWER_NUMBER + (GameConstants.MAX_ANSWER_NUMBER - GameConstants.MIN_ANSWER_NUMBER) / 2;
            bool rightSide = answerNumber % 2 == 0;
            bool downSide = answerNumber > midwayNumber;

            RectangleF answerRectangle = new RectangleF(
                x: areaRectangle.X + (rightSide ? areaRectangle.Width / 2 : 0),
                y: areaRectangle.Y + (areaRectangle.Height * (downSide ? 0.75f : 0.5f)),
                width: areaRectangle.Width / 2,
                height: areaRectangle.Height / 4);

            string answer = answerNumber switch
            {
                1 => question.Answer1,
                2 => question.Answer2,
                3 => question.Answer3,
                4 => question.Answer4,
                _ => string.Empty
            };

            string answerText = string.Format("{0}: {1}", Utils.AnswerToLetter(answerNumber), answer);

            using Font font = FontHelper.GetMaxFont(answerText, graphics, DrawingConstants.QUESTION_ANSWER_DRAWING_FONT, answerRectangle.Size);
            using Brush brush = new SolidBrush(color);
            graphics.DrawString(answerText, font, brush, answerRectangle, _textCenterDrawingFormat);
        }

        private void PaintQuestionBarEnvelopesAndQuestion(IList<Envelope> envelopes, Question? question, Graphics graphics, RectangleF areaRectangle)
        {
            if (envelopes is null)
                throw new ArgumentNullException(nameof(envelopes));
            if (envelopes.Any(envelope => envelope is null))
                throw new ArgumentException($"At least one envelope is null.", nameof(envelopes));
            if (graphics is null)
                throw new ArgumentNullException(nameof(graphics));
            if (areaRectangle.IsEmpty)
                throw new ArgumentException($"Area rectangle is empty!", nameof(areaRectangle));

            RectangleF envelopeRectangle;
            if (question is not null)
            {
                RectangleF upperHalf = new RectangleF(
                    x: areaRectangle.X,
                    y: areaRectangle.Y,
                    width: areaRectangle.Width,
                    height: areaRectangle.Height / 2.0f);

                using (Font font = FontHelper.GetMaxFont(question.Text, graphics, DrawingConstants.QUESTION_ANSWER_DRAWING_FONT, upperHalf.Size))
                {
                    graphics.DrawString(question.Text, font, Brushes.White, upperHalf, _textCenterDrawingFormat);
                }

                envelopeRectangle = new RectangleF(
                    x: areaRectangle.X,
                    y: areaRectangle.Y + areaRectangle.Height / 2.0f,
                    width: areaRectangle.Width,
                    height: areaRectangle.Height / 2.0f);
            }
            else
            {
                envelopeRectangle = new RectangleF(
                    x: areaRectangle.X,
                    y: areaRectangle.Y,
                    width: areaRectangle.Width,
                    height: areaRectangle.Height);
            }

            // Draw envelopes

            int maxCount = envelopes.Count;
            float totalWidth = DrawingConstants.ENVELOPE_SIZE_WITH_PADDING.Width * (maxCount - 1) + DrawingConstants.ENVELOPE_SIZE.Width;
            for (int i = 0; i < maxCount; i++)
            {
                Envelope envelope = envelopes[i];

                RectangleF rectangle = new RectangleF(
                    x: envelopeRectangle.X + ((envelopeRectangle.Width - totalWidth) / 2.0f) + i * DrawingConstants.ENVELOPE_SIZE_WITH_PADDING.Width,
                    y: envelopeRectangle.Y + (envelopeRectangle.Height - DrawingConstants.ENVELOPE_SIZE.Height) / 2.0f,
                    width: DrawingConstants.ENVELOPE_SIZE.Width,
                    height: DrawingConstants.ENVELOPE_SIZE.Height);

                PaintEnvelopeInArea(envelope, graphics, rectangle);
            }
        }

        #endregion

        #region Question Counter

        private void QuestionCounter_Paint(object? sender, PaintEventArgs e)
        {
            Rectangle clientRectangle = _questionCountPictureBox.ClientRectangle;
            Size size = clientRectangle.Size;
            Point location = clientRectangle.Location;
            var gameStateInstance = GameState.Instance;
            string questionNumber = string.Format("PYTANIE {0}/{1}", gameStateInstance.QuestionNumber, gameStateInstance.MaxQuestionCount);

            RectangleF allRectangle = new RectangleF(
                x: location.X,
                y: location.Y,
                width: size.Width,
                height: size.Height);

            RectangleF questionCounterRectangle = new RectangleF(
                x: location.X + DrawingConstants.QUESTION_COUNTER_BORDER.Width,
                y: location.Y + DrawingConstants.QUESTION_COUNTER_BORDER.Height,
                width: size.Width - DrawingConstants.QUESTION_COUNTER_BORDER.Width * 2,
                height: size.Height - DrawingConstants.QUESTION_COUNTER_BORDER.Height * 2);

            using (Brush outsideBrush = new SolidBrush(DrawingConstants.QUESTION_COUNTER_BACKGROUND_OUT))
            {
                e.Graphics.FillRectangle(outsideBrush, allRectangle);
            }

            using (Brush insideBrush = new SolidBrush(DrawingConstants.QUESTION_COUNTER_BACKGROUND_IN))
            {
                e.Graphics.FillRectangle(insideBrush, questionCounterRectangle);
            }

            e.Graphics.DrawString(questionNumber, DrawingConstants.QUESTION_COUNTER_FONT, Brushes.White, questionCounterRectangle, _textCenterDrawingFormat);
        }

        private void QuestionPlayingForPictureBox_Paint(object? sender, PaintEventArgs e)
        {
            Envelope? currentEnvelope = GameState.Instance.EnvelopePlayedFor;
            if (currentEnvelope is null)
                return;

            // PLAYING_FOR changes color, we just want white
            if (currentEnvelope.State == EnvelopeState.PLAYING_FOR)
                PaintEnvelopeInArea(currentEnvelope, e.Graphics, _questionPlayingForPictureBox.ClientRectangle, Color.White);
            else
                PaintEnvelopeInArea(currentEnvelope, e.Graphics, _questionPlayingForPictureBox.ClientRectangle);
        }

        #endregion
    }
}