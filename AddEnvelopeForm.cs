using CluelessControl.Envelopes;

namespace CluelessControl
{
    public partial class AddEnvelopeForm : Form
    {
        public TradingSide Side
        {
            get;
            private set;
        }

        private List<int> EnvelopeNumbers
        {
            get;
            set;
        }

        public Envelope? SelectedEnvelope
        {
            get;
            private set;
        }

        private bool ItWasReset
        {
            get;
            set;
        }

        public AddEnvelopeForm()
        {
            InitializeComponent();
            EnvelopeNumbers = new List<int>(capacity: Constants.GameConstants.MAX_ENVELOPES_COUNT);
        }

        private void AddEnvelopeForm_Load(object sender, EventArgs e)
        {
            if (!ItWasReset)
                throw new InvalidOperationException();
        }

        public void Reset(TradingSide side)
        {
            // Clear the AddEnvelopeSelectListBox item
            AddEnvelopeSelectListBox.SelectedIndex = -1;

            // Clear the envelope label
            AddEnvelopeSelectedEnvelopeLbl.Text = string.Empty;

            // Add the envelopes to AddEnvelopeSelectListBox
            EnvelopeNumbers.Clear();

            AddEnvelopeSelectListBox.Items.Clear();
            foreach (Envelope envelope in GameState.Instance.EnvelopeTable.EnvelopesOnTable)
            {
                AddEnvelopeSelectListBox.Items.Add($"{envelope.EnvelopeNumber}. {envelope.Cheque.ValueString}");
                EnvelopeNumbers.Add(envelope.EnvelopeNumber);
            }

            // And save the current side for future reference
            Side = side;

            // Mark that no envelope has been selected
            SelectedEnvelope = null;

            // Mark that is was reset
            ItWasReset = true;
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

            int envelopeNumber = EnvelopeNumbers[index];
            Envelope envelope = GameState.Instance.EnvelopeTable.GetEnvelope(envelopeNumber);
            AddEnvelopeSelectedEnvelopeLbl.Text = envelope.GetEnvelopeValueForDirector();
            AddEnvelopeConfirmBtn.Enabled = true;
        }

        private void AddEnvelopeConfirmBtn_Click(object sender, EventArgs e)
        {
            var gameStateInstance = GameState.Instance;

            int index = AddEnvelopeSelectListBox.SelectedIndex;
            int envelopeNumber = EnvelopeNumbers[index];
            Envelope newEnvelope = gameStateInstance.EnvelopeTable.GetEnvelope(envelopeNumber);
            newEnvelope.MarkAsNeutral();

            SelectedEnvelope = newEnvelope;

            this.Close();
        }
    }
}
