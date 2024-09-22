using CluelessControl.Cheques;
using CluelessControl.Constants;

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

            for (int i = GameConstants.MIN_ENVELOPE_NUMBER; i <= GameConstants.MAX_ENVELOPE_NUMBER; ++i)
            {
                int chequeIndex = i - GameConstants.MIN_ENVELOPE_NUMBER;
                BaseCheque originalCheque = settings.ChequeList[chequeIndex];
                var newEnvelope = Envelope.Create(i, originalCheque.CloneCheque());
                envelopeList.Add(newEnvelope);
            }

            return new(envelopeList);
        }

        public bool IsEnvelopePresent(int envelopeNumber)
        {
            if (envelopeNumber < GameConstants.MIN_ENVELOPE_NUMBER || envelopeNumber > GameConstants.MAX_ENVELOPE_NUMBER)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(envelopeNumber),
                    $"Envelope number must be in range [{GameConstants.MIN_ENVELOPE_NUMBER}...{GameConstants.MAX_ENVELOPE_NUMBER}]!");
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
            if (envelopeNumber < GameConstants.MIN_ENVELOPE_NUMBER || envelopeNumber > GameConstants.MAX_ENVELOPE_NUMBER)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(envelopeNumber),
                    $"Envelope number must be in range [{GameConstants.MIN_ENVELOPE_NUMBER}...{GameConstants.MAX_ENVELOPE_NUMBER}]!");
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
            if (envelopeNumber < GameConstants.MIN_ENVELOPE_NUMBER || envelopeNumber > GameConstants.MAX_ENVELOPE_NUMBER)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(envelopeNumber),
                    $"Envelope number must be in range [{GameConstants.MIN_ENVELOPE_NUMBER}...{GameConstants.MAX_ENVELOPE_NUMBER}]!");
            }

            EnvelopesOnTable.RemoveWhere(current => current.EnvelopeNumber == envelopeNumber);
        }

        public void DeleteEnvelope(Envelope envelope)
        {
            if (envelope == null)
                throw new ArgumentNullException(nameof(envelope));

            EnvelopesOnTable.Remove(envelope);
        }

        public IList<Envelope> SelectEnvelopesWhere(Predicate<Envelope> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return EnvelopesOnTable.Where(x => predicate(x)).ToList();
        }

        public void ForAll(Action<Envelope> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            foreach (var envelope in EnvelopesOnTable)
            {
                action(envelope);
            }
        }

        public void ForSelected(Action<Envelope> action, Predicate<Envelope> predicate)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            EnvelopesOnTable.Where(envelope => predicate(envelope));

            foreach (var envelope in EnvelopesOnTable)
            {
                if (predicate(envelope))
                    action(envelope);
            }
        }
    }
}
