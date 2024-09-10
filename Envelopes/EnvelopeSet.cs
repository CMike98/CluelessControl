using CluelessControl.Cheques;

namespace CluelessControl.Envelopes
{
    public class EnvelopeSet
    {
        private List<Envelope> _envelopes;

        public List<Envelope> Envelopes => _envelopes;

        public int EnvelopeCount => _envelopes.Count;

        public bool IsEmpty => _envelopes.Count == 0;

        #region Constructors
        private EnvelopeSet()
        {
            _envelopes = [];
        }

        private EnvelopeSet(List<Envelope> envelopes)
        {
            _envelopes = envelopes;
        }
        #endregion

        #region Create Methods
        public static EnvelopeSet Create()
        {
            return new EnvelopeSet();
        }

        public static EnvelopeSet Create(List<Envelope> envelopes)
        {
            if (envelopes is null)
                throw new ArgumentNullException(nameof(envelopes));
            if (envelopes.Any(envelope => envelope is null))
                throw new ArgumentException($"At least one envelope is null.", nameof(envelopes));

            return new EnvelopeSet(envelopes);
        }
        #endregion

        #region Envelope Methods
        public Envelope GetEnvelope(int envelopeIndex)
        {
            if (envelopeIndex < 0 || envelopeIndex >= EnvelopeCount)
                throw new IndexOutOfRangeException($"Envelope index (here {envelopeIndex}) should be [0...{EnvelopeCount}]!");

            return _envelopes[envelopeIndex];
        }

        public void AddEnvelope(Envelope envelope)
        {
            if (envelope is null)
                throw new ArgumentNullException(nameof(envelope));

            _envelopes.Add(envelope);
        }

        public void RemoveEnvelope(Envelope envelope)
        {
            if (envelope is null)
                throw new ArgumentNullException(nameof(envelope));

            _envelopes.Remove(envelope);
        }

        public void ClearEnvelopeList()
        {
            _envelopes.Clear();
        }
        #endregion

        #region Other Methods
        public IEnumerable<BaseCheque> GetCheques()
        {
            return _envelopes.Select(env => env.Cheque);
        }

        public void RemoveDestroyedEnvelopes()
        {
            _envelopes.RemoveAll(envelope => envelope.State == EnvelopeState.DESTROYED);
        }

        public void MarkAllAsNeutral()
        {
            _envelopes.ForEach(envelope => envelope.MarkAsNeutral());
        }

        #endregion

        #region Transfer
        public void TransferEnvelope(EnvelopeSet recipient, Envelope envelopeToTransfer)
        {
            if (recipient is null)
                throw new ArgumentNullException(nameof(recipient));
            if (envelopeToTransfer is null)
                throw new ArgumentNullException(nameof(envelopeToTransfer));

            _envelopes.Remove(envelopeToTransfer);
            recipient._envelopes.Add(envelopeToTransfer);
        }
        #endregion

        #region Sorting
        public void SortByEnvelopeNumbers()
        {
            static int EnvelopeNumberComparer(Envelope env1, Envelope env2)
            {
                if (env1 == null && env2 == null)
                    return 0;
                if (env1 == null)
                    return -1;
                if (env2 == null)
                    return 1;

                return env1.EnvelopeNumber.CompareTo(env2.EnvelopeNumber);
            }

            _envelopes.Sort(EnvelopeNumberComparer);
        }
        #endregion
    }
}
