using CluelessControl.Cheques;

namespace CluelessControl.EnvelopeColorStates
{
    public class ForDestructionColorState : ColorState
    {
        public ForDestructionColorState(BaseCheque cheque) : base(cheque)
        {
        }

        public override EnvelopeColorCollection GetColorPairingForScreen()
        {
            return new EnvelopeColorCollection()
            {
                BackgroundColor = Color.Red,
                LineColor       = Color.White,
                NumberFontColor = Color.White,
                ChequeFontColor = Color.White
            };
        }

        public override EnvelopeColorCollection GetColorPairingForTv()
        {
            return new EnvelopeColorCollection()
            {
                BackgroundColor = Color.Red,
                LineColor       = Color.White,
                NumberFontColor = Color.White,
                ChequeFontColor = Color.White
            };
        }
    }
}
