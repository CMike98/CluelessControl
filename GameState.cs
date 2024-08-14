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
        #endregion

        #region Constructor
        private GameState()
        {
            Settings = new GameSettings();
            QuestionSet = new QuestionSet();
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
    }
}
