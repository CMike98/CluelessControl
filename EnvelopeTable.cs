namespace CluelessControl
{
    public class EnvelopeTable
    {
        private List<Envelope> EnvelopesOnTable
        {
            get;
            set;
        }

        public EnvelopeTable()
        {
            EnvelopesOnTable = new List<Envelope>();
        }

        public void GenerateTable(ChequeSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            EnvelopesOnTable = new List<Envelope>();

            for (int i = Constants.MIN_ENVELOPE_NUMBER; i <= Constants.MAX_ENVELOPE_NUMBER; ++i)
            {
                var newEnvelope = Envelope.Create(i, settings.ChequeList[i - Constants.MIN_ENVELOPE_NUMBER]);
                EnvelopesOnTable.Add(newEnvelope);
            }
        }

        public Envelope GetEnvelope(int envelopeNumber)
        {
            return EnvelopesOnTable.Where(envelope => envelope.EnvelopeNumber == envelopeNumber).First();
        }

        public void DeleteEnvelope(int envelopeNumber)
        {
            var foundEnvelope = GetEnvelope(envelopeNumber);
            DeleteEnvelope(foundEnvelope);
        }

        public void DeleteEnvelope(Envelope envelope)
        {
            EnvelopesOnTable.Remove(envelope);
        }
    }
}
