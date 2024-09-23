using CluelessControl.Cheques;

namespace CluelessControl.EnvelopeColorStates
{
    public class DestroyedColorState : ColorState
    {
        public DestroyedColorState(BaseCheque cheque) : base(cheque)
        {
        }

        public override EnvelopeColorCollection GetColorPairingForScreen()
        {
            return new EnvelopeColorCollection()
            {
                BackgroundColor = Color.Gray,
                LineColor       = Color.White,
                NumberFontColor = Color.White,
                ChequeFontColor = Color.White
            };
        }

        public override EnvelopeColorCollection GetColorPairingForTv()
        {
            return new EnvelopeColorCollection()
            {
                BackgroundColor = Color.Gray,
                LineColor       = Color.White,
                NumberFontColor = Color.White,
                ChequeFontColor = Color.White
            };
        }
    }
}
