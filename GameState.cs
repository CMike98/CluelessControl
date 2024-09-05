using CluelessControl.Cheques;

namespace CluelessControl
{
    public class GameState
    {
        #region Instance
        private static object _lock = new();
        private static GameState? _instance = null;

        public static GameState Instance
        {
            get
            {
                lock (_lock)
                {
                    _instance ??= new GameState();
                    return _instance;
                }
            }
        }
        #endregion

        #region Fields/Variables
        public GameSettings GameSettings
        {
            get;
            private set;
        }

        public ChequeSettings ChequeSettings
        {
            get;
            private set;
        }

        public EnvelopeTable EnvelopeTable
        {
            get;
            private set;
        }

        public QuestionSet QuestionSet
        {
            get;
            private set;
        }

        public List<Envelope> ContestantEnvelopes
        {
            get;
            private set;
        }

        public List<Envelope> HostEnvelopes
        {
            get;
            private set;
        }

        public Envelope? EnvelopePlayedFor
        {
            get;
            private set;
        }

        public decimal ContestantCash
        {
            get;
            private set;
        }

        public decimal CashOffer
        {
            get;
            private set;
        }

        public int QuestionIndex
        {
            get;
            private set;
        }

        public int? ContestantAnswer
        {
            get;
            private set;
        }

        #endregion

        #region Calculated Properties

        public int QuestionNumber => QuestionIndex + 1;

        public int MaxQuestionCount => Math.Min(GameSettings.StartEnvelopeCount, QuestionSet.QuestionCount);

        public bool HasQuestionsLeft => QuestionNumber < MaxQuestionCount;

        #endregion

        #region Events
        public event EventHandler? EventClearQuestion;
        public event EventHandler? EventShowQuestion;
        public event EventHandler? EventShowAnswers;
        public event EventHandler? EventAnswerSelected;
        public event EventHandler? EventCorrectAnswerShown;
        public event EventHandler? EventRefreshEnvelopes;
        public event EventHandler? EventStartTrading;
        public event EventHandler? EventRefreshOffer;
        public event EventHandler? EventGameOver;
        #endregion

        #region Constructor
        private GameState()
        {
            GameSettings = GameSettings.Create();
            QuestionSet = QuestionSet.Create();
            ChequeSettings = ChequeSettings.Create();
            EnvelopeTable = EnvelopeTable.Create();

            ContestantEnvelopes = new(Constants.MAX_ENVELOPE_POSSIBLE_COUNT);
            HostEnvelopes = new(Constants.MAX_ENVELOPE_POSSIBLE_COUNT);
            EnvelopePlayedFor = null;

            ContestantAnswer = null;
            QuestionIndex = -1;
            ContestantCash = 0;
            CashOffer = 0;
        }
        #endregion

        #region Moving The Game
        public void NewGame()
        {
            ContestantEnvelopes.Clear();
            HostEnvelopes.Clear();

            EnvelopeTable = EnvelopeTable.Create(ChequeSettings);

            EnvelopePlayedFor = null;
            ContestantAnswer = null;
            QuestionIndex = -1;
            ContestantCash = 0;
            CashOffer = 0;
        }

        public void NextQuestion()
        {
            EnvelopePlayedFor = null;
            ContestantAnswer = null;
            QuestionIndex++;

            EventClearQuestion?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Loading
        public void LoadGameSettings(GameSettings settings)
        {
            ArgumentNullException.ThrowIfNull(settings, nameof(settings));
            GameSettings = settings;
        }

        public void LoadGameSettingsFromFile(string fileName)
        {
            ArgumentNullException.ThrowIfNull(fileName, nameof(fileName));
            GameSettings = GameSettings.LoadFromFile(fileName);
        }

        public void LoadQuestionSet(QuestionSet questionSet)
        {
            ArgumentNullException.ThrowIfNull(questionSet, nameof(questionSet));
            QuestionSet = questionSet;
        }

        public void LoadQuestionSetFromFile(string fileName)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(fileName, nameof(fileName));
            QuestionSet = QuestionSet.LoadFromFile(fileName);
        }

        public void LoadChequeSettings(ChequeSettings chequeSettings)
        {
            ArgumentNullException.ThrowIfNull(chequeSettings, nameof(chequeSettings));
            ChequeSettings = chequeSettings;
        }

        public void LoadChequeSettingsFromFile(string fileName)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(fileName, nameof(fileName));
            ChequeSettings = ChequeSettings.LoadFromFile(fileName);
        }
        #endregion

        #region Envelopes

        public void SortEnvelopesByNumber()
        {
            static int EnvelopeNumberComparer(Envelope env1, Envelope env2)
            {
                if (env1 == null && env2 == null)
                    return 0;
                if (env1 == null)
                    return -1;
                if (env2 == null)
                    return 1;

                return env1.EnvelopeNumber.CompareTo(env2.EnvelopeNumber);
            }

            ContestantEnvelopes.Sort(EnvelopeNumberComparer);
            HostEnvelopes.Sort(EnvelopeNumberComparer);

            EventRefreshEnvelopes?.Invoke(this, EventArgs.Empty);
        }

        public void AddContestantEnvelope(Envelope newEnvelope)
        {
            if (newEnvelope == null)
                throw new ArgumentNullException(nameof(newEnvelope));

            ContestantEnvelopes.Add(newEnvelope);

            EventRefreshEnvelopes?.Invoke(this, EventArgs.Empty);
        }

        public void ClearEnvelopeToPlayFor()
        {
            EnvelopePlayedFor = null;

            EventRefreshEnvelopes?.Invoke(this, EventArgs.Empty);
        }

        public void SelectEnvelopeToPlayFor(Envelope envelope)
        {
            if (envelope == null)
                throw new ArgumentNullException(nameof(envelope));

            EnvelopePlayedFor = envelope;
            EnvelopePlayedFor.State = EnvelopeState.PLAYING_FOR;

            EventRefreshEnvelopes?.Invoke(this, EventArgs.Empty);
        }

        public Envelope? GetContestantEnvelope(int index)
        {
            if (index < 0 || index >= Constants.MAX_ENVELOPE_POSSIBLE_COUNT)
                throw new ArgumentOutOfRangeException(nameof(index), $"Index must be between 0 and {Constants.MAX_ENVELOPE_POSSIBLE_COUNT - 1}.");

            if (index >= ContestantEnvelopes.Count)
                return null;
            else
                return ContestantEnvelopes[index];
        }

        public Envelope? GetHostEnvelope(int index)
        {
            if (index < 0 || index >= Constants.MAX_ENVELOPE_POSSIBLE_COUNT)
                throw new ArgumentOutOfRangeException(nameof(index), $"Index must be between 0 and {Constants.MAX_ENVELOPE_POSSIBLE_COUNT - 1}.");

            if (index >= HostEnvelopes.Count)
                return null;
            else
                return HostEnvelopes[index];
        }

        public Envelope? GetEnvelopeFromTag(string tag)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));
            if (tag.Length != 2)
                throw new ArgumentException($"The tag must be 2 characters. First 'C' or 'H' (contestant/host) and the other: the envelope index.", nameof(tag));

            char playerTag = char.ToUpper(tag[0]);
            int envelopeIndex = (int)char.GetNumericValue(tag[1]);
            if (envelopeIndex < 0 || envelopeIndex >= Constants.MAX_ENVELOPE_POSSIBLE_COUNT)
                throw new ArgumentException($"The second character (envelope index) must be a digit between 0 and {Constants.MAX_ENVELOPE_POSSIBLE_COUNT - 1}.", nameof(tag));

            return playerTag switch
            {
                'C' => GetContestantEnvelope(envelopeIndex),
                'H' => GetHostEnvelope(envelopeIndex),
                _ => null
            };
        }

        public void KeepOrDestroyBasedOnAnswer()
        {
            if (EnvelopePlayedFor is null)
                throw new InvalidOperationException($"EnvelopePlayedFor is null");

            EnvelopePlayedFor.State = IsAnswerCorrect() ? EnvelopeState.WON : EnvelopeState.DESTROYED;

            EventRefreshEnvelopes?.Invoke(this, EventArgs.Empty);
        }

        public void RemoveDestroyedEnvelopes()
        {
            ContestantEnvelopes.RemoveAll(envelope => envelope.State == EnvelopeState.DESTROYED);

            EventRefreshEnvelopes?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Cash And Offers

        public void SetContestantCash(decimal newCash)
        {
            if (newCash < 0)
                throw new ArgumentOutOfRangeException(nameof(newCash), $"Contestant's cash mustn't be negative.");

            ContestantCash = newCash;
        }

        public void SetCashOffer(decimal newOffer)
        {
            if (ContestantCash + newOffer < 0)
                throw new ArgumentOutOfRangeException(nameof(newOffer), $"Offer cannot make the contestant's cash go negative.");

            CashOffer = newOffer;
        }

        #endregion

        #region Questions

        public void ShowQuestion()
        {
            EventShowQuestion?.Invoke(this, EventArgs.Empty);
        }

        public void ShowPossibleAnswers()
        {
            EventShowAnswers?.Invoke(this, EventArgs.Empty);
        }

        public void ShowCorrectAnswer()
        {
            EventCorrectAnswerShown?.Invoke(this, EventArgs.Empty);
        }

        public void CancelQuestion()
        {
            if (EnvelopePlayedFor is not null)
                EnvelopePlayedFor.State = EnvelopeState.NEUTRAL;

            QuestionIndex--;
            ClearAnswer();
            ClearEnvelopeToPlayFor();
        }

        public Question GetCurrentQuestion()
        {
            if (QuestionIndex < 0 || QuestionIndex >= QuestionSet.QuestionCount)
                throw new IndexOutOfRangeException($"Question index should be in valid range.");

            return QuestionSet.QuestionList[QuestionIndex];
        }

        public void ClearAnswer()
        {
            ContestantAnswer = null;

            EventAnswerSelected?.Invoke(this, EventArgs.Empty);
        }

        public void SelectAnswer(int answer)
        {
            if (answer < Constants.MIN_ANSWER_NUMBER || answer > Constants.MAX_ANSWER_NUMBER)
                throw new ArgumentOutOfRangeException(nameof(answer), $"The answer number must be between {Constants.MIN_ANSWER_NUMBER} and {Constants.MAX_ANSWER_NUMBER}!");

            ContestantAnswer = answer;

            EventAnswerSelected?.Invoke(this, EventArgs.Empty);
        }

        public bool IsAnswerCorrect()
        {
            if (ContestantAnswer is null)
                throw new InvalidOperationException();

            var currentQuestion = GetCurrentQuestion();
            return currentQuestion.CorrectAnswerNumber == ContestantAnswer;
        }
        #endregion

        #region Trading

        public void StartTrading()
        {
            RemoveDestroyedEnvelopes();

            ContestantEnvelopes.ForEach(envelope => envelope.State = EnvelopeState.NEUTRAL);

            EventStartTrading?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Game Over
        public decimal CalculateFinalPrize()
        {
            decimal cash = ContestantCash;
            decimal multiplier = 1;

            IEnumerable<BaseCheque> cheques = ContestantEnvelopes.Select(envelope => envelope.Cheque);
            foreach (var cheque in cheques)
            {
                switch (cheque)
                {
                    case CashCheque cashCheque:
                        cash += cashCheque.CashAmount;
                        break;
                    case PercentageCheque percentageCheque:
                        multiplier *= percentageCheque.CashMultiplier;
                        break;
                    default:
                        throw new NotImplementedException($"Not implemented cheque type!");
                }
            }

            decimal prize = cash * multiplier;
            return Math.Round(prize, GameSettings.DecimalPlaces, MidpointRounding.AwayFromZero);
        }

        #endregion
    }
}
