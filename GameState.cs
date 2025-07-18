﻿using CluelessControl.Cheques;
using CluelessControl.Constants;
using CluelessControl.Envelopes;
using CluelessControl.Questions;

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

        public EnvelopeSet ContestantEnvelopeSet
        {
            get;
            private set;
        }

        public EnvelopeSet HostEnvelopeSet
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

        public string FinalPrize
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
        public event EventHandler<string>? EventShowContestantName;
        public event EventHandler? EventHideContestantName;

        public event EventHandler? EventClearEverything;
        public event EventHandler? EventShowEnvelopesStart;
        public event EventHandler? EventHideEnvelopesStart;
        public event EventHandler? EventClearQuestion;
        public event EventHandler? EventShowQuestion;
        public event EventHandler? EventShowEnvelopesBeforeQuestion;
        public event EventHandler? EventShowAnswers;
        public event EventHandler? EventAnswerSelected;
        public event EventHandler? EventCorrectAnswerShown;
        public event EventHandler? EventShowEnvelopesAfterQuestion;
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

            ContestantEnvelopeSet = EnvelopeSet.Create();
            HostEnvelopeSet = EnvelopeSet.Create();
            EnvelopePlayedFor = null;

            ContestantAnswer = null;
            QuestionIndex = -1;
            ContestantCash = 0;
            CashOffer = 0;
            FinalPrize = string.Empty;
        }
        #endregion

        #region Name
        public void ShowContestantName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            EventShowContestantName?.Invoke(this, name.Trim());
        }

        public void HideContestantName()
        {
            EventHideContestantName?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Clearing

        public void ClearEverything()
        {
            EventClearEverything?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Moving The Game
        public void PrepareNewGame()
        {
            ContestantEnvelopeSet.ClearEnvelopeList();
            HostEnvelopeSet.ClearEnvelopeList();

            EnvelopeTable = EnvelopeTable.Create(ChequeSettings);

            EnvelopePlayedFor = null;
            ContestantAnswer = null;
            QuestionIndex = -1;
            ContestantCash = 0;
            CashOffer = 0;
            FinalPrize = string.Empty;

            ClearEverything();
        }

        public void NextQuestion()
        {
            EnvelopePlayedFor = null;
            ContestantAnswer = null;
            QuestionIndex++;

            EventClearQuestion?.Invoke(this, EventArgs.Empty);
        }

        public void HideEnvelopesAfterSelection()
        {
            EventHideEnvelopesStart?.Invoke(this, EventArgs.Empty);
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

        public void ShowEnvelopesAtStart()
        {
            EventShowEnvelopesStart?.Invoke(this, EventArgs.Empty);
        }

        public void RefreshEnvelopes()
        {
            EventRefreshEnvelopes?.Invoke(this, EventArgs.Empty);
        }

        public void SortEnvelopesByNumber()
        {
            ContestantEnvelopeSet.SortByEnvelopeNumbers();
            HostEnvelopeSet.SortByEnvelopeNumbers();

            EventRefreshEnvelopes?.Invoke(this, EventArgs.Empty);
        }

        public void AddContestantEnvelope(Envelope envelope)
        {
            if (envelope == null)
                throw new ArgumentNullException(nameof(envelope));

            ContestantEnvelopeSet.AddEnvelope(envelope);

            EventRefreshEnvelopes?.Invoke(this, EventArgs.Empty);
        }
        
        public void RemoveContestantEnvelope(Envelope envelope)
        {
            if (envelope == null)
                throw new ArgumentNullException(nameof(envelope));

            ContestantEnvelopeSet.RemoveEnvelope(envelope);

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
            EnvelopePlayedFor.MarkAsPlayingFor();

            EventRefreshEnvelopes?.Invoke(this, EventArgs.Empty);
        }

        public Envelope? GetContestantEnvelope(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), "The envelope index must be positive!");

            if (index >= ContestantEnvelopeSet.EnvelopeCount)
                return null;
            else
                return ContestantEnvelopeSet.GetEnvelope(index);
        }

        public Envelope? GetHostEnvelope(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), "The envelope index must be positive!");

            if (index >= HostEnvelopeSet.EnvelopeCount)
                return null;
            else
                return HostEnvelopeSet.GetEnvelope(index);
        }

        public Envelope? GetEnvelopeFromTag(string tag)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));

            string[] tagParts = tag.ToUpper().Split('-');
            if (tagParts.Length != 2)
                throw new ArgumentException("The tag must be 2 parts. First 'C' or 'H' (contestant/host), a dash, and the envelope index. Example: \"C-0\".", nameof(tag));

            string playerTag = tagParts[0];
            int envelopeIndex = int.Parse(tagParts[1]);
            if (envelopeIndex < 0 || envelopeIndex >= GameConstants.MAX_ENVELOPE_COUNT_PERSON)
                throw new ArgumentException($"The second character (envelope index) must be a digit between 0 and {GameConstants.MAX_ENVELOPE_COUNT_PERSON - 1}.", nameof(tag));

            return playerTag switch
            {
                "C" => GetContestantEnvelope(envelopeIndex),
                "H" => GetHostEnvelope(envelopeIndex),
                _ => throw new ArgumentException("Player tag is neither 'C' nor 'H'!")
            };
        }

        public void KeepOrDestroyBasedOnAnswer()
        {
            if (EnvelopePlayedFor is null)
                throw new InvalidOperationException($"EnvelopePlayedFor is null");

            if (IsAnswerCorrect())
                EnvelopePlayedFor.MarkAsWon();
            else
                EnvelopePlayedFor.MarkAsDestroyed();

            EventRefreshEnvelopes?.Invoke(this, EventArgs.Empty);
        }

        public void MarkWinOrLose()
        {
            if (EnvelopePlayedFor is null)
                throw new InvalidOperationException($"EnvelopePlayedFor is null");

            if (!IsAnswerCorrect())
                EnvelopePlayedFor.MarkAsForDestruction();

            EventRefreshEnvelopes?.Invoke(this, EventArgs.Empty);
        }

        public void ClearNotSelectedMarkings()
        {
            EnvelopeTable.ForSelected(
                action: envelope => envelope.MarkAsNeutral(),
                predicate: envelope => envelope.State == EnvelopeState.NOT_SELECTED);

            EventRefreshEnvelopes?.Invoke(this, EventArgs.Empty);
        }

        public void MarkNotSelectedEnvelopesAtStart()
        {
            EnvelopeTable.ForSelected(
                action: envelope => envelope.MarkAsNotSelected(),
                predicate: envelope => envelope.State != EnvelopeState.SELECTED_AT_START);

            EventRefreshEnvelopes?.Invoke(this, EventArgs.Empty);
        }

        public void UnmarkSelectionAtStart()
        {
            EnvelopeTable.ForSelected(
                action: envelope => envelope.MarkAsNeutral(),
                predicate: envelope => envelope.State == EnvelopeState.SELECTED_AT_START);

            EventRefreshEnvelopes?.Invoke(this, EventArgs.Empty);
        }

        public void RemoveEnvelopesMarkedForDestruction()
        {
            List<Envelope> contestantEnvelopes = ContestantEnvelopeSet.Envelopes
                .Where(envelope => envelope.State == EnvelopeState.FOR_DESTRUCTION)
                .ToList();

            List<Envelope> hostEnvelopes = HostEnvelopeSet.Envelopes
                .Where(envelope => envelope.State == EnvelopeState.FOR_DESTRUCTION)
                .ToList();

            contestantEnvelopes.ForEach(envelope => envelope.MarkAsDestroyed());
            hostEnvelopes.ForEach(envelope => envelope.MarkAsDestroyed());

            RemoveDestroyedEnvelopes();
        }

        public void RemoveDestroyedEnvelopes()
        {
            ContestantEnvelopeSet.RemoveDestroyedEnvelopes();
            HostEnvelopeSet.RemoveDestroyedEnvelopes();
            EventRefreshEnvelopes?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Cash And Offers

        public void RefreshOffer()
        {
            EventRefreshOffer?.Invoke(this, EventArgs.Empty);
        }

        public bool CanSetAmountAsOffer(decimal newOffer)
        {
            return ContestantCash + newOffer >= 0;
        }

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

        public bool CanSetEnvelopesAsOffer()
        {
            int tradedContestantEnvCount = ContestantEnvelopeSet.CountEnvelopes(env => env.State == EnvelopeState.MARKED_FOR_TRADE);
            int tradedHostEnvCount = HostEnvelopeSet.CountEnvelopes(env => env.State == EnvelopeState.MARKED_FOR_TRADE);
            
            int contestantEnvelopeCount = ContestantEnvelopeSet.EnvelopeCount - tradedContestantEnvCount + tradedHostEnvCount;
            int hostEnvelopeCount = HostEnvelopeSet.EnvelopeCount - tradedHostEnvCount + tradedContestantEnvCount;

            return Math.Max(contestantEnvelopeCount, hostEnvelopeCount) <= Constants.GameConstants.MAX_ENVELOPE_COUNT_PERSON;
        }

        #endregion

        #region Questions

        public void ShowQuestion()
        {
            EventShowQuestion?.Invoke(this, EventArgs.Empty);
        }

        public void ShowEnvelopesBeforeQuestion()
        {
            EventShowEnvelopesBeforeQuestion?.Invoke(this, EventArgs.Empty);
        }

        public void ShowEnvelopesAfterQuestion()
        {
            EventShowEnvelopesAfterQuestion?.Invoke(this, EventArgs.Empty);
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
            EnvelopePlayedFor?.MarkAsNeutral();

            // 2, because the NextQuestion function will increment it, and the 'Next Question' button, will increment it again
            QuestionIndex -= 2;
            NextQuestion();
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
            if (answer < GameConstants.MIN_ANSWER_NUMBER || answer > GameConstants.MAX_ANSWER_NUMBER)
                throw new ArgumentOutOfRangeException(nameof(answer), $"The answer number must be between {GameConstants.MIN_ANSWER_NUMBER} and {GameConstants.MAX_ANSWER_NUMBER}!");

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
            ContestantEnvelopeSet.MarkNotDestroyedAsNeutral();
            HostEnvelopeSet.ClearEnvelopeList();

            EventRefreshEnvelopes?.Invoke(this, EventArgs.Empty);
            EventStartTrading?.Invoke(this, EventArgs.Empty);
        }

        public void AcceptOffer()
        {
            ContestantCash += CashOffer;
            CashOffer = 0;

            IEnumerable<Envelope> contestantTrades = ContestantEnvelopeSet.SelectTradedEnvelopes();
            IEnumerable<Envelope> hostTrades = HostEnvelopeSet.SelectTradedEnvelopes();

            ContestantEnvelopeSet.TransferEnvelopesTo(HostEnvelopeSet, contestantTrades);
            HostEnvelopeSet.TransferEnvelopesTo(ContestantEnvelopeSet, hostTrades);

            ContestantEnvelopeSet.MarkNotDestroyedAsNeutral();
            HostEnvelopeSet.MarkNotDestroyedAsNeutral();

            ContestantEnvelopeSet.SortByEnvelopeNumbers();
            HostEnvelopeSet.SortByEnvelopeNumbers();

            EventRefreshOffer?.Invoke(this, EventArgs.Empty);
        }

        public void ClearOffer()
        {
            ContestantEnvelopeSet.MarkNotDestroyedAsNeutral();
            HostEnvelopeSet.MarkNotDestroyedAsNeutral();
            CashOffer = 0;

            EventRefreshOffer?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Game Over

        public void GameOver()
        {
            ContestantEnvelopeSet.RemoveDestroyedEnvelopes();
            ContestantEnvelopeSet.MarkNotDestroyedAsNeutral();
            ContestantEnvelopeSet.OpenAll();

            HostEnvelopeSet.RemoveDestroyedEnvelopes();
            HostEnvelopeSet.MarkNotDestroyedAsNeutral();
            HostEnvelopeSet.OpenAll();

            decimal finalPrize = EnvelopeCalculator.CalculateFinalPrize(GameSettings, ContestantEnvelopeSet.Envelopes, ContestantCash);
            FinalPrize = Utils.AmountToString(finalPrize);

            EventRefreshEnvelopes?.Invoke(this, EventArgs.Empty);
            EventGameOver?.Invoke(this, EventArgs.Empty);
        }

        public void RefreshGameOver(string newFinalPrize)
        {
            if (string.IsNullOrWhiteSpace(newFinalPrize))
                throw new ArgumentNullException(nameof(newFinalPrize));

            FinalPrize = newFinalPrize.Trim().ToUpper();

            EventGameOver?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
