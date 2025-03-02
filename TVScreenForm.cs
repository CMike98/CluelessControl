using CluelessControl.Cheques;
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

        private static readonly QuestionBarState[] StatesWithoutQuestion =
            [
                QuestionBarState.CLEAR,
                QuestionBarState.SHOW_ENVELOPES_ONLY
            ];

        private string _contestantName = string.Empty;
        private PictureBox _introductionNamePictureBox = new PictureBox();

        private PictureBox[] _envelopeSelectionPictureBoxes = new PictureBox[GameConstants.MAX_ENVELOPES_COUNT];

        private QuestionBarState _questionBarState = QuestionBarState.CLEAR;
        private PictureBox _questionBarPictureBox = new PictureBox();
        private PictureBox _questionCountPictureBox = new PictureBox();
        private PictureBox _questionPlayingForPictureBox = new PictureBox();

        private PictureBox _contestantWordPictureBox = new PictureBox();
        private PictureBox[] _contestantEnvelopeTradePictureBoxes = new PictureBox[GameConstants.MAX_ENVELOPE_COUNT_PERSON];
        private PictureBox _contestantCashPictureBox = new PictureBox();

        private PictureBox _hostWordPictureBox = new PictureBox();
        private PictureBox[] _hostEnvelopeTradePictureBoxes = new PictureBox[GameConstants.MAX_ENVELOPE_COUNT_PERSON];
        private PictureBox _hostOfferCashPictureBox = new PictureBox();

        private PictureBox _gameOverPictureBox = new PictureBox();

        private static readonly Dictionary<int, int> MaxWidthsForEnvelopes = new Dictionary<int, int>();

        public TVScreenForm()
        {
            InitializeComponent();
        }

        private static int CalculateMaxWidthForEnvelopeCount(int envelopeCount)
        {
            return DrawingConstants.ENVELOPE_SIZE_WITH_PADDING.Width * (envelopeCount - 1) + DrawingConstants.ENVELOPE_SIZE.Width;
        }

        private void TVScreenForm_Load(object sender, EventArgs e)
        {
            MaxWidthsForEnvelopes.Add(0, 0);

            for (int i = 1; i <= Constants.GameConstants.MAX_ENVELOPE_COUNT_PERSON; ++i)
            {
                MaxWidthsForEnvelopes.Add(i, CalculateMaxWidthForEnvelopeCount(i));
            }

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

        private void PrepareIntroductionNamePictureBox()
        {
            _introductionNamePictureBox = new PictureBox()
            {
                Name = "IntroductionNamePictureBox",
                Visible = false,
                Size = DrawingConstants.INTRODUCE_BAR_SIZE_FULL,
                Location = DrawingConstants.INTRODUCE_BAR_LOCATION
            };
            _introductionNamePictureBox.Paint += IntroductionName_Paint;
            Controls.Add(_introductionNamePictureBox);
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
                    Name = $"StartEnvelope{i}PictureBox",
                    Visible = false,
                    Tag = i + 1,
                    Size = DrawingConstants.ENVELOPE_SIZE,
                    Location = calculatedLocation
                };
                newPictureBox.Paint += AllEnvelopePicture_Paint;

                _envelopeSelectionPictureBoxes[i] = newPictureBox;
            }

            Controls.AddRange(_envelopeSelectionPictureBoxes);
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

        private void PrepareContestantTradingPictureBoxes()
        {
            for (int i = 0; i < GameConstants.MAX_ENVELOPE_COUNT_PERSON; ++i)
            {
                Point location = new Point(
                    x: DrawingConstants.BOTTOM_TRADING_CONTESTANT_LOCATION.X,
                    y: DrawingConstants.BOTTOM_TRADING_CONTESTANT_LOCATION.Y - i * DrawingConstants.ENVELOPE_SIZE_WITH_PADDING.Height);

                var newPictureBox = new PictureBox()
                {
                    Name = $"ContestantTrading{i}PictureBox",
                    Visible = false,
                    Tag = i,
                    Size = DrawingConstants.ENVELOPE_SIZE,
                    Location = location,
                    BackColor = Color.Transparent
                };
                newPictureBox.Paint += ContestantTradeEnvelope_Paint;

                _contestantEnvelopeTradePictureBoxes[i] = newPictureBox;
            }

            Controls.AddRange(_contestantEnvelopeTradePictureBoxes);
        }

        private void PrepareHostTradingPictureBoxes()
        {
            for (int i = 0; i < GameConstants.MAX_ENVELOPE_COUNT_PERSON; ++i)
            {
                Point location = new Point(
                    x: DrawingConstants.BOTTOM_TRADING_HOST_LOCATION.X,
                    y: DrawingConstants.BOTTOM_TRADING_HOST_LOCATION.Y - i * DrawingConstants.ENVELOPE_SIZE_WITH_PADDING.Height);

                var newPictureBox = new PictureBox()
                {
                    Name = $"HostTrading{i}PictureBox",
                    Visible = false,
                    Tag = i,
                    Size = DrawingConstants.ENVELOPE_SIZE,
                    Location = location,
                    BackColor = Color.Transparent
                };
                newPictureBox.Paint += HostTradeEnvelope_Paint;

                _hostEnvelopeTradePictureBoxes[i] = newPictureBox;
            }

            Controls.AddRange(_hostEnvelopeTradePictureBoxes);
        }

        private void PrepareContestantCashPictureBox()
        {
            _contestantCashPictureBox = new PictureBox()
            {
                Name = "ContestantCashPictureBox",
                Visible = false,
                Size = DrawingConstants.CONTESTANT_CASH_SIZE,
                Location = DrawingConstants.CONTESTANT_CASH_LOCATION
            };
            _contestantCashPictureBox.Paint += ContestantCashPictureBox_Paint;

            Controls.Add(_contestantCashPictureBox);
        }

        private void PrepareHostOfferPictureBox()
        {
            _hostOfferCashPictureBox = new PictureBox()
            {
                Name = "HostOfferCashPictureBox",
                Visible = false,
                Size = DrawingConstants.HOST_OFFER_SIZE,
                Location = DrawingConstants.HOST_OFFER_LOCATION
            };
            _hostOfferCashPictureBox.Paint += HostOfferPictureBox_Paint;

            Controls.Add(_hostOfferCashPictureBox);
        }

        private void PrepareTradingContestantWord()
        {
            _contestantWordPictureBox = new PictureBox()
            {
                Name = "ContestantWordPictureBox",
                Visible = false,
                Size = DrawingConstants.CONTESTANT_WORD_SIZE,
                Location = DrawingConstants.CONTESTANT_WORD_LOCATION
            };
            _contestantWordPictureBox.Paint += ContestantWordPictureBox_Paint;

            Controls.Add(_contestantWordPictureBox);
        }

        private void PrepareTradingHostName()
        {
            _hostWordPictureBox = new PictureBox()
            {
                Name = "HostWordPictureBox",
                Visible = false,
                Size = DrawingConstants.HOST_WORD_SIZE,
                Location = DrawingConstants.HOST_WORD_LOCATION
            };
            _hostWordPictureBox.Paint += HostWordPictureBox_Paint;

            Controls.Add(_hostWordPictureBox);
        }

        private void PrepareGameOverPictureBox()
        {
            _gameOverPictureBox = new PictureBox()
            {
                Name = "GameOverPictureBox",
                Visible = false,
                Size = DrawingConstants.GAME_OVER_SIZE,
                Location = DrawingConstants.GAME_OVER_LOCATION
            };
            _gameOverPictureBox.Paint += GameOverPictureBox_Paint;

            Controls.Add(_gameOverPictureBox);
        }

        private void PreparePictureBoxes()
        {
            PrepareIntroductionNamePictureBox();
            PrepareEnvelopeSelectionPictureBoxes();
            PrepareQuestionStatisticsPictureBox();
            PrepareQuestionCountPictureBox();
            PrepareQuestionPlayingForPictureBox();
            PrepareContestantTradingPictureBoxes();
            PrepareHostTradingPictureBoxes();
            PrepareContestantCashPictureBox();
            PrepareHostOfferPictureBox();
            PrepareTradingContestantWord();
            PrepareTradingHostName();
            PrepareGameOverPictureBox();
        }

        #region Events
        private void AddEvents()
        {
            var gameState = GameState.Instance;

            gameState.EventShowContestantName += GameState_EventShowContestantName;
            gameState.EventHideContestantName += GameState_EventHideContestantName;
            gameState.EventClearEverything += GameState_EventClearEverything;
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

        private void GameState_EventShowContestantName(object? sender, string name)
        {
            _contestantName = name;

            SetVisibleIntroductionPictureBox(visible: true);
        }

        private void GameState_EventHideContestantName(object? sender, EventArgs e)
        {
            SetVisibleIntroductionPictureBox(visible: false);
        }

        private void GameState_EventClearEverything(object? sender, EventArgs e)
        {
            _questionBarState = QuestionBarState.CLEAR;

            SetVisibleIntroductionPictureBox(visible: false);
            SetVisibleEnvelopeSelectionPictureBoxes(visible: false);
            SetVisibleEnvelopePlayingFor(visible: false);
            SetVisibleQuestionCounter(visible: false);
            SetVisibleQuestionBar(visible: false);
            SetVisibleTradingBoxes(visible: false);
            SetVisibleTradingEnvelopes(visible: false);
            SetVisibleGameOverBox(visible: false);
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

            SetVisibleEnvelopeSelectionPictureBoxes(visible: false);
            SetVisibleEnvelopePlayingFor(visible: false);
            SetVisibleQuestionBar(visible: false);
            SetVisibleQuestionCounter(visible: false);
        }

        private void GameState_EventShowQuestion(object? sender, EventArgs e)
        {
            _questionBarState = QuestionBarState.QUESTION_ONLY;

            SetVisibleQuestionBar(visible: true);
            SetVisibleQuestionCounter(visible: true);
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
            SetVisibleQuestionBar(visible: true);
        }
        private void GameState_EventRefreshEnvelopes(object? sender, EventArgs e)
        {
            RedrawEnvelopes();
        }

        private void GameState_EventStartTrading(object? sender, EventArgs e)
        {
            SetVisibleQuestionCounter(visible: false);
            SetVisibleEnvelopePlayingFor(visible: false);
            SetVisibleEnvelopeSelectionPictureBoxes(visible: false);
            SetVisibleQuestionBar(visible: false);

            SetVisibleTradingBoxes(visible: true);
            SetVisibleTradingEnvelopes(visible: true);
        }

        private void GameState_EventRefreshOffer(object? sender, EventArgs e)
        {
            foreach (var pictureBox in _contestantEnvelopeTradePictureBoxes)
            {
                pictureBox.Invalidate();
            }

            foreach (var pictureBox in _hostEnvelopeTradePictureBoxes)
            {
                pictureBox.Invalidate();
            }

            _contestantCashPictureBox.Invalidate();
            _hostOfferCashPictureBox.Invalidate();

            Refresh();
        }

        private void GameState_EventGameOver(object? sender, EventArgs e)
        {
            SetVisibleQuestionBar(visible: false);
            SetVisibleQuestionCounter(visible: false);
            SetVisibleEnvelopePlayingFor(visible: false);
            SetVisibleTradingBoxes(visible: false);
            SetVisibleTradingEnvelopes(visible: false);

            SetVisibleGameOverBox(visible: true);
        }

        #endregion

        #region Methods

        private void SetVisibleIntroductionPictureBox(bool visible)
        {
            _introductionNamePictureBox.Visible = visible;
            _introductionNamePictureBox.Invalidate();
        }

        private void SetVisibleEnvelopeSelectionPictureBoxes(bool visible)
        {
            foreach (var pictureBox in _envelopeSelectionPictureBoxes)
            {
                pictureBox.Visible = visible;
                pictureBox.Invalidate();
            }
        }

        private void SetVisibleQuestionBar(bool visible)
        {
            _questionBarPictureBox.Visible = visible;
            _questionBarPictureBox.Invalidate();
        }

        private void SetVisibleQuestionCounter(bool visible)
        {
            _questionCountPictureBox.Visible = visible;
            _questionCountPictureBox.Invalidate();
        }

        private void SetVisibleEnvelopePlayingFor(bool visible)
        {
            _questionPlayingForPictureBox.Visible = visible;
            _questionPlayingForPictureBox.Invalidate();
        }

        public void SetVisibleTradingBoxes(bool visible)
        {
            _contestantWordPictureBox.Visible = visible;
            _contestantCashPictureBox.Visible = visible;
            _hostWordPictureBox.Visible = visible;
            _hostOfferCashPictureBox.Visible = visible;

            _contestantWordPictureBox.Invalidate();
            _hostWordPictureBox.Invalidate();
            _contestantCashPictureBox.Invalidate();
            _hostOfferCashPictureBox.Invalidate();
        }

        public void SetVisibleTradingEnvelopes(bool visible)
        {
            foreach (var pictureBox in _contestantEnvelopeTradePictureBoxes)
            {
                pictureBox.Visible = visible;
                pictureBox.Invalidate();
            }

            foreach (var pictureBox in _hostEnvelopeTradePictureBoxes)
            {
                pictureBox.Visible = visible;
                pictureBox.Invalidate();
            }
        }

        private void SetVisibleGameOverBox(bool visible)
        {
            _gameOverPictureBox.Visible = visible;
            _gameOverPictureBox.Invalidate();
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

        #region Introduction

        private void IntroductionName_Paint(object? sender, PaintEventArgs e)
        {
            var clientRectangle = _introductionNamePictureBox.ClientRectangle;
            Size size = clientRectangle.Size;
            Point location = clientRectangle.Location;

            using (Brush brush = new SolidBrush(DrawingConstants.BOX_BACKGROUND_OUT))
            {
                e.Graphics.FillRectangle(brush, location.X, location.Y, size.Width, size.Height);
            }

            Rectangle introductionInsideRectangle = new(
                x: location.X + DrawingConstants.INTRODUCE_BAR_BORDER.Width,
                y: location.Y + DrawingConstants.INTRODUCE_BAR_BORDER.Height,
                width: size.Width - DrawingConstants.INTRODUCE_BAR_BORDER.Width * 2,
                height: size.Height - DrawingConstants.INTRODUCE_BAR_BORDER.Height * 2);

            using (Brush brush = new SolidBrush(DrawingConstants.BOX_BACKGROUND_IN))
            {
                e.Graphics.FillRectangle(brush, introductionInsideRectangle);
            }

            using (Font font = FontHelper.GetMaxFont(_contestantName, e.Graphics, DrawingConstants.INTRODUCTION_DRAWING_FONT, size, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS))
            {
                TextRenderer.DrawText(e.Graphics, _contestantName, font, introductionInsideRectangle, Color.White, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS);
            }
        }

        #endregion

        #region Envelope Drawing

        private void PaintEnvelopeInArea(Envelope envelope, Graphics graphics, Rectangle areaRectangle, Color? requestedBackgroundColor = null)
        {
            if (envelope is null)
                throw new ArgumentNullException(nameof(envelope));
            if (graphics is null)
                throw new ArgumentNullException(nameof(graphics));
            if (areaRectangle.IsEmpty)
                throw new ArgumentException($"Area rectangle is empty!", nameof(areaRectangle));

            Size size = areaRectangle.Size;

            Point leftPoint = areaRectangle.Location;
            Point centerPoint = new(leftPoint.X + size.Width / 2, leftPoint.Y + size.Height / 2);
            Point rightPoint = new(leftPoint.X + size.Width, leftPoint.Y);

            var colorCollection = envelope.GetColorsForTv();

            Color backgroundColor = requestedBackgroundColor ?? colorCollection.BackgroundColor;

            using (Brush backgroundBrush = new SolidBrush(backgroundColor))
            {
                graphics.FillRectangle(backgroundBrush, areaRectangle);
            }

            using (Pen linePen = new(colorCollection.LineColor))
            {
                graphics.DrawLine(linePen, leftPoint, centerPoint);
                graphics.DrawLine(linePen, centerPoint, rightPoint);
            }

            string envelopeNumberString = string.Format("{0,2}", envelope.EnvelopeNumber);
            Size envelopeNumberSize = TextRenderer.MeasureText(graphics, envelopeNumberString, DrawingConstants.ENVELOPE_DRAWING_FONT);

            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.GammaCorrected;
            graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;

            using (Brush backgroundBrush = new SolidBrush(backgroundColor))
            {
                graphics.FillRectangle(backgroundBrush, leftPoint.X, leftPoint.Y, envelopeNumberSize.Width, envelopeNumberSize.Height);
            }

            TextRenderer.DrawText(graphics, envelopeNumberString, DrawingConstants.ENVELOPE_DRAWING_FONT, leftPoint, colorCollection.NumberFontColor);

            if (GameState.Instance.GameSettings.ShowAmountsOnTv || envelope.IsOpen)
            {
                BaseCheque cheque = envelope.Cheque;
                string chequeString = cheque.ToValueString();
                Size bottomHalfSize = new(width: size.Width, height: size.Height / 2);

                using (Font maxFont = FontHelper.GetMaxFont(chequeString, graphics, DrawingConstants.ENVELOPE_DRAWING_FONT, bottomHalfSize, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS))
                {
                    Size chequeValueSize = TextRenderer.MeasureText(chequeString, maxFont); 

                    Rectangle bottomRight = new Rectangle(
                        x: leftPoint.X + size.Width - chequeValueSize.Width,
                        y: leftPoint.Y + size.Height - chequeValueSize.Height,
                        width: chequeValueSize.Width,
                        height: chequeValueSize.Height);

                    TextRenderer.DrawText(graphics, chequeString, maxFont, bottomRight, colorCollection.ChequeFontColor, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS);
                }
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

            using (Brush brush = new SolidBrush(DrawingConstants.BOX_BACKGROUND_OUT))
            {
                e.Graphics.FillRectangle(brush, location.X, location.Y, size.Width, size.Height);
            }

            Rectangle questionBarRectangle = new(
                x: location.X + DrawingConstants.QUESTION_BAR_BORDER.Width,
                y: location.Y + DrawingConstants.QUESTION_BAR_BORDER.Height,
                width: size.Width - DrawingConstants.QUESTION_BAR_BORDER.Width * 2,
                height: size.Height - DrawingConstants.QUESTION_BAR_BORDER.Height * 2);

            using (Brush brush = new SolidBrush(DrawingConstants.BOX_BACKGROUND_IN))
            {
                e.Graphics.FillRectangle(brush, questionBarRectangle);
            }

            var gameStateInstance = GameState.Instance;
            Question? currentQuestion = null;

            if (!StatesWithoutQuestion.Contains(_questionBarState))
                currentQuestion = gameStateInstance.GetCurrentQuestion();

            switch (_questionBarState)
            {
                case QuestionBarState.QUESTION_ONLY:
                    PaintQuestion(currentQuestion!, e.Graphics, questionBarRectangle);
                    break;
                case QuestionBarState.QUESTION_AND_ANSWERS:
                    PaintQuestionAndAnswers(currentQuestion!, e.Graphics, questionBarRectangle, lockedInAnswer: null, correctAnswer: null);
                    break;
                case QuestionBarState.QUESTION_AND_ANSWERS_LOCK:
                    PaintQuestionAndAnswers(currentQuestion!, e.Graphics, questionBarRectangle, lockedInAnswer: gameStateInstance.ContestantAnswer, correctAnswer: null);
                    break;
                case QuestionBarState.QUESTION_AND_ANSWERS_CORRECT:
                    PaintQuestionAndAnswers(currentQuestion!, e.Graphics, questionBarRectangle, lockedInAnswer: gameStateInstance.ContestantAnswer, correctAnswer: currentQuestion!.CorrectAnswerNumber);
                    break;
                case QuestionBarState.SHOW_ENVELOPES_AND_QUESTION:
                    PaintQuestionBarEnvelopesAndQuestion(gameStateInstance.ContestantEnvelopeSet.Envelopes, currentQuestion!, e.Graphics, questionBarRectangle);
                    break;
                case QuestionBarState.SHOW_ENVELOPES_ONLY:
                    PaintQuestionBarEnvelopes(gameStateInstance.ContestantEnvelopeSet.Envelopes, e.Graphics, questionBarRectangle);
                    break;
                case QuestionBarState.CLEAR:
                    break;
                default:
                    throw new NotImplementedException($"Not recognized question bar state: {_questionBarState}!");
            }
        }

        private void PaintQuestion(Question question, Graphics graphics, Rectangle areaRectangle)
        {
            if (question is null)
                throw new ArgumentNullException(nameof(question));
            if (graphics is null)
                throw new ArgumentNullException(nameof(graphics));
            if (areaRectangle.IsEmpty)
                throw new ArgumentException($"The question rectangle is empty.", nameof(areaRectangle));

            string questionToDraw = question.Text;

            using (Font font = FontHelper.GetMaxFont(questionToDraw, graphics, DrawingConstants.QUESTION_DRAWING_FONT, areaRectangle.Size, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS))
            {
                TextRenderer.DrawText(graphics, questionToDraw, DrawingConstants.QUESTION_DRAWING_FONT, areaRectangle, Color.White, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS);
            }
        }

        private void PaintQuestionAndAnswers(Question question, Graphics graphics, Rectangle areaRectangle, int? lockedInAnswer = null, int? correctAnswer = null)
        {
            if (question is null)
                throw new ArgumentNullException(nameof(question));
            if (graphics is null)
                throw new ArgumentNullException(nameof(graphics));
            if (areaRectangle.IsEmpty)
                throw new ArgumentException($"The question rectangle is empty.", nameof(areaRectangle));

            // Draw question

            Rectangle questionRectangle = new Rectangle(areaRectangle.X, areaRectangle.Y, areaRectangle.Width, areaRectangle.Height / 2);
            using (Font font = FontHelper.GetMaxFont(question.Text, graphics, DrawingConstants.QUESTION_ANSWER_DRAWING_FONT, questionRectangle.Size, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS))
            {
                TextRenderer.DrawText(graphics, question.Text, font, questionRectangle, Color.White, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS);
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

        private void PaintAnswer(Question question, Graphics graphics, Rectangle areaRectangle, int answerNumber, Color color)
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

            Rectangle answerRectangle = new Rectangle(
                x: areaRectangle.X + (rightSide ? areaRectangle.Width / 2 : 0),
                y: areaRectangle.Y + (areaRectangle.Height * (downSide ? 3 : 2) / 4),
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

            using Font font = FontHelper.GetMaxFont(answerText, graphics, DrawingConstants.QUESTION_ANSWER_DRAWING_FONT, answerRectangle.Size, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS);
            TextRenderer.DrawText(graphics, answerText, font, answerRectangle, color, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS);
        }

        private void PaintQuestionBarEnvelopesAndQuestion(IList<Envelope> envelopes, Question question, Graphics graphics, Rectangle areaRectangle)
        {
            if (envelopes is null)
                throw new ArgumentNullException(nameof(envelopes));
            if (envelopes.Any(envelope => envelope is null))
                throw new ArgumentException($"At least one envelope is null.", nameof(envelopes));
            if (graphics is null)
                throw new ArgumentNullException(nameof(graphics));
            if (areaRectangle.IsEmpty)
                throw new ArgumentException($"Area rectangle is empty!", nameof(areaRectangle));
            if (question is null)
                throw new ArgumentNullException(nameof(question));

            int envelopeCount = envelopes.Count;
            bool allEnvelopesInOneRow = envelopeCount <= DrawingConstants.ENVELOPE_MAX_IN_ROW_QUESTION;

            int questionHeight = allEnvelopesInOneRow ? areaRectangle.Height / 2 : areaRectangle.Height / 3;

            Rectangle upperHalf = new Rectangle(
                x: areaRectangle.X,
                y: areaRectangle.Y,
                width: areaRectangle.Width,
                height: questionHeight);

            using (Font font = FontHelper.GetMaxFont(question.Text, graphics, DrawingConstants.QUESTION_ANSWER_DRAWING_FONT, upperHalf.Size, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS))
            {
                TextRenderer.DrawText(graphics, question.Text, font, upperHalf, Color.White, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS);
            }

            Rectangle envelopeRectangle = new Rectangle(
                x: areaRectangle.X,
                y: areaRectangle.Y + questionHeight,
                width: areaRectangle.Width,
                height: areaRectangle.Height - questionHeight);

            // Draw envelopes

            if (allEnvelopesInOneRow)
            {
                int totalWidth = MaxWidthsForEnvelopes[envelopeCount];
                for (int i = 0; i < envelopeCount; i++)
                {
                    Envelope envelope = envelopes[i];

                    Rectangle rectangle = new Rectangle(
                        x: envelopeRectangle.X + (envelopeRectangle.Width - totalWidth) / 2 + i * DrawingConstants.ENVELOPE_SIZE_WITH_PADDING.Width,
                        y: envelopeRectangle.Y + (envelopeRectangle.Height - DrawingConstants.ENVELOPE_SIZE.Height) / 2,
                        width: DrawingConstants.ENVELOPE_SIZE.Width,
                        height: DrawingConstants.ENVELOPE_SIZE.Height);

                    PaintEnvelopeInArea(envelope, graphics, rectangle);
                }
            }
            else
            {
                int envelopesInSecondRow = envelopeCount / 2;
                int maxWidthSecondRow = MaxWidthsForEnvelopes[envelopesInSecondRow];

                int envelopesInFirstRow = envelopeCount - envelopesInSecondRow;
                int maxWidthFirstRow = MaxWidthsForEnvelopes[envelopesInFirstRow];

                for (int i = 0; i < envelopesInFirstRow; ++i)
                {
                    Envelope envelope = envelopes[i];

                    Rectangle rectangle = new Rectangle(
                        x: envelopeRectangle.X + (envelopeRectangle.Width - maxWidthFirstRow) / 2 + i * DrawingConstants.ENVELOPE_SIZE_WITH_PADDING.Width,
                        y: envelopeRectangle.Y + (int) (envelopeRectangle.Height * DrawingConstants.QUESTION_BAR_MOVE_TOP_PART_WITH_QUESTION) - DrawingConstants.ENVELOPE_SIZE.Height / 2,
                        width: DrawingConstants.ENVELOPE_SIZE.Width,
                        height: DrawingConstants.ENVELOPE_SIZE.Height);

                    PaintEnvelopeInArea(envelope, graphics, rectangle);
                }

                for (int i = envelopesInFirstRow; i < envelopeCount; ++i)
                {
                    Envelope envelope = envelopes[i];

                    Rectangle rectangle = new Rectangle(
                        x: envelopeRectangle.X + (envelopeRectangle.Width - maxWidthSecondRow) / 2 + (i - envelopesInFirstRow) * DrawingConstants.ENVELOPE_SIZE_WITH_PADDING.Width,
                        y: envelopeRectangle.Y + (int) (envelopeRectangle.Height * DrawingConstants.QUESTION_BAR_MOVE_BOTTOM_PART_WITH_QUESTION) - DrawingConstants.ENVELOPE_SIZE.Height / 2,
                        width: DrawingConstants.ENVELOPE_SIZE.Width,
                        height: DrawingConstants.ENVELOPE_SIZE.Height);

                    PaintEnvelopeInArea(envelope, graphics, rectangle);
                }
            }
        }

        private void PaintQuestionBarEnvelopes(IList<Envelope> envelopes, Graphics graphics, Rectangle areaRectangle)
        {
            if (envelopes is null)
                throw new ArgumentNullException(nameof(envelopes));
            if (envelopes.Any(envelope => envelope is null))
                throw new ArgumentException($"At least one envelope is null.", nameof(envelopes));
            if (graphics is null)
                throw new ArgumentNullException(nameof(graphics));
            if (areaRectangle.IsEmpty)
                throw new ArgumentException($"Area rectangle is empty!", nameof(areaRectangle));

            int envelopeCount = envelopes.Count;
            bool allEnvelopesInOneRow = envelopeCount <= DrawingConstants.ENVELOPE_MAX_IN_ROW_QUESTION;

            Rectangle envelopeRectangle = new Rectangle(
                    x: areaRectangle.X,
                    y: areaRectangle.Y,
                    width: areaRectangle.Width,
                    height: areaRectangle.Height);

            // Draw envelopes

            if (allEnvelopesInOneRow)
            {
                int totalWidth = MaxWidthsForEnvelopes[envelopeCount];
                for (int i = 0; i < envelopeCount; i++)
                {
                    Envelope envelope = envelopes[i];

                    Rectangle rectangle = new Rectangle(
                        x: envelopeRectangle.X + (envelopeRectangle.Width - totalWidth) / 2 + i * DrawingConstants.ENVELOPE_SIZE_WITH_PADDING.Width,
                        y: envelopeRectangle.Y + (envelopeRectangle.Height - DrawingConstants.ENVELOPE_SIZE.Height) / 2,
                        width: DrawingConstants.ENVELOPE_SIZE.Width,
                        height: DrawingConstants.ENVELOPE_SIZE.Height);

                    PaintEnvelopeInArea(envelope, graphics, rectangle);
                }
            }
            else
            {
                int envelopesInSecondRow = envelopeCount / 2;
                int maxWidthSecondRow = MaxWidthsForEnvelopes[envelopesInSecondRow];

                int envelopesInFirstRow = envelopeCount - envelopesInSecondRow;
                int maxWidthFirstRow = MaxWidthsForEnvelopes[envelopesInFirstRow];

                for (int i = 0; i < envelopesInFirstRow; ++i)
                {
                    Envelope envelope = envelopes[i];

                    Rectangle rectangle = new Rectangle(
                        x: envelopeRectangle.X + (envelopeRectangle.Width - maxWidthFirstRow) / 2 + i * DrawingConstants.ENVELOPE_SIZE_WITH_PADDING.Width,
                        y: envelopeRectangle.Y + (int) (envelopeRectangle.Height * DrawingConstants.QUESTION_BAR_MOVE_TOP_PART_NO_QUESTION) - DrawingConstants.ENVELOPE_SIZE.Height / 2,
                        width: DrawingConstants.ENVELOPE_SIZE.Width,
                        height: DrawingConstants.ENVELOPE_SIZE.Height);

                    PaintEnvelopeInArea(envelope, graphics, rectangle);
                }

                for (int i = envelopesInFirstRow; i < envelopeCount; ++i)
                {
                    Envelope envelope = envelopes[i];

                    Rectangle rectangle = new Rectangle(
                        x: envelopeRectangle.X + (envelopeRectangle.Width - maxWidthSecondRow) / 2 + (i - envelopesInFirstRow) * DrawingConstants.ENVELOPE_SIZE_WITH_PADDING.Width,
                        y: envelopeRectangle.Y + (int) (envelopeRectangle.Height * DrawingConstants.QUESTION_BAR_MOVE_BOTTOM_PART_NO_QUESTION) - DrawingConstants.ENVELOPE_SIZE.Height / 2,
                        width: DrawingConstants.ENVELOPE_SIZE.Width,
                        height: DrawingConstants.ENVELOPE_SIZE.Height);

                    PaintEnvelopeInArea(envelope, graphics, rectangle);
                }
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
            

            Rectangle allRectangle = new Rectangle(
                x: location.X,
                y: location.Y,
                width: size.Width,
                height: size.Height);

            Rectangle questionCounterRectangle = new Rectangle(
                x: location.X + DrawingConstants.QUESTION_COUNTER_BORDER.Width,
                y: location.Y + DrawingConstants.QUESTION_COUNTER_BORDER.Height,
                width: size.Width - DrawingConstants.QUESTION_COUNTER_BORDER.Width * 2,
                height: size.Height - DrawingConstants.QUESTION_COUNTER_BORDER.Height * 2);

            using (Brush outsideBrush = new SolidBrush(DrawingConstants.BOX_BACKGROUND_OUT))
            {
                e.Graphics.FillRectangle(outsideBrush, allRectangle);
            }

            using (Brush insideBrush = new SolidBrush(DrawingConstants.BOX_BACKGROUND_IN))
            {
                e.Graphics.FillRectangle(insideBrush, questionCounterRectangle);
            }

            string questionNumber = string.Format("PYTANIE {0}/{1}", gameStateInstance.QuestionNumber, gameStateInstance.MaxQuestionCount);

            using (Font font = FontHelper.GetMaxFont(questionNumber, e.Graphics, DrawingConstants.QUESTION_COUNTER_FONT, questionCounterRectangle.Size, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS))
            {
                TextRenderer.DrawText(e.Graphics, questionNumber, font, questionCounterRectangle, Color.White, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS);
            }
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

        #region Trading Paint

        private void ContestantCashPictureBox_Paint(object? sender, PaintEventArgs e)
        {
            Rectangle clientRectangle = _contestantCashPictureBox.ClientRectangle;
            Size size = clientRectangle.Size;
            Point location = clientRectangle.Location;

            Rectangle insideRectangle = new Rectangle(
                x: location.X + DrawingConstants.CONTESTANT_CASH_PADDING.Width,
                y: location.Y + DrawingConstants.CONTESTANT_CASH_PADDING.Height,
                width: size.Width - DrawingConstants.CONTESTANT_CASH_PADDING.Width * 2,
                height: size.Height - DrawingConstants.CONTESTANT_CASH_PADDING.Height * 2);

            using (Brush outsideBrush = new SolidBrush(DrawingConstants.BOX_BACKGROUND_OUT))
            {
                e.Graphics.FillRectangle(outsideBrush, clientRectangle);
            }

            using (Brush insideBrush = new SolidBrush(DrawingConstants.BOX_BACKGROUND_IN))
            {
                e.Graphics.FillRectangle(insideBrush, insideRectangle);
            }

            Rectangle upperPart = new Rectangle(
                x: insideRectangle.X,
                y: insideRectangle.Y,
                width: insideRectangle.Size.Width,
                height: (int) (insideRectangle.Size.Height * DrawingConstants.TRADING_TITLE_PART_FRACTION));

            Rectangle lowerPart = new Rectangle(
                x: insideRectangle.X,
                y: insideRectangle.Y + (int)(insideRectangle.Size.Height * DrawingConstants.TRADING_TITLE_PART_FRACTION),
                width: insideRectangle.Size.Width,
                height: (int)(insideRectangle.Size.Height * DrawingConstants.TRADING_REST_PART_FRACTION));

            using (Pen pen = new(DrawingConstants.BOX_BACKGROUND_OUT))
            {
                e.Graphics.DrawRectangle(pen, upperPart);
            }

            using (Font font = FontHelper.GetMaxFont(GameConstants.CASH_STRING, e.Graphics, DrawingConstants.TRADING_SMALL_FONT, upperPart.Size, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS))
            {
                TextRenderer.DrawText(e.Graphics, GameConstants.CASH_STRING, font, upperPart, Color.White, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS);
            }

            string cash = Utils.AmountToString(GameState.Instance.ContestantCash);
            using (Font font = FontHelper.GetMaxFont(cash, e.Graphics, DrawingConstants.TRADING_BIG_FONT, lowerPart.Size, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS))
            {
                TextRenderer.DrawText(e.Graphics, cash, font, lowerPart, Color.White, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS);
            }
        }

        private void HostOfferPictureBox_Paint(object? sender, PaintEventArgs e)
        {
            Rectangle clientRectangle = _hostOfferCashPictureBox.ClientRectangle;
            Size size = clientRectangle.Size;
            Point location = clientRectangle.Location;

            Rectangle insideRectangle = new Rectangle(
                x: location.X + DrawingConstants.HOST_OFFER_PADDING.Width,
                y: location.Y + DrawingConstants.HOST_OFFER_PADDING.Height,
                width: size.Width - DrawingConstants.HOST_OFFER_PADDING.Width * 2,
                height: size.Height - DrawingConstants.HOST_OFFER_PADDING.Height * 2);

            using (Brush outsideBrush = new SolidBrush(DrawingConstants.BOX_BACKGROUND_OUT))
            {
                e.Graphics.FillRectangle(outsideBrush, clientRectangle);
            }

            using (Brush insideBrush = new SolidBrush(DrawingConstants.BOX_BACKGROUND_IN))
            {
                e.Graphics.FillRectangle(insideBrush, insideRectangle);
            }

            Rectangle upperPart = new Rectangle(
                x: insideRectangle.X,
                y: insideRectangle.Y,
                width: insideRectangle.Size.Width,
                height: (int)(insideRectangle.Size.Height * DrawingConstants.TRADING_TITLE_PART_FRACTION));

            Rectangle lowerPart = new Rectangle(
                x: insideRectangle.X,
                y: insideRectangle.Y + (int)(insideRectangle.Size.Height * DrawingConstants.TRADING_TITLE_PART_FRACTION),
                width: insideRectangle.Size.Width,
                height: (int)(insideRectangle.Size.Height * DrawingConstants.TRADING_REST_PART_FRACTION));

            using (Pen pen = new(DrawingConstants.BOX_BACKGROUND_OUT))
            {
                e.Graphics.DrawRectangle(pen, upperPart);
            }

            using (Font font = FontHelper.GetMaxFont(GameConstants.OFFER_STRING, e.Graphics, DrawingConstants.TRADING_SMALL_FONT, upperPart.Size, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS))
            {
                TextRenderer.DrawText(e.Graphics, GameConstants.OFFER_STRING, font, upperPart, Color.White, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS);
            }

            string offer = Utils.AmountToString(GameState.Instance.CashOffer);
            using (Font font = FontHelper.GetMaxFont(offer, e.Graphics, DrawingConstants.TRADING_BIG_FONT, lowerPart.Size, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS))
            {
                TextRenderer.DrawText(e.Graphics, offer, font, lowerPart, Color.White, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS);
            }
            
        }

        private void ContestantTradeEnvelope_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not PictureBox envelopeBox)
                return;

            Rectangle clientRectangle = envelopeBox.ClientRectangle;
            Point location = clientRectangle.Location;
            Size size = clientRectangle.Size;

            Rectangle rectangle = new Rectangle(
                x: location.X,
                y: location.Y,
                width: size.Width,
                height: size.Height);

            var gameStateInstance = GameState.Instance;

            int tag = (int)(envelopeBox.Tag ?? throw new InvalidOperationException($"Envelope is missing the tag."));
            int envelopeCount = gameStateInstance.ContestantEnvelopeSet.EnvelopeCount;

            int index = envelopeCount - tag - 1;
            Envelope? envelope;
            if (index < 0 || ((envelope = gameStateInstance.GetContestantEnvelope(index)) is null))
            {
                using (Brush transparentBrush = new SolidBrush(Color.Transparent))
                {
                    e.Graphics.FillRectangle(transparentBrush, rectangle);
                }
            }
            else
            {
                PaintEnvelopeInArea(envelope, e.Graphics, rectangle);
            }
        }

        private void HostTradeEnvelope_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not PictureBox envelopeBox)
                return;

            var clientRectangle = envelopeBox.ClientRectangle;
            var location = clientRectangle.Location;
            var size = clientRectangle.Size;

            Rectangle rectangle = new Rectangle(
                x: location.X,
                y: location.Y,
                width: size.Width,
                height: size.Height);

            var gameStateInstance = GameState.Instance;

            int tag = (int)(envelopeBox.Tag ?? throw new InvalidOperationException($"Envelope is missing the tag."));
            int envelopeCount = gameStateInstance.HostEnvelopeSet.EnvelopeCount;

            int index = envelopeCount - tag - 1;
            Envelope? envelope;
            if (index < 0 || ((envelope = gameStateInstance.GetHostEnvelope(index)) is null))
            {
                using (Brush transparentBrush = new SolidBrush(Color.Transparent))
                {
                    e.Graphics.FillRectangle(transparentBrush, rectangle);
                }
            }
            else
            {
                PaintEnvelopeInArea(envelope, e.Graphics, rectangle);
            }
        }

        private void ContestantWordPictureBox_Paint(object? sender, PaintEventArgs e)
        {
            var clientRectangle = _contestantWordPictureBox.ClientRectangle;
            var location = clientRectangle.Location;
            var size = clientRectangle.Size;

            Rectangle insideRectangle = new Rectangle(
                x: location.X + DrawingConstants.CONTESTANT_WORD_PADDING.Width,
                y: location.Y + DrawingConstants.CONTESTANT_WORD_PADDING.Height,
                width: size.Width - DrawingConstants.CONTESTANT_WORD_PADDING.Width * 2,
                height: size.Height - DrawingConstants.CONTESTANT_WORD_PADDING.Height * 2);

            using (Brush outsideBrush = new SolidBrush(DrawingConstants.BOX_BACKGROUND_OUT))
            {
                e.Graphics.FillRectangle(outsideBrush, clientRectangle);
            }

            using (Brush insideBrush = new SolidBrush(DrawingConstants.BOX_BACKGROUND_IN))
            {
                e.Graphics.FillRectangle(insideBrush, insideRectangle);
            }

            using (Font font = FontHelper.GetMaxFont(GameConstants.CONTESTANT_STRING, e.Graphics, DrawingConstants.TRADING_SMALL_FONT, insideRectangle.Size, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS))
            {
                TextRenderer.DrawText(e.Graphics, GameConstants.CONTESTANT_STRING, font, insideRectangle, Color.White, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS);
            }
        }

        private void HostWordPictureBox_Paint(object? sender, PaintEventArgs e)
        {
            var clientRectangle = _hostWordPictureBox.ClientRectangle;
            var location = clientRectangle.Location;
            var size = clientRectangle.Size;

            Rectangle insideRectangle = new Rectangle(
                x: location.X + DrawingConstants.HOST_WORD_PADDING.Width,
                y: location.Y + DrawingConstants.HOST_WORD_PADDING.Height,
                width: size.Width - DrawingConstants.HOST_WORD_PADDING.Width * 2,
                height: size.Height - DrawingConstants.HOST_WORD_PADDING.Height * 2);

            using (Brush outsideBrush = new SolidBrush(DrawingConstants.BOX_BACKGROUND_OUT))
            {
                e.Graphics.FillRectangle(outsideBrush, clientRectangle);
            }

            using (Brush insideBrush = new SolidBrush(DrawingConstants.BOX_BACKGROUND_IN))
            {
                e.Graphics.FillRectangle(insideBrush, insideRectangle);
            }

            using (Font font = FontHelper.GetMaxFont(GameConstants.HOST_STRING, e.Graphics, DrawingConstants.TRADING_SMALL_FONT, insideRectangle.Size, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS))
            {
                TextRenderer.DrawText(e.Graphics, GameConstants.HOST_STRING, font, insideRectangle, Color.White, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS);
            }
        }

        #endregion

        #region Game Over

        private void GameOverPictureBox_Paint(object? sender, PaintEventArgs e)
        {
            Rectangle clientRectangle = _gameOverPictureBox.ClientRectangle;
            Size size = clientRectangle.Size;
            Point location = clientRectangle.Location;

            Rectangle insideRectangle = new Rectangle(
                x: location.X + DrawingConstants.GAME_OVER_PADDING.Width,
                y: location.Y + DrawingConstants.GAME_OVER_PADDING.Height,
                width: size.Width - DrawingConstants.GAME_OVER_PADDING.Width * 2,
                height: size.Height - DrawingConstants.GAME_OVER_PADDING.Height * 2);

            using (Brush outsideBrush = new SolidBrush(DrawingConstants.BOX_BACKGROUND_OUT))
            {
                e.Graphics.FillRectangle(outsideBrush, clientRectangle);
            }

            using (Brush insideBrush = new SolidBrush(DrawingConstants.BOX_BACKGROUND_IN))
            {
                e.Graphics.FillRectangle(insideBrush, insideRectangle);
            }

            Rectangle upperPart = new Rectangle(
                x: insideRectangle.X,
                y: insideRectangle.Y,
                width: insideRectangle.Size.Width,
                height: (int)(insideRectangle.Size.Height * DrawingConstants.GAME_OVER_TITLE_PART_FRACTION));

            Rectangle lowerPart = new Rectangle(
                x: insideRectangle.X,
                y: insideRectangle.Y + (int)(insideRectangle.Size.Height * DrawingConstants.GAME_OVER_TITLE_PART_FRACTION),
                width: insideRectangle.Size.Width,
                height: (int)(insideRectangle.Size.Height * DrawingConstants.GAME_OVER_REST_PART_FRACTION));

            using (Pen pen = new(DrawingConstants.BOX_BACKGROUND_OUT))
            {
                e.Graphics.DrawRectangle(pen, upperPart);
            }

            using (Font font = FontHelper.GetMaxFont(GameConstants.PRIZE_STRING, e.Graphics, DrawingConstants.GAME_OVER_SMALL_FONT, upperPart.Size, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS))
            {
                TextRenderer.DrawText(e.Graphics, GameConstants.PRIZE_STRING, font, upperPart, Color.White, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS);
            }

            string finalPrize = GameState.Instance.FinalPrize.Trim();
            using (Font font = FontHelper.GetMaxFont(finalPrize, e.Graphics, DrawingConstants.TRADING_BIG_FONT, lowerPart.Size, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS, limitScale: false))
            {
                TextRenderer.DrawText(e.Graphics, finalPrize, font, lowerPart, Color.White, DrawingConstants.TEXT_CENTER_DRAWING_FORMAT_FLAGS);
            }
        }

        #endregion

        private void TVScreenForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                Refresh();
        }
    }
}