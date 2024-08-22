using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

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

        private bool EditedBeforeSave => _envelopeSettingsEdited || _questionEditorEdited;

        public DirectorForm()
        {
            InitializeComponent();
        }

        private void DirectorForm_Load(object sender, EventArgs e)
        {
            ShowAllForms();
        }

        private static void ShowAllForms()
        {
            _hostScreenForm.Show();
            _contestantScreenForm.Show();
            _tvScreenForm.Show();
        }

        private void DirectorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ;
        }

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
                int startEnvelopeCount = (int) SettingsEnvelopeStartCountNumeric.Value;
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

                result = GameSettings.Create(startEnvelopeCount, decimalPlaces, onlyWorstMinusCounts, onlyBestPlusCounts);
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
    }
}
