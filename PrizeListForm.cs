using CluelessControl.Constants;
using CluelessControl.Prizes;
using System.Data;
using System.DirectoryServices.ActiveDirectory;

namespace CluelessControl
{
    public partial class PrizeListForm : Form
    {
        private class PrizeDataListItem
        {
            public string Code
            {
                get;
                set;
            } = string.Empty;

            public PrizeData? PrizeData
            {
                get;
                set;
            }
        }

        private List<PrizeDataListItem> _prizeDataList = [];
        private int _lastSelectedIndex = -1;
        private bool _skipPrizeChange = false;

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

        public bool AskYesNoQuestion(string question)
        {
            if (string.IsNullOrWhiteSpace(question))
                throw new ArgumentNullException(nameof(question));

            return MessageBox.Show(question, GameConstants.PROGRAM_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        #endregion

        public PrizeListForm(PrizeList? prizeList)
        {
            InitializeComponent();

            _prizeDataList = new List<PrizeDataListItem>();
            if (prizeList is not null)
            {
                _prizeDataList = prizeList.PrizeDictionary.Select(kvp => new PrizeDataListItem()
                {
                    Code = kvp.Key,
                    PrizeData = kvp.Value
                }).ToList();
            }
        }

        private void PrizeListForm_Load(object sender, EventArgs e)
        {
            RefreshAll();
        }

        public PrizeList ConvertToPrizeList()
        {
            IEnumerable<PrizeDataListItem> prizeDataListNotNull = _prizeDataList
                .Where(item => !string.IsNullOrWhiteSpace(item.Code) && item.PrizeData is not null)
                .OrderBy(item => item.Code);

            Dictionary<string, PrizeData> dict = prizeDataListNotNull.ToDictionary(
                keySelector: item => item.Code,
                elementSelector: item => item.PrizeData!);

            return PrizeList.Create(dict);
        }

        private void RefreshPrizeList()
        {
            PrizeListSelectListBox.Items.Clear();

            int count = 0;
            foreach (PrizeDataListItem item in _prizeDataList)
            {
                ++count;
                PrizeListSelectListBox.Items.Add($"{count}. {item.Code}");
            }

            PrizeListPrizeCountLbl.Text = string.Format("Nagród: {0}", count);
        }

        private void RefreshButtons()
        {
            bool anyItemSelected = PrizeListSelectListBox.SelectedIndex > -1;

            PrizeListAddBtn.Enabled = true;
            PrizeListRemoveBtn.Enabled = anyItemSelected;
            PrizeListSaveBtn.Enabled = anyItemSelected;
            PrizeListRetrieveBtn.Enabled = anyItemSelected;
        }

        private void RefreshAll()
        {
            RefreshPrizeList();
            RefreshButtons();
        }

        private bool VerifyNewPrizeData(out string errorMessage)
        {
            string inputCode = PrizeListCodeTxtBox.Text.Trim().ToUpper();
            if (string.IsNullOrWhiteSpace(inputCode))
            {
                errorMessage = "Pole z kodem nagrody jest puste!";
                return false;
            }

            if (_prizeDataList.Any(prize => prize.Code == inputCode))
            {
                errorMessage = "Już istnieje nagroda o takim kodzie!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(PrizeListNameTxtBox.Text))
            {
                errorMessage = "Pole z nazwą nagrody jest puste!";
                return false;
            }

            if (!decimal.TryParse(PrizeListRoundingUnitTxtBox.Text.Trim(), out decimal roundingUnit))
            {
                errorMessage = "Pole z jednostką nagrody (do zaokrąglania) jest puste!";
                return false;
            }

            if (roundingUnit <= 0)
            {
                errorMessage = "Pole z jednostką nagrody (do zaokrąglania) musi mieć liczbę dodatnią!";
                return false;
            }

            errorMessage = "";
            return true;
        }

        private bool TryToSavePrizeData(int index, out string errorMessage)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"Index must not be negative!");
            }

            if (!VerifyNewPrizeData(out errorMessage))
            {
                return false;
            }

            errorMessage = "";
            string prizeCode = PrizeListCodeTxtBox.Text.Trim().ToUpper();

            string prizeName = PrizeListNameTxtBox.Text.Trim().ToUpper();
            decimal roundingUnit = decimal.Parse(PrizeListRoundingUnitTxtBox.Text.Trim());
            RoundingMethod roundingMethod = Enum.Parse<RoundingMethod>(PrizeListRoundingMethodComboBox.SelectedIndex.ToString());
            var newPrizeData = PrizeData.CreatePrize(prizeName, roundingUnit, roundingMethod);

            var newListItem = new PrizeDataListItem()
            {
                Code = prizeCode,
                PrizeData = newPrizeData
            };

            _prizeDataList[index] = newListItem;
            return true;
        }

        private bool HasPrizeDataChanged()
        {
            int selectedIndex = PrizeListSelectListBox.SelectedIndex;
            if (selectedIndex < 0)
            {
                return false;
            }

            return true;
        }

        private void PrizeListSelectListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_skipPrizeChange)
                return;

            int selectedIndex = PrizeListSelectListBox.SelectedIndex;

            // Have we selected the same prize (not selecting one doesn't count)?
            // Don't do anything.
            if (selectedIndex >= 0 && selectedIndex == _lastSelectedIndex)
                return;

            // Has the prize selected changed? (and the last prize doesn't happen to be "NOTHING")
            if (HasPrizeDataChanged() && _lastSelectedIndex >= 0)
            {
                // If so, save the changes, if you can.
                // If you can't, let the user know, and ask if they want
                // to make changes or to destroy the faulty data and move on.
                try
                {
                    if (!TryToSavePrizeData(selectedIndex, out string errorMessage))
                    {
                        ShowErrorMessage(string.Format("Błąd przy zapisywaniu: {0}", errorMessage));
                        throw new Exception(errorMessage);
                    }

                    RefreshPrizeList();

                    _skipPrizeChange = true;
                    PrizeListSelectListBox.SelectedIndex = selectedIndex;
                    _skipPrizeChange = false;

                    RefreshButtons();
                }
                catch (Exception ex)
                {
                    var answer = MessageBox.Show(
                            "Nie można zapisać nagrody - przejście spowoduje utratę danych!",
                            Constants.GameConstants.PROGRAM_TITLE,
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Warning);

                    if (answer == DialogResult.Cancel)
                    {
                        _skipPrizeChange = true;
                        PrizeListSelectListBox.SelectedIndex = _lastSelectedIndex;
                        _skipPrizeChange = false;
                    }
                }
            }

            // Is nothing selected? Lock everything and end this function.
            if (selectedIndex < 0)
            {
                PrizeListCodeTxtBox.Clear();
                PrizeListCodeTxtBox.Enabled = false;

                PrizeListNameTxtBox.Clear();
                PrizeListNameTxtBox.Enabled = false;

                PrizeListRoundingUnitTxtBox.Text = decimal.One.ToString();
                PrizeListRoundingUnitTxtBox.Enabled = false;

                PrizeListRoundingMethodComboBox.SelectedIndex = 0;
                PrizeListRoundingMethodComboBox.Enabled = false;

                return;
            }

            // Bring the prize to the screen.
            PrizeDataListItem item = _prizeDataList[selectedIndex];

            PrizeListCodeTxtBox.Enabled = true;
            PrizeListCodeTxtBox.Text = item.Code;

            PrizeListNameTxtBox.Enabled = true;
            PrizeListNameTxtBox.Text = item.PrizeData?.PrizeName ?? string.Empty;

            PrizeListRoundingUnitTxtBox.Enabled = true;
            PrizeListRoundingUnitTxtBox.Text = (item.PrizeData?.RoundingUnit.ToString()) ?? decimal.One.ToString();

            PrizeListRoundingMethodComboBox.Enabled = true;
            PrizeListRoundingMethodComboBox.SelectedIndex = ((int?) item.PrizeData?.RoundingMethod) ?? 0;

            _lastSelectedIndex = selectedIndex;
        }

        private void PrizeListAddBtn_Click(object sender, EventArgs e)
        {
            _prizeDataList.Add(new PrizeDataListItem());

            RefreshPrizeList();

            PrizeListSelectListBox.SelectedIndex = _prizeDataList.Count - 1;

            RefreshButtons();
        }

        private void PrizeListRemoveBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = PrizeListSelectListBox.SelectedIndex;

            if (selectedIndex < 0)
            {
                ShowErrorMessage("Nie zaznaczono żadnej nagrody do usunięcia!");
                return;
            }

            _prizeDataList.RemoveAt(selectedIndex);

            RefreshPrizeList();

            PrizeListSelectListBox_SelectedIndexChanged(this, e);

            RefreshButtons();
        }

        private void PrizeListSaveBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = PrizeListSelectListBox.SelectedIndex;
            if (selectedIndex < 0)
            {
                ShowErrorMessage("Nie zaznaczono żadnej nagrody do zapisania!");
                return;
            }

            if (!TryToSavePrizeData(selectedIndex, out string errorMessage))
            {
                ShowErrorMessage(errorMessage);
                return;
            }

            RefreshPrizeList();
            PrizeListSelectListBox.SelectedIndex = selectedIndex;
            RefreshButtons();
        }

        private void PrizeListRetrieveBtn_Click(object sender, EventArgs e)
        {
            int selectedIndex = PrizeListSelectListBox.SelectedIndex;
            if (selectedIndex < 0)
            {
                ShowErrorMessage("Nie zaznaczono żadnej nagrody do przywrócenia!");
                return;
            }

            if (!AskYesNoQuestion("Czy na pewno chcesz przywrócić dane nagrody? Tego nie będzie można cofnąć!"))
            {
                return;
            }

            PrizeDataListItem item = _prizeDataList[selectedIndex];

            PrizeListCodeTxtBox.Text = item.Code.ToUpper();
            PrizeListNameTxtBox.Text = item.PrizeData?.PrizeName.ToUpper() ?? string.Empty;
            PrizeListRoundingUnitTxtBox.Text = (item.PrizeData?.RoundingUnit ?? 1).ToString();
            PrizeListRoundingMethodComboBox.SelectedIndex = (int)(item.PrizeData?.RoundingMethod ?? 0);
        }

        private void PrizeListRoundingUnitTxtBox_TextChanged(object sender, EventArgs e)
        {
            if (!decimal.TryParse(PrizeListRoundingUnitTxtBox.Text.Trim(), out decimal count) || count <= 0)
            {
                PrizeListRoundingUnitTxtBox.BackColor = Color.Red;
                PrizeListRoundingUnitTxtBox.ForeColor = Color.White;
                PrizeListSaveBtn.Enabled = false;
            }
            else
            {
                PrizeListRoundingUnitTxtBox.BackColor = Color.White;
                PrizeListRoundingUnitTxtBox.ForeColor = Color.Black;
                PrizeListSaveBtn.Enabled = true;
            }
        }
    }
}
