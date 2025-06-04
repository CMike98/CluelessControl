using CluelessControl.Cheques;
using CluelessControl.Constants;
using CluelessControl.EnvelopeColorStates;
using CluelessControl.Envelopes;
using CluelessControl.Questions;
using CluelessControl.Sounds;

namespace CluelessControl
{
    public partial class DirectorForm : Form
    {
        private const int NO_ITEM_INDEX = -1;

        #region Sound
        private const string QUEUE_NAME_INTRO_MUSIC = "intro-music";
        private const string QUEUE_NAME_WELCOME_CONTESTANT = "welcome-contestant";
        private const string QUEUE_NAME_GAME_START = "game-start";
        private const string QUEUE_NAME_ENVELOPE_SELECTION = "envelope-selection";
        private const string QUEUE_NAME_MOVE_TO_QUESTION_GAME = "move-to-question-game-start";
        private const string QUEUE_NAME_QUESTION_GAME = "question-game";
        private const string QUEUE_NAME_ANSWER_REACTION = "answer-reaction";
        private const string QUEUE_NAME_TRADING_BACKGROUND = "trading-background";
        private const string QUEUE_NAME_GAME_OVER = "game-over";
        private const string QUEUE_NAME_OUTRO_MUSIC = "outro-music";

        private float _volumeLevel = 1;
        private readonly SoundManager _soundManager = new();
        #endregion

        #region Envelope Settings Screen Variables
        private bool _envelopeSettingsEdited = false;
        private bool _envelopeSettingsSkipIndexChange = false;
        private int _envelopeSettingsLastSelectedIndex = NO_ITEM_INDEX;
        #endregion

        #region Question Editor Screen Variables
        private bool _questionEditorEdited = false;
        private bool _questionEditorSkipIndexChange = false;
        private int _questionEditorLastSelectedIndex = NO_ITEM_INDEX;
        #endregion

        #region Envelope Selection Screen

        private record EnvelopeControlsPair
        {
            public required TextBox EnvelopeTextBox
            {
                get;
                init;
            }

            public required Label EnvelopeLabel
            {
                get;
                init;
            }
        }

        private readonly EnvelopeControlsPair[] _envelopeControlPairs = new EnvelopeControlsPair[GameConstants.MAX_ENVELOPE_COUNT_PERSON];
        #endregion

        #region Question Game Screen
        private readonly Dictionary<int, Label> _questionGameAnswerLabels;
        private int _questionGameEnvelopeIndex;
        #endregion

        #region Trading Screen
        private const int TRADING_SCREEN_MAX_ON_PAGE = 5;

        // Contestant
        private int _tradingContestantPage = 0;
        private bool _tradingAreContestantCheckboxesChanging = false;
        private readonly Label[] _tradingContestantEnvelopeLabels = new Label[TRADING_SCREEN_MAX_ON_PAGE];
        private readonly CheckBox[] _tradingContestantCheckboxes = new CheckBox[TRADING_SCREEN_MAX_ON_PAGE];
        private int TradingContestantMaxPages
        {
            get
            {
                int envelopeCount = GameState.Instance.ContestantEnvelopeSet.EnvelopeCount;
                return (envelopeCount / TRADING_SCREEN_MAX_ON_PAGE) + (envelopeCount % TRADING_SCREEN_MAX_ON_PAGE == 0 ? 0 : 1);
            }
        }
        private int TradingContestantMaxPageIndex => Math.Max(TradingContestantMaxPages - 1, 0);

        // Host
        private int _tradingHostPage = 0;
        private bool _tradingAreHostCheckboxesChanging = false;
        private readonly Label[] _tradingHostEnvelopeLabels = new Label[TRADING_SCREEN_MAX_ON_PAGE];
        private readonly CheckBox[] _tradingHostCheckboxes = new CheckBox[TRADING_SCREEN_MAX_ON_PAGE];
        private int TradingHostMaxPages
        {
            get
            {
                int envelopeCount = GameState.Instance.HostEnvelopeSet.EnvelopeCount;
                return (envelopeCount / TRADING_SCREEN_MAX_ON_PAGE) + (envelopeCount % TRADING_SCREEN_MAX_ON_PAGE == 0 ? 0 : 1);
            }
        }
        private int TradingHostMaxPageIndex => Math.Max(TradingHostMaxPages - 1, 0);
        #endregion

        private bool EditedBeforeSave => _envelopeSettingsEdited || _questionEditorEdited || EnvelopeSettingsDidChequeChange() || QuestionEditorDidQuestionChange();

        public DirectorForm()
        {
            InitializeComponent();

            _questionGameAnswerLabels = new()
            {
                { 1, QuestionGameAns1Lbl },
                { 2, QuestionGameAns2Lbl },
                { 3, QuestionGameAns3Lbl },
                { 4, QuestionGameAns4Lbl },
            };
        }

        private void DirectorForm_Load(object sender, EventArgs e)
        {
            PrepareEnvelopeSelectionBoxes();
            PrepareTradingEnvelopeBoxes();
            PreShowPrepare();
            ShowAllForms();

            SettingsRoundingMethodComboBox.SelectedIndex = 0;
        }

        private static void ShowAllForms()
        {
            Program.HostScreenForm.Show();
            Program.ContestantScreenForm.Show();
            Program.TVScreenForm.Show();
        }

        private void PrepareEnvelopeSelectionBoxes()
        {
            for (int i = 0; i < GameConstants.MAX_ENVELOPE_COUNT_PERSON; ++i)
            {
                string numberName = string.Format("EnvelopeSelectionNum{0}TxtBox", i);
                string valueName = string.Format("EnvelopeSelectionContent{0}Lbl", i);

                TextBox numberControl = (TextBox?)Controls.Find(numberName, searchAllChildren: true).First() ?? throw new MissingMemberException(this.Name, numberName);
                Label valueControl = (Label?)Controls.Find(valueName, searchAllChildren: true).First() ?? throw new MissingMemberException(this.Name, valueName);

                _envelopeControlPairs[i] = new EnvelopeControlsPair()
                {
                    EnvelopeTextBox = numberControl,
                    EnvelopeLabel = valueControl
                };
            }
        }

        private void PrepareTradingEnvelopeBoxes()
        {
            for (int i = 0; i < TRADING_SCREEN_MAX_ON_PAGE; ++i)
            {
                string contestantEnvelopeName = string.Format("TradingContestantEnvelope{0}Lbl", i);
                string hostEnvelopeName = string.Format("TradingHostEnvelope{0}Lbl", i);
                string contestantCheckBoxName = string.Format("TradingContestantEnvelope{0}ChkBox", i);
                string hostCheckBoxName = string.Format("TradingHostEnvelope{0}ChkBox", i);

                Label contestantControl = (Label)Controls.Find(contestantEnvelopeName, searchAllChildren: true).First() ?? throw new MissingMemberException(this.Name, contestantEnvelopeName);
                Label hostControl = (Label)Controls.Find(hostEnvelopeName, searchAllChildren: true).First() ?? throw new MissingMemberException(this.Name, hostEnvelopeName);

                CheckBox contestantCheckBox = (CheckBox)Controls.Find(contestantCheckBoxName, searchAllChildren: true).First() ?? throw new MissingMemberException(this.Name, contestantCheckBoxName);
                CheckBox hostCheckBox = (CheckBox)Controls.Find(hostCheckBoxName, searchAllChildren: true).First() ?? throw new MissingMemberException(this.Name, hostCheckBoxName);
                contestantCheckBox.Tag = i;
                hostCheckBox.Tag = i;

                _tradingContestantEnvelopeLabels[i] = contestantControl;
                _tradingHostEnvelopeLabels[i] = hostControl;

                _tradingContestantCheckboxes[i] = contestantCheckBox;
                _tradingHostCheckboxes[i] = hostCheckBox;
            }
        }

        private void DirectorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (EditedBeforeSave)
            {
                var closingResult = MessageBox.Show(
                    text: "Masz niezapisane zmiany! Jeżeli wyjdziesz, one przepadną! Na pewno chcesz wyjść?",
                    GameConstants.PROGRAM_TITLE,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (closingResult == DialogResult.No)
                    e.Cancel = true;
            }
        }

        #region Sound Methods

        private void VolumeTrackBar_Scroll(object sender, EventArgs e)
        {
            if (MuteCheckBox.Checked)
                return;

            _volumeLevel = VolumeTrackBar.Value / 100.0f;
            _soundManager.SetVolume(_volumeLevel);
            VolumeLabel.Text = string.Format("Głośność: {0:##0%}", _volumeLevel);
        }

        private void MuteCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (MuteCheckBox.Checked)
            {
                _volumeLevel = 0;
            }
            else
            {
                _volumeLevel = VolumeTrackBar.Value / 100.0f;
            }

            _soundManager.SetVolume(_volumeLevel);
            VolumeLabel.Text = string.Format("Głośność: {0:##0%}", _volumeLevel);
        }

        private void PlaySingleSound(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File has not been found.", filePath);

            var sound = new Sound(filePath, _volumeLevel);
            _soundManager.PlaySingleSound(sound);
        }

        #endregion

        #region Messages
        public void ShowErrorMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            MessageBox.Show(message, GameConstants.PROGRAM_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowOkMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            MessageBox.Show(message, GameConstants.PROGRAM_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region PreShow

        private void PreShowPrepare()
        {
            var gameStateInstance = GameState.Instance;

            // Clear player name
            PreShowNameTxtBox.Clear();

            // Clear cheque list
            gameStateInstance.ChequeSettings.ClearChequeList();
            EnvelopeSettingsUpdateAll();

            // Clear the last selected envelope index
            _envelopeSettingsLastSelectedIndex = NO_ITEM_INDEX;

            // Clear the envelope settings list box editor
            // We have to call SelectedIndexChanged manually after setting it to -1
            EnvelopeSettingsListBox.SelectedIndex = NO_ITEM_INDEX;
            EnvelopeSettingsListBox_SelectedIndexChanged(this, EventArgs.Empty);

            // Clear question set
            gameStateInstance.QuestionSet.ClearQuestionSet();
            QuestionEditorUpdateAll();

            // Clear the last selected question index
            _questionEditorLastSelectedIndex = NO_ITEM_INDEX;

            // Clear the question editor list box
            // We have to call SelectedIndexChanged manually after setting it to -1
            QuestionEditorListBox.SelectedIndex = NO_ITEM_INDEX;
            QuestionEditorListBox_SelectedIndexChanged(this, EventArgs.Empty);
        }

        private void PreShowIntroBtn_Click(object sender, EventArgs e)
        {
            // Stop all music
            _soundManager.StopAllQueues();

            var introMusic = new Sound("snd/intro.wav", _volumeLevel);
            var afterIntroLoop = new Sound("snd/after-intro-loop.wav", _volumeLevel);
            afterIntroLoop.SetInfiniteLoop();

            _soundManager.CreateQueue(queueName: QUEUE_NAME_INTRO_MUSIC);
            _soundManager.AddSoundToQueue(queueName: QUEUE_NAME_INTRO_MUSIC, sound: introMusic);
            _soundManager.AddSoundToQueue(queueName: QUEUE_NAME_INTRO_MUSIC, sound: afterIntroLoop);
            _soundManager.PlayQueue(queueName: QUEUE_NAME_INTRO_MUSIC);

            GameState.Instance.HideContestantName();
        }

        private void PreShowWelcomeBtn_Click(object sender, EventArgs e)
        {
            // Stop music
            _soundManager.StopAllQueues();

            // Play music
            var welcomeSound = new Sound("snd/welcome-contestant.wav", _volumeLevel);
            var loopSound = new Sound("snd/after-intro-loop.wav", _volumeLevel);
            loopSound.SetInfiniteLoop();

            _soundManager.CreateQueue(queueName: QUEUE_NAME_WELCOME_CONTESTANT);
            _soundManager.AddSoundToQueue(queueName: QUEUE_NAME_WELCOME_CONTESTANT, welcomeSound);
            _soundManager.AddSoundToQueue(queueName: QUEUE_NAME_WELCOME_CONTESTANT, loopSound);
            _soundManager.PlayQueue(queueName: QUEUE_NAME_WELCOME_CONTESTANT);

            // Get name and display
            string name = PreShowNameTxtBox.Text.Trim();
            if (!string.IsNullOrWhiteSpace(name))
            {
                GameState.Instance.ShowContestantName(name);
            }
        }

        private void PreShowStartGameBtn_Click(object sender, EventArgs e)
        {
            var gameState = GameState.Instance;
            if (gameState.ChequeSettings.ChequeList.Count < GameConstants.MAX_ENVELOPES_COUNT || gameState.QuestionSet.QuestionList.Count < gameState.GameSettings.StartEnvelopeCount)
            {
                string message = string.Format(
                    "Musisz najpierw wczytać koperty (min. {0} kopert) i zestaw pytań (min. {1} pytań)!",
                    GameConstants.MAX_ENVELOPES_COUNT,
                    gameState.GameSettings.StartEnvelopeCount);

                ShowErrorMessage(message);
                return;
            }

            gameState.PrepareNewGame();

            EnvelopeSelectionUnlockButtons();
            DirectorTabControl.SelectTab("GamePickEnvelopesTab");

            // Play sounds
            EnvelopeSelectionStartMusic();
        }

        private void PreShowGameSettingsBtn_Click(object sender, EventArgs e)
        {
            DirectorTabControl.SelectTab("GameSettingsTab");
        }

        private void PreShowEnvelopeSettingsBtn_Click(object sender, EventArgs e)
        {
            DirectorTabControl.SelectTab("GameSettingsEnvelopesTab");
        }

        private void PreShowQuestionEditorBtn_Click(object sender, EventArgs e)
        {
            DirectorTabControl.SelectTab("QuestionEditorTab");
        }

        #endregion

        #region Game Settings

        public bool SettingsTryCreatingSettings(out GameSettings? result)
        {
            try
            {
                int startEnvelopeCount = (int)SettingsEnvelopeStartCountNumeric.Value;
                int decimalPlaces = int.Parse(SettingsDecimalPlacesTxtBox.Text);

                bool onlyWorstMinusCounts;
                if (SettingsMinusPercentWorstRadio.Checked)
                    onlyWorstMinusCounts = true;
                else if (SettingsMinusPercentAllRadio.Checked)
                    onlyWorstMinusCounts = false;
                else
                    throw new InvalidOperationException("No minus percentage radio checked.");

                bool onlyBestPlusCounts;
                if (SettingsPlusPercentBestRadio.Checked)
                    onlyBestPlusCounts = true;
                else if (SettingsPlusPercentAllRadio.Checked)
                    onlyBestPlusCounts = false;
                else
                    throw new InvalidOperationException("No plus percentage radio checked.");

                bool showAmountsOnTv;
                if (SettingsShowOnTvYesRadio.Checked)
                    showAmountsOnTv = true;
                else if (SettingsShowOnTvNoRadio.Checked)
                    showAmountsOnTv = false;
                else
                    throw new InvalidOperationException("No \"Show on TV\" radio checked.");

                RoundingMethod roundingMethod = (RoundingMethod) SettingsRoundingMethodComboBox.SelectedIndex;

                result = GameSettings.Create(startEnvelopeCount, decimalPlaces, onlyWorstMinusCounts, onlyBestPlusCounts, showAmountsOnTv, roundingMethod);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        private void SettingsUpdateFromMemory()
        {
            var gameSettings = GameState.Instance.GameSettings;

            SettingsEnvelopeStartCountNumeric.Value = gameSettings.StartEnvelopeCount;

            SettingsDecimalPlacesTxtBox.Text = gameSettings.DecimalPlaces.ToString();

            if (gameSettings.OnlyWorstMinusCounts)
                SettingsMinusPercentWorstRadio.Checked = true;
            else
                SettingsMinusPercentAllRadio.Checked = true;

            if (gameSettings.OnlyBestPlusCounts)
                SettingsPlusPercentBestRadio.Checked = true;
            else
                SettingsPlusPercentAllRadio.Checked = true;

            if (gameSettings.ShowAmountsOnTv)
                SettingsShowOnTvYesRadio.Checked = true;
            else
                SettingsShowOnTvNoRadio.Checked = true;

            SettingsRoundingMethodComboBox.SelectedIndex = (int)gameSettings.Rounding;
        }

        private void SettingsDecimalPlacesTxtBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(SettingsDecimalPlacesTxtBox.Text, out _))
            {
                SettingsDecimalPlacesTxtBox.BackColor = Color.White;
                SettingsDecimalPlacesTxtBox.ForeColor = Color.Black;

                SettingsSaveToMemoryBtn.Enabled = true;
                SettingsSaveToFileBtn.Enabled = true;
            }
            else
            {
                SettingsDecimalPlacesTxtBox.BackColor = Color.Red;
                SettingsDecimalPlacesTxtBox.ForeColor = Color.White;

                SettingsSaveToMemoryBtn.Enabled = false;
                SettingsSaveToFileBtn.Enabled = false;
            }
        }

        private void SettingsLoadFromMemoryBtn_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                text: "Jesteś pewny? Niezapisane ustawienia przepadną!",
                GameConstants.PROGRAM_TITLE,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.No)
                return;

            SettingsUpdateFromMemory();
        }

        private void SettingsSaveToMemoryBtn_Click(object sender, EventArgs e)
        {
            if (!SettingsTryCreatingSettings(out GameSettings? temp) || temp == null)
            {
                ShowErrorMessage("Błąd - sprawdź jeszcze raz formularz.");
                return;
            }

            try
            {
                GameState.Instance.LoadGameSettings(temp);

                ShowOkMessage("Ustawienia zapisano pomyślnie.");
            }
            catch (Exception ex)
            {
#if DEBUG
                ShowErrorMessage(ex.ToString());
#else
                ShowErrorMessage("Zapisywanie zakończone niepowodzeniem!");
#endif
            }

        }

        private void SettingsLoadFromFileBtn_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                text: "Jesteś pewny? Niezapisane ustawienia przepadną!",
                GameConstants.PROGRAM_TITLE,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.No)
                return;

            if (SettingsOpen.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                GameState.Instance.LoadGameSettingsFromFile(SettingsOpen.FileName);

                SettingsUpdateFromMemory();
            }
            catch (Exception ex)
            {
#if DEBUG
                ShowErrorMessage(ex.ToString());
#else
                ShowErrorMessage("Zapisywanie zakończone niepowodzeniem!");
#endif
            }
        }

        private void SettingsSaveToFileBtn_Click(object sender, EventArgs e)
        {
            if (!SettingsTryCreatingSettings(out GameSettings? temp) || temp == null)
            {
                ShowErrorMessage("Błąd - sprawdź jeszcze raz formularz.");
                return;
            }

            if (SettingsSave.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                GameState.Instance.LoadGameSettings(temp);

                GameState.Instance.GameSettings.SaveToFile(SettingsSave.FileName);

                ShowOkMessage("Ustawienia zapisano pomyślnie.");
            }
            catch (Exception ex)
            {
#if DEBUG
                ShowErrorMessage(ex.ToString());
#else
                ShowErrorMessage("Zapisywanie zakończone niepowodzeniem!");
#endif
            }
        }

        #endregion

        #region Envelope Settings

        private void EnvelopeSettingsUpdateListBox()
        {
            List<BaseCheque> chequeList = GameState.Instance.ChequeSettings.ChequeList;
            int counter = 1;

            EnvelopeSettingsListBox.Items.Clear();

            foreach (BaseCheque cheque in chequeList)
            {
                EnvelopeSettingsListBox.Items.Add($"{counter}. {cheque.ToValueString()}");
                ++counter;
            }

            EnvelopeSettingsCountLbl.Text = $"Kopert: {chequeList.Count}";
        }

        private void EnvelopeSettingsUpdateButtons()
        {
            int selectedIndex = EnvelopeSettingsListBox.SelectedIndex;
            int itemCount = GameState.Instance.ChequeSettings.ChequeList.Count;
            bool isItemSelected = selectedIndex != NO_ITEM_INDEX;

            EnvelopeSettingsMoveUpBtn.Enabled = selectedIndex > 0;
            EnvelopeSettingsMoveDownBtn.Enabled = isItemSelected && selectedIndex < itemCount - 1;
            EnvelopeSettingsNewBtn.Enabled = true;
            EnvelopeSettingsDeleteBtn.Enabled = isItemSelected;
            EnvelopeSettingsRandomiseBtn.Enabled = itemCount > 1;
            EnvelopeSettingsLoadFromFileBtn.Enabled = true;
            EnvelopeSettingsSaveBtn.Enabled = isItemSelected;
            EnvelopeSettingsSaveToFileBtn.Enabled = itemCount > 0;
        }

        private void EnvelopeSettingsUpdateAll()
        {
            EnvelopeSettingsUpdateListBox();
            EnvelopeSettingsUpdateButtons();
        }

        private BaseCheque EnvelopeSettingsCreateChequeFromRadios()
        {
            BaseCheque createdCheque;
            if (EnvelopeSettingsCashRadio.Checked)
                createdCheque = ChequeFactory.CreateCashCheque(decimal.Parse(EnvelopeSettingsCashTxtBox.Text));
            else if (EnvelopeSettingsPercentageRadio.Checked)
                createdCheque = ChequeFactory.CreatePercentageCheque(decimal.Parse(EnvelopeSettingsPercentageTxtBox.Text));
            else
                throw new NotSupportedException($"Not recognised envelope type selected.");
            return createdCheque;
        }

        private bool EnvelopeSettingsDidChequeChange()
        {
            if (EnvelopeSettingsListBox.SelectedIndex == NO_ITEM_INDEX)
                return false;

            int index = EnvelopeSettingsListBox.SelectedIndex;
            BaseCheque cheque = GameState.Instance.ChequeSettings.ChequeList[index];

            switch (cheque)
            {
                case CashCheque cashCheque:
                    if (!EnvelopeSettingsCashRadio.Checked)
                        return true;
                    return cashCheque.CashAmount != decimal.Parse(EnvelopeSettingsCashTxtBox.Text);
                case PercentageCheque percentageCheque:
                    if (!EnvelopeSettingsPercentageRadio.Checked)
                        return true;
                    return percentageCheque.Percentage != decimal.Parse(EnvelopeSettingsPercentageTxtBox.Text);
                default:
                    throw new NotSupportedException($"Not recognised envelope type selected.");
            }
        }

        private void EnvelopeSettingsCashRadio_CheckedChanged(object sender, EventArgs e)
        {
            EnvelopeSettingsCashTxtBox.Enabled = true;
            EnvelopeSettingsPercentageTxtBox.Enabled = false;

            EnvelopeSettingsCashTxtBox.Text = "0";
            EnvelopeSettingsPercentageTxtBox.Clear();

            _envelopeSettingsEdited = true;
        }

        private void EnvelopeSettingsCashTxtBox_TextChanged(object sender, EventArgs e)
        {
            if (!EnvelopeSettingsCashTxtBox.Enabled)
            {
                EnvelopeSettingsCashTxtBox.BackColor = Color.White;
                EnvelopeSettingsCashTxtBox.ForeColor = Color.Black;
                return;
            }

            if (decimal.TryParse(EnvelopeSettingsCashTxtBox.Text, out _))
            {
                EnvelopeSettingsCashTxtBox.BackColor = Color.White;
                EnvelopeSettingsCashTxtBox.ForeColor = Color.Black;
            }
            else
            {
                EnvelopeSettingsCashTxtBox.BackColor = Color.Red;
                EnvelopeSettingsCashTxtBox.ForeColor = Color.White;
            }

            _envelopeSettingsEdited = true;
        }

        private void EnvelopeSettingsPercentageRadio_CheckedChanged(object sender, EventArgs e)
        {
            EnvelopeSettingsCashTxtBox.Enabled = false;
            EnvelopeSettingsPercentageTxtBox.Enabled = true;

            EnvelopeSettingsCashTxtBox.Clear();
            EnvelopeSettingsPercentageTxtBox.Text = "0";

            _envelopeSettingsEdited = true;
        }

        private void EnvelopeSettingsPercentageTxtBox_TextChanged(object sender, EventArgs e)
        {
            if (!EnvelopeSettingsPercentageTxtBox.Enabled)
            {
                EnvelopeSettingsPercentageTxtBox.BackColor = Color.White;
                EnvelopeSettingsPercentageTxtBox.ForeColor = Color.Black;
                return;
            }

            if (decimal.TryParse(EnvelopeSettingsPercentageTxtBox.Text, out decimal percentage) || percentage < GameConstants.MIN_PERCENTAGE)
            {
                EnvelopeSettingsPercentageTxtBox.BackColor = Color.White;
                EnvelopeSettingsPercentageTxtBox.ForeColor = Color.Black;
            }
            else
            {
                EnvelopeSettingsPercentageTxtBox.BackColor = Color.Red;
                EnvelopeSettingsPercentageTxtBox.ForeColor = Color.White;
            }

            _envelopeSettingsEdited = true;
        }

        private void EnvelopeSettingsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_envelopeSettingsSkipIndexChange)
                return;

            int selectedIndex = EnvelopeSettingsListBox.SelectedIndex;
            if (selectedIndex != NO_ITEM_INDEX && selectedIndex == _envelopeSettingsLastSelectedIndex)
                return;

            var chequeList = GameState.Instance.ChequeSettings.ChequeList;

            if (EnvelopeSettingsDidChequeChange() && _envelopeSettingsLastSelectedIndex != NO_ITEM_INDEX)
            {
                try
                {
                    chequeList[_envelopeSettingsLastSelectedIndex] = EnvelopeSettingsCreateChequeFromRadios();

                    EnvelopeSettingsUpdateAll();

                    _envelopeSettingsSkipIndexChange = true;
                    EnvelopeSettingsListBox.SelectedIndex = selectedIndex;
                }
                catch (Exception)
                {
                    var answer = MessageBox.Show(
                        text: "Nie można zapisać koperty! Przejście spowoduje utratę danych.",
                        GameConstants.PROGRAM_TITLE,
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning);

                    if (answer == DialogResult.Cancel)
                    {
                        _envelopeSettingsSkipIndexChange = true;
                        EnvelopeSettingsListBox.SelectedIndex = _envelopeSettingsLastSelectedIndex;
                        _envelopeSettingsSkipIndexChange = false;

                        _envelopeSettingsEdited = true;
                        EnvelopeSettingsUpdateButtons();
                        return;
                    }
                }
            }

            if (selectedIndex == NO_ITEM_INDEX)
            {
                EnvelopeSettingsCashRadio.Enabled = false;
                EnvelopeSettingsCashTxtBox.Enabled = false;
                EnvelopeSettingsCashTxtBox.Clear();

                EnvelopeSettingsPercentageRadio.Enabled = false;
                EnvelopeSettingsPercentageTxtBox.Enabled = false;
                EnvelopeSettingsPercentageTxtBox.Clear();

                return;
            }

            var cheque = chequeList[selectedIndex];
            switch (cheque)
            {
                case CashCheque cashCheque:
                    EnvelopeSettingsCashRadio.Checked = true;
                    EnvelopeSettingsCashTxtBox.Text = cashCheque.CashAmount.ToString();
                    break;
                case PercentageCheque percentageCheque:
                    EnvelopeSettingsPercentageRadio.Checked = true;
                    EnvelopeSettingsPercentageTxtBox.Text = percentageCheque.Percentage.ToString();
                    break;
                default:
                    throw new NotSupportedException($"Not recognised envelope type selected.");
            }

            EnvelopeSettingsCashRadio.Enabled = true;
            EnvelopeSettingsPercentageRadio.Enabled = true;

            EnvelopeSettingsUpdateButtons();

            _envelopeSettingsEdited = false;
            _envelopeSettingsSkipIndexChange = false;
            _envelopeSettingsLastSelectedIndex = selectedIndex;
        }

        private void EnvelopeSettingsMoveUpBtn_Click(object sender, EventArgs e)
        {
            List<BaseCheque> chequeList = GameState.Instance.ChequeSettings.ChequeList;
            int selectedIndex = EnvelopeSettingsListBox.SelectedIndex;

            chequeList.Reverse(selectedIndex - 1, 2);

            EnvelopeSettingsUpdateListBox();

            EnvelopeSettingsListBox.SelectedIndex = selectedIndex - 1;
            _envelopeSettingsEdited = true;
        }

        private void EnvelopeSettingsMoveDownBtn_Click(object sender, EventArgs e)
        {
            List<BaseCheque> chequeList = GameState.Instance.ChequeSettings.ChequeList;
            int selectedIndex = EnvelopeSettingsListBox.SelectedIndex;

            chequeList.Reverse(selectedIndex, 2);

            EnvelopeSettingsUpdateListBox();

            EnvelopeSettingsListBox.SelectedIndex = selectedIndex + 1;
            _envelopeSettingsEdited = true;
        }

        private void EnvelopeSettingsNewBtn_Click(object sender, EventArgs e)
        {
            var chequeList = GameState.Instance.ChequeSettings.ChequeList;
            var blankCheque = ChequeFactory.CreateCashCheque(0);
            chequeList.Add(blankCheque);

            EnvelopeSettingsUpdateAll();

            EnvelopeSettingsListBox.SelectedIndex = chequeList.Count - 1;
            _envelopeSettingsEdited = true;
        }

        private void EnvelopeSettingsDeleteBtn_Click(object sender, EventArgs e)
        {
            var confirmAnswer = MessageBox.Show(
                text: "Czy na pewno chcesz usunąć tę kopertę?",
                GameConstants.PROGRAM_TITLE,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmAnswer == DialogResult.No)
                return;

            int index = EnvelopeSettingsListBox.SelectedIndex;
            GameState.Instance.ChequeSettings.ChequeList.RemoveAt(index);

            EnvelopeSettingsUpdateAll();

            EnvelopeSettingsListBox.SelectedIndex = NO_ITEM_INDEX;
            EnvelopeSettingsListBox_SelectedIndexChanged(sender, e);

            _envelopeSettingsEdited = true;
        }

        private void EnvelopeSettingsRandomiseBtn_Click(object sender, EventArgs e)
        {
            var confirmAnswer = MessageBox.Show(
                text: "Jesteś pewny? Poprzedniej kolejności nie będzie można odzyskać bez wczytania z pliku lub ręcznego odzyskania.",
                GameConstants.PROGRAM_TITLE,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmAnswer == DialogResult.No)
                return;

            GameState.Instance.ChequeSettings.Randomise();
            EnvelopeSettingsUpdateAll();

            // Clear the cash
            EnvelopeSettingsCashRadio.Checked = false;

            // Clear the percentage
            EnvelopeSettingsPercentageRadio.Checked = false;

            // Clear the selection
            _envelopeSettingsLastSelectedIndex = NO_ITEM_INDEX;
            EnvelopeSettingsListBox.SelectedIndex = NO_ITEM_INDEX;
            EnvelopeSettingsListBox_SelectedIndexChanged(sender, e);

            // Clear the editing
            _envelopeSettingsEdited = false;
            _envelopeSettingsSkipIndexChange = false;
        }

        private void EnvelopeSettingsSaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = EnvelopeSettingsListBox.SelectedIndex;

                GameState.Instance.ChequeSettings.ChequeList[selectedIndex] = EnvelopeSettingsCreateChequeFromRadios();

                EnvelopeSettingsUpdateListBox();

                _envelopeSettingsEdited = false;
                _envelopeSettingsSkipIndexChange = true;
                _envelopeSettingsLastSelectedIndex = NO_ITEM_INDEX;
                EnvelopeSettingsListBox.SelectedIndex = selectedIndex;

                _envelopeSettingsSkipIndexChange = false;

                EnvelopeSettingsUpdateButtons();

                ShowOkMessage("Zapisywanie zakończono pomyślnie!");
            }
            catch (Exception ex)
            {
#if DEBUG
                ShowErrorMessage(ex.ToString());
#else
                ShowErrorMessage("Zapisywanie zakończone niepowodzeniem!");
#endif
            }
        }

        private void EnvelopeSettingsLoadFromFileBtn_Click(object sender, EventArgs e)
        {
            if (GameState.Instance.ChequeSettings.ChequeList.Count > 0)
            {
                var confirmResult = MessageBox.Show(
                text: "Na pewno? Niezapisany zestaw kopert przepadnie!",
                caption: GameConstants.PROGRAM_TITLE,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.No)
                    return;
            }


            if (EnvelopeSettingsOpen.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                GameState.Instance.LoadChequeSettingsFromFile(EnvelopeSettingsOpen.FileName);

                EnvelopeSettingsUpdateAll();

                _envelopeSettingsEdited = false;
                _envelopeSettingsSkipIndexChange = false;
                _envelopeSettingsLastSelectedIndex = NO_ITEM_INDEX;

                EnvelopeSettingsListBox.SelectedIndex = NO_ITEM_INDEX;
                EnvelopeSettingsListBox_SelectedIndexChanged(this, e);

                ShowOkMessage("Wczytywanie zakończono pomyślnie!");
            }
            catch (Exception ex)
            {
#if DEBUG
                ShowErrorMessage(ex.ToString());
#else
                ShowErrorMessage("Wczytywanie zakończone niepowodzeniem!");
#endif
            }
        }

        private void EnvelopeSettingsSaveToFileBtn_Click(object sender, EventArgs e)
        {
            if (EnvelopeSettingsSave.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                int selectedIndex = EnvelopeSettingsListBox.SelectedIndex;

                GameState.Instance.ChequeSettings.ChequeList[selectedIndex] = EnvelopeSettingsCreateChequeFromRadios();

                EnvelopeSettingsUpdateAll();

                _envelopeSettingsEdited = false;
                _envelopeSettingsSkipIndexChange = true;
                _envelopeSettingsLastSelectedIndex = NO_ITEM_INDEX;

                EnvelopeSettingsListBox.SelectedIndex = selectedIndex;

                _envelopeSettingsSkipIndexChange = false;

                GameState.Instance.ChequeSettings.SaveToFile(EnvelopeSettingsSave.FileName);

                ShowOkMessage("Zapisywanie zakończono pomyślnie!");
            }
            catch (Exception ex)
            {
#if DEBUG
                ShowErrorMessage(ex.ToString());
#else
                ShowErrorMessage("Zapisywanie zakończone niepowodzeniem!");
#endif
            }
        }
        #endregion

        #region Question Editor

        private void QuestionEditorUpdateList()
        {
            List<Question> questionList = GameState.Instance.QuestionSet.QuestionList;
            int count = 1;

            QuestionEditorListBox.Items.Clear();

            foreach (Question question in questionList)
            {
                QuestionEditorListBox.Items.Add($"{count}. {question.Text}");
                ++count;
            }

            QuestionEditorCountLbl.Text = string.Format("Pytań: {0}", questionList.Count);
        }

        private void QuestionEditorUpdateButtons()
        {
            List<Question> questionList = GameState.Instance.QuestionSet.QuestionList;
            int questionCount = GameState.Instance.QuestionSet.QuestionCount;
            int selectedIndex = QuestionEditorListBox.SelectedIndex;

            bool itemSelected = selectedIndex != NO_ITEM_INDEX;
            bool questionsPresent = questionCount > 0;

            QuestionEditorMoveUpBtn.Enabled = selectedIndex > 0 && selectedIndex < questionCount;
            QuestionEditorMoveDownBtn.Enabled = itemSelected && selectedIndex < questionCount - 1;
            QuestionEditorNewBtn.Enabled = true;
            QuestionEditorDeleteBtn.Enabled = itemSelected;
            QuestionEditorClearBtn.Enabled = questionsPresent;
            QuestionEditorSaveBtn.Enabled = itemSelected;
            QuestionEditorLoadFromFileBtn.Enabled = true;
            QuestionEditorSaveToFileBtn.Enabled = questionsPresent;
        }

        private void QuestionEditorUpdateAll()
        {
            QuestionEditorUpdateList();
            QuestionEditorUpdateButtons();
        }

        private Question QuestionEditorGetQuestionFromForm()
        {
            string questionText = QuestionEditorTextBox.Text.Trim();
            string answer1 = QuestionEditorAnsATxtBox.Text.Trim();
            string answer2 = QuestionEditorAnsBTxtBox.Text.Trim();
            string answer3 = QuestionEditorAnsCTxtBox.Text.Trim();
            string answer4 = QuestionEditorAnsDTxtBox.Text.Trim();
            string comment = QuestionEditorCommentTxtBox.Text.Trim();

            int correctAnswer;
            if (QuestionEditorAnsARadio.Checked)
                correctAnswer = 1;
            else if (QuestionEditorAnsBRadio.Checked)
                correctAnswer = 2;
            else if (QuestionEditorAnsCRadio.Checked)
                correctAnswer = 3;
            else if (QuestionEditorAnsDRadio.Checked)
                correctAnswer = 4;
            else
                throw new Exception();

            return Question.Create(questionText, answer1, answer2, answer3, answer4, correctAnswer, comment);
        }

        private bool QuestionEditorDidQuestionChange()
        {
            int selectedIndex = QuestionEditorListBox.SelectedIndex;
            if (selectedIndex < 0)
                return false;

            Question question = GameState.Instance.QuestionSet.QuestionList[selectedIndex];

            if (QuestionEditorTextBox.Text.Trim() != question.Text)
                return true;
            if (QuestionEditorAnsATxtBox.Text.Trim() != question.Answer1)
                return true;
            if (QuestionEditorAnsBTxtBox.Text.Trim() != question.Answer2)
                return true;
            if (QuestionEditorAnsCTxtBox.Text.Trim() != question.Answer3)
                return true;
            if (QuestionEditorAnsDTxtBox.Text.Trim() != question.Answer4)
                return true;
            if (QuestionEditorCommentTxtBox.Text.Trim() != question.Comment)
                return true;

            switch (question.CorrectAnswerNumber)
            {
                case 1:
                    if (!QuestionEditorAnsARadio.Checked)
                        return true;
                    break;
                case 2:
                    if (!QuestionEditorAnsBRadio.Checked)
                        return true;
                    break;
                case 3:
                    if (!QuestionEditorAnsCRadio.Checked)
                        return true;
                    break;
                case 4:
                    if (!QuestionEditorAnsDRadio.Checked)
                        return true;
                    break;
                default:
                    throw new InvalidDataException($"Correct answer number must be between 1 and 4.");
            }

            return false;
        }

        private void QuestionEditorSetLockBoxes(bool enabled)
        {
            QuestionEditorTextBox.Enabled = enabled;
            QuestionEditorAnsATxtBox.Enabled = enabled;
            QuestionEditorAnsBTxtBox.Enabled = enabled;
            QuestionEditorAnsCTxtBox.Enabled = enabled;
            QuestionEditorAnsDTxtBox.Enabled = enabled;

            QuestionEditorAnsARadio.Enabled = enabled;
            QuestionEditorAnsBRadio.Enabled = enabled;
            QuestionEditorAnsCRadio.Enabled = enabled;
            QuestionEditorAnsDRadio.Enabled = enabled;

            QuestionEditorCommentTxtBox.Enabled = enabled;
        }

        private void QuestionEditorClearBoxes()
        {
            QuestionEditorTextBox.Text = string.Empty;

            QuestionEditorAnsATxtBox.Text = string.Empty;
            QuestionEditorAnsBTxtBox.Text = string.Empty;
            QuestionEditorAnsCTxtBox.Text = string.Empty;
            QuestionEditorAnsDTxtBox.Text = string.Empty;

            QuestionEditorAnsARadio.Checked = false;
            QuestionEditorAnsBRadio.Checked = false;
            QuestionEditorAnsCRadio.Checked = false;
            QuestionEditorAnsDRadio.Checked = false;

            QuestionEditorCommentTxtBox.Text = string.Empty;
        }

        private void QuestionEditorCopyFromQuestion(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            QuestionEditorTextBox.Text = question.Text;

            QuestionEditorAnsATxtBox.Text = question.Answer1;
            QuestionEditorAnsBTxtBox.Text = question.Answer2;
            QuestionEditorAnsCTxtBox.Text = question.Answer3;
            QuestionEditorAnsDTxtBox.Text = question.Answer4;

            switch (question.CorrectAnswerNumber)
            {
                case 1:
                    QuestionEditorAnsARadio.Checked = true;
                    break;
                case 2:
                    QuestionEditorAnsBRadio.Checked = true;
                    break;
                case 3:
                    QuestionEditorAnsCRadio.Checked = true;
                    break;
                case 4:
                    QuestionEditorAnsDRadio.Checked = true;
                    break;
                default:
                    throw new InvalidDataException($"Correct Answer Number must be in range 1...4.");
            }

            QuestionEditorCommentTxtBox.Text = question.Comment;
        }

        private void QuestionEditorListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_questionEditorSkipIndexChange)
                return;

            int selectedIndex = QuestionEditorListBox.SelectedIndex;
            if (selectedIndex != NO_ITEM_INDEX && selectedIndex == _questionEditorLastSelectedIndex)
                return;

            var questionSet = GameState.Instance.QuestionSet.QuestionList;

            if (QuestionEditorDidQuestionChange() && _questionEditorLastSelectedIndex != NO_ITEM_INDEX)
            {
                try
                {
                    questionSet[_questionEditorLastSelectedIndex] = QuestionEditorGetQuestionFromForm();

                    QuestionEditorUpdateAll();

                    _questionEditorSkipIndexChange = true;
                    QuestionEditorListBox.SelectedIndex = selectedIndex;
                }
                catch (Exception)
                {
                    var answer = MessageBox.Show(
                        text: "Nie można zapisać pytania! Przejście spowoduje utratę danych.",
                        GameConstants.PROGRAM_TITLE,
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning);

                    if (answer == DialogResult.Cancel)
                    {
                        _questionEditorSkipIndexChange = true;
                        QuestionEditorListBox.SelectedIndex = _questionEditorLastSelectedIndex;
                        _questionEditorSkipIndexChange = false;

                        _questionEditorEdited = true;
                        QuestionEditorUpdateButtons();
                        return;
                    }
                }
            }

            if (selectedIndex == NO_ITEM_INDEX)
            {
                QuestionEditorSetLockBoxes(enabled: false);
                QuestionEditorClearBoxes();
                return;
            }

            QuestionEditorSetLockBoxes(enabled: true);
            QuestionEditorCopyFromQuestion(questionSet[selectedIndex]);
            QuestionEditorUpdateButtons();

            _questionEditorEdited = false;
            _questionEditorSkipIndexChange = false;
            _questionEditorLastSelectedIndex = selectedIndex;
        }

        private void QuestionEditorMoveUpBtn_Click(object sender, EventArgs e)
        {
            var questionList = GameState.Instance.QuestionSet.QuestionList;
            int selectedIndex = QuestionEditorListBox.SelectedIndex;

            questionList.Reverse(selectedIndex - 1, 2);
            QuestionEditorUpdateList();

            QuestionEditorListBox.SelectedIndex = selectedIndex - 1;
            _questionEditorEdited = true;
        }

        private void QuestionEditorMoveDownBtn_Click(object sender, EventArgs e)
        {
            var questionList = GameState.Instance.QuestionSet.QuestionList;
            int selectedIndex = QuestionEditorListBox.SelectedIndex;

            questionList.Reverse(selectedIndex, 2);
            QuestionEditorUpdateList();

            QuestionEditorListBox.SelectedIndex = selectedIndex + 1;
            _questionEditorEdited = true;
        }

        private void QuestionEditorNewBtn_Click(object sender, EventArgs e)
        {
            var blankQuestion = Question.Create(
                text: "Puste pytanie",
                answer1: "Odp A",
                answer2: "Odp B",
                answer3: "Odp C",
                answer4: "Odp D",
                correctAnswerNumber: 1,
                comment: string.Empty);

            var questionSet = GameState.Instance.QuestionSet;

            questionSet.AddNewQuestion(blankQuestion);

            QuestionEditorUpdateAll();

            QuestionEditorListBox.SelectedIndex = questionSet.QuestionCount - 1;

            _questionEditorEdited = true;
        }

        private void QuestionEditorDeleteBtn_Click(object sender, EventArgs e)
        {
            var confirmAnswer = MessageBox.Show(
                text: "Czy na pewno chcesz usunąć wskazane pytanie?",
                GameConstants.PROGRAM_TITLE,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmAnswer == DialogResult.No)
                return;

            int selectedIndex = QuestionEditorListBox.SelectedIndex;

            GameState.Instance.QuestionSet.DeleteQuestionByIndex(selectedIndex);

            QuestionEditorUpdateAll();

            _questionEditorEdited = GameState.Instance.QuestionSet.QuestionCount > 0;
            _questionEditorSkipIndexChange = false;
            _questionEditorLastSelectedIndex = NO_ITEM_INDEX;
        }

        private void QuestionEditorClearBtn_Click(object sender, EventArgs e)
        {
            var confirmAnswer = MessageBox.Show(
                text: "Czy na pewno chcesz usunąć cały zestaw pytań?",
                GameConstants.PROGRAM_TITLE,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmAnswer == DialogResult.No)
                return;

            GameState.Instance.QuestionSet.ClearQuestionSet();

            QuestionEditorUpdateAll();

            _questionEditorEdited = false;
            _questionEditorSkipIndexChange = false;
            _questionEditorLastSelectedIndex = NO_ITEM_INDEX;

            // We have to call this manually, because we set the index to -1
            QuestionEditorListBox_SelectedIndexChanged(this, e);
        }

        private void QuestionEditorSaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = QuestionEditorListBox.SelectedIndex;

                GameState.Instance.QuestionSet.QuestionList[selectedIndex] = QuestionEditorGetQuestionFromForm();

                QuestionEditorUpdateList();

                _questionEditorEdited = false;
                _questionEditorSkipIndexChange = true;
                _questionEditorLastSelectedIndex = NO_ITEM_INDEX;
                QuestionEditorListBox.SelectedIndex = selectedIndex;

                QuestionEditorUpdateButtons();

                _questionEditorSkipIndexChange = false;

                ShowOkMessage("Zapisywanie zakończono pomyślnie!");
            }
            catch (Exception ex)
            {
#if DEBUG
                ShowErrorMessage(ex.ToString());
#else
                ShowErrorMessage("Zapisywanie zakończone niepowodzeniem!");
#endif
            }
        }

        private void QuestionEditorLoadFromFileBtn_Click(object sender, EventArgs e)
        {
            if (GameState.Instance.QuestionSet.QuestionCount > 0)
            {
                var confirmResult = MessageBox.Show(
                    text: "Na pewno? Niezapisany zestaw pytań przepadnie!",
                    caption: GameConstants.PROGRAM_TITLE,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.No)
                    return;
            }

            if (QuestionSetOpen.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                GameState.Instance.LoadQuestionSetFromFile(QuestionSetOpen.FileName);

                QuestionEditorUpdateAll();

                _questionEditorEdited = false;
                _questionEditorLastSelectedIndex = NO_ITEM_INDEX;
                _questionEditorSkipIndexChange = false;

                QuestionEditorListBox.SelectedIndex = NO_ITEM_INDEX;
                QuestionEditorListBox_SelectedIndexChanged(this, e);

                ShowOkMessage("Wczytywanie zakończono pomyślnie!");
            }
            catch (Exception ex)
            {
#if DEBUG
                ShowErrorMessage(ex.ToString());
#else
                ShowErrorMessage("Wczytywanie zakończone niepowodzeniem!");
#endif
            }
        }

        private void QuestionEditorSaveToFileBtn_Click(object sender, EventArgs e)
        {
            if (QuestionSetSave.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                int selectedIndex = QuestionEditorListBox.SelectedIndex;

                GameState.Instance.QuestionSet.QuestionList[selectedIndex] = QuestionEditorGetQuestionFromForm();

                QuestionEditorUpdateList();

                _questionEditorEdited = false;
                _questionEditorSkipIndexChange = true;
                _questionEditorLastSelectedIndex = NO_ITEM_INDEX;
                QuestionEditorListBox.SelectedIndex = selectedIndex;

                _questionEditorSkipIndexChange = false;

                QuestionEditorUpdateButtons();

                GameState.Instance.QuestionSet.SaveToFile(QuestionSetSave.FileName);

                ShowOkMessage("Zapisywanie zakończono pomyślnie!");
            }
            catch (Exception ex)
            {
#if DEBUG
                ShowErrorMessage(ex.ToString());
#else
                ShowErrorMessage("Zapisywanie zakończone niepowodzeniem!");
#endif
            }
        }

        #endregion

        #region Game - Envelope Selection

        private void EnvelopeSelectionStartMusic()
        {
            var startSound = new Sound("snd/start-game.wav", _volumeLevel);
            var startLoop = new Sound("snd/envelope-selection-loop.wav", _volumeLevel);
            startLoop.SetInfiniteLoop();

            _soundManager.StopAllQueues();
            _soundManager.CreateQueue(QUEUE_NAME_GAME_START);
            _soundManager.AddSoundToQueue(QUEUE_NAME_GAME_START, startSound);
            _soundManager.AddSoundToQueue(QUEUE_NAME_GAME_START, startLoop);
            _soundManager.PlayQueue(QUEUE_NAME_GAME_START);
        }

        private void EnvelopeSelectionUnlockButtons()
        {
            // Unlock the start game button
            EnvelopeSelectionStartGameBtn.Enabled = true;
        }

        private void EnvelopeSelectionLockButtons()
        {
            // Lock all unlocked buttons
            EnvelopeSelectionStartGameBtn.Enabled = false;
            EnvelopeSelectionConfirmBtn.Enabled = false;
            EnvelopeSelectionRetractBtn.Enabled = false;
            EnvelopeSelectionNextPartBtn.Enabled = false;
        }

        private void EnvelopeSelectionUpdateAvailability()
        {
            var gameStateInstance = GameState.Instance;
            int envelopeCount = gameStateInstance.ContestantEnvelopeSet.EnvelopeCount;
            int startEnvelopeCount = gameStateInstance.GameSettings.StartEnvelopeCount;

            if (envelopeCount < startEnvelopeCount)
            {
                int i = 0;
                foreach (var envelopeLabelPair in _envelopeControlPairs)
                {
                    envelopeLabelPair.EnvelopeTextBox.Enabled = (i == envelopeCount);
                    ++i;
                }

                _envelopeControlPairs[envelopeCount].EnvelopeTextBox.Focus();
            }
            else
            {
                foreach (var envelopeLabelPair in _envelopeControlPairs)
                {
                    envelopeLabelPair.EnvelopeTextBox.Enabled = false;
                }
            }

            EnvelopeSelectionConfirmBtn.Enabled = envelopeCount < startEnvelopeCount;
            EnvelopeSelectionRetractBtn.Enabled = envelopeCount > 0;
            EnvelopeSelectionNextPartBtn.Enabled = envelopeCount >= startEnvelopeCount;
        }

        private void EnvelopeSelectionNumTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EnvelopeSelectionConfirmBtn_Click(this, e);
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void EnvelopeSelectionNumTxtBox_TextChanged(object sender, EventArgs e)
        {
            // Check if the sender is a textbox
            if (sender is not TextBox numberBox)
                throw new ArgumentException("The sender is not a textbox.");

            // Get the appropriate value label
            var pair = _envelopeControlPairs.First(envelopePair => envelopePair.EnvelopeTextBox == numberBox);
            Label valueLabel = pair.EnvelopeLabel;

            // If the textbox is empty, then clear the value label
            if (string.IsNullOrEmpty(numberBox.Text))
            {
                valueLabel.Text = string.Empty;
                return;
            }

            var gameStateInstance = GameState.Instance;
            var envelopeTable = gameStateInstance.EnvelopeTable;

            // Check for errors
            string errorMessage = string.Empty;
            Envelope? envelope = null;

            if (!int.TryParse(numberBox.Text, out int envelopeNumber))
            {
                errorMessage = "To nie jest liczba!";
            }
            else if (envelopeNumber < GameConstants.MIN_ENVELOPE_NUMBER || envelopeNumber > GameConstants.MAX_ENVELOPE_NUMBER)
            {
                errorMessage = string.Format("W przedziale {0}...{1}!", GameConstants.MIN_ENVELOPE_NUMBER, GameConstants.MAX_ENVELOPE_NUMBER);
            }
            else
            {
                envelope = envelopeTable.GetEnvelope(envelopeNumber);
                if (envelope.State == EnvelopeState.SELECTED_AT_START)
                {
                    errorMessage = "Koperta już wybrana wcześniej!";
                }
            }

            // If there's no error message, display the correct envelope value
            // If there is an error, display it
            if (string.IsNullOrEmpty(errorMessage) && envelope is not null)
            {
                valueLabel.Text = envelope.Cheque.ToValueString();
                valueLabel.ForeColor = Color.Black;
            }
            else
            {
                valueLabel.Text = errorMessage;
                valueLabel.ForeColor = Color.Red;
            }
        }

        private void EnvelopeSelectionStartGameBtn_Click(object sender, EventArgs e)
        {
            // Lock the start game button
            EnvelopeSelectionStartGameBtn.Enabled = false;

            // Unlock the first envelope
            EnvelopeSelectionNum0TxtBox.Enabled = true;

            // Unlock the confirmation button
            EnvelopeSelectionConfirmBtn.Enabled = true;

            // Focus on the first envelope textbox
            EnvelopeSelectionNum0TxtBox.Focus();

            // Show envelopes
            GameState.Instance.ShowEnvelopesAtStart();
        }

        private void EnvelopeSelectionConfirmBtn_Click(object sender, EventArgs e)
        {
            var gameStateInstance = GameState.Instance;
            var contestantEnvelopes = gameStateInstance.ContestantEnvelopeSet;
            var gameSettings = gameStateInstance.GameSettings;
            int index = contestantEnvelopes.EnvelopeCount;

            // Check if the envelope number is valid
            if (!int.TryParse(_envelopeControlPairs[index].EnvelopeTextBox.Text.Trim(), out int envelopeNumber) ||
                envelopeNumber < GameConstants.MIN_ENVELOPE_NUMBER || envelopeNumber > GameConstants.MAX_ENVELOPE_NUMBER)
            {
                string message = string.Format("Numer koperty musi być w przedziale [{0}...{1}]!", GameConstants.MIN_ENVELOPE_NUMBER, GameConstants.MAX_ENVELOPE_NUMBER);
                ShowErrorMessage(message);
                return;
            }

            // Get the envelope and add to the contestant envelopes
            var envelopeTable = gameStateInstance.EnvelopeTable;
            if (!envelopeTable.IsEnvelopePresent(envelopeNumber))
            {
                ShowErrorMessage(message: "Koperta wybrana wcześniej.");
                return;
            }

            var newEnvelope = envelopeTable.GetEnvelope(envelopeNumber);
            newEnvelope.MarkAsSelectedAtStart();

            gameStateInstance.AddContestantEnvelope(newEnvelope);

            if (contestantEnvelopes.EnvelopeCount >= gameSettings.StartEnvelopeCount)
            {
                gameStateInstance.MarkNotSelectedEnvelopesAtStart();
            }

            // Update button availability
            EnvelopeSelectionUpdateAvailability();

            PlaySingleSound(filePath: "snd/envelope-selection-ping.wav");
        }

        private void EnvelopeSelectionRetractBtn_Click(object sender, EventArgs e)
        {
            var gameStateInstance = GameState.Instance;
            var contestantEnvelopes = gameStateInstance.ContestantEnvelopeSet;
            int lastEnvelopeIndex = contestantEnvelopes.EnvelopeCount - 1;

            // Get the last envelope
            var lastEnvelope = contestantEnvelopes.GetEnvelope(lastEnvelopeIndex);
            lastEnvelope.MarkAsNeutral();

            // Remove the last envelope
            gameStateInstance.RemoveContestantEnvelope(lastEnvelope);

            // Get the last textbox and clear it
            _envelopeControlPairs[lastEnvelopeIndex].EnvelopeTextBox.Clear();

            gameStateInstance.ClearNotSelectedMarkings();

            // Update button availability
            EnvelopeSelectionUpdateAvailability();
        }

        private void EnvelopeSelectionNextPartBtn_Click(object sender, EventArgs e)
        {
            var gameStateInstance = GameState.Instance;

            gameStateInstance.HideEnvelopesAfterSelection();
            gameStateInstance.UnmarkSelectionAtStart();

            foreach (Envelope envelope in gameStateInstance.ContestantEnvelopeSet.Envelopes)
            {
                gameStateInstance.EnvelopeTable.DeleteEnvelope(envelope);
            }
            
            gameStateInstance.SortEnvelopesByNumber();
            gameStateInstance.ShowEnvelopesAfterQuestion();

            EnvelopeSelectionLockButtons();
            QuestionGameUnlockFirstButtons();
            QuestionGamePlayStartingMusic();

            DirectorTabControl.SelectTab("GameQuestionsTab");
        }

        #endregion

        #region Game - Questions

        private void QuestionGamePlayStartingMusic()
        {
            var startGameSound = new Sound("snd/question-game-start.wav", _volumeLevel);
            var talkLoop = new Sound("snd/question-game-talk.wav", _volumeLevel);
            talkLoop.SetInfiniteLoop();

            _soundManager.StopAllQueues();
            _soundManager.CreateQueue(QUEUE_NAME_MOVE_TO_QUESTION_GAME);
            _soundManager.AddSoundToQueue(QUEUE_NAME_MOVE_TO_QUESTION_GAME, startGameSound);
            _soundManager.AddSoundToQueue(QUEUE_NAME_MOVE_TO_QUESTION_GAME, talkLoop);
            _soundManager.PlayQueue(QUEUE_NAME_MOVE_TO_QUESTION_GAME);
        }

        private void QuestionGamePlayAskingMusic()
        {
            var letsPlaySound = new Sound("snd/question-game-ask-question.wav", _volumeLevel);
            var questionLoop = new Sound("snd/question-game-question-loop.wav", _volumeLevel);
            questionLoop.SetInfiniteLoop();

            _soundManager.StopAllQueues();
            _soundManager.CreateQueue(QUEUE_NAME_QUESTION_GAME);
            _soundManager.AddSoundToQueue(QUEUE_NAME_QUESTION_GAME, letsPlaySound);
            _soundManager.AddSoundToQueue(QUEUE_NAME_QUESTION_GAME, questionLoop);
            _soundManager.PlayQueue(QUEUE_NAME_QUESTION_GAME);
        }

        private void QuestionGamePlayStatisticsUpdate()
        {
            PlaySingleSound(filePath: "snd/statistics-update.wav");
        }

        private void QuestionGamePlayShredderSound()
        {
            var shredderSound = new Sound(filePath: "snd/envelope-destroyed.wav", _volumeLevel);
            shredderSound.EventStoppedPlayback += (s, e) =>
            {
                _soundManager.ResumeQueue(QUEUE_NAME_ANSWER_REACTION);
            };

            _soundManager.PauseQueue(QUEUE_NAME_ANSWER_REACTION);
            _soundManager.PlaySingleSound(shredderSound);
        }

        private void QuestionGameUnlockFirstButtons()
        {
            QuestionGameNextQuestionBtn.Enabled = true;
        }

        private void QuestionGameSetAnswerEnabled(bool enabled)
        {
            QuestionGameAns1Btn.Enabled = enabled;
            QuestionGameAns2Btn.Enabled = enabled;
            QuestionGameAns3Btn.Enabled = enabled;
            QuestionGameAns4Btn.Enabled = enabled;
        }

        private void QuestionGameLockAllButtons()
        {
            QuestionGameSetAnswerEnabled(enabled: false);

            QuestionGameOpenEnvelopeButton.Enabled = false;
            QuestionGameNextQuestionBtn.Enabled = false;
            QuestionGameShowQuestionBtn.Enabled = false;
            QuestionGameDisplayEnvelopesBtn.Enabled = false;
            QuestionGameConfirmEnvelopeBtn.Enabled = false;
            QuestionGameShowAnswersBtn.Enabled = false;
            QuestionGameShowCorrectBtn.Enabled = false;
            QuestionGameCheckAnswerBtn.Enabled = false;
            QuestionGameKeepDestroyEnvelopeBtn.Enabled = false;
            QuestionGameCancelQuestionBtn.Enabled = false;
            QuestionGameStartTradingBtn.Enabled = false;
        }

        private void QuestionGameUpdateEnvelopeLabel()
        {
            Envelope? selectedEnvelope = GameState.Instance.GetContestantEnvelope(_questionGameEnvelopeIndex);

            if (selectedEnvelope is null)
                throw new NullReferenceException($"Selected envelope is null.");

            EnvelopeColorCollection colorPairing = selectedEnvelope.GetColorsForScreen();
            QuestionGameEnvelopeLabel.BackColor = colorPairing.BackgroundColor;

            QuestionGameEnvelopeLabel.Text = selectedEnvelope.GetEnvelopeValueForDirector();
        }

        private void QuestionGameUpdateEnvelopeButtons()
        {
            var gameStateInstance = GameState.Instance;

            int startEnvelopeCount = gameStateInstance.GameSettings.StartEnvelopeCount;

            if (_questionGameEnvelopeIndex < 0)
            {
                _questionGameEnvelopeIndex = 0;
            }
            else if (_questionGameEnvelopeIndex >= startEnvelopeCount)
            {
                _questionGameEnvelopeIndex = startEnvelopeCount - 1;
            }

            var contestantEnvelopeSet = gameStateInstance.ContestantEnvelopeSet;
            var envelope = contestantEnvelopeSet.GetEnvelope(_questionGameEnvelopeIndex);

            QuestionGameConfirmEnvelopeBtn.Enabled = envelope.State == EnvelopeState.NEUTRAL;
            QuestionGamePreviousEnvelopeBtn.Enabled = _questionGameEnvelopeIndex > 0;
            QuestionGameNextEnvelopeBtn.Enabled = _questionGameEnvelopeIndex < startEnvelopeCount - 1;
        }


        private void QuestionGameClearQuestionBoxes()
        {
            QuestionGameQuestionLbl.Text = string.Empty;

            QuestionGameAns1Lbl.BackColor = SystemColors.Control;
            QuestionGameAns2Lbl.BackColor = SystemColors.Control;
            QuestionGameAns3Lbl.BackColor = SystemColors.Control;
            QuestionGameAns4Lbl.BackColor = SystemColors.Control;

            QuestionGameAns1Lbl.Text = string.Empty;
            QuestionGameAns2Lbl.Text = string.Empty;
            QuestionGameAns3Lbl.Text = string.Empty;
            QuestionGameAns4Lbl.Text = string.Empty;

            QuestionGameCorrectAnswerLabel.Text = string.Empty;

            QuestionGameCommentLbl.Text = string.Empty;
        }

        private void QuestionGamePlaceCurrentQuestionInBoxes()
        {
            var gameState = GameState.Instance;
            var currentQuestion = gameState.GetCurrentQuestion();

            QuestionGameQuestionLbl.Text = string.Format("{0}/{1}: {2}", gameState.QuestionIndex + 1, gameState.MaxQuestionCount, currentQuestion.Text);

            QuestionGameAns1Lbl.Text = currentQuestion.Answer1;
            QuestionGameAns2Lbl.Text = currentQuestion.Answer2;
            QuestionGameAns3Lbl.Text = currentQuestion.Answer3;
            QuestionGameAns4Lbl.Text = currentQuestion.Answer4;

            QuestionGameCorrectAnswerLabel.Text = Utils.AnswerToLetter(currentQuestion.CorrectAnswerNumber);

            QuestionGameCommentLbl.Text = currentQuestion.IsCommentPresent ? currentQuestion.Comment : "N/D";
        }

        private void QuestionGameLockAnswer(int answerNumber)
        {
            if (answerNumber < GameConstants.MIN_ANSWER_NUMBER || answerNumber > GameConstants.MAX_ANSWER_NUMBER)
                throw new ArgumentOutOfRangeException(nameof(answerNumber), $"The answer number must be between {GameConstants.MIN_ANSWER_NUMBER} and {GameConstants.MAX_ANSWER_NUMBER}.");

            GameState.Instance.SelectAnswer(answerNumber);

            QuestionGameSetAnswerEnabled(enabled: false);
            QuestionGameShowCorrectBtn.Enabled = true;

            _questionGameAnswerLabels[answerNumber].BackColor = DrawingConstants.LOCK_IN_ANS_COLOR;

            PlaySingleSound(filePath: "snd/question-game-lock-in-ping.wav");
        }

        private void QuestionGameShowCorrectAnswer()
        {
            var gameState = GameState.Instance;
            var currentQuestion = gameState.GetCurrentQuestion();

            _questionGameAnswerLabels[currentQuestion.CorrectAnswerNumber].BackColor = DrawingConstants.CORRECT_ANS_COLOR;

            gameState.ShowCorrectAnswer();
        }

        private void QuestionGameAns1Btn_Click(object sender, EventArgs e)
        {
            QuestionGameLockAnswer(answerNumber: 1);
        }

        private void QuestionGameAns2Btn_Click(object sender, EventArgs e)
        {
            QuestionGameLockAnswer(answerNumber: 2);
        }

        private void QuestionGameAns3Btn_Click(object sender, EventArgs e)
        {
            QuestionGameLockAnswer(answerNumber: 3);
        }

        private void QuestionGameAns4Btn_Click(object sender, EventArgs e)
        {
            QuestionGameLockAnswer(answerNumber: 4);
        }

        private void QuestionGameNextQuestionBtn_Click(object sender, EventArgs e)
        {
            GameState.Instance.NextQuestion();
            QuestionGameClearQuestionBoxes();
            QuestionGamePlaceCurrentQuestionInBoxes();

            QuestionGameCancelQuestionBtn.Enabled = true;
            QuestionGameNextQuestionBtn.Enabled = false;
            QuestionGameShowQuestionBtn.Enabled = true;

            QuestionGamePlayAskingMusic();
        }

        private void QuestionGameShowQuestionBtn_Click(object sender, EventArgs e)
        {
            QuestionGameShowQuestionBtn.Enabled = false;
            QuestionGameDisplayEnvelopesBtn.Enabled = true;

            GameState.Instance.ShowQuestion();

            PlaySingleSound(filePath: "snd/question-game-show-question.wav");
        }

        private void QuestionGameDisplayEnvelopesBtn_Click(object sender, EventArgs e)
        {
            QuestionGameDisplayEnvelopesBtn.Enabled = false;
            QuestionGameConfirmEnvelopeBtn.Enabled = true;

            GameState.Instance.ShowEnvelopesBeforeQuestion();

            _questionGameEnvelopeIndex = 0;
            QuestionGameUpdateEnvelopeLabel();
            QuestionGameUpdateEnvelopeButtons();

            PlaySingleSound(filePath: "snd/question-game-show-envelopes.wav");
        }

        private void QuestionGameConfirmEnvelopeBtn_Click(object sender, EventArgs e)
        {
            var gameStateInstance = GameState.Instance;
            var selectedEnvelope = gameStateInstance.GetContestantEnvelope(_questionGameEnvelopeIndex);
            if (selectedEnvelope is null)
                throw new InvalidOperationException($"Envelope confirmed is null.");

            if (selectedEnvelope.State != EnvelopeState.NEUTRAL)
            {
                ShowErrorMessage(message: "Tę kopertę wybrano wcześniej! Wybierz inną!");
                return;
            }

            gameStateInstance.SelectEnvelopeToPlayFor(selectedEnvelope);

            QuestionGameConfirmEnvelopeBtn.Enabled = false;
            QuestionGameShowAnswersBtn.Enabled = true;

            QuestionGamePreviousEnvelopeBtn.Enabled = false;
            QuestionGameNextEnvelopeBtn.Enabled = false;

            QuestionGameUpdateEnvelopeLabel();

            PlaySingleSound(filePath: "snd/question-game-envelope-picked.wav");
        }

        private void QuestionGameShowAnswersBtn_Click(object sender, EventArgs e)
        {
            QuestionGameSetAnswerEnabled(enabled: true);
            QuestionGameShowAnswersBtn.Enabled = false;

            GameState.Instance.ShowPossibleAnswers();

            PlaySingleSound(filePath: "snd/question-game-show-answers.wav");
        }

        private void QuestionGameShowCorrectBtn_Click(object sender, EventArgs e)
        {
            QuestionGameShowCorrectAnswer();

            QuestionGameShowCorrectBtn.Enabled = false;
            QuestionGameCheckAnswerBtn.Enabled = true;

            PlaySingleSound(filePath: "snd/question-game-show-correct-ping.wav");
        }

        private void QuestionGameCheckAnswerBtn_Click(object sender, EventArgs e)
        {
            var gameStateInstance = GameState.Instance;
            gameStateInstance.MarkWinOrLose();
            gameStateInstance.ShowEnvelopesAfterQuestion();

            QuestionGameUpdateEnvelopeLabel();

            QuestionGameCheckAnswerBtn.Enabled = false;
            QuestionGameKeepDestroyEnvelopeBtn.Enabled = true;
            QuestionGameOpenEnvelopeButton.Enabled = !gameStateInstance.IsAnswerCorrect();

            QuestionGamePlayAnswerSound(gameStateInstance.IsAnswerCorrect());
        }

        private void QuestionGamePlayAnswerSound(bool isAnswerRight)
        {
            Sound answerReactionSound;
            if (isAnswerRight)
                answerReactionSound = new Sound("snd/question-game-correct-answer.wav", _volumeLevel);
            else
                answerReactionSound = new Sound("snd/question-game-wrong-answer.wav", _volumeLevel);

            Sound backgroundSound = new Sound("snd/question-game-talk.wav", _volumeLevel);
            backgroundSound.SetInfiniteLoop();

            _soundManager.StopAllQueues();
            _soundManager.CreateQueue(QUEUE_NAME_ANSWER_REACTION);
            _soundManager.AddSoundToQueue(QUEUE_NAME_ANSWER_REACTION, answerReactionSound);
            _soundManager.AddSoundToQueue(QUEUE_NAME_ANSWER_REACTION, backgroundSound);
            _soundManager.PlayQueue(QUEUE_NAME_ANSWER_REACTION);
        }

        private void QuestionGameKeepDestroyEnvelopeBtn_Click(object sender, EventArgs e)
        {
            var gameStateInstance = GameState.Instance;
            gameStateInstance.KeepOrDestroyBasedOnAnswer();

            if (gameStateInstance.IsAnswerCorrect())
                QuestionGamePlayStatisticsUpdate();
            else
                QuestionGamePlayShredderSound();

            if (gameStateInstance.HasQuestionsLeft)
                QuestionGameNextQuestionBtn.Enabled = true;
            else
                QuestionGameStartTradingBtn.Enabled = true;

            QuestionGameOpenEnvelopeButton.Enabled = false;
            QuestionGameKeepDestroyEnvelopeBtn.Enabled = false;

            QuestionGameUpdateEnvelopeLabel();
        }

        private void QuestionGameCancelQuestionBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                text: "Czy na pewno chcesz wycofać całe pytanie?",
                caption: GameConstants.PROGRAM_TITLE,
                buttons: MessageBoxButtons.YesNo,
                icon: MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            var gameStateInstance = GameState.Instance;
            gameStateInstance.CancelQuestion();

            QuestionGameClearQuestionBoxes();
            QuestionGameUpdateEnvelopeLabel();
            QuestionGameLockAllButtons();
            QuestionGameNextQuestionBtn.Enabled = true;

            _soundManager.StopAllQueues();

            MessageBox.Show(
                text: "Pytanie zostało wycofane. Jeżeli jest taka potrzeba, możesz je wymienić w edytorze pytań.",
                caption: GameConstants.PROGRAM_TITLE,
                buttons: MessageBoxButtons.OK,
                icon: MessageBoxIcon.Information);
        }

        private void QuestionGameOpenEnvelopeButton_Click(object sender, EventArgs e)
        {
            var gameStateInstance = GameState.Instance;
            Envelope? envelope = gameStateInstance.EnvelopePlayedFor ?? throw new InvalidOperationException("No envelope selected!");
            envelope.Open();

            QuestionGameUpdateEnvelopeLabel();
            gameStateInstance.RefreshEnvelopes();

            QuestionGameOpenEnvelopeButton.Enabled = false;
        }

        private void QuestionGamePreviousEnvelopeBtn_Click(object sender, EventArgs e)
        {
            --_questionGameEnvelopeIndex;
            QuestionGameUpdateEnvelopeLabel();
            QuestionGameUpdateEnvelopeButtons();
        }

        private void QuestionGameNextEnvelopeBtn_Click(object sender, EventArgs e)
        {
            ++_questionGameEnvelopeIndex;
            QuestionGameUpdateEnvelopeLabel();
            QuestionGameUpdateEnvelopeButtons();
        }

        private void QuestionGameStartTradingBtn_Click(object sender, EventArgs e)
        {
            QuestionGameLockAllButtons();
            QuestionGameStartTradingBtn.Enabled = false;

            var gameStateInstance = GameState.Instance;
            if (gameStateInstance.ContestantEnvelopeSet.IsEmpty)
            {
                TradingGameOver(bigWin: false);
            }
            else
            {
                // Start trading
                gameStateInstance.StartTrading();
                TradingStartScreen();
                DirectorTabControl.SelectTab("GameTradingTab");
            }
        }

        #endregion

        #region Game - Trading

        private void TradingStartScreen()
        {
            _tradingContestantPage = 0;
            _tradingHostPage = 0;

            _tradingAreContestantCheckboxesChanging = false;
            _tradingAreHostCheckboxesChanging = false;

            TradingOfferTextBox.Text = Utils.AmountToString(amount: 0);
            TradingUnlockButtons();
            TradingUpdateEnvelopes();
            TradingStartPlayingMusic();

            TradingUpdateContestantPages();
            TradingUpdateHostPages();
        }

        private void TradingStartPlayingMusic()
        {
            var backgroundLoop = new Sound("snd/trading-background-loop.wav", _volumeLevel);
            backgroundLoop.SetInfiniteLoop();

            _soundManager.StopAllQueues();
            _soundManager.CreateQueue(QUEUE_NAME_TRADING_BACKGROUND);
            _soundManager.AddSoundToQueue(QUEUE_NAME_TRADING_BACKGROUND, backgroundLoop);
            _soundManager.PlayQueue(QUEUE_NAME_TRADING_BACKGROUND);
        }

        private void TradingPlayOfferPing()
        {
            PlaySingleSound(filePath: "snd/trading-offer-ping.wav");
        }

        private void TradingPlayUpdateSound()
        {
            PlaySingleSound(filePath: "snd/trading-update.wav");
        }

        private void TradingPlayShredderSoundAndUnlockButtons()
        {
            var shredderSound = new Sound(filePath: "snd/envelope-destroyed.wav", _volumeLevel);
            shredderSound.EventStoppedPlayback += (s, e) =>
            {
                var gameStateInstance = GameState.Instance;
                gameStateInstance.RemoveDestroyedEnvelopes();
                gameStateInstance.RefreshEnvelopes();

                TradingClearCheckboxes();
                TradingUpdateEnvelopes();
                TradingUpdateCashLabels();
                TradingUpdatePages();

                TradingUnlockButtons();

                _soundManager.ResumeQueue(QUEUE_NAME_TRADING_BACKGROUND);
            };

            _soundManager.PauseQueue(QUEUE_NAME_TRADING_BACKGROUND);
            _soundManager.PlaySingleSound(shredderSound);
        }

        private void TradingGameOver(bool bigWin = false)
        {
            // Music
            var bigWinSound = new Sound("snd/big-win.wav", _volumeLevel);
            var gameOverSound = new Sound("snd/game-over.wav", _volumeLevel);

            _soundManager.StopAllQueues();
            _soundManager.CreateQueue(QUEUE_NAME_GAME_OVER);

            if (bigWin)
                _soundManager.AddSoundToQueue(QUEUE_NAME_GAME_OVER, bigWinSound);

            _soundManager.AddSoundToQueue(QUEUE_NAME_GAME_OVER, gameOverSound);
            _soundManager.PlayQueue(QUEUE_NAME_GAME_OVER);

            // Game Over
            GameState.Instance.GameOver();
            GameOverUnlockButtons();
            DirectorTabControl.SelectTab("GameOverTab");
        }

        private void TradingUnlockButtons()
        {
            var gameStateInstance = GameState.Instance;
            int contestantEnvelopeCount = gameStateInstance.ContestantEnvelopeSet.EnvelopeCount;
            int hostEnvelopeCount = gameStateInstance.HostEnvelopeSet.EnvelopeCount;

            int contestantOffset = _tradingContestantPage * TRADING_SCREEN_MAX_ON_PAGE;
            int hostOffset = _tradingHostPage * TRADING_SCREEN_MAX_ON_PAGE;
            for (int i = 0; i < TRADING_SCREEN_MAX_ON_PAGE; ++i)
            {
                _tradingContestantCheckboxes[i].Enabled = (i + contestantOffset) < contestantEnvelopeCount;
                _tradingContestantCheckboxes[i].Checked = false;

                _tradingHostCheckboxes[i].Enabled = (i + hostOffset) < hostEnvelopeCount;
                _tradingHostCheckboxes[i].Checked = false;
            }

            TradingOfferTextBox.Enabled = true;

            TradingPresentOfferBtn.Enabled = true;
            TradingBringMoneyBtn.Enabled = true;
            TradingClearOfferBtn.Enabled = true;
            TradingAcceptOfferBtn.Enabled = true;
            TradingOpenCloseEnvelopeBtn.Enabled = true;
            TradingShredderBtn.Enabled = true;
            TradingEndGameNormalBtn.Enabled = true;
            TradingEndGameFireworksBtn.Enabled = true;

            TradingUpdateCashLabels();
        }

        private void TradingLockButtons()
        {
            for (int i = 0; i < TRADING_SCREEN_MAX_ON_PAGE; ++i)
            {
                _tradingContestantCheckboxes[i].Enabled = false;
                _tradingContestantCheckboxes[i].Checked = false;

                _tradingHostCheckboxes[i].Enabled = false;
                _tradingHostCheckboxes[i].Checked = false;
            }

            TradingOfferTextBox.Enabled = false;

            TradingPresentOfferBtn.Enabled = false;
            TradingBringMoneyBtn.Enabled = false;
            TradingClearOfferBtn.Enabled = false;
            TradingAcceptOfferBtn.Enabled = false;
            TradingOpenCloseEnvelopeBtn.Enabled = false;
            TradingShredderBtn.Enabled = false;
            TradingEndGameNormalBtn.Enabled = false;
            TradingEndGameFireworksBtn.Enabled = false;

            TradingContestantPreviousPageButton.Enabled = false;
            TradingContestantNextPageButton.Enabled = false;
            TradingContestantAddEnvelopeButton.Enabled = false;

            TradingHostPreviousPageButton.Enabled = false;
            TradingHostNextPageButton.Enabled = false;
            TradingHostAddEnvelopeButton.Enabled = false;
        }

        private void TradingUpdateContestantEnvelopes()
        {
            var gameStateInstance = GameState.Instance;

            int envelopeCount = gameStateInstance.ContestantEnvelopeSet.EnvelopeCount;
            TradingContestantEnvelopeCountLbl.Text = string.Format("Kopert: {0}", envelopeCount);
            TradingContestantEnvelopePageNumberLbl.Text = string.Format("Strona {0}/{1}", _tradingContestantPage + 1, TradingContestantMaxPages);

            int offset = _tradingContestantPage * TRADING_SCREEN_MAX_ON_PAGE;

            _tradingAreContestantCheckboxesChanging = true;
            for (int i = 0; i < TRADING_SCREEN_MAX_ON_PAGE; ++i)
            {
                Envelope? envelope = gameStateInstance.GetContestantEnvelope(i + offset);
                if (envelope is null || envelope.State == EnvelopeState.DESTROYED)
                {
                    _tradingContestantEnvelopeLabels[i].BackColor = Color.White;
                    _tradingContestantEnvelopeLabels[i].Text = string.Empty;

                    _tradingContestantCheckboxes[i].Checked = false;
                    _tradingContestantCheckboxes[i].Enabled = false;
                }
                else
                {
                    EnvelopeColorCollection colorPairing = envelope.GetColorsForScreen();

                    _tradingContestantEnvelopeLabels[i].BackColor = colorPairing.BackgroundColor;
                    _tradingContestantEnvelopeLabels[i].Text = envelope.GetEnvelopeValueForDirector();

                    _tradingContestantCheckboxes[i].Checked = envelope.TradingCheckbox;
                    _tradingContestantCheckboxes[i].Enabled = true;
                }
            }
            _tradingAreContestantCheckboxesChanging = false;
        }

        private void TradingUpdateHostEnvelopes()
        {
            var gameStateInstance = GameState.Instance;

            int envelopeCount = gameStateInstance.HostEnvelopeSet.EnvelopeCount;
            TradingHostEnvelopeCountLbl.Text = string.Format("Kopert: {0}", envelopeCount);
            TradingHostEnvelopePageNumberLbl.Text = string.Format("Strona {0}/{1}", _tradingHostPage + 1, TradingHostMaxPages);

            int offset = _tradingHostPage * TRADING_SCREEN_MAX_ON_PAGE;

            _tradingAreHostCheckboxesChanging = true;
            for (int i = 0; i < TRADING_SCREEN_MAX_ON_PAGE; ++i)
            {
                Envelope? envelope = gameStateInstance.GetHostEnvelope(i + offset);
                if (envelope is null || envelope.State == EnvelopeState.DESTROYED)
                {
                    _tradingHostEnvelopeLabels[i].BackColor = Color.White;
                    _tradingHostEnvelopeLabels[i].Text = string.Empty;

                    _tradingHostCheckboxes[i].Checked = false;
                    _tradingHostCheckboxes[i].Enabled = false;
                }
                else
                {
                    EnvelopeColorCollection colorPairing = envelope.GetColorsForScreen();

                    _tradingHostEnvelopeLabels[i].BackColor = colorPairing.BackgroundColor;
                    _tradingHostEnvelopeLabels[i].Text = envelope.GetEnvelopeValueForDirector();

                    _tradingHostCheckboxes[i].Checked = envelope.TradingCheckbox;
                    _tradingHostCheckboxes[i].Enabled = true;
                }
            }

            _tradingAreHostCheckboxesChanging = false;
        }

        public void TradingUpdateEnvelopes()
        {
            TradingUpdateContestantPages();
            TradingUpdateContestantEnvelopes();

            TradingUpdateHostPages();
            TradingUpdateHostEnvelopes();
        }

        public void TradingClearContestantCheckboxes()
        {
            _tradingAreContestantCheckboxesChanging = true;

            for (int i = 0; i < TRADING_SCREEN_MAX_ON_PAGE; ++i)
            {
                _tradingContestantCheckboxes[i].Checked = false;
            }

            foreach (Envelope envelope in GameState.Instance.ContestantEnvelopeSet.Envelopes)
            {
                envelope.TradingCheckbox = false;
            }

            _tradingAreContestantCheckboxesChanging = false;
        }

        public void TradingClearHostCheckboxes()
        {
            _tradingAreHostCheckboxesChanging = true;

            for (int i = 0; i < TRADING_SCREEN_MAX_ON_PAGE; ++i)
            {
                _tradingHostCheckboxes[i].Checked = false;
            }

            foreach (Envelope envelope in GameState.Instance.HostEnvelopeSet.Envelopes)
            {
                envelope.TradingCheckbox = false;
            }

            _tradingAreHostCheckboxesChanging = false;
        }

        public void TradingClearCheckboxes()
        {
            TradingClearContestantCheckboxes();
            TradingClearHostCheckboxes();
        }

        private void TradingContestantEnvelopeChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is not CheckBox checkBox)
                throw new ArgumentException("Sender is not a checkbox.", nameof(sender));

            if (_tradingAreContestantCheckboxesChanging)
                return;

            int pageOffset = _tradingContestantPage * TRADING_SCREEN_MAX_ON_PAGE;
            int tag = (int)(checkBox.Tag ?? throw new InvalidOperationException("This checkbox's tag is missing or incorrect."));

            Envelope? envelope = GameState.Instance.GetContestantEnvelope(pageOffset + tag);
            if (envelope is null)
                throw new InvalidOperationException("This envelope should not be available.");

            envelope.TradingCheckbox = checkBox.Checked;
        }

        private void TradingHostEnvelopeChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is not CheckBox checkBox)
                throw new ArgumentException("Sender is not a checkbox.", nameof(sender));

            if (_tradingAreHostCheckboxesChanging)
                return;

            int pageOffset = _tradingHostPage * TRADING_SCREEN_MAX_ON_PAGE;
            int tag = (int)(checkBox.Tag ?? throw new InvalidOperationException("This checkbox's tag is missing or incorrect."));

            Envelope? envelope = GameState.Instance.GetHostEnvelope(pageOffset + tag);
            if (envelope is null)
                throw new InvalidOperationException("This envelope should not be available.");

            envelope.TradingCheckbox = checkBox.Checked;
        }

        public void TradingUpdateCashLabels()
        {
            var gameStateInstance = GameState.Instance;

            decimal cashWhenAccepted = gameStateInstance.ContestantCash + gameStateInstance.CashOffer;

            decimal currentPrize = EnvelopeCalculator.CalculateFinalPrize(gameStateInstance.GameSettings,
                gameStateInstance.ContestantEnvelopeSet.Envelopes,
                gameStateInstance.ContestantCash);

            TradingCashLbl.Text = Utils.AmountToString(gameStateInstance.ContestantCash);
            TradingCashWhenAcceptedLbl.Text = Utils.AmountToString(cashWhenAccepted);
            TradingCurrentPrizeLbl.Text = Utils.AmountToString(currentPrize);
        }

        public void TradingUpdateContestantPages()
        {
            _tradingContestantPage = Utils.Clamp(_tradingContestantPage, min: 0, max: TradingContestantMaxPageIndex);

            TradingContestantPreviousPageButton.Enabled = _tradingContestantPage > 0;
            TradingContestantNextPageButton.Enabled = _tradingContestantPage < TradingContestantMaxPageIndex;
            TradingContestantAddEnvelopeButton.Enabled = GameState.Instance.ContestantEnvelopeSet.EnvelopeCount < GameConstants.MAX_ENVELOPE_COUNT_PERSON;
        }

        public void TradingUpdateHostPages()
        {
            _tradingHostPage = Utils.Clamp(_tradingHostPage, min: 0, max: TradingHostMaxPageIndex);

            TradingHostPreviousPageButton.Enabled = _tradingHostPage > 0;
            TradingHostNextPageButton.Enabled = _tradingHostPage < TradingHostMaxPageIndex;
            TradingHostAddEnvelopeButton.Enabled = GameState.Instance.HostEnvelopeSet.EnvelopeCount < GameConstants.MAX_ENVELOPE_COUNT_PERSON;
        }

        public void TradingUpdatePages()
        {
            TradingUpdateContestantPages();
            TradingUpdateHostPages();
        }

        private void TradingContestantPreviousPageButton_Click(object sender, EventArgs e)
        {
            --_tradingContestantPage;

            TradingUpdateContestantEnvelopes();
            TradingUpdateContestantPages();
        }

        private void TradingContestantNextPageButton_Click(object sender, EventArgs e)
        {
            ++_tradingContestantPage;

            TradingUpdateContestantEnvelopes();
            TradingUpdateContestantPages();
        }

        private void TradingContestantAddEnvelopeButton_Click(object sender, EventArgs e)
        {
            var envelopeForm = new AddEnvelopeForm();
            envelopeForm.Reset(TradingSide.Contestant);
            envelopeForm.ShowDialog();

            if (envelopeForm.SelectedEnvelope is not null)
            {
                var gameStateInstance = GameState.Instance;

                gameStateInstance.ContestantEnvelopeSet.AddEnvelope(envelopeForm.SelectedEnvelope);
                gameStateInstance.ContestantEnvelopeSet.SortByEnvelopeNumbers();

                gameStateInstance.EnvelopeTable.DeleteEnvelope(envelopeForm.SelectedEnvelope);
                gameStateInstance.RefreshEnvelopes();

                Program.DirectorForm.TradingUpdateContestantEnvelopes();
                Program.DirectorForm.TradingUpdateCashLabels();
                Program.DirectorForm.TradingClearContestantCheckboxes();
                Program.DirectorForm.TradingUpdateContestantPages();

                PlaySingleSound(filePath: "snd/envelope-selection-ping.wav");
            }
        }

        private void TradingHostPreviousPageButton_Click(object sender, EventArgs e)
        {
            --_tradingHostPage;

            TradingUpdateHostEnvelopes();
            TradingUpdateHostPages();
        }

        private void TradingHostNextPageButton_Click(object sender, EventArgs e)
        {
            ++_tradingHostPage;

            TradingUpdateHostEnvelopes();
            TradingUpdateHostPages();
        }

        private void TradingHostAddEnvelopeButton_Click(object sender, EventArgs e)
        {
            var envelopeForm = new AddEnvelopeForm();
            envelopeForm.Reset(TradingSide.Host);
            envelopeForm.ShowDialog();

            if (envelopeForm.SelectedEnvelope is not null)
            {
                var gameStateInstance = GameState.Instance;

                gameStateInstance.HostEnvelopeSet.AddEnvelope(envelopeForm.SelectedEnvelope);
                gameStateInstance.HostEnvelopeSet.SortByEnvelopeNumbers();

                gameStateInstance.EnvelopeTable.DeleteEnvelope(envelopeForm.SelectedEnvelope);
                gameStateInstance.RefreshEnvelopes();

                Program.DirectorForm.TradingUpdateHostEnvelopes();
                Program.DirectorForm.TradingUpdateCashLabels();
                Program.DirectorForm.TradingClearHostCheckboxes();
                Program.DirectorForm.TradingUpdateHostPages();

                PlaySingleSound(filePath: "snd/envelope-selection-ping.wav");
            }
        }

        private void TradingPresentOfferBtn_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(TradingOfferTextBox.Text, out decimal cashOffer))
            {
                ShowErrorMessage(message: "Kwota oferty nie jest liczbą!");
                return;
            }

            var gameStateInstance = GameState.Instance;

            if (!gameStateInstance.CanSetAmountAsOffer(cashOffer))
            {
                ShowErrorMessage(message: "Oferta nie może sprawić, że gotówka zawodnika będzie ujemna.");
                return;
            }

            foreach (Envelope envelope in gameStateInstance.ContestantEnvelopeSet.Envelopes)
            {
                if (envelope.TradingCheckbox)
                    envelope.MarkForTrade();
                else
                    envelope.MarkAsNeutral();
            }

            foreach (Envelope envelope in gameStateInstance.HostEnvelopeSet.Envelopes)
            {
                if (envelope.TradingCheckbox)
                    envelope.MarkForTrade();
                else
                    envelope.MarkAsNeutral();
            }

            if (!gameStateInstance.CanSetEnvelopesAsOffer())
            {
                ShowErrorMessage(
                    string.Format(
                        "Każda strona może mieć nie więcej niż {0} kopert. Po przyjęciu tej oferty jedna ze stron miałaby więcej, więc takiej oferty nie można złożyć.",
                        Constants.GameConstants.MAX_ENVELOPE_COUNT_PERSON));

                gameStateInstance.ClearOffer();
                TradingClearCheckboxes();
                TradingUpdateEnvelopes();
                return;
            }

            gameStateInstance.SetCashOffer(cashOffer);

            TradingUpdateEnvelopes();
            TradingUpdateCashLabels();

            gameStateInstance.RefreshOffer();

            TradingPlayOfferPing();
        }

        private void TradingPlayBringMoneySound()
        {
            var bringMoneySound = new Sound("snd/trading-bring-money.wav", _volumeLevel);
            bringMoneySound.EventStoppedPlayback += (s, e) =>
            {
                _soundManager.ResumeQueue(QUEUE_NAME_TRADING_BACKGROUND);
            };

            _soundManager.PauseQueue(QUEUE_NAME_TRADING_BACKGROUND);
            _soundManager.PlaySingleSound(bringMoneySound);
        }

        private void TradingBringMoneyBtn_Click(object sender, EventArgs e)
        {
            TradingPlayBringMoneySound();
        }

        private void TradingClearOfferBtn_Click(object sender, EventArgs e)
        {
            GameState.Instance.ClearOffer();
            TradingOfferTextBox.Text = Utils.AmountToString(amount: 0);

            TradingClearCheckboxes();
            TradingUpdateEnvelopes();
            TradingUpdateCashLabels();
        }

        private void TradingAcceptOfferBtn_Click(object sender, EventArgs e)
        {
            GameState.Instance.AcceptOffer();
            TradingOfferTextBox.Text = Utils.AmountToString(amount: 0);

            TradingClearCheckboxes();
            TradingUpdateEnvelopes();
            TradingUpdateCashLabels();

            TradingPlayUpdateSound();
        }

        private void TradingOpenCloseEnvelopeBtn_Click(object sender, EventArgs e)
        {
            var gameStateInstance = GameState.Instance;

            // Open/Close Contestant Envelopes
            foreach (Envelope envelope in gameStateInstance.ContestantEnvelopeSet.Envelopes)
            {
                if (envelope.TradingCheckbox)
                    envelope.ToggleOpenClose();
            }

            // Open/Close Host Envelopes
            foreach (Envelope envelope in gameStateInstance.HostEnvelopeSet.Envelopes)
            {
                if (envelope.TradingCheckbox)
                    envelope.ToggleOpenClose();
            }

            gameStateInstance.RefreshEnvelopes();
            TradingUpdateEnvelopes();
        }

        private bool TradingIsShredderWarningNecessary()
        {
            var gameStateInstance = GameState.Instance;

            bool isAnyContestantEnvelopeChecked =
                gameStateInstance.ContestantEnvelopeSet.Envelopes.Any(envelope => envelope.TradingCheckbox);
            bool isAnyHostEnvelopeChecked =
                gameStateInstance.HostEnvelopeSet.Envelopes.Any(envelope => envelope.TradingCheckbox);

            if (isAnyContestantEnvelopeChecked && isAnyHostEnvelopeChecked)
                return true;

            for (int i = 0; i < TradingContestantMaxPageIndex; ++i)
            {
                if (i == _tradingContestantPage)
                    continue;

                int pageOffset = i * TRADING_SCREEN_MAX_ON_PAGE;

                for (int j = 0; j < TRADING_SCREEN_MAX_ON_PAGE; ++j)
                {
                    Envelope? envelope = gameStateInstance.GetContestantEnvelope(i + pageOffset);
                    if (envelope is not null)
                    {
                        if (envelope.TradingCheckbox && envelope.State != EnvelopeState.DESTROYED)
                            return true;
                    }
                }
            }

            for (int i = 0; i < TradingHostMaxPageIndex; ++i)
            {
                if (i == _tradingHostPage)
                    continue;

                int pageOffset = i * TRADING_SCREEN_MAX_ON_PAGE;

                for (int j = 0; j < TRADING_SCREEN_MAX_ON_PAGE; ++j)
                {
                    Envelope? envelope = gameStateInstance.GetHostEnvelope(i + pageOffset);
                    if (envelope is not null)
                    {
                        if (envelope.TradingCheckbox && envelope.State != EnvelopeState.DESTROYED)
                            return true;
                    }
                }
            }

            return false;
        }

        private void TradingShredderBtn_Click(object sender, EventArgs e)
        {
            if (TradingIsShredderWarningNecessary())
            {
                DialogResult destructionResult = MessageBox.Show(
                    text: "Masz zaznaczone koperty po obu stronach lub koperty, których obecnie nie widzisz! Na pewno chcesz usuwać?",
                    GameConstants.PROGRAM_TITLE,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (destructionResult == DialogResult.No)
                    return;
            }

            var gameStateInstance = GameState.Instance;

            // Destroy Contestant Envelopes
            foreach (Envelope envelope in gameStateInstance.ContestantEnvelopeSet.Envelopes)
            {
                if (envelope.TradingCheckbox)
                    envelope.MarkAsDestroyed();
            }

            // Destroy Host Envelopes
            foreach (Envelope envelope in gameStateInstance.HostEnvelopeSet.Envelopes)
            {
                if (envelope.TradingCheckbox)
                    envelope.MarkAsDestroyed();
            }

            TradingLockButtons();
            TradingPlayShredderSoundAndUnlockButtons();

            gameStateInstance.RefreshEnvelopes();
        }

        private void TradingEndGameNormalBtn_Click(object sender, EventArgs e)
        {
            TradingGameOver(bigWin: false);
        }

        private void TradingEndGameFireworksBtn_Click(object sender, EventArgs e)
        {
            TradingGameOver(bigWin: true);
        }

        #endregion

        #region Game Over

        private void GameOverUnlockButtons()
        {
            GameOverPrizeTxtBox.Text = GameState.Instance.FinalPrize;

            GameOverPrizeTxtBox.Enabled = true;
            GameOverRefreshBtn.Enabled = true;
            GameOverBringBackBtn.Enabled = true;
            GameOverMusicBtn.Enabled = true;
            GameOverRestartBtn.Enabled = true;
        }

        private void GameOverLockButtons()
        {
            GameOverPrizeTxtBox.Clear();

            GameOverPrizeTxtBox.Enabled = false;
            GameOverRefreshBtn.Enabled = false;
            GameOverBringBackBtn.Enabled = false;
            GameOverMusicBtn.Enabled = false;
            GameOverRestartBtn.Enabled = false;
        }

        private void GameOverPrizeTxtBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GameOverPrizeTxtBox.Text))
            {
                GameOverPrizeTxtBox.BackColor = Color.Red;
                GameOverPrizeTxtBox.ForeColor = Color.White;
                GameOverRefreshBtn.Enabled = false;
            }
            else
            {
                GameOverPrizeTxtBox.BackColor = Color.White;
                GameOverPrizeTxtBox.ForeColor = Color.Black;
                GameOverRefreshBtn.Enabled = true;
            }
        }

        private void GameOverRefreshBtn_Click(object sender, EventArgs e)
        {
            GameState.Instance.RefreshGameOver(GameOverPrizeTxtBox.Text);
        }

        private void GameOverBringBackBtn_Click(object sender, EventArgs e)
        {
            GameOverPrizeTxtBox.Text = GameState.Instance.FinalPrize.Trim();
        }

        private void GameOverMusicBtn_Click(object sender, EventArgs e)
        {
            GameState.Instance.ClearEverything();

            _soundManager.StopAllQueues();
            PlaySingleSound(filePath: "snd/outro.wav");
        }

        private void GameOverRestartBtn_Click(object sender, EventArgs e)
        {
            GameState.Instance.ContestantEnvelopeSet.ClearEnvelopeList();
            GameState.Instance.HostEnvelopeSet.ClearEnvelopeList();
            GameState.Instance.ClearEverything();

            PreShowPrepare();
            GameOverLockButtons();
            DirectorTabControl.SelectTab("BeforeTheShowTab");
        }

        #endregion
    }
}
