using CluelessControl.Cheques;

namespace CluelessControl.EnvelopeColorStates
{
    public class SelectedColorState : ColorState
    {
        public SelectedColorState(BaseCheque cheque) : base(cheque)
        {
        }

        public override EnvelopeColorCollection GetColorPairingForScreen()
        {
            return new EnvelopeColorCollection()
            {
                BackgroundColor = Color.White,
                LineColor       = Color.Black,
                NumberFontColor = Color.Black,
                ChequeFontColor = Cheque.GetTextColor()
            };
        }

        public override EnvelopeColorCollection GetColorPairingForTv()
        {
            return new EnvelopeColorCollection()
            {
                BackgroundColor = Color.Yellow,
                LineColor       = Color.Black,
                NumberFontColor = Color.Black,
                ChequeFontColor = Cheque.GetTextColor()
            };
        }
    }
}
