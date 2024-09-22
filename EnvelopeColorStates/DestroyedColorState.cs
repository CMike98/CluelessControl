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
                LineColor       = Color.Red,
                NumberFontColor = Color.Black,
                ChequeFontColor = Color.Black
            };
        }

        public override EnvelopeColorCollection GetColorPairingForTv()
        {
            return new EnvelopeColorCollection()
            {
                BackgroundColor = Color.Gray,
                LineColor       = Color.Red,
                NumberFontColor = Color.Black,
                ChequeFontColor = Color.Black
            };
        }
    }
}
