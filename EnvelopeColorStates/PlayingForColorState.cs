using CluelessControl.Cheques;

namespace CluelessControl.EnvelopeColorStates
{
    public class PlayingForColorState : ColorState
    {
        public PlayingForColorState(BaseCheque cheque) : base(cheque)
        {
        }

        public override EnvelopeColorCollection GetColorPairingForScreen()
        {
            return new EnvelopeColorCollection()
            {
                BackgroundColor = Color.Orange,
                LineColor       = Color.Black,
                NumberFontColor = Color.Black,
                ChequeFontColor = Cheque.GetDefaultTextColor()
            };
        }

        public override EnvelopeColorCollection GetColorPairingForTv()
        {
            return new EnvelopeColorCollection()
            {
                BackgroundColor = Color.Orange,
                LineColor       = Color.Black,
                NumberFontColor = Color.Black,
                ChequeFontColor = Cheque.GetDefaultTextColor()
            };
        }
    }
}
