using CluelessControl.Cheques;
using CluelessControl.Envelopes;
using CluelessControl.Questions;
using CluelessControl.Sounds;

namespace CluelessControl
{
    public partial class DirectorForm : Form
    {
        #region Other Forms
        private static readonly HostScreenForm _hostScreenForm = new();
        private static readonly ContestantScreenForm _contestantScreenForm = new();
        private static readonly TVScreenForm _tvScreenForm = new();
        #endregion

        private const int NO_ITEM_INDEX = -1;

        #region Sound
        private int _volumeLevel = 100;
        private SoundManager _soundManager = new();
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
        private readonly Dictionary<TextBox, Label> _envelopeSelectTxtBoxesAndLabels = [];
        #endregion

        #region Question Game Screen
        private readonly Dictionary<int, Label> _questionGameAnswerLabels;
        private int _questionGameEnvelopeIndex;
        #endregion

        #region Trading Screen
        private readonly Label[] _tradingContestantEnvelopeLabels = new Label[Constants.MAX_ENVELOPE_POSSIBLE_COUNT];
        private readonly Label[] _tradingHostEnvelopeLabels = new Label[Constants.MAX_ENVELOPE_POSSIBLE_COUNT];

        private readonly CheckBox[] _tradingContestantCheckboxes = new CheckBox[Constants.MAX_ENVELOPE_POSSIBLE_COUNT];
        private readonly CheckBox[] _tradingHostCheckboxes = new CheckBox[Constants.MAX_ENVELOPE_POSSIBLE_COUNT];
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
            ShowAllForms();
        }

        private static void ShowAllForms()
        {
            _hostScreenForm.Show();
            _contestantScreenForm.Show();
            _tvScreenForm.Show();
        }

        private void PrepareEnvelopeSelectionBoxes()
        {
            for (int i = 0; i < Constants.MAX_ENVELOPE_POSSIBLE_COUNT; ++i)
            {
                string numberName = string.Format("EnvelopeSelectionNum{0}TxtBox", i);
                string valueName = string.Format("EnvelopeSelectionContent{0}Lbl", i);

                TextBox numberControl = (TextBox?)Controls.Find(numberName, searchAllChildren: true).First() ?? throw new MissingMemberException(this.Name, numberName);
                Label valueControl = (Label?)Controls.Find(valueName, searchAllChildren: true).First() ?? throw new MissingMemberException(this.Name, valueName);

                _envelopeSelectTxtBoxesAndLabels.Add(numberControl, valueControl);
            }
        }

        private void PrepareTradingEnvelopeBoxes()
        {
            for (int i = 0; i < Constants.MAX_ENVELOPE_POSSIBLE_COUNT; ++i)
            {
                string contestantEnvelopeName = string.Format("TradingContestantEnvelope{0}Lbl", i);
                string hostEnvelopeName = string.Format("TradingHostEnvelope{0}Lbl", i);
                string contestantCheckBoxName = string.Format("TradingContestantEnvelope{0}ChkBox", i);
                string hostCheckBoxName = string.Format("TradingHostEnvelope{0}ChkBox", i);

                Label contestantControl = (Label)Controls.Find(contestantEnvelopeName, searchAllChildren: true).First() ?? throw new MissingMemberException(this.Name, contestantEnvelopeName);
                Label hostControl = (Label)Controls.Find(hostEnvelopeName, searchAllChildren: true).First() ?? throw new MissingMemberException(this.Name, hostEnvelopeName);

                CheckBox contestantCheckBox = (CheckBox) Controls.Find(contestantCheckBoxName, searchAllChildren: true).First() ?? throw new MissingMemberException(this.Name, contestantCheckBoxName);
                CheckBox hostCheckBox = (CheckBox)Controls.Find(hostCheckBoxName, searchAllChildren: true).First() ?? throw new MissingMemberException(this.Name, hostCheckBoxName);

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
                    Constants.PROGRAM_TITLE,
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

            _volumeLevel = VolumeTrackBar.Value;
            _soundManager.SetVolume(_volumeLevel / 100.0f);
            VolumeLabel.Text = string.Format("Głośność: {0}%", _volumeLevel);
        }

        private void MuteCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (MuteCheckBox.Checked)
            {
                _volumeLevel = 0;
            }
            else
            {
                _volumeLevel = VolumeTrackBar.Value;
            }
            
            _soundManager.SetVolume(_volumeLevel / 100.0f);
            VolumeLabel.Text = string.Format("Głośność: {0}%", _volumeLevel);
        }

        #endregion

        #region Messages
        public void ShowErrorMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            MessageBox.Show(message, Constants.PROGRAM_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowOkMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            MessageBox.Show(message, Constants.PROGRAM_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region PreShow

        private void PreShowIntroBtn_Click(object sender, EventArgs e)
        {
            ;
        }

        private void PreShowStartGameBtn_Click(object sender, EventArgs e)
        {
            var gameState = GameState.Instance;
            if (gameState.ChequeSettings.ChequeList.Count < Constants.MAX_ENVELOPES_COUNT || gameState.QuestionSet.QuestionList.Count < gameState.GameSettings.StartEnvelopeCount)
            {
                string message = string.Format(
                    "Musisz najpierw wczytać koperty (min. {0} kopert) i zestaw pytań (min. {1} pytań)!",
                    Constants.MAX_ENVELOPES_COUNT,
                    gameState.GameSettings.StartEnvelopeCount);

                ShowErrorMessage(message);
                return;
            }

            gameState.NewGame();

            EnvelopeSelectionUnlockButtons();
            DirectorTabControl.SelectTab("GamePickEnvelopesTab");
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
                    throw new InvalidOperationException("No minus radio checked.");

                bool onlyBestPlusCounts;
                if (SettingsPlusPercentBestRadio.Checked)
                    onlyBestPlusCounts = true;
                else if (SettingsPlusPercentAllRadio.Checked)
                    onlyBestPlusCounts = false;
                else
                    throw new InvalidOperationException("No plus radio checked.");

                Color tvBackgroundColor = SettingsTVBackgroundColorPicture.BackColor;

                result = GameSettings.Create(startEnvelopeCount, decimalPlaces, onlyWorstMinusCounts, onlyBestPlusCounts, tvBackgroundColor);
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

            SettingsTVBackgroundColorPicture.BackColor = gameSettings.TVBackgroundColor;
            _tvScreenForm.ChangeBackgroundColor(gameSettings.TVBackgroundColor);
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

        private void SettingsTVBackgroundColorPicture_Click(object sender, EventArgs e)
        {
            if (SettingsTVBackgroundColorDialog.ShowDialog() != DialogResult.OK)
                return;

            SettingsTVBackgroundColorPicture.BackColor = SettingsTVBackgroundColorDialog.Color;
        }

        private void SettingsLoadFromMemoryBtn_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                text: "Jesteś pewny? Niezapisane ustawienia przepadną!",
                Constants.PROGRAM_TITLE,
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
                _tvScreenForm.ChangeBackgroundColor(temp.TVBackgroundColor);

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
                Constants.PROGRAM_TITLE,
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
            bool isItemSelected = selectedIndex > -1;

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

            if (decimal.TryParse(EnvelopeSettingsPercentageTxtBox.Text, out decimal percentage) || percentage < Constants.MIN_PERCENTAGE)
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
                        Constants.PROGRAM_TITLE,
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
                Constants.PROGRAM_TITLE,
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
                Constants.PROGRAM_TITLE,
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
                caption: Constants.PROGRAM_TITLE,
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

            QuestionEditorMoveUpBtn.Enabled = selectedIndex > 0 && selectedIndex < questionCount;
            QuestionEditorMoveDownBtn.Enabled = selectedIndex > -1 && selectedIndex < questionCount - 1;
            QuestionEditorNewBtn.Enabled = true;
            QuestionEditorDeleteBtn.Enabled = selectedIndex > -1;
            QuestionEditorClearBtn.Enabled = questionCount > 0;
            QuestionEditorSaveBtn.Enabled = selectedIndex > -1;
            QuestionEditorLoadFromFileBtn.Enabled = true;
            QuestionEditorSaveToFileBtn.Enabled = questionCount > 0;
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
                        Constants.PROGRAM_TITLE,
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
                Constants.PROGRAM_TITLE,
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
                Constants.PROGRAM_TITLE,
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
                    caption: Constants.PROGRAM_TITLE,
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

        private void EnvelopeSelectionUnlockButtons()
        {
            // Unlock the first envelope
            EnvelopeSelectionNum0TxtBox.Enabled = true;

            // Unlock the confirmation button
            EnvelopeSelectionConfirmBtn.Enabled = true;
        }

        private void EnvelopeSelectionLockButtons()
        {
            // Lock all unlocked buttons
            EnvelopeSelectionConfirmBtn.Enabled = false;
            EnvelopeSelectionRetractBtn.Enabled = false;
            EnvelopeSelectionNextPartBtn.Enabled = false;
        }

        private void EnvelopeSelectionUpdateAvailability()
        {
            var gameStateInstance = GameState.Instance;
            int envelopeCount = gameStateInstance.ContestantEnvelopeSet.EnvelopeCount;
            int startEnvelopeCount = gameStateInstance.GameSettings.StartEnvelopeCount;

            TextBox[] numberTextBoxes = _envelopeSelectTxtBoxesAndLabels.Keys.ToArray();

            for (int i = 0; i < numberTextBoxes.Length; ++i)
            {
                numberTextBoxes[i].Enabled = (i == envelopeCount);
            }

            if (envelopeCount < startEnvelopeCount)
            {
                numberTextBoxes[envelopeCount].Focus();
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
            Label valueLabel = _envelopeSelectTxtBoxesAndLabels[numberBox];

            // If the textbox is empty, then clear the value label
            if (string.IsNullOrEmpty(numberBox.Text))
            {
                valueLabel.Text = string.Empty;
                return;
            }

            var envelopeTable = GameState.Instance.EnvelopeTable;

            // Check for errors
            string errorMessage = string.Empty;
            if (!int.TryParse(numberBox.Text, out int envelopeNumber))
            {
                errorMessage = "Podany numer koperty nie jest liczbą!";
            }
            else if (envelopeNumber < Constants.MIN_ENVELOPE_NUMBER || envelopeNumber > Constants.MAX_ENVELOPE_NUMBER)
            {
                errorMessage = string.Format("Numer koperty musi być z przedziału {0}...{1}!", Constants.MIN_ENVELOPE_NUMBER, Constants.MAX_ENVELOPE_NUMBER);
            }
            else if (!envelopeTable.IsEnvelopePresent(envelopeNumber))
            {
                errorMessage = "Koperta już wybrana wcześniej!";
            }

            // If there's no error message, display the correct envelope value
            // If there is an error, display it
            if (string.IsNullOrEmpty(errorMessage))
            {
                Envelope envelope = envelopeTable.GetEnvelope(envelopeNumber);

                valueLabel.Text = envelope.Cheque.ToValueString();
                valueLabel.ForeColor = Color.Black;
            }
            else
            {
                valueLabel.Text = errorMessage;
                valueLabel.ForeColor = Color.Red;
            }
        }

        private void EnvelopeSelectionConfirmBtn_Click(object sender, EventArgs e)
        {
            var gameStateInstance = GameState.Instance;
            var contestantEnvelopes = gameStateInstance.ContestantEnvelopeSet;
            int index = contestantEnvelopes.EnvelopeCount;

            // Get the correct textbox
            TextBox[] numberTextBoxes = _envelopeSelectTxtBoxesAndLabels.Keys.ToArray();

            // Check if the envelope number is valid
            if (!int.TryParse(numberTextBoxes[index].Text, out int envelopeNumber) ||
                envelopeNumber < Constants.MIN_ENVELOPE_NUMBER || envelopeNumber > Constants.MAX_ENVELOPE_NUMBER)
            {
                string message = string.Format("Numer koperty musi być liczbą [{0}...{1}].", Constants.MIN_ENVELOPE_NUMBER, Constants.MAX_ENVELOPE_NUMBER);
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
            envelopeTable.DeleteEnvelope(newEnvelope);
            gameStateInstance.AddContestantEnvelope(newEnvelope);

            // Update button availability
            EnvelopeSelectionUpdateAvailability();
        }

        private void EnvelopeSelectionRetractBtn_Click(object sender, EventArgs e)
        {
            var gameStateInstance = GameState.Instance;
            var contestantEnvelopes = gameStateInstance.ContestantEnvelopeSet;
            int lastEnvelopeIndex = contestantEnvelopes.EnvelopeCount - 1;

            // Get the last envelope
            var lastEnvelope = contestantEnvelopes.GetEnvelope(lastEnvelopeIndex);

            // Bring the last envelope back to the table
            gameStateInstance.EnvelopeTable.AddEnvelope(lastEnvelope);

            // Remove the last envelope
            gameStateInstance.RemoveContestantEnvelope(lastEnvelope);

            // Get the last textbox and clear it
            TextBox[] numberTextBoxes = _envelopeSelectTxtBoxesAndLabels.Keys.ToArray();
            TextBox currentNumberTxtBox = numberTextBoxes[lastEnvelopeIndex];

            currentNumberTxtBox.Clear();

            // Update button availability
            EnvelopeSelectionUpdateAvailability();
        }

        private void EnvelopeSelectionNextPartBtn_Click(object sender, EventArgs e)
        {
            GameState.Instance.SortEnvelopesByNumber();

            EnvelopeSelectionLockButtons();
            QuestionGameUnlockFirstButtons();

            DirectorTabControl.SelectTab("GameQuestionsTab");
        }

        #endregion

        #region Game - Questions

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

            QuestionGameEnvelopeLabel.BackColor = selectedEnvelope.GetBackgroundColor();
            QuestionGameEnvelopeLabel.Text = selectedEnvelope.GetEnvelopeValueText();
        }

        private void QuestionGameUpdateEnvelopeButtons()
        {
            int startEnvelopeCount = GameState.Instance.GameSettings.StartEnvelopeCount;

            if (_questionGameEnvelopeIndex < 0)
            {
                _questionGameEnvelopeIndex = 0;
            }
            else if (_questionGameEnvelopeIndex >= startEnvelopeCount)
            {
                _questionGameEnvelopeIndex = startEnvelopeCount - 1;
            }

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
            if (answerNumber < Constants.MIN_ANSWER_NUMBER || answerNumber > Constants.MAX_ANSWER_NUMBER)
                throw new ArgumentOutOfRangeException(nameof(answerNumber), $"The answer number must be between {Constants.MIN_ANSWER_NUMBER} and {Constants.MAX_ANSWER_NUMBER}.");

            GameState.Instance.SelectAnswer(answerNumber);

            QuestionGameSetAnswerEnabled(enabled: false);
            QuestionGameShowCorrectBtn.Enabled = true;

            _questionGameAnswerLabels[answerNumber].BackColor = Constants.LOCK_IN_ANS_COLOR;
        }

        private void QuestionGameShowCorrectAnswer()
        {
            var gameState = GameState.Instance;
            var currentQuestion = gameState.GetCurrentQuestion();

            _questionGameAnswerLabels[currentQuestion.CorrectAnswerNumber].BackColor = Constants.CORRECT_ANS_COLOR;

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
        }

        private void QuestionGameShowQuestionBtn_Click(object sender, EventArgs e)
        {
            QuestionGameShowQuestionBtn.Enabled = false;
            QuestionGameDisplayEnvelopesBtn.Enabled = true;

            GameState.Instance.ShowQuestion();
        }

        private void QuestionGameDisplayEnvelopesBtn_Click(object sender, EventArgs e)
        {
            QuestionGameDisplayEnvelopesBtn.Enabled = false;
            QuestionGameConfirmEnvelopeBtn.Enabled = true;

            _questionGameEnvelopeIndex = 0;
            QuestionGameUpdateEnvelopeLabel();
            QuestionGameUpdateEnvelopeButtons();
        }

        private void QuestionGameConfirmEnvelopeBtn_Click(object sender, EventArgs e)
        {
            var gameStateInstance = GameState.Instance;
            var selectedEnvelope = gameStateInstance.GetContestantEnvelope(_questionGameEnvelopeIndex);
            if (selectedEnvelope is null)
                throw new NullReferenceException($"Envelope confirmed is null.");

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
        }

        private void QuestionGameShowAnswersBtn_Click(object sender, EventArgs e)
        {
            QuestionGameSetAnswerEnabled(enabled: true);
            QuestionGameShowAnswersBtn.Enabled = false;

            GameState.Instance.ShowPossibleAnswers();
        }

        private void QuestionGameShowCorrectBtn_Click(object sender, EventArgs e)
        {
            QuestionGameShowCorrectAnswer();

            QuestionGameShowCorrectBtn.Enabled = false;
            QuestionGameCheckAnswerBtn.Enabled = true;
        }

        private void QuestionGameCheckAnswerBtn_Click(object sender, EventArgs e)
        {
            QuestionGameCheckAnswerBtn.Enabled = false;
            QuestionGameKeepDestroyEnvelopeBtn.Enabled = true;

            QuestionGameUpdateEnvelopeLabel();
        }

        private void QuestionGameKeepDestroyEnvelopeBtn_Click(object sender, EventArgs e)
        {
            var gameStateInstance = GameState.Instance;
            gameStateInstance.KeepOrDestroyBasedOnAnswer();

            if (gameStateInstance.HasQuestionsLeft)
            {
                QuestionGameNextQuestionBtn.Enabled = true;
            }
            else
            {
                QuestionGameStartTradingBtn.Enabled = true;
            }

            QuestionGameKeepDestroyEnvelopeBtn.Enabled = false;
            
            QuestionGameUpdateEnvelopeLabel();
        }

        private void QuestionGameCancelQuestionBtn_Click(object sender, EventArgs e)
        {
            var gameStateInstance = GameState.Instance;
            gameStateInstance.CancelQuestion();

            QuestionGameLockAllButtons();
            QuestionGameNextQuestionBtn.Enabled = true;
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
            gameStateInstance.StartTrading();
            if (gameStateInstance.ContestantEnvelopeSet.IsEmpty)
            {
                // Game Over
                DirectorTabControl.SelectTab("GameOverTab");
            }
            else
            {
                // Start trading
                TradingUnlockButtons();
                TradingUpdateEnvelopes();
                DirectorTabControl.SelectTab("GameTradingTab");
            }
        }

        #endregion

        #region Trading
        private void TradingUnlockButtons()
        {
            var gameStateInstance = GameState.Instance;
            int contestantEnvelopeCount = gameStateInstance.ContestantEnvelopeSet.EnvelopeCount;
            int hostEnvelopeCount = gameStateInstance.HostEnvelopeSet.EnvelopeCount;

            for (int i = 0; i < Constants.MAX_ENVELOPE_POSSIBLE_COUNT; ++i)
            {
                _tradingContestantCheckboxes[i].Enabled = i < contestantEnvelopeCount;
                _tradingContestantCheckboxes[i].Checked = false;

                _tradingHostCheckboxes[i].Enabled = i < hostEnvelopeCount;
                _tradingHostCheckboxes[i].Checked = false;
            }
        }

        private void TradingLockButtons()
        {
            for (int i = 0; i < Constants.MAX_ENVELOPE_POSSIBLE_COUNT; ++i)
            {
                _tradingContestantCheckboxes[i].Enabled = false;
                _tradingContestantCheckboxes[i].Checked = false;

                _tradingHostCheckboxes[i].Enabled = false;
                _tradingHostCheckboxes[i].Checked = false;
            }
        }

        private void TradingUpdateEnvelopes()
        {
            var gameStateInstance = GameState.Instance;
            
            for (int i = 0; i < Constants.MAX_ENVELOPE_POSSIBLE_COUNT; ++i)
            {
                Envelope? envelope = gameStateInstance.GetContestantEnvelope(i);
                _tradingContestantEnvelopeLabels[i].BackColor = envelope?.GetBackgroundColor() ?? Color.White;
                _tradingContestantEnvelopeLabels[i].Text = envelope?.GetEnvelopeValueText() ?? string.Empty;
            }

            for (int i = 0; i < Constants.MAX_ENVELOPE_POSSIBLE_COUNT; ++i)
            {
                Envelope? envelope = gameStateInstance.GetHostEnvelope(i);
                _tradingHostEnvelopeLabels[i].BackColor = envelope?.GetBackgroundColor() ?? Color.White;
                _tradingHostEnvelopeLabels[i].Text = envelope?.GetEnvelopeValueText() ?? string.Empty;
            }
        }

        #endregion
    }
}
