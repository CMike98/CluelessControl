using CluelessControl.Cheques;
using System.Windows.Forms.VisualStyles;

namespace CluelessControl.EnvelopeColorStates
{
    public class NotSelectedColorState : ColorState
    {
        private const int NEW_ALPHA = 52;

        public NotSelectedColorState(BaseCheque cheque) : base(cheque)
        {
        }

        public override EnvelopeColorCollection GetColorPairingForScreen()
        {
            return new EnvelopeColorCollection()
            { 
                BackgroundColor = Color.FromArgb(0x73, 0x73, 0x73),
                LineColor       = Color.Transparent,
                NumberFontColor = Color.FromArgb(NEW_ALPHA, Color.Black),
                ChequeFontColor = Color.FromArgb(NEW_ALPHA, Cheque.GetDefaultTextColor()),
            };
        }

        public override EnvelopeColorCollection GetColorPairingForTv()
        {
            return new EnvelopeColorCollection()
            {
                BackgroundColor = Color.FromArgb(0x73, 0x73, 0x73),
                LineColor       = Color.Transparent,
                NumberFontColor = Color.FromArgb(NEW_ALPHA, Color.Black),
                ChequeFontColor = Color.FromArgb(NEW_ALPHA, Cheque.GetDefaultTextColor()),
            };
        }
    }
}
