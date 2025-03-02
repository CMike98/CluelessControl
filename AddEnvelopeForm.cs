using CluelessControl.Envelopes;

namespace CluelessControl
{
    public partial class AddEnvelopeForm : Form
    {
        private TradingSide _side;
        private bool _isShowing;
        private List<int> _envelopeNumbers;

        public bool IsShowing => _isShowing;

        public AddEnvelopeForm()
        {
            InitializeComponent();
            _envelopeNumbers = new List<int>(capacity: Constants.GameConstants.MAX_ENVELOPES_COUNT);
        }

        public void Reset(TradingSide side)
        {
            // Clear the AddEnvelopeSelectListBox item
            AddEnvelopeSelectListBox.SelectedIndex = -1;

            // Clear the envelope label
            AddEnvelopeSelectedEnvelopeLbl.Text = string.Empty;

            // Add the envelopes to AddEnvelopeSelectListBox
            _envelopeNumbers.Clear();

            AddEnvelopeSelectListBox.Items.Clear();
            foreach (Envelope envelope in GameState.Instance.EnvelopeTable.EnvelopesOnTable)
            {
                AddEnvelopeSelectListBox.Items.Add($"{envelope.EnvelopeNumber}. {envelope.Cheque.ToValueString()}");
                _envelopeNumbers.Add(envelope.EnvelopeNumber);
            }

            // And save the current side for future reference
            _side = side;

            _isShowing = true;
        }

        private void AddEnvelopeSelectListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = AddEnvelopeSelectListBox.SelectedIndex;
            if (index == -1)
            {
                AddEnvelopeSelectedEnvelopeLbl.Text = string.Empty;
                AddEnvelopeConfirmBtn.Enabled = false;
                return;
            }

            int envelopeNumber = _envelopeNumbers[index];
            Envelope envelope = GameState.Instance.EnvelopeTable.GetEnvelope(envelopeNumber);
            AddEnvelopeSelectedEnvelopeLbl.Text = envelope.GetEnvelopeValueForDirector();
            AddEnvelopeConfirmBtn.Enabled = true;
        }

        private void AddEnvelopeConfirmBtn_Click(object sender, EventArgs e)
        {
            var gameStateInstance = GameState.Instance;

            int index = AddEnvelopeSelectListBox.SelectedIndex;
            int envelopeNumber = _envelopeNumbers[index];
            Envelope newEnvelope = gameStateInstance.EnvelopeTable.GetEnvelope(envelopeNumber);
            newEnvelope.MarkAsNeutral();

            EnvelopeSet set = _side == TradingSide.Contestant ? gameStateInstance.ContestantEnvelopeSet : gameStateInstance.HostEnvelopeSet;
            set.AddEnvelope(newEnvelope);

            gameStateInstance.ContestantEnvelopeSet.SortByEnvelopeNumbers();
            gameStateInstance.HostEnvelopeSet.SortByEnvelopeNumbers();

            Program.DirectorForm.TradingUpdateEnvelopes();
            Program.DirectorForm.TradingUpdateCashLabels();
            Program.DirectorForm.TradingClearCheckboxes();
            Program.DirectorForm.TradingUpdatePages();

            gameStateInstance.EnvelopeTable.DeleteEnvelope(newEnvelope);
            gameStateInstance.RefreshEnvelopes();
            
            _isShowing = false;

            this.OnFormClosing(new FormClosingEventArgs(closeReason: CloseReason.UserClosing, cancel: true));
        }

        private void AddEnvelopeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _isShowing = false;

            Hide();
            e.Cancel = true;
        }
    }
}
