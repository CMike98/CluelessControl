using System.Net;
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

        #region Json Serializer Options
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            Converters = { new JsonChequeConverter(), new JsonQuestionConverter(), new JsonQuestionSetConverter() },
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };
        #endregion

        private const int NO_ITEM_INDEX = -1;

        #region Envelope Settings Screen Variables
        private bool _envelopeSettingsEdited = false;
        private bool _envelopeSettingsSkipIndexChange = false;
        private int _envelopeSettingsLastSelectedIndex = NO_ITEM_INDEX;
        #endregion

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

        #region Messages
        public void ShowErrorMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            MessageBox.Show(message, Constants.PROGRAM_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void EnvelopeSettingsUpdate()
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

        private void EnvelopeSettingsLoadFromFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));
            if (!File.Exists(fileName))
                throw new FileNotFoundException("File not found.", nameof(fileName));

            string json = File.ReadAllText(fileName);
            ChequeSettings settings = JsonSerializer.Deserialize<ChequeSettings>(json, _jsonSerializerOptions) ?? throw new FileFormatException("Envelope settings loading failed.");

            GameState.Instance.LoadChequeSettings(settings);
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

                    EnvelopeSettingsUpdate();

                    _envelopeSettingsSkipIndexChange = true;
                    EnvelopeSettingsListBox.SelectedIndex = selectedIndex;
                }
                catch (Exception)
                {
                    var answer = MessageBox.Show(
                        text: "Nie mo¿na zapisaæ koperty! Przejœcie spowoduje utratê danych.",
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

            EnvelopeSettingsUpdate();

            EnvelopeSettingsListBox.SelectedIndex = chequeList.Count - 1;
            _envelopeSettingsEdited = true;
        }

        private void EnvelopeSettingsDeleteBtn_Click(object sender, EventArgs e)
        {
            var confirmAnswer = MessageBox.Show(
                text: "Czy na pewno chcesz usun¹æ tê kopertê?",
                Constants.PROGRAM_TITLE,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmAnswer == DialogResult.No)
                return;

            int index = EnvelopeSettingsListBox.SelectedIndex;
            GameState.Instance.ChequeSettings.ChequeList.RemoveAt(index);

            EnvelopeSettingsUpdate();

            EnvelopeSettingsListBox.SelectedIndex = NO_ITEM_INDEX;
            EnvelopeSettingsListBox_SelectedIndexChanged(sender, e);

            _envelopeSettingsEdited = true;
        }

        private void EnvelopeSettingsRandomiseBtn_Click(object sender, EventArgs e)
        {
            var confirmAnswer = MessageBox.Show(
                text: "Jesteœ pewny? Poprzedniej kolejnoœci nie bêdzie mo¿na odzyskaæ bez wczytania z pliku lub rêcznego odzyskania.",
                Constants.PROGRAM_TITLE,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmAnswer == DialogResult.No)
                return;

            GameState.Instance.ChequeSettings.Randomise();
            EnvelopeSettingsUpdate();
            
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

                EnvelopeSettingsUpdate();

                _envelopeSettingsEdited = false;
                _envelopeSettingsSkipIndexChange = true;
                _envelopeSettingsLastSelectedIndex = NO_ITEM_INDEX;
                EnvelopeSettingsListBox.SelectedIndex = selectedIndex;

                _envelopeSettingsSkipIndexChange = false;
            }
            catch (Exception ex)
            {
#if DEBUG
                ShowErrorMessage(ex.ToString());
#else
                ShowErrorMessage("Zapisywanie zakoñczone niepowodzeniem!");
#endif
            }
        }

        private void EnvelopeSettingsLoadFromFileBtn_Click(object sender, EventArgs e)
        {
            if (EnvelopeSettingsOpen.ShowDialog() != DialogResult.OK)
                return;

            string fileName = EnvelopeSettingsOpen.FileName;

            try
            {
                EnvelopeSettingsLoadFromFile(fileName);
            }
            catch (Exception ex)
            {
#if DEBUG
                ShowErrorMessage(ex.ToString());
#else
                ShowErrorMessage("Zapisywanie zakoñczone niepowodzeniem!");
#endif
            }
        }

        private void EnvelopeSettingsSaveToFileBtn_Click(object sender, EventArgs e)
        {
            if (EnvelopeSettingsSave.ShowDialog() != DialogResult.OK)
                return;

            string fileName = EnvelopeSettingsSave.FileName;

            try
            {

            }
            catch (Exception ex)
            {
#if DEBUG
                ShowErrorMessage(ex.ToString());
#else
                ShowErrorMessage("Zapisywanie zakoñczone niepowodzeniem!");
#endif
            }
        }

#endregion
    }
}
