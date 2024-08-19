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

        public decimal Cash
        {
            get;
            private set;
        }

        public decimal CashOffer
        {
            get;
            private set;
        }

        public int QuestionNumber
        {
            get;
            private set;
        }

        #endregion

        #region Constructor
        private GameState()
        {
            GameSettings = GameSettings.Create();
            QuestionSet = QuestionSet.Create();
            ContestantEnvelopes = new List<Envelope>(Constants.HOW_MUCH_ENVELOPES_TO_PICK);
            HostEnvelopes = new List<Envelope>(Constants.HOW_MUCH_ENVELOPES_TO_PICK);

            ResetGame();
        }
        #endregion

        #region Reset
        public void ResetGame()
        {
            ContestantEnvelopes.Clear();
            HostEnvelopes.Clear();

            QuestionNumber = -1;
            Cash = 0;
            CashOffer = 0;
        }
        #endregion

        #region Loading
        public void LoadGameSettings(GameSettings settings)
        {
            ArgumentNullException.ThrowIfNull(settings, nameof(settings));
            GameSettings = settings;
        }

        public void LoadQuestionSet(QuestionSet questionSet)
        {
            ArgumentNullException.ThrowIfNull(questionSet, nameof(questionSet));
            QuestionSet = questionSet;
        }
        #endregion

        #region Envelopes

        public Envelope? GetContestantEnvelope(int index)
        {
            if (index < 0 || index >= Constants.HOW_MUCH_ENVELOPES_TO_PICK)
                throw new ArgumentOutOfRangeException(nameof(index), $"Index must be between 0 and {Constants.HOW_MUCH_ENVELOPES_TO_PICK - 1}.");

            if (index >= ContestantEnvelopes.Count)
                return null;
            else
                return ContestantEnvelopes[index];
        }

        public Envelope? GetHostEnvelope(int index)
        {
            if (index < 0 || index >= Constants.HOW_MUCH_ENVELOPES_TO_PICK)
                throw new ArgumentOutOfRangeException(nameof(index), $"Index must be between 0 and {Constants.HOW_MUCH_ENVELOPES_TO_PICK - 1}.");

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
            if (envelopeIndex < 0 || envelopeIndex >= Constants.HOW_MUCH_ENVELOPES_TO_PICK)
                throw new ArgumentException($"The second character (envelope index) must be a digit between 0 and {Constants.HOW_MUCH_ENVELOPES_TO_PICK - 1}.", nameof(tag));

            return playerTag switch
            {
                'C' => GetContestantEnvelope(envelopeIndex),
                'H' => GetHostEnvelope(envelopeIndex),
                _ => null
            };
        }
        #endregion

        #region Cash And Offers

        public void SetContestantCash(decimal newCash)
        {
            if (newCash < 0)
                throw new ArgumentOutOfRangeException(nameof(newCash), $"Contestant's cash mustn't be negative.");

            Cash = newCash;
        }

        public void SetCashOffer(decimal newOffer)
        {
            if (Cash + newOffer < 0)
                throw new ArgumentOutOfRangeException(nameof(newOffer), $"Offer cannot make the contestant's cash go negative.");

            CashOffer = newOffer;
        }

        #endregion
    }
}
