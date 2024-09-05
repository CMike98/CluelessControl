using CluelessControl.Cheques;

namespace CluelessControl.Envelopes
{
    public class EnvelopeTable
    {
        private HashSet<Envelope> EnvelopesOnTable
        {
            get;
            set;
        }

        private EnvelopeTable()
        {
            EnvelopesOnTable = new HashSet<Envelope>();
        }

        private EnvelopeTable(HashSet<Envelope> envelopesOnTable)
        {
            EnvelopesOnTable = envelopesOnTable;
        }

        public static EnvelopeTable Create()
        {
            return new EnvelopeTable();
        }

        public static EnvelopeTable Create(ChequeSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            var envelopeList = new HashSet<Envelope>();

            for (int i = Constants.MIN_ENVELOPE_NUMBER; i <= Constants.MAX_ENVELOPE_NUMBER; ++i)
            {
                int chequeIndex = i - Constants.MIN_ENVELOPE_NUMBER;
                BaseCheque originalCheque = settings.ChequeList[chequeIndex];
                var newEnvelope = Envelope.Create(i, originalCheque.CloneCheque());
                envelopeList.Add(newEnvelope);
            }

            return new(envelopeList);
        }

        public bool IsEnvelopePresent(int envelopeNumber)
        {
            if (envelopeNumber < Constants.MIN_ENVELOPE_NUMBER || envelopeNumber > Constants.MAX_ENVELOPE_NUMBER)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(envelopeNumber),
                    $"Envelope number must be in range [{Constants.MIN_ENVELOPE_NUMBER}...{Constants.MAX_ENVELOPE_NUMBER}]!");
            }

            return EnvelopesOnTable.FirstOrDefault(current => current.EnvelopeNumber == envelopeNumber) is not null;
        }

        public bool IsEnvelopePresent(Envelope envelope)
        {
            if (envelope == null)
                throw new ArgumentNullException(nameof(envelope));

            return EnvelopesOnTable.Contains(envelope);
        }

        public Envelope GetEnvelope(int envelopeNumber)
        {
            if (envelopeNumber < Constants.MIN_ENVELOPE_NUMBER || envelopeNumber > Constants.MAX_ENVELOPE_NUMBER)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(envelopeNumber),
                    $"Envelope number must be in range [{Constants.MIN_ENVELOPE_NUMBER}...{Constants.MAX_ENVELOPE_NUMBER}]!");
            }

            return EnvelopesOnTable.Where(envelope => envelope.EnvelopeNumber == envelopeNumber).First();
        }

        public void AddEnvelope(Envelope envelope)
        {
            if (envelope == null)
                throw new ArgumentNullException(nameof(envelope));

            EnvelopesOnTable.Add(envelope);
        }

        public void DeleteEnvelope(int envelopeNumber)
        {
            if (envelopeNumber < Constants.MIN_ENVELOPE_NUMBER || envelopeNumber > Constants.MAX_ENVELOPE_NUMBER)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(envelopeNumber),
                    $"Envelope number must be in range [{Constants.MIN_ENVELOPE_NUMBER}...{Constants.MAX_ENVELOPE_NUMBER}]!");
            }

            EnvelopesOnTable.RemoveWhere(current => current.EnvelopeNumber == envelopeNumber);
        }

        public void DeleteEnvelope(Envelope envelope)
        {
            if (envelope == null)
                throw new ArgumentNullException(nameof(envelope));

            EnvelopesOnTable.Remove(envelope);
        }
    }
}
