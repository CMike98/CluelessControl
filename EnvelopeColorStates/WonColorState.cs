using CluelessControl.Cheques;

namespace CluelessControl.EnvelopeColorStates
{
    public class WonColorState : ColorState
    {
        public WonColorState(BaseCheque cheque) : base(cheque)
        {
        }

        public override EnvelopeColorCollection GetColorPairingForScreen()
        {
            return new EnvelopeColorCollection()
            { 
                BackgroundColor = Color.LightGreen,
                LineColor       = Color.Black,
                NumberFontColor = Color.Black,
                ChequeFontColor = Cheque.GetTextColor()
            };
        }

        public override EnvelopeColorCollection GetColorPairingForTv()
        {
            return new EnvelopeColorCollection()
            {
                BackgroundColor = Color.LightGreen,
                LineColor       = Color.Black,
                NumberFontColor = Color.Black,
                ChequeFontColor = Cheque.GetTextColor()
            };
        }
    }
}
