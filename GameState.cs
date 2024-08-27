﻿namespace CluelessControl
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

        #region Events
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

            QuestionNumber = -1;
            Cash = 0;
            CashOffer = 0;
        }
        #endregion

        #region New Game
        public void NewGame()
        {
            ContestantEnvelopes.Clear();
            HostEnvelopes.Clear();

            EnvelopeTable = EnvelopeTable.Create(ChequeSettings);

            EnvelopePlayedFor = null;
            QuestionNumber = -1;
            Cash = 0;
            CashOffer = 0;
        }

        public void NextQuestion()
        {
            QuestionNumber++;
            EnvelopePlayedFor = null;
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
