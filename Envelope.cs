namespace CluelessControl
{
    public class Envelope
    {
        public int EnvelopeNumber
        {
            get;
            private set;
        }

        public BaseCheque Cheque
        {
            get;
            private set;
        }

        public EnvelopeState State
        {
            get;
            private set;
        }

        public bool IsOpen
        {
            get;
            set;
        }

        private Envelope(int envelopeNumber, BaseCheque cheque)
        {
            EnvelopeNumber = envelopeNumber;
            Cheque = cheque;
            State = EnvelopeState.NEUTRAL;
            IsOpen = false;
        }

        internal static Envelope Create(int envelopeNumber, BaseCheque cheque)
        {
            if (envelopeNumber < Constants.MIN_ENVELOPE_NUMBER || envelopeNumber > Constants.MAX_ENVELOPE_NUMBER)
                throw new ArgumentOutOfRangeException(nameof(envelopeNumber), $"Envelope number ({envelopeNumber}) must be in the range [{Constants.MIN_ENVELOPE_NUMBER}...{Constants.MAX_ENVELOPE_NUMBER}]");
            if (cheque == null)
                throw new ArgumentNullException(nameof(cheque));

            return new Envelope(envelopeNumber, cheque);
        }

        public Color GetBackgroundColor()
        {
            return State switch
            {
                EnvelopeState.NEUTRAL => Color.White,
                EnvelopeState.PLAYING_FOR => Color.Orange,
                EnvelopeState.WON => Color.Green,
                EnvelopeState.MARKED_FOR_TRADE => Color.Orange,
                EnvelopeState.TO_BE_DESTROYED => Color.Red,
                EnvelopeState.DESTROYED => Color.Gray,
                _ => throw new InvalidOperationException($"Not recognized envelope state: '{State}'"),
            };
        }
    }
}
