using CluelessControl.Cheques;

namespace CluelessControl.EnvelopeColorStates
{
    public abstract class ColorState
    {
        protected BaseCheque Cheque { get; init; }

        public ColorState(BaseCheque cheque)
        {
            if (cheque == null)
                throw new ArgumentNullException(nameof(cheque));

            Cheque = cheque;
        }

        public abstract EnvelopeColorCollection GetColorPairingForScreen();

        public abstract EnvelopeColorCollection GetColorPairingForTv();
    }
}
