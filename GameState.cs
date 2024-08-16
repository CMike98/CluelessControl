using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public GameSettings Settings
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
        #endregion

        #region Constructor
        private GameState()
        {
            Settings = new GameSettings();
            QuestionSet = QuestionSet.Create();
            ContestantEnvelopes = [];
            HostEnvelopes = [];
        }
        #endregion

        #region Loading
        public void LoadGameSettings(GameSettings settings)
        {
            ArgumentNullException.ThrowIfNull(settings, nameof(settings));
            Settings = settings;
        }

        public void LoadQuestionSet(QuestionSet questionSet)
        {
            ArgumentNullException.ThrowIfNull(questionSet, nameof(questionSet));
            QuestionSet = questionSet;
        }
        #endregion

        #region Envelopes
        public void AddEnvelopeToContestant(Envelope envelope)
        {
            if (envelope == null)
                throw new ArgumentNullException(nameof(envelope));

            ContestantEnvelopes.Add(envelope);
        }
        #endregion
    }
}
